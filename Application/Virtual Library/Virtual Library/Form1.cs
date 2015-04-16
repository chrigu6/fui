using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Virtual_Library
{
    public partial class Form1 : Form
    {
        List<Books> library = new List<Books>();

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
            foreach(Books book in library)
            {
                System.Diagnostics.Process.Start(book.getPath());
            }
        }

        private void textBox1_MouseClick(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void InitializeLibrary()
        {
            Books book1 = new Books("A History of Mathematics", "Florian Cajori", 2010, @"\\psf\Dropbox\Unibe\3. Semester\FUI\Project\Application\Virtual Library\Virtual Library\books\A History of Mathematics.pdf");
            book1.setTag("history");
            book1.setTag("mathematics");
            library.Add(book1);
            Books book2 = new Books("A Short Account of the History of Mathematics", "W. W. Rouse Ball", 2010, @"\\psf\Dropbox\Unibe\3. Semester\FUI\Project\Application\Virtual Library\Virtual Library\books\A Short Account of the History of Mathematics.pdf");
            book2.setTag("history");
            book2.setTag("mathematics");
            library.Add(book2);
            Books book3 = new Books("First Six Books of the Elements of Euclid", "John Casey", 2007, @"\\psf\Dropbox\Unibe\3. Semester\FUI\Project\Application\Virtual Library\Virtual Library\books\First Six Books of the Elements of Euclid.pdf");
            book3.setTag("elements");
            book3.setTag("mathematics");
            book3.setTag("euclid");
            library.Add(book3);
        }
    }
}
