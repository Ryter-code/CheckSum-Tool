using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CheckSum_Tool
{
    public partial class Form1 : Form
    {

        String file_path = "None";

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {

            // check the object can be dragged to the control box
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            /**
             * GetData() will return string[], The content is the object path,
             * This API is allow user to drag multiple objects at once,
             * So we always use object path[0] only.
             */
            string[] entriesPath = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            this.textBox1.Text = "Drag and drop files to here...\r\nGet File: " + entriesPath[0];
            file_path = entriesPath[0];
        }

        // get checksum button 
        private void button1_Click(object sender, EventArgs e)
        {
            int checksum = 0;
            try
            {
                checksum = get_checksum(file_path);
                this.textBox2.Text = "0x" + checksum.ToString("X");
            }
            catch (System.IO.FileNotFoundException e1)
            {
                MessageBox.Show("System.IO.FileNotFoundException:\nFile Not Found.", "Warning");
            }
            catch (Exception e2)
            {
                MessageBox.Show("Unknown error:\n" + e2.ToString(), "Warning");
            }

        }

        // exit button
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            System.Environment.Exit(0);
        }

        // copy button
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Clipboard.SetText(this.textBox2.Text);
            }
            catch(System.ArgumentNullException e1)
            {
                return;
            }
            return;
        }

        // Get Checksum
        private int get_checksum(String file)
        {
            int checksum = 0;
            string read_text = File.ReadAllText(file);
            byte[] ASCIIbytes = Encoding.ASCII.GetBytes(read_text);

            for (int i = 0; i < ASCIIbytes.Length; i++)
            {
                checksum += Convert.ToInt32(ASCIIbytes[i]);
            }
            return checksum;
        }

        // initall windows form location
        private void Form1_Load(object sender, EventArgs e)
        {

            /**
             *    x = Windows Width - App Width
             *    y = Windows Height - App Height
             *     -------------------------------------
             *     |                                           |
             *     |                                           |
             *     |                            (x,y)-------|
             *     |                              |            | 
             *     |                              |            | 
             *     -------------------------------------
             */

            int x = (System.Windows.Forms.SystemInformation.WorkingArea.Width - this.Size.Width);
            int y = (System.Windows.Forms.SystemInformation.WorkingArea.Height - this.Size.Height);

            // Setup windows form position is control by Location
            this.StartPosition = FormStartPosition.Manual;
            // Setup windows form position to (x,y)
            this.Location = (Point)new Size(x, y);
        }
    }
}
