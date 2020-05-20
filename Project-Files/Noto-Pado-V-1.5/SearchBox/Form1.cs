using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Diagnostics;
using Microsoft.CSharp;

namespace SearchBox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void keypressedz(Object o, KeyPressEventArgs e)
        {
            // The keypressed method uses the KeyChar property to check 
            // whether the ENTER key is pressed. 

            // If the ENTER key is pressed, the Handled property is set to true, 
            // to indicate the event is handled.
            if (e.KeyChar == (char)Keys.Return)
            {
                button1.PerformClick();
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Search in Text & Highlight
            string[] Words = txtSearch.Text.Split();
            foreach(string word in Words)
            {
                int StartIndex = 0;
                while (StartIndex < richTextBox1.TextLength)
                {
                    int WordstartIndex = richTextBox1.Find(word, StartIndex, RichTextBoxFinds.None);
                    if (WordstartIndex != -1)
                    {
                        richTextBox1.SelectionStart = WordstartIndex;
                        richTextBox1.SelectionLength = word.Length;
                        richTextBox1.SelectionBackColor = Color.Yellow;
                    }
                    else
                        break;
                    StartIndex += WordstartIndex + word.Length;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectAll();
            richTextBox1.SelectionBackColor=Color.White;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
           
        }
        
        private void textBox1_KeyPress(object sender, KeyEventArgs e)
        {
            // Conditions for (search & Clear highlight) if Enter is pressed performs the search event function which 
            // is assigned to a button so the click event is excuted as well as the same goes for the clear highlight 
            // 
            if (e.KeyCode== Keys.Enter) { button1.PerformClick(); }
            else if (e.KeyCode == Keys.Back) { button2.PerformClick(); }
            KeyPressEventArgs arg = new KeyPressEventArgs(Convert.ToChar(Keys.Enter));
            keypressedz(sender, arg);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        public void button3_Click(object sender, EventArgs e)
        {
            
            //Creating event to open dialog to open a file
            //Make new object of Open file Dialog
            OpenFileDialog Z = new OpenFileDialog();
            Z.DefaultExt = "*.txt";
            
            if(Z.ShowDialog()== System.Windows.Forms.DialogResult.OK && Z.FileName.Length>0)
            {
                Process b = new Process();
                richTextBox1.LoadFile(Z.FileName, RichTextBoxStreamType.PlainText);
                b.StartInfo.FileName = Z.FileName;
                
            }
            //if (Z.FileName is )
           /* Word.ApplicationClass wordApp = new ApplicationClass();
            object file = path;
            object nullobj = System.Reflection.Missing.Value;
            Word.Document doc = wordApp.Documents.Open(
            ref file, ref nullobj, ref nullobj,
                                                  ref nullobj, ref nullobj, ref nullobj,
                                                  ref nullobj, ref nullobj, ref nullobj,
                                                  ref nullobj, ref nullobj, ref nullobj);
            doc.ActiveWindow.Selection.WholeStory();
            doc.ActiveWindow.Selection.Copy();
            IDataObject data = Clipboard.GetDataObject();
            txtFileContent.Text = data.GetData(DataFormats.Text).ToString();
            doc.Close(); */
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            richTextBox1.Clear();
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text==string.Empty) { SystemSounds.Beep.Play(); }
            txtSearch.Clear();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            //SearchBox The Web & open it in Browser
            string s = txtSearch.Text;
           string  Url = "https://www.google.com.eg/search?q=";
            Process.Start(Url+Uri.EscapeDataString(s));
        }
    }
}
