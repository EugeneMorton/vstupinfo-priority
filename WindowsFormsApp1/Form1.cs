using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(textBox1.Text, @"http:\/\/vstup\.info\/2017\/.+$"))
            {
                MessageBox.Show("Введён некорректный адрес","Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string vstupsite = client.DownloadString(textBox1.Text);
                vstupsite = Regex.Split(vstupsite, @">\s<tbody>")[1];
                vstupsite = Regex.Split(vstupsite, @"<\/td><\/tr>\s<\/table>")[0];
                DataSet1 vstup = new DataSet1();
                List<string> vstrow = new List<string>();
                var matches = Regex.Matches(vstupsite, @"<tr><td>(\d+)<\/td><td>(.+ .\. .\.)<\/td><td>Допущено<\/td><td>(\d)<\/td><td>(\d+\.\d+)");
                for (int i = 0; i < matches.Count; i++)
                {
                    try { vstup.DataTable1.Rows.Add(new object[] { int.Parse(matches[i].Groups[1].ToString()), matches[i].Groups[2], int.Parse(matches[i].Groups[3].ToString()), matches[i].Groups[4] }); }
                    catch { }
                }
                dataGridView1.DataSource = vstup.DataTable1;
                dataGridView1.Refresh();
                if (!Regex.IsMatch(textBox2.Text, @"\S+\s.\.\s.\."))
                {
                    label2.Text = "Not available | Некорректное имя";
                }
                else if (vstup.DataTable1.Rows.Find(textBox2.Text) == null)
                {
                    label2.Text = "Not available | Имени нет в списке";
                }
                short first = 0, second = 0, third = 0, forth = 0, fifth = 0, sixth = 0, seventh = 0, eighth = 0, ninth = 0;

                for (int i = 0; i < int.Parse(vstup.DataTable1.Rows.Find(textBox2.Text)[0].ToString()); i++)
                {
                    switch (vstup.DataTable1.Rows[i][2])
                    {
                        case 1: first++; continue;
                        case 2: second++; continue;
                        case 3: third++; continue;
                        case 4: forth++; continue;
                        case 5: fifth++; continue;
                        case 6: sixth++; continue;
                        case 7: seventh++; continue;
                        case 8: eighth++; continue;
                        case 9: ninth++; continue;
                    }
                }
                label2.Text = "До вас: \nПервых приоритетов - " + first + "\nВторых приоритетов - " + second + "\nТретьих приоритетов - " + third + "\nЧетвёртых приоритетов - " + forth + "\nПятых приоритетов - " + fifth + "\nШестых приоритетов - " + sixth + "\nСедьмых приоритетов - " + seventh + "\nВосьмых приоритетов - " + eighth + "\nДевятых приоритетов - " + ninth;
                //float.Parse(vstup.DataTable1.Rows[5][3].ToString(), System.Globalization.CultureInfo.InvariantCulture);
            }
        }
    }
}
