using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using MetroFramework;

namespace Noto_Pado
{
    public partial class Main : MetroFramework.Forms.MetroForm
    {
        public Main()
        {
            InitializeComponent();
            this.StyleManager = modmain;
            KeyPreview = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
             
        }

        public  void High_Lit()
        {
            int index = 0;
            string temp = bar.Text;
            bar.Text = "";
            bar.Text = temp;
            while (index <= bar.Text.LastIndexOf(txsear.Text))
            {
                //Searches the text in a RichtextBox Control for a string within the range of text 
                bar.Find(txsear.Text, index, bar.TextLength, RichTextBoxFinds.None);
                //Selection Color. This is applied auto if matched text is found
                bar.SelectionColor = Color.Yellow;
                //After matach is found the index is increased so the search won't stop at the same match again. 
                index = bar.Text.IndexOf(txsear.Text, index) + 1;

            }
        }

        public void Savo_check()
        {
            //Check changes over text from original file and write out to label   
            string ORIGT = "";
            string CMP = bar.Text;
            //CMP = CMP.Replace("\n", string.Empty); //Remove only if check over character changes with the below statement 
            if (!(Orig_text is null))
            {
                //ORIGT = Orig_text.Trim(); //Trim the opened file text from white spaces if wanted to keep check over characters only
                ORIGT = Orig_text;
                ORIGT = ORIGT.Replace("\r", string.Empty);
            }
            if (ORIGT != CMP)
                Savo.Text = "YES";
            else if (ORIGT == CMP)
                Savo.Text = "NO";
        }



        public void High_Litrev()
        {
            //Search in Text Box & Highlight
            string[] Words = txsear.Text.Split();
            foreach (string word in Words)
            {
                int StartIndex = 0;
                while (StartIndex < bar.TextLength)
                {
                    int WordstartIndex = bar.Find(word, StartIndex, RichTextBoxFinds.None);
                    if (WordstartIndex != -1)
                    {
                        bar.SelectionStart = WordstartIndex;
                        bar.SelectionLength = word.Length;
                        bar.SelectionBackColor = Color.Green;
                    }
                    else
                        break;
                    StartIndex += WordstartIndex + word.Length;
                }
            }
        }

        private void Log_Paint(object sender, PaintEventArgs e)
        {

        }
        public string FullFileName { get; set; }
        public string Orig_text { get; set; }
        public void Opf_Click(object sender, EventArgs e)
        {

            //Creating event to open dialog to open a file
            //Make new object of Open file Dialog

            OpenFileDialog Z = new OpenFileDialog();
            Z.DefaultExt = "*.txt";
            Z.Filter = "(*.txt)|*.txt";



            if (Z.ShowDialog() == System.Windows.Forms.DialogResult.OK && Z.FileName.Length > 0)
            {

                
                Process b = new Process();
                b.StartInfo.FileName = Z.FileName;
                bar.LoadFile(Z.FileName, RichTextBoxStreamType.PlainText);
                Savo.Text = "NO"; //When Text is loaded to RichTextBox Label is changed from N/A to No default
                int CharCount = Z.FileName.Length;  //total char 
                int LinesCount = File.ReadLines(Z.FileName).Count(); ;  //total lines
                FullFileName = Z.FileName;
                //Orig_text = System.IO.File.ReadAllText(Z.FileName);
               
                

                // total Words through stream reader
                StreamReader sr = new StreamReader(Z.FileName);

                int counter = 0;
                string delim = " ,."; //maybe some more delimiters like ?! and so on
                string[] fields = null;
                string line = null;

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();//each time you read a line you should split it into the words
                    line.Trim();
                    fields = line.Split(delim.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    counter += fields.Length; //and just add how many of them there is
                }


                sr.Close();

                //Modify labels to display numbers
                nol.Text = LinesCount.ToString();
                charno.Text = CharCount.ToString();
                wrdsc.Text = counter.ToString();
                b.Close();
                
            }

        }


        
        private void Opf_MouseHover(object sender, EventArgs e)
        {
            //Change Opefn File Tile back color to green when hovered over
            Opf.BackColor =  Color.Red;
            //Open Text File Tool Tip
            metroToolTip2.Show("Open Text File", Opf);
        }

        private void Opf_MouseLeave(object sender, EventArgs e)
        {
            //Revert back Open file Tile back to normal when mouse leaves
            Opf.BackColor = default;
        }

        private void sear_Click(object sender, EventArgs e)
        {
            //Search Button click Event
            High_Lit();
        }

        

        private void Clrb_Click(object sender, EventArgs e)
        {
            //Clear TextBox
            bar.Clear();
        }

        private void websear_Click(object sender, EventArgs e)
        {
            //SearchBox The Web & open it in Browser
            string s = txsear.Text;
            string Url = "https://www.google.com.eg/search?q=";
            Process.Start(Url + Uri.EscapeDataString(s));
        }

        private void Clear_sear_Click(object sender, EventArgs e)
        {
            //Clear Search bar
            txsear.Clear();
        }

