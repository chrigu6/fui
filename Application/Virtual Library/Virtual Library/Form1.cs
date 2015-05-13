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

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void addPictureBox(Books book)
        {
            PictureBox pb = new PictureBox();
            pb.Name = book.getName();
            if(tn.Count < 1) {
                pb.Location = new Point(textBox1.Left, textBox1.Bottom + 5);
            }
            else
            {
                pb.Location = new Point(tn[tn.Count() - 1].Right + 8, textBox1.Bottom + 5);
            }
            pb.Size = new Size(120, 200);
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Image = book.getThumbnail();
            pb.Visible = true;
            pb.Cursor = Cursors.Hand;
            pb.Click += new EventHandler(thumbnailClick);
            pb.MouseEnter += new EventHandler(highlightBook);
            pb.MouseLeave += new EventHandler(resizeBook);
            this.Controls.Add(pb);
            tn.Add(pb);
        }

        private void highlightBook(object sender, EventArgs e)
        {
            var book = (PictureBox)sender;
            book.Location = new Point(book.Location.X - 3, book.Location.Y);
            book.BorderStyle = BorderStyle.FixedSingle;
            book.Size = new Size(126, 206);
        }

        private void resizeBook(object sender, EventArgs e)
        {
            var book = (PictureBox)sender;
            book.Location = new Point(book.Location.X + 3, book.Location.Y);
            book.BorderStyle = BorderStyle.None;
            book.Size = new Size(120, 200);
        }

        private void thumbnailClick(object sender, EventArgs e)
        {
            var clickedPicture = (PictureBox)sender;
            var book = searchBook(clickedPicture.Name);
            InitializeAdobe(book.getPath());
        }

        private void InitializeAdobe(string filePath)
        {
            try
            {
                this.axAcroPDF1.LoadFile(filePath);
                this.axAcroPDF1.src = filePath;
                this.axAcroPDF1.setShowToolbar(true);
                this.axAcroPDF1.setView("FitH");
                this.axAcroPDF1.setLayoutMode("SinglePage");
                this.axAcroPDF1.Show();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private Books searchBook(String name)
        {
            foreach (Books book in library)
            {
                if (book.getName() == name)
                {
                    return book;
                }
            }
            return null;
        }

        private const string defaultText = "Type query";

        private void textBox1_MouseClick(object sender, EventArgs e)
        {
            if (textBox1.Text == defaultText)
            {
                textBox1.Text = string.Empty;
            }
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
                @"..\..\books\historymath.jpg",
                @"..\..\books\A History of Mathematics.pdf"
            );
            book1.setTag("history");
            book1.setTag("mathematics");
            library.Add(book1);
            Books book2 = new Books("A Short Account of the History of Mathematics", 
                "W. W. Rouse Ball", 2010,
                @"..\..\books\accountmath.jpg",
                @"..\..\books\A Short Account of the History of Mathematics.pdf"
            );
            book2.setTag("history");
            book2.setTag("mathematics");
            library.Add(book2);
            Books book3 = new Books("First Six Books of the Elements of Euclid", 
                "John Casey", 2007,
                @"..\..\books\euclid.jpg",
                @"..\..\books\First Six Books of the Elements of Euclid.pdf"
            );
            book3.setTag("elements");
            book3.setTag("mathematics");
            book3.setTag("euclid");
            library.Add(book3);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
