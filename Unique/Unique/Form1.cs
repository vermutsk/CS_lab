using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace Unique
{
    public partial class Form1 : Form
    {
        //List<Tuple<string, string>> data; 
        //Tuple<string, string> tempdata;
        //int one;
        //Form2 form2;
        public Form1()
        {
            InitializeComponent();
            Save.Click += Saving;
            Create.Click += Creating;
            Options.Click += Optioning;
            Graphic.Click += Graphing;
            try
            {
                
                TextFieldParser parser = new TextFieldParser("D:\\cs\\CS_lab\\Graphics\\Options.txt");
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters("\t");
                string[] check = parser.ReadFields();   //полноэкранный режим
                string[] autosave = parser.ReadFields();
                if (check[0] == "1")
                {
                    FormBorderStyle = FormBorderStyle.None;
                    SetBounds(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                }
                if (autosave[0] != "0")
                {
                    timer1.Interval = Convert.ToInt32(autosave[0]);
                    timer1.Enabled = true;
                }
            }
            catch
            { }
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            //проверка расширения
            if (files[0].Substring(files[0].Length - 4) != ".csv")
            {
                MessageBox.Show("Перетяните файл с расширением .csv");
            }
            else
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                TextFieldParser parser = new TextFieldParser(files[0]);
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                string[] fields = parser.ReadFields();
                //кол_во столбцов создаем пустые
                dataGridView1.ColumnCount = fields.Length;
                //вносим заголовки
                for (int i = 0; i < fields.Length; i++)
                {
                    dataGridView1.Columns[i].Name = fields[i];
                }
                //вносим значения
                while (!parser.EndOfData)
                {
                    fields = parser.ReadFields();
                    dataGridView1.Rows.Add(fields);
                }
            }
        }
        //добавляет в эффекты таблицы
        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }
        private void Saving(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            //запрет на добавление строк
            dataGridView1.AllowUserToAddRows = false;
            var table = "";
            //добавление заголовков
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                if (i != dataGridView1.ColumnCount - 1)
                {
                    table += dataGridView1.Columns[i].Name + ", ";
                }
                else
                {
                    table += dataGridView1.Columns[i].Name + "\n";
                }
            }
            //добавление элементов
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int k = 0; k < dataGridView1.ColumnCount; k++)
                {
                    if (k == dataGridView1.ColumnCount - 1)
                    {
                        try
                        { 
                            table += dataGridView1.Rows[i].Cells[k].Value.ToString() + "\n";
                        }
                        catch
                        {
                            table += "" + "\n";
                        }
                    }
                    else
                    {
                        try
                        {
                            table += dataGridView1.Rows[i].Cells[k].Value.ToString() + ", ";
                        }
                        catch
                        {
                            table += "" + ", ";
                        }
                    }
                }
            }
            //сохраняем файл по данным и пути
            var filename = saveFileDialog1.FileName;
            System.IO.File.WriteAllText(filename, table);
            //разрешаем вносить элементы в таблицу
            dataGridView1.AllowUserToAddRows = true;

            MessageBox.Show("Файл сохранен");
        }
        private void Creating(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "Name";
            dataGridView1.Columns[1].Name = "Value";
        }
        private void Optioning(object sender, EventArgs e)
        {
            //отключаем кнопку
            Options.Enabled = false;
            Form2 form2 = new Form2(this);
            form2.Show();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.AllowUserToAddRows = false;
                var table = "";
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    if (i != dataGridView1.ColumnCount - 1)
                    {
                        table += dataGridView1.Columns[i].Name + ", ";
                    }
                    else
                    {
                        table += dataGridView1.Columns[i].Name + "\n";
                    }
                }
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int k = 0; k < dataGridView1.ColumnCount; k++)
                    {
                        if (k == dataGridView1.ColumnCount - 1)
                        {
                            try
                            {
                                table += dataGridView1.Rows[i].Cells[k].Value.ToString() + "\n";
                            }
                            catch
                            {
                                table += "" + "\n";
                            }
                        }
                        else
                        {
                            try
                            {
                                table += dataGridView1.Rows[i].Cells[k].Value.ToString() + ", ";
                            }
                            catch
                            {
                                table += "" + ", ";
                            }
                        }
                    }
                }
                var filename = "D:\\cs\\CS_lab\\Graphics\\autosave.csv";
                System.IO.File.WriteAllText(filename, table);
                dataGridView1.AllowUserToAddRows = true;
            }
            catch
            { }
        }
        private void Graphing(object sender, EventArgs e)
        {
            Graphic.Enabled = false;
            Form3 form3 = new Form3(this);
            form3.Show();
        }
        //проверка на число во втором столбце
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
            catch
            {
                dataGridView1.Rows[e.RowIndex].Cells[1].Value = "0";
            }
        }
        //удаление строки
        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены?", "Удаление", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
