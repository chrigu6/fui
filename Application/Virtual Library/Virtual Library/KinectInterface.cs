using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KinectMouse
{
    public class KinectInterface
    {
        // Fields
        private static KinectInterface _instance = null;
        private int clickDelay = 100;
        private const int filterLength = 0x10;
        private double gaussFactor = 0.0;
        private int[] gaussFilter = new int[] { 0x40, 0x37, 0x2d, 0x23, 0x19, 20, 15, 10, 8, 7, 6, 5, 4, 3, 2, 1 };
        private List<Joint> HistoricHandJoints = new List<Joint>();
        private List<Joint> HistoricShoulderJoints = new List<Joint>();
        public KinectSensor Kinect = null;
        private List<Point> mousePositionHistory = new List<Point>();
        private int oldX = 15;
        private int oldY = -15;

        private KinectInterface()
        {
        }
    

        public Joint? GetJoint(JointType _jointType)
        {
            Func<Joint, bool> predicate = null;
            Joint joint = new Joint();
            if (this.SkeletonData.Count<Skeleton>() > 0)
            {
                Skeleton skeleton = (from skd in this.SkeletonData
                                     where (skd != null) && (skd.TrackingState == SkeletonTrackingState.Tracked)
                                     select skd).FirstOrDefault<Skeleton>();
                if (skeleton != null)
                {
                    if (predicate == null)
                    {
                        predicate = joint2 => joint2.JointType == _jointType;
                    }
                    return new Joint?(skeleton.Joints.Where<Joint>(predicate).SingleOrDefault<Joint>());
                }
            }
            return null;
        }

        public static KinectInterface Instance()
        {
            Console.WriteLine("Started");
            if (_instance == null)
            {
                _instance = new KinectInterface();
            }
            return _instance;
        }

        private void kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame frame = e.OpenSkeletonFrame())
            {
                if ((frame != null) && (this.SkeletonData != null))
                {
                    frame.CopySkeletonDataTo(this.SkeletonData);
                }
                bool rightHand = true;
                Joint? joint = this.GetJoint(JointType.HandRight);
                Joint? nullable2 = this.GetJoint(JointType.HandLeft);
                Joint? nullable3 = this.GetJoint(JointType.ShoulderCenter);
                Joint? nullable4 = null;
                if (joint.HasValue && (joint.Value.TrackingState == JointTrackingState.Tracked))
                {
                    nullable4 = joint;
                    rightHand = true;
                }
                if ((nullable2.HasValue && (nullable2.Value.TrackingState == JointTrackingState.Tracked)) && !(nullable4.HasValue && (nullable4.Value.Position.Y >= nullable2.Value.Position.Y)))
                {
                    nullable4 = nullable2;
                    rightHand = false;
                }
                if (nullable4.HasValue && nullable3.HasValue)
                {
                    int num;
                    int num2;
                    this.ScaleXY(nullable3.Value, rightHand, nullable4.Value, out num, out num2);
                    if (this.mousePositionHistory.Count > 100)
                    {
                        this.mousePositionHistory.RemoveAt(0);
                    }
                    this.mousePositionHistory.Add(new Point(num, num2));
                    if ((this.mousePositionHistory.Count > 0x10) && ((((this.oldX != num) || (this.oldY != num2)) && (Math.Abs((int)(this.oldX - num)) < 350)) && (Math.Abs((int)(this.oldY - num2)) < 350)))
                    {
                        Point point = this.SmoothMousePosition();
                        if ((nullable4.Value.TrackingState == JointTrackingState.Tracked) && (nullable3.Value.TrackingState == JointTrackingState.Tracked))
                        {
                            MouseControl.MouseControl.Move((int)Math.Round(point.X), (int)Math.Round(point.Y));
                            this.oldX = (int)point.X;
                            this.oldY = (int)point.Y;
                        }
                        this.HistoricHandJoints.Add(nullable4.Value);
                        this.HistoricShoulderJoints.Add(nullable3.Value);
                        if (this.HistoricHandJoints.Count > 100)
                        {
                            this.HistoricHandJoints.RemoveAt(0);
                            this.HistoricShoulderJoints.RemoveAt(0);
                        }
                        
                        this.clickDelay++;
                    }
                }
            }
        }


        public void ScaleXY(Joint shoulderCenter, bool rightHand, Joint joint, out int scaledX, out int scaledY)
        {
            double primaryScreenWidth = SystemParameters.PrimaryScreenWidth;
            double num2 = 0.0;
            double num3 = this.ScaleY(joint);
            if (rightHand)
            {
                num2 = ((joint.Position.X - shoulderCenter.Position.X) * primaryScreenWidth) * 2.0;
            }
            else
            {
                num2 = primaryScreenWidth - ((shoulderCenter.Position.X - joint.Position.X) * (primaryScreenWidth * 2.0));
            }
            if (num2 < 0.0)
            {
                num2 = 0.0;
            }
            else if (num2 > (primaryScreenWidth - 5.0))
            {
                num2 = primaryScreenWidth - 5.0;
            }
            if (num3 < 0.0)
            {
                num3 = 0.0;
            }
            scaledX = (int)num2;
            scaledY = (int)num3;
        }

        public double ScaleY(Joint joint)
        {
            return (((SystemParameters.PrimaryScreenHeight / 0.4) * -joint.Position.Y) + (SystemParameters.PrimaryScreenHeight / 2.0));
        }

        private Point SmoothMousePosition()
        {
            Point point = new Point(0.0, 0.0);
            for (int i = 0; i < 15; i++)
            {
                Point point3 = this.mousePositionHistory[(this.mousePositionHistory.Count - 1) - i];
                point.X += point3.X * this.gaussFilter[i];
                point3 = this.mousePositionHistory[(this.mousePositionHistory.Count - 1) - i];
                point.Y += point3.Y * this.gaussFilter[i];
            }
            point.X = (float)(point.X / this.gaussFactor);
            point.Y = (float)(point.Y / this.gaussFactor);
            return point;
        }

        public void StartKinectST()
        {
            int[] gaussFilter = this.gaussFilter;
            for (int i = 0; i < gaussFilter.Length; i++)
            {
                double num = gaussFilter[i];
                this.gaussFactor += num;
            }
            this.Kinect = KinectSensor.KinectSensors.FirstOrDefault<KinectSensor>(s => s.Status == KinectStatus.Connected);
            if(this.Kinect != null)
            {
                TransformSmoothParameters parameters = new TransformSmoothParameters
                {
                    Smoothing = 0.5f,
                    Correction = 0.5f,
                    Prediction = 0.5f,
                    JitterRadius = 0.05f,
                    MaxDeviationRadius = 0.04f
                };
                this.Kinect.SkeletonStream.Enable();
                this.SkeletonData = new Skeleton[this.Kinect.SkeletonStream.FrameSkeletonArrayLength];
                this.Kinect.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(this.kinect_SkeletonFrameReady);
                this.Kinect.Start();
            }
        }

        // Properties
        public Skeleton[] SkeletonData { get; set; }


    }
}
