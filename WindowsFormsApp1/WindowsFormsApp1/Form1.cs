﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public long first = 0;
        public string sign = "0";
        public long second = 0;
        public long result = 0;
        public string member = "0";
        public string error = "0";
        public bool equel = true;
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "0";
        }
        private void Add_text(string text)
        {
            textBox1.Text += text;
        }
        private void Add_text_2(string text)
        {
            textBox2.Text += text;
        }
        private void Choose_sign()
        {
            switch (sign)
            {
                case "+":
                    result = Calculator.Sum(first, second);
                    break;
                case "-":
                    result = Calculator.Minus(first, second);
                    break;
                case "/":
                    if (second == 0)
                    {
                        error = "Error";
                        first = 0;
                        second = 0;
                        sign = "0";
                    }
                    else
                    {
                        result = Calculator.Div(first, second);
                    }
                    break;
                case "*":
                    result = Calculator.Multi(first, second);
                    break;
                case "\u221a":
                    if (first < 0)
                    {
                        error = "Error";
                        first = 0;
                        second = 0;
                        sign = "0";
                    }
                    else
                    {
                        result = Calculator.Root(first);
                    }
                    break;
                case "^":
                    result = Calculator.Pow(first, second);
                    break;
            }
        }
        private void Dobble_operation(string new_sign, long new_second)
        {
            equel = true;
            if (error == "Error")
            {
                textBox1.Text = "0";
                textBox2.Clear();
                Add_text_2(error);
                To_zero();
            }
            else
            {
                if (sign != "0")
                {
                    second = Convert.ToInt32(textBox1.Text);
                    Choose_sign();
                    first = result;
                    sign = new_sign;
                    second = new_second;
                    textBox2.Clear();
                    Add_text_2(Convert.ToString(first) + sign);
                }
                else
                {
                    textBox2.Clear();
                    sign = new_sign;
                    ///System.OverflowException
                    first = Convert.ToInt64(textBox1.Text);
                    Add_text_2(textBox1.Text + sign);
                }
                textBox1.Text = "0";
            }  
        }

        private void Start_zero()
        {
            if (textBox1.Text == "0")
            {
                textBox1.Clear();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void but0_Click(object sender, EventArgs e)
        {
            Start_zero();
            Add_text("0");
        }

        private void but1_Click(object sender, EventArgs e)
        {
            Start_zero();
            Add_text("1");
        }

        private void but2_Click(object sender, EventArgs e)
        {
            Start_zero();
            Add_text("2");
        }

        private void but3_Click(object sender, EventArgs e)
        {
            Start_zero();
            Add_text("3");
        }

        private void but4_Click(object sender, EventArgs e)
        {
            Start_zero();
            Add_text("4");
        }

        private void but5_Click(object sender, EventArgs e)
        {
            Start_zero();
            Add_text("5");
        }

        private void but6_Click(object sender, EventArgs e)
        {
            Start_zero();
            Add_text("6");
        }

        private void but7_Click(object sender, EventArgs e)
        {
            Start_zero();
            Add_text("7");
        }

        private void but8_Click(object sender, EventArgs e)
        {
            Start_zero();
            Add_text("8");
        }

        private void but9_Click(object sender, EventArgs e)
        {
            Start_zero();
            Add_text("9");
        }

        private void but_plus_Click(object sender, EventArgs e)
        {
            Dobble_operation("+", Convert.ToInt64(textBox1.Text));
        }

        private void but_minus_Click(object sender, EventArgs e)
        {
            Dobble_operation("-", Convert.ToInt64(textBox1.Text)); 
        }

        private void but_share_Click(object sender, EventArgs e)
        {
            Dobble_operation("/", Convert.ToInt64(textBox1.Text));
        }

        private void but_multi_Click(object sender, EventArgs e)
        {
            Dobble_operation("*", Convert.ToInt64(textBox1.Text));
        }

        private void but_sqrt_Click(object sender, EventArgs e)
        {
            equel = true;
            if (sign != "0")
            {
                second = Convert.ToInt32(textBox1.Text);
                Choose_sign();
                if (error != "0")
                {
                    textBox2.Clear();
                    Add_text_2(error);
                    error = "0";
                }
                else
                {
                    first = result;
                    sign = "\u221a";
                    textBox2.Clear();
                    Add_text_2(Convert.ToString(sign + first));
                }
            }
            else
            {
                textBox2.Clear();
                sign = "\u221a";
                ///System.OverflowException
                first = Convert.ToInt64(textBox1.Text);
                Add_text_2(sign + textBox1.Text);
            }
            textBox1.Text = "0";
        }

        private void but_row_Click(object sender, EventArgs e)
        {
            Dobble_operation("^", Convert.ToInt64(textBox1.Text)); 
        }

        private void but_c_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            textBox2.Text = "0";
            first = 0;
            second = 0;
            result = 0;
            sign = "0";
            error = "0";
        }

        private void but_mk_Click(object sender, EventArgs e)
        {
            member = Convert.ToString(result);
        }

        private void but_m_Click(object sender, EventArgs e)
        {
            textBox1.Text = member;
        }

        private void but_mc_Click(object sender, EventArgs e)
        {
            member = "0";
        }

        private void but_qually_Click(object sender, EventArgs e)
        {
            if (equel) {
                equel = false;
                if (error == "Error")
                {
                    textBox1.Clear();
                    textBox2.Clear();
                    Add_text_2(error);
                    To_zero();
                }
                else
                {
                    second = Convert.ToInt64(textBox1.Text);
                    Choose_sign();
                    if (error != "0")
                    {
                        textBox1.Text = "0";
                        textBox2.Clear();
                        Add_text_2(error);
                        error = "0";
                    }
                    else
                    {
                        textBox1.Text = Convert.ToString(result);
                        if (sign != "\u221a")
                        {
                            Add_text_2(Convert.ToString(second) + "=" + Convert.ToString(result));
                        }
                        else
                        {
                            Add_text_2("=" + Convert.ToString(result));
                        }

                        first = result;
                        second = 0;
                        sign = "0";
                        result = 0;
                    }
                }   
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void but_del_Click(object sender, EventArgs e)
        {
            int text = Convert.ToInt32(textBox1.Text) / 10;
            textBox1.Text = Convert.ToString(text);   
        }

        private void To_zero()
        {
            first = 0;
            second = 0;
            result = 0;
            sign = "0";
            error = "0";
        } 
    }
}