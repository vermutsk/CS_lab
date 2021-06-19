using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snowflake
{
    public partial class Form1 : Form
    {
        private int n = -1;
        private int memory = -1;
        private Pen pen1;
        private Graphics g;
        private Pen pen2;
        private bool create = false;
        private bool color = true;
        private PointF point1 = new PointF(150, 200);
        private PointF point2 = new PointF(750, 200);
        private PointF point3 = new PointF(450, 700);
        private PointF[][] levels = new PointF[5][];
        public Form1()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (n > 0)
            {
                n--;
                color = false;
                Fractal();
            }
            
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (n < 5)
            {
                n++;
                color = true;
                if (n > memory)
                {
                    memory++;
                }
                if (memory > n)
                {
                    Draw();
                }
                else
                {
                    Fractal();
                }
            }
            
        }
        private void Draw()
        {
            g = panel1.CreateGraphics();
            
            //Выбираем цвета зарисовки 
            pen1 = new Pen(Color.Green, 1);
            List<Color> colors = new List<Color>()
                { Color.Red, Color.Orange, Color.Yellow, Color.LimeGreen, Color.DeepSkyBlue, Color.Blue, Color.DarkViolet };
            Random rnd = new Random();
            int f = rnd.Next()%7;
            pen1 = new Pen(colors[f], 1);
            pen2 = new Pen(Color.MintCream, 2);
            
            //Определяем объект "g" класса Graphics 
            if (color == true)
            {
                int i = 0;
                try
                {
                    while (true)
                    {
                        if (i % 3 == 0)
                        {
                            f = rnd.Next() % 7;
                            pen1 = new Pen(colors[f], 1);
                        }

                        var p4 = levels[n][i];
                        var p5 = levels[n][i + 1];
                        //координаты вершины угла
                        var p6 = levels[n][i + 2];
                        //Зарисуем треугольник
                        g.DrawLine(pen1, p4, p5);
                        g.DrawLine(pen1, p5, p6);
                        if (create == true)
                        {
                            //g.DrawLine(pen2, p6, p4);
                        }
                        else
                        {
                            g.DrawLine(pen1, p6, p4);
                            create = true;
                        }
                        i += 3;
                    }
                }
                catch
                {

                }
            }
            else
            {
                int i = 0;
                try
                {
                    while (true)
                    {
                        var p4 = levels[n+1][i];
                        var p5 = levels[n+1][i + 1];
                        //координаты вершины угла
                        var p6 = levels[n+1][i + 2];
                        //Зарисуем треугольник
                        g.DrawLine(pen2, p4, p5);
                        g.DrawLine(pen2, p5, p6);
                        i += 3;
                    }
                }
                catch
                {

                }
            }
                       
        }


        //рекурсивная функция рисования кривой Коха
        private void Fractal()
        {
            if (create == false)
            {
                levels[0] = new PointF[3];
                levels[0][0] = point1;
                levels[0][1] = point3;
                levels[0][2] = point2;
                Draw();
            }
            
            int i = 0;
            int limit;
            if (n == 1)
            {
                limit = 3;
            }
            else
            {
                limit = 2;
            }
            try
            {
                int j = 0;
                while (true)
                {
                    for (int z = 0; z < limit; z++)
                    {
                        var p1 = levels[n - 1][(i + z) % 3 + j];
                        var p2 = levels[n - 1][(i + z + 1) % 3 + j];
                        var p3 = levels[n - 1][(i + z + 2) % 3 + j];

                        var p4 = new PointF((p2.X + 2 * p1.X) / 3, (p2.Y + 2 * p1.Y) / 3);
                        var p5 = new PointF((2 * p2.X + p1.X) / 3, (p1.Y + 2 * p2.Y) / 3);
                        //координаты вершины угла
                        var ps = new PointF((p2.X + p1.X) / 2, (p2.Y + p1.Y) / 2);
                        var pn = new PointF((4 * ps.X - p3.X) / 3, (4 * ps.Y - p3.Y) / 3);
                        Array.Resize(ref levels[n], i + 3);
                        levels[n][i] = p4;
                        levels[n][i + 1] = pn;
                        levels[n][i + 2] = p5;

                        i += 3;
                        Calculate(p4, pn, p5, i);
                        i += 3;
                        Calculate(pn, p5, p4, i);
                        i += 3;
                        Calculate(p1, p4, new PointF((2 * p1.X + p3.X) / 3, (2 * p1.Y + p3.Y) / 3), i);
                        i += 3;
                        Calculate(p5, p2, new PointF((2 * p2.X + p3.X) / 3, (2 * p2.Y + p3.Y) / 3), i);
                        i += 3;
                    }
                    j += 3;
                } 
            }
            catch
            {
            }
                
            Draw();

        }
        private void Calculate(PointF p1, PointF p2, PointF p3, int i)
        {
            try
            {
                
                var p4 = new PointF((p2.X + 2 * p1.X) / 3, (p2.Y + 2 * p1.Y) / 3);
                var p5 = new PointF((2 * p2.X + p1.X) / 3, (p1.Y + 2 * p2.Y) / 3);
                //координаты вершины угла
                var ps = new PointF((p2.X + p1.X) / 2, (p2.Y + p1.Y) / 2);
                var pn = new PointF((4 * ps.X - p3.X) / 3, (4 * ps.Y - p3.Y) / 3);
                Array.Resize(ref levels[n], i + 3);
                levels[n][i] = p4;
                levels[n][i + 1] = pn;
                levels[n][i + 2] = p5;
                
            }
            catch
            {
            }

        }
            
    }
}
