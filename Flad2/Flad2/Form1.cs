using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flad2
{
    public partial class Form1 : Form
    {
        int line_counter = 0;
        private Point startCoord;
        private Point endCoord;
        private int animationValue;
        PointF[][][] polygons = new PointF[7][][];
        bool isCreated = false;

        public Form1()
        {
            InitializeComponent();
            animationValue = 0;
        }

        private void AnimationTimerOn(object sender, EventArgs e)
        {
            animationValue += 1;
            CalculateFlag();
            this.drawPanel.Invalidate();
        }
        private void mouseDown(object sender, MouseEventArgs e)
        {
            this.drawPanel.MouseMove += mouseMove;
            startCoord = e.Location;
            endCoord = e.Location;
            // timer1.Stop();
        }

        private void mouseUp(object sender, MouseEventArgs e)
        {
            //this.MouseMove -= mouseMove;
            this.drawPanel.MouseMove -= mouseMove;
            // timer1.Start();
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            CalculateFlag();
            this.drawPanel.Invalidate();
            endCoord = e.Location;

        }
        private void paint(object sender, PaintEventArgs e)
        {
            DrawFlag(startCoord, endCoord);
        }

        private void CalculateFlag()
        {
            if (!isCreated)
                isCreated = true;
            var lenWidth = endCoord.X - startCoord.X;
            var lenHeight = (endCoord.Y - startCoord.Y) / 7;
            polygons[0] = CreatePolygon(lenWidth, lenHeight, startCoord.Y);
            for (int n = 1; n<7; n++)
            {
                polygons[n] = CreatePolygon(lenWidth, lenHeight, startCoord.Y + lenHeight * n);
            }
        }

        void Show(string message)
        {
            line_counter += 1;
            logBox.Text += String.Format("{0}: {1}\r\n", line_counter, message);
            logBox.SelectionStart = logBox.Text.Length;
            logBox.ScrollToCaret();

        }

        private void DrawFlag(Point startCoord, Point endCoord)
        {
            Show(String.Format("DrawFlag({0}, {1}, {2})", startCoord, endCoord, isCreated));
            if (!isCreated)
            {
                return;
            }
            int n = 0;
            if (startCoord.Y <= endCoord.Y)
            {
                List<Color> colors = new List<Color>()
                { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.LightBlue, Color.Blue, Color.Violet };

                Show(String.Format("{0}:{1}", colors.Count, polygons.Length));
                /*foreach (var item in colors)
                {
                    Show(String.Format("{0}", item.Name));
                }*/
                /*foreach (var row in polygons)
                {
                    foreach (var polygons in row)
                    {
                            Show(String.Format("{0}", polygons[0]));
                    }
                }*/


                foreach (Color i in colors)
                {
                    DrawLine(new SolidBrush(i), polygons[n]);
                    n++;
                }
            }
            else
            {
                List<Color> colors = new List<Color>()
                {Color.Violet, Color.Blue, Color.LightBlue, Color.Green, Color.Yellow, Color.Orange, Color.Red};

                foreach (Color i in colors)
                {
                    DrawLine(new SolidBrush(i), polygons[n]);
                    n++;
                }
            }
            /*foreach (Color i in colors)
            {
                DrawLine(new SolidBrush(i), polygons[n]);
                n++;
            }*/

        }
        private PointF [][] CreatePolygon(int lenWight, int lenHeight, int startY)
        {
            int count = 150;
            int size = count;
            PointF[][] polygon = new PointF[4][];
            polygon[0] = new PointF[size];
            polygon[2] = new PointF[size];
            for(int i = 0; i<size; i++)
            {
                polygon[0][i].X = startCoord.X + (lenWight * i)/size;
                polygon[0][i].Y = startY + ((float)Math.Sin(i / 20f + animationValue)) * 30;
                polygon[2][size - 1 - i].X = polygon[0][i].X;
                polygon[2][size - 1 - i].Y = polygon[0][i].Y + lenHeight;
            }
            polygon[1] = new PointF[2];
            polygon[3] = new PointF[2];
            polygon[1][0].X = polygon[0][size - 1].X;
            polygon[1][0].Y = polygon[0][size - 1].Y;
            polygon[1][1].X = polygon[2][0].X;
            polygon[1][1].Y = polygon[2][0].Y;
            polygon[3][0].X = polygon[2][size - 1].X;
            polygon[3][0].Y = polygon[2][size - 1].Y;
            polygon[3][1].X = polygon[0][0].X;
            polygon[3][1].Y = polygon[0][0].Y;

            return polygon;
        }

        private void DrawLine(SolidBrush brush, PointF[][] polygon)
        {
            System.Drawing.Graphics formGraphics;
            formGraphics = this.drawPanel.CreateGraphics();
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddCurve(polygon[0], tension: 4.5f);
            path.AddLine(polygon[1][0], polygon[1][1]);
            path.AddCurve(polygon[2], tension: 4.5f);
            path.AddLine(polygon[3][0], polygon[3][1]);
            formGraphics.FillPath(brush, path);
            brush.Dispose();
            formGraphics.Dispose();
        }

    }
}
