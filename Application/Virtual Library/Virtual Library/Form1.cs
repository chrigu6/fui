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
using System.Runtime.InteropServices;

namespace Virtual_Library
{
    public partial class Form1 : Form
    {
        List<Books> library = new List<Books>();
        List<PictureBox> tn = new List<PictureBox>();
        List<Books> bookmarks = new List<Books>();
        Books activeBook = null;

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        public Form1()
        {
            InitializeComponent();
            InitializeLibrary();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern void mouse_event(long dwFlags, long dx, long dy);

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchMethod();
        }

        public void searchMethod()
        {
            foreach (PictureBox thumb in tn)
            {
                thumb.Dispose();
            }
            tn.Clear();
            List<Books> results = Search(textBox1.Text);
            if (results.Count == 0)
            {
                textBox2.Text = "Sorry. I did not find any results with the keyword " + textBox1.Text + ".";
            }
            foreach (Books book in results)
            {
                addPictureBox(book);
                textBox2.Text = "I am searching in the database for you. You requested the word: " + textBox1.Text;
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
                pb.Location = new Point(textBox1.Left, textBox1.Bottom + 30);
            }
            else
            {
                pb.Location = new Point(tn[tn.Count() - 1].Right + 8, textBox1.Bottom + 30);
            }
            pb.Size = new Size(120, 200);
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Image = book.getThumbnail();
            pb.Visible = true;
            pb.Cursor = Cursors.Hand;
            pb.Click += new EventHandler(open);
            pb.MouseEnter += new EventHandler(highlightBook);
            pb.MouseLeave += new EventHandler(resizeBook);
            this.Controls.Add(pb);
            if (!tn.Exists(x => x.Name == pb.Name))
            {
                tn.Add(pb);
            }
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

        public void open(object sender, EventArgs e)
        {
            var clickedPicture = (PictureBox)sender;
            var book = searchBook(clickedPicture.Name);
            this.activeBook = book;
            activeBookLabel.Text = "Active Book: " + book.getName();
            InitializeAdobe(book.getPath());
        }

        private void InitializeAdobe(string filePath)
        {
            try
            {
                this.axAcroPDF1.LoadFile(filePath);
                this.axAcroPDF1.setShowToolbar(true);
                this.axAcroPDF1.setView("FitH");
                this.axAcroPDF1.setLayoutMode("SinglePage");
                this.axAcroPDF1.Show();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void goToPage(int n)
        {
            this.axAcroPDF1.setCurrentPage(n);
        }

        public void goToNextPage()
        {
            this.axAcroPDF1.gotoNextPage();
        }

        public void goToPreviousPage()
        {
            this.axAcroPDF1.gotoPreviousPage();
        }

        public void searchDocument(String searchTerm)
        {
            this.axAcroPDF1.Select();
            SendKeys.Send("^f");
            SendKeys.Flush();
            SendKeys.Send(searchTerm);
            SendKeys.Flush();
        }

        public void searchNextOccurence()
        {
            this.axAcroPDF1.Select();
            SendKeys.Send("^f");
            SendKeys.Send("{ENTER}");
        }

        public void exitSearchBox()
        {
            this.axAcroPDF1.Select();
            SendKeys.Send("{ESC}");
        }

        public void zoom(float zoomPercent)
        {
            this.axAcroPDF1.setZoom(zoomPercent);
        }

        public void bookmark(Books book)
        {
            if (!bookmarks.Exists(x => x.getName() == book.getName()))
            {
                bookmarks.Add(book);
                textBox2.Text = "Bookmarked the book " + book.getName() + ".";
            }
        }

        public void deleteBookmark(Books book)
        {
            bookmarks.Remove(bookmarks.Find(x => x.getName() == book.getName()));
            textBox2.Text = "You've deleted the book " + book.getName() + " from your bookmarks.";
            showBookmarks();
        }

        public void showBookmarks()
        {
            foreach (PictureBox thumb in tn)
            {
                thumb.Dispose();
            }
            tn.Clear();
            if (bookmarks.Count == 0)
            {
                textBox2.Text = "You don't have any bookmarks";
            }
            else
            {
                textBox2.Text = "Your Bookmarks:";
                foreach (Books book in bookmarks)
                {
                    addPictureBox(book);
                }
            }

        }

        public void hightlightText()
        {
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN, X, Y);
            mouse_event(MOUSEEVENTF_LEFTUP, X, Y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, X, Y);
            mouse_event(MOUSEEVENTF_LEFTUP, X, Y);
        }

        public void searchHighlightedWord()
        {
            this.axAcroPDF1.Select();
            SendKeys.Send("^c");
            textBox1.Text = Clipboard.GetText();
            searchMethod();
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
                    if (tag.Contains(keyword))
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

        public void swipeLeft()
        {
            this.goToPreviousPage();
        }

        public void swipeRight()
        {
            this.goToNextPage();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchMethod();
            }
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void bookmarkbutton_Click(object sender, EventArgs e)
        {
            if (this.activeBook != null)
            {
                bookmark(this.activeBook);
            }
            else
            {
                textBox2.Text = "There is no book selected";
            }
        }

        private void showBookmarkButton_Click(object sender, EventArgs e)
        {
            showBookmarks();
        }

        private void deleteBookmarkButton_Click(object sender, EventArgs e)
        {
            if (bookmarks.Exists(x => x.getName() == activeBook.getName()))
            {
                deleteBookmark(activeBook);
            }
        }

        public void rightHandTracked(bool state)
        {
            if(state)
            {
                this.rightHandBox.BackColor = Color.Green;
            }

            else
            {
                this.rightHandBox.BackColor = Color.Red;
            }
        }

        public void leftHandTracked(bool state)
        {
            if (state)
            {
                this.leftHandBox.BackColor = Color.Green;
            }

            else
            {
                this.leftHandBox.BackColor = Color.Red;
            }
        }


    }
}
