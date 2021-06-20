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
        private Pen pen1;
        private Graphics g;
        private Pen pen2;
        bool memory = true;
        private PointF point1 = new PointF(150, 200);
        private PointF point2 = new PointF(750, 200);
        private PointF point3 = new PointF(450, 700);
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            n--;
            g.Clear(Color.MintCream);
            Fractal(point1, point2, point3, n);
            Fractal(point2, point3, point1, n);
            Fractal(point3, point1, point2, n);
            memory = true;
        }

        private void Next_Click(object sender, EventArgs e)
        {
            n++;
            Fractal(point1, point2, point3, n);
            Fractal(point2, point3, point1, n);
            Fractal(point3, point1, point2, n);
            memory = true;
        }

        //рекурсивная функция рисования кривой Коха
        public int Fractal(PointF p1, PointF p2, PointF p3, int iter)
        {
            g = panel1.CreateGraphics();
            pen1 = new Pen(Color.Green, 1);
            pen2 = new Pen(Color.MintCream, 2);
            if (n - iter == 0 & memory == true)
            {
                g.DrawLine(pen1, point1, point2);
                g.DrawLine(pen1, point2, point3);
                g.DrawLine(pen1, point3, point1);
                memory = false;
            }
            //n -количество итераций
            if (iter > 0)  //условие выхода из рекурсии
            {
                //средняя треть отрезка
                var p4 = new PointF((p2.X + 2 * p1.X) / 3, (p2.Y + 2 * p1.Y) / 3);
                var p5 = new PointF((2 * p2.X + p1.X) / 3, (p1.Y + 2 * p2.Y) / 3);
                //координаты вершины угла
                var ps = new PointF((p2.X + p1.X) / 2, (p2.Y + p1.Y) / 2);
                var pn = new PointF((4 * ps.X - p3.X) / 3, (4 * ps.Y - p3.Y) / 3);
                //рисуем его
                g.DrawLine(pen1, p4, pn);
                g.DrawLine(pen1, p5, pn);
                g.DrawLine(pen2, p4, p5);


                //рекурсивно вызываем функцию нужное число раз
                Fractal(p4, pn, p5, iter - 1);
                Fractal(pn, p5, p4, iter - 1);
                Fractal(p1, p4, new PointF((2 * p1.X + p3.X) / 3, (2 * p1.Y + p3.Y) / 3), iter - 1);
                Fractal(p5, p2, new PointF((2 * p2.X + p3.X) / 3, (2 * p2.Y + p3.Y) / 3), iter - 1);

            }
            return iter;
        }

    }
}
