using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Calculator
    {
        static public long Sum(long first, long second)
        {
            return first + second;
        }
        static public long Minus(long first, long second)
        {
            return first - second;
        }
        static public long Multi(long first, long second)
        {
            return first * second;
        }
        static public long Div(long first, long second)
        {
            long result;
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
        static public long Pow(long first, long second)
        {
            return Convert.ToInt64(Math.Pow(first, second));
        }
        static public long Root(long first)
        {
            return Convert.ToInt64(Math.Sqrt(first));
        }
    }
}
