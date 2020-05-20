using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            string[] Words = txtSearch.Text.Split(',');
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
            if (e.KeyCode== Keys.Enter) { button1.PerformClick(); }
            else if (e.KeyCode == Keys.Back) { button2.PerformClick(); }
            KeyPressEventArgs arg = new KeyPressEventArgs(Convert.ToChar(Keys.Enter));
            keypressedz(sender, arg);
        }
    }
}
