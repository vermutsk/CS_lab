using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace flag_gif
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Rectangle RcDraw;
        private Point startCoord = new Point();
        private Point endCoord = new Point();


        private void start(object sender, MouseEventArgs e)
        {
            timer1.Stop();
        }

        private void end(object sender, MouseEventArgs e)
        {
            timer1.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void PaintForm(object sender, PaintEventArgs e)
        {
            var redBrash = new SolidBrush(Color.Red);
            var formGrafics = this.drawPanel.CreateGraphics();
            //formGrafics.FillPath(redBrash, path);
            
        }
        private void CalculateFlag()
        { 

        }
    }

    
}