        private void Clear_sear_MouseHover(object sender, EventArgs e)
        {
            //Change Clear Search tile back Color to red when hovered over
            ClearSear.BackColor = Color.Red;
            metroToolTip1.Show("Clear Text Search Bar",ClearSear);
        }

        private void svf_Click(object sender, EventArgs e)
        {
            //Save Richbox Text content to the Current Opened File & Display Message with Operation
            System.IO.File.WriteAllText(FullFileName, bar.Text.Replace("\n", Environment.NewLine));
            MetroMessageBox.Show(this,"Text Saved to File","SAVED");
        }


        private void svf_MouseHover(object sender, EventArgs e)
        {
            //Change Opefn File Tile back color to green when hovered over
            svf.BackColor = Color.Yellow;
            //Save Edited Text to same File ToolTip
            metroToolTip3.Show("Save Edited Text to same File", svf);
        }

        private void svf_MouseLeave(object sender, EventArgs e)
        {
            //Revert back Save  Tile back to normal when mouse leaves
            svf.BackColor = default;
        }

 
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(FullFileName is null))
            {
                Orig_text = System.IO.File.ReadAllText(FullFileName);
            }
            string ORIGT="";
            string CMP = bar.Text.Trim(); //add .Trim() when switching to check without spaces
            CMP = CMP.Replace("\n", string.Empty); //Remove only if check over character changes with the below statement 
            if (!(Orig_text is null))
            {
                ORIGT = Orig_text.Trim(); //Trim the opened file text from white spaces if wanted to keep check over characters only
                //ORIGT = Orig_text; //remove comment when switching to check with spaces
                ORIGT = ORIGT.Replace("\r\n", string.Empty); //add \n when to remove newlines
            }
           

            // Determine if text has changed in the textbox by comparing to original text.
            if (CMP != ORIGT)
            {
                DialogResult RES = MetroMessageBox.Show(this, "Do you want to save changes to your text?", "Warning", MessageBoxButtons.YesNoCancel);
                // Display a MsgBox asking the user to save changes or abort.
                if ( RES == DialogResult.Yes)
                {
                    // Cancel the Closing event from closing the form.
                    e.Cancel = true;
                    if (FullFileName is null)
                    {
                        SaveFileDialog savefile = new SaveFileDialog();
                        // set a default file name
                        savefile.FileName = "unknown.txt";
                        // set filters - this can be done in properties as well
                        savefile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

                        if (savefile.ShowDialog() == DialogResult.OK)
                        {
                           System.IO.File.WriteAllText(savefile.FileName, bar.Text.Replace("\n", Environment.NewLine));
                        }
                    }
                    else
                    {
                        // Call method to save file
                        System.IO.File.WriteAllText(FullFileName, bar.Text.Replace("\n", Environment.NewLine));
                    }
                    
                    e.Cancel = false;
                }
               // else if (RES == DialogResult.No)
                //{

               // }
                else if (RES == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        
        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
            {
                System.IO.File.WriteAllText(FullFileName, bar.Text.Replace("\n", Environment.NewLine));
                MetroMessageBox.Show(this, "Text Saved to File", "SAVED");
                Savo_check();
            }
            if (e.KeyCode == Keys.O && (e.Modifiers == Keys.Control))
            {
                Opf_Click(sender , e);
            }
        }

        private void txsear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                High_Lit();
            }
            else
            {
                 //High_Lit();
                //Find(bar, txsear.Text, Color.Green);
            }
        }

        private void sear_MouseHover(object sender, EventArgs e)
        {
            //Search Through Displayed text Tool Tip
            metroToolTip4.Show("Search Through Displayed text", sear);
            //Sear back color when hovered over
            sear.BackColor = Color.Cyan;
        }

        private void websear_MouseHover(object sender, EventArgs e)
        {
            //Search The web by searched text Tool Tip
            metroToolTip5.Show("Search The web by searched text", websear);
            //websear back color when hovered over
            websear.BackColor = Color.LimeGreen;
        }

        private void Clrb_MouseHover(object sender, EventArgs e)
        {
            //Clear the Displayed text Tool Tip
            metroToolTip6.Show("Clear the Displayed text", Clrb);
            //Clrb back color when hovered over
            Clrb.BackColor = Color.Orange;
        }

        private void sear_MouseLeave(object sender, EventArgs e)
        {
            //Sear back color when Left
            sear.BackColor = default;
        }

        private void websear_MouseLeave(object sender, EventArgs e)
        {
            //websear back color when Left
            websear.BackColor = default;
        }

        private void Clrb_MouseLeave(object sender, EventArgs e)
        {
            //Clrb back color when Left
            Clrb.BackColor = default;
        }

        private void Ht_C_MouseHover(object sender, EventArgs e)
        {
            //Clear Highlight Tool Tip
            metroToolTip7.Show("Clear Highlight", Ht_C);
            //Ht_c back color when hovered over
            Ht_C.BackColor = Color.Yellow;
        }

        private void Ht_C_Click(object sender, EventArgs e)
        {
            bar.SelectionColor = Color.White;
        }

        private void bar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Savo_check();
            
        }

       
    }
}
