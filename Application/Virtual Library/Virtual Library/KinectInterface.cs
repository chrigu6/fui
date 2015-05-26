using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;
using System.IO;
using Virtual_Library;
using System.Runtime.InteropServices;


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
        private float[] leftHandX = new float[30];
        private float[] leftHandY = new float[30];
        private int leftHandTracked = 0;
        private int leftHandUnTracked = 0;
        private int gestureReset = 40;
        private Form1 form;

        // Speech recognition
        private SpeechRecognitionEngine spRecEng;
        // Confidence treshold
        private const double ConfidenceThreshold = 0.8;
        public Form1 Form1;
        public TextBox textbox1;

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






                Joint? rightHand = this.GetJoint(JointType.HandRight);
                Joint? shoulder = this.GetJoint(JointType.ShoulderCenter);
                Joint? leftHand = this.GetJoint(JointType.HandLeft);
                Joint? leftEllbow = this.GetJoint(JointType.ElbowLeft);

                //Move Cursor

                if (rightHand.HasValue && shoulder.HasValue)
                {
                    form.rightHandTracked(true);
                    int num;
                    int num2;
                    this.ScaleXY(shoulder.Value, true, rightHand.Value, out num, out num2);
                    if (this.mousePositionHistory.Count > 100)
                    {
                        this.mousePositionHistory.RemoveAt(0);
                    }
                    this.mousePositionHistory.Add(new Point(num, num2));
                    if ((this.mousePositionHistory.Count > 0x10) && ((((this.oldX != num) || (this.oldY != num2)) && (Math.Abs((int)(this.oldX - num)) < 350)) && (Math.Abs((int)(this.oldY - num2)) < 350)))
                    {
                        Point point = this.SmoothMousePosition();
                        if ((rightHand.Value.TrackingState == JointTrackingState.Tracked) && (shoulder.Value.TrackingState == JointTrackingState.Tracked))
                        {
                            MouseControl.MouseControl.Move((int)Math.Round(point.X), (int)Math.Round(point.Y));
                            this.oldX = (int)point.X;
                            this.oldY = (int)point.Y;
                        }
                        this.HistoricHandJoints.Add(rightHand.Value);
                        this.HistoricShoulderJoints.Add(rightHand.Value);
                        if (this.HistoricHandJoints.Count > 100)
                        {
                            this.HistoricHandJoints.RemoveAt(0);
                            this.HistoricShoulderJoints.RemoveAt(0);
                        }

                        this.clickDelay++;
                    }
                }
                else
                {
                    form.rightHandTracked(false);
                }

                //Check for left hand gestures
                if (leftHand.HasValue && leftEllbow.HasValue && (leftEllbow.Value.Position.Y < leftHand.Value.Position.Y))
                {
                    form.leftHandTracked(true);
                    leftHandX[leftHandTracked] = leftHand.Value.Position.X;
                    this.leftHandTracked++;
                    this.gestureReset++;

                    if (leftHandTracked >= 30 && gestureReset >= 30)
                    {
                        gestureReset = 0;
                        leftHandTracked = 0;
                        leftHandUnTracked = 0;
                        if ((leftHandX[29] - leftHandX[0]) > 0.3)
                        {
                            this.form.swipeRight();
                        }

                        if ((leftHandX[29] - leftHandX[0]) < -0.25)
                        {
                            this.form.swipeLeft();
                        }
                    }
                }
                else
                {
                    form.leftHandTracked(false);
                    this.leftHandUnTracked++;
                    if (leftHandUnTracked >= 45)
                    {
                        leftHandTracked = 0;
                        leftHandUnTracked = 0;
                        Console.WriteLine("reset");
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

        // Code for mouse click
        public class MouseOperations
        {
            [Flags]
            public enum MouseEventFlags
            {
                LeftDown = 0x00000002,
                LeftUp = 0x00000004,
                MiddleDown = 0x00000020,
                MiddleUp = 0x00000040,
                Move = 0x00000001,
                Absolute = 0x00008000,
                RightDown = 0x00000008,
                RightUp = 0x00000010
            }

            [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool SetCursorPos(int X, int Y);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool GetCursorPos(out MousePoint lpMousePoint);

            [DllImport("user32.dll")]
            private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

            public static void SetCursorPosition(int X, int Y)
            {
                SetCursorPos(X, Y);
            }

            public static MousePoint GetCursorPosition()
            {
                MousePoint currentMousePoint;
                var gotPoint = GetCursorPos(out currentMousePoint);
                if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
                return currentMousePoint;
            }

            public static void MouseEvent(MouseEventFlags value)
            {
                MousePoint position = GetCursorPosition();

                mouse_event
                    ((int)value,
                     position.X,
                     position.Y,
                     0,
                     0)
                    ;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct MousePoint
            {
                public int X;
                public int Y;

                public MousePoint(int x, int y)
                {
                    X = x;
                    Y = x;
                }

            }

        }

        //Speech Recognizer
        private static RecognizerInfo GetKinectRecognizer()
        {
            foreach (RecognizerInfo recognizer in SpeechRecognitionEngine.InstalledRecognizers())
            {
                string value;
                recognizer.AdditionalInfo.TryGetValue("Kinect", out value);
                if ("True".Equals(value, StringComparison.OrdinalIgnoreCase) && "en-US".Equals(recognizer.Culture.Name, StringComparison.OrdinalIgnoreCase))
                {
                    return recognizer;
                }
            }
            return null;
        }


        void spRecEng_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show("I did not understand. Could you repeat, please?");
            TextBox t2 = System.Windows.Forms.Application.OpenForms["Form1"].Controls["textBox2"] as TextBox;
            t2.Text = ("I did not understand. Could you repeat, please ?");
            //MyForm.textBox2.Text = "What did you say? You said: ???";
            //this.semanticRep.Text = "What did you say?";
            //this.spoken.Text = "???";
        }
        void spRecEng_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //Form1 form1 = new Form1();
            //reference for textbox1's content
            TextBox t1 = System.Windows.Forms.Application.OpenForms["Form1"].Controls["textBox1"] as TextBox;
            TextBox t2 = System.Windows.Forms.Application.OpenForms["Form1"].Controls["textBox2"] as TextBox;
            

            if (e.Result.Confidence < ConfidenceThreshold)
            {
                //engine is not confident about result => ignore it
                return;
            }
            string semantic = "";
            switch (e.Result.Semantics.Value.ToString())
            {
                case "NEW SEARCH":
                    semantic = "Ok. Let's start a new search";
                    t1.Text = "";
                    t2.Text = "Say \"Search\" when you are ready.";
                    // set the focus on textBox1
                    this.form.ActiveControl = t1;
                    break;

                case "SEARCH CONTENT":
                    semantic = "Say 'search' when you are ready";

                    t1.Text = e.Result.Text;

                    break;
                case "CLICK":
                    semantic = "Click" + e.Result.Text;
                    //MouseOperations.SetCursorPosition();
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);

                    break;

                case "SEARCH":
                    this.form.ActiveControl = Form1;
                    semantic = "Here are the results for the keyword: " + t1.Text;
                    form.searchMethod();
                    t2.Text = (semantic);

                    break;
                case "OPEN":
                    semantic = "I will open it.";
                    t2.Text = (semantic);


                    break;
                case "CLOSE":
                    semantic = "I have closed this document.";
                    t2.Text = (semantic);

                    break;
                case "SELECTION":
                    semantic = "You talk about a document, you said: " + e.Result.Text;
                    t2.Text = (semantic);

                    break;
                case "EXIT_COMMANDS":
                    semantic = "Goodbye !";
                    t2.Text = (semantic);

                    System.Threading.Thread.Sleep(2000);

                    form.Close();
                    break;
                default:
                    semantic = "I am confident that I heard something, but I don't know what.";
                    t2.Text = (semantic);

                    break;
            }


            // the spoken word is stored in spoken.Text variable
            string spokenText = e.Result.Text;
        }


        public void StartKinectST(Form1 form)
        {
            this.form = form;
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


                // starting the speech recognizer
                RecognizerInfo ri = GetKinectRecognizer();
                if (ri != null)
                {
                    this.spRecEng = new SpeechRecognitionEngine(ri.Id);
                    //MemoryStream stream = new MemoryStream();
                    //XmlDocument xDocument = new XmlDocument();
                    //xDocument.Save(stream);

                    using (var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(Virtual_Library.Properties.Resources.SpeechGrammar)))
                    {
                        var g = new Grammar(memoryStream);
                        spRecEng.LoadGrammar(g);
                    }
                    spRecEng.SpeechRecognized += spRecEng_SpeechRecognized;
                    spRecEng.SpeechRecognitionRejected += spRecEng_SpeechRecognitionRejected;

                    spRecEng.SetInputToAudioStream(Kinect.AudioSource.Start(), new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
                    spRecEng.RecognizeAsync(RecognizeMode.Multiple);

                }
                else
                {
                    //no speech recognizer
                }
            }
        }
        // Properties
        public Skeleton[] SkeletonData { get; set; }


    }
}
