using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Calculator
    {
        static public double Sum(double first, double second)
        {
            return first + second;
        }
        static public double Minus(double first, double second)
        {
            return first - second;
        }
        static public double Multi(double first, double second)
        {
            return first * second;
        }
        static public double Div(double first, double second)
        {
            double result;
            if (second == 0)
            {
                result = 0;
            }
            else
            {
                result = first / second;
            }
            return result;
        }
        static public double Pow(double first, double second)
        {
            return Convert.ToDouble(Math.Pow(first, second));
        }
        static public double Root(double first)
        {
            return Convert.ToDouble(Math.Sqrt(first));
        }
        static public double Sin(double first)
        {
            return Convert.ToDouble(Math.Sin(first));
        }
    }
}
