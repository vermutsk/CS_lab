using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Unique
{
    public partial class Form3 : Form
    {
        int beginY, width, max = 0, buildmax;
        float koef;
        Form1 f1;
        public Form3()
        {
            InitializeComponent();
        }
        public Form3(Form1 f1)
        {
            InitializeComponent();
            this.f1 = f1;
        }
        //изменение размеров окна графика
        private void Form3_Resize(object sender, EventArgs e)
        {
            panel1.Width = Width - 24;
            panel1.Height = Height - 55;
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                
                //сделать еще ниже....................................................
                beginY = panel1.ClientSize.Height / 5 * 4;
                buildmax = Convert.ToInt32(beginY * 0.9);
                f1.dataGridView1.AllowUserToAddRows = false;
                //ширина пробелов и столбцов
                width = panel1.Width / (f1.dataGridView1.RowCount);
                //ищем маkсимальное значение из данных
                for (int i = 0; i < f1.dataGridView1.RowCount; i++)
                {
                    var tp = Math.Abs(Convert.ToInt32(f1.dataGridView1.Rows[i].Cells[1].Value.ToString()));
                    if (max < tp)
                    {
                        max = tp;
                    }
                }

                koef = (float)buildmax / max;
                for (int i = 0; i < f1.dataGridView1.RowCount; i++)
                {
                    int val = Convert.ToInt32(Convert.ToInt32(f1.dataGridView1.Rows[i].Cells[1].Value.ToString()) * koef);
                    DrawLine(FillPolygon(val, i), i);
                }
                f1.dataGridView1.AllowUserToAddRows = true;
            }
            catch
            { f1.dataGridView1.AllowUserToAddRows = true; }
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            f1.Graphic.Enabled = true;
        }

        Point[] FillPolygon(int Value, int i)
        {
            Point[] polygon = new Point[4];

            polygon[0].X = width * i;
            polygon[0].Y = beginY;

            polygon[3].X = polygon[0].X + width-10;
            polygon[3].Y = beginY;

            polygon[1].X = polygon[0].X;
            polygon[1].Y = beginY - Value;

            polygon[2].X = polygon[0].X + width-10;
            polygon[2].Y = polygon[1].Y;

            return polygon;
        }
        void DrawLine(Point[] polygon, int i)
        {
            Graphics formGraphics;
            formGraphics = panel1.CreateGraphics();
            SolidBrush brush = new SolidBrush(Color.DarkSeaGreen);
            GraphicsPath path = new GraphicsPath();
            path.AddLine(polygon[0], polygon[1]);
            path.AddLine(polygon[1], polygon[2]);
            path.AddLine(polygon[2], polygon[3]);
            path.AddLine(polygon[3], polygon[0]);
            formGraphics.FillPath(brush, path);

            string Name = f1.dataGridView1.Rows[i].Cells[0].Value.ToString();
            string Value = f1.dataGridView1.Rows[i].Cells[1].Value.ToString();
            Font drawFont = new Font("Segoe UI", 10);
            SolidBrush NameBrush = new SolidBrush(Color.Indigo);
            SolidBrush ValueBrush = new SolidBrush(Color.Black);
            int x = polygon[0].X + ((polygon[3].X - polygon[0].X) / 8);
            int y;
            if (Convert.ToInt32(Value) < 0)
            {
                y = polygon[1].Y + 10;
            }
            else
            {
                y = polygon[1].Y - 30;
            }
            StringFormat drawFormat = new StringFormat();
            formGraphics.DrawString(Name, drawFont, NameBrush, x, y);
            drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            x = polygon[0].X + ((polygon[3].X - polygon[0].X) / 3);
            y = polygon[0].Y + 5;
            formGraphics.DrawString(Value, drawFont, ValueBrush, x, y, drawFormat);
        }
    }
}
