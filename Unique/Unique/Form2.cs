using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace Unique
{
    public partial class Form2 : Form
    {
        Form1 f1;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(Form1 f1)
        {
            InitializeComponent();
            this.f1 = f1;
            try
            {
                TextFieldParser parser = new TextFieldParser("D:\\cs\\CS_lab\\Graphics\\Options.txt");
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters("\t");
                string[] check = parser.ReadFields();
                string[] autosave = parser.ReadFields();
                if (check[0] == "1")
                {
                    checkBox1.Checked = true;
                }
                if (autosave[0] != "0")
                {
                    numericUpDown1.Value = Convert.ToInt32(autosave[0]);
                }
            }
            catch
            { }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            f1.Options.Enabled = true;
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            int check;
            if (checkBox1.Checked is true)
            {
                check = 1;
                f1.FormBorderStyle = FormBorderStyle.None;
                f1.SetBounds(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                f1.dataGridView1.Size = new Size(863, 1000);
            }
            else
            {
                check = 0;
                f1.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                f1.ClientSize = new Size(892, 544);
                f1.dataGridView1.Size = new Size(863, 486);
            }
            if (numericUpDown1.Value == 0)
            {
                f1.timer1.Enabled = false;
            }
            else
            {
                f1.timer1.Interval = (int)numericUpDown1.Value;
                f1.timer1.Enabled = true;
            }
            System.IO.File.WriteAllText("D:\\cs\\CS_lab\\Graphics\\Options.txt", check.ToString() + "\n" + numericUpDown1.Value.ToString());
        }
    }
}
