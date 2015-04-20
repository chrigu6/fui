using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Virtual_Library
{
    public partial class Form1 : Form
    {
        List<Books> library = new List<Books>();
        List<PictureBox> tn = new List<PictureBox>();
        Label alert = new Label();

        public Form1()
        {
            InitializeComponent();
            InitializeLibrary();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (PictureBox thumb in tn)
            {
                thumb.Dispose();
            }
            alert.Visible = false;
            tn.Clear();
            List<Books> results = Search(textBox1.Text);
            if (results.Count == 0)
            {
                alert.Location = new Point(textBox1.Left, textBox1.Bottom + 5);
                alert.Size = new Size(300, 30);
                alert.Visible = true;
                alert.Text = "There were no results with the provided keyword";
                this.Controls.Add(alert);
            }
            foreach(Books book in results) 
            {
                addPictureBox(book);
            }
        }

        private void addPictureBox(Books book)
        {
            PictureBox pb = new PictureBox();
            pb.Name = "Thumbnail_" + book.getName();
            if(tn.Count < 1) {
                pb.Location = new Point(textBox1.Left, textBox1.Bottom + 5);
            }
            else
            {
                pb.Location = new Point(tn[tn.Count() - 1].Right + 5, textBox1.Bottom + 5);
            }
            pb.Size = new Size(120, 200);
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Image = book.getThumbnail();
            pb.Visible = true;
            this.Controls.Add(pb);
            tn.Add(pb);
        }

        private void textBox1_MouseClick(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private List<Books> Search(String keyword)
        {
            List<Books> result = new List<Books>();
            foreach (Books book in library)
            {
                foreach (String tag in book.getTags())
                {
                    if (tag == keyword)
                    {
                        result.Add(book);
                        break;
                    }
                }
            }
            return result;
        }

        private void InitializeLibrary()
        {
            Books book1 = new Books("A History of Mathematics", 
                "Florian Cajori", 2010, 
                @"\\psf\Dropbox\Unibe\3. Semester\FUI\Project\Application\Virtual Library\Virtual Library\books\historymath.jpg", 
                @"\\psf\Dropbox\Unibe\3. Semester\FUI\Project\Application\Virtual Library\Virtual Library\books\A History of Mathematics.pdf"
            );
            book1.setTag("history");
            book1.setTag("mathematics");
            library.Add(book1);
            Books book2 = new Books("A Short Account of the History of Mathematics", 
                "W. W. Rouse Ball", 2010,
                @"\\psf\Dropbox\Unibe\3. Semester\FUI\Project\Application\Virtual Library\Virtual Library\books\accountmath.jpg",
                @"\\psf\Dropbox\Unibe\3. Semester\FUI\Project\Application\Virtual Library\Virtual Library\books\A Short Account of the History of Mathematics.pdf"
            );
            book2.setTag("history");
            book2.setTag("mathematics");
            library.Add(book2);
            Books book3 = new Books("First Six Books of the Elements of Euclid", 
                "John Casey", 2007, 
                @"\\psf\Dropbox\Unibe\3. Semester\FUI\Project\Application\Virtual Library\Virtual Library\books\euclid.jpg",
                @"\\psf\Dropbox\Unibe\3. Semester\FUI\Project\Application\Virtual Library\Virtual Library\books\First Six Books of the Elements of Euclid.pdf"
            );
            book3.setTag("elements");
            book3.setTag("mathematics");
            book3.setTag("euclid");
            library.Add(book3);
        }
    }
}
