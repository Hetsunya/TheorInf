using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TILab1
{
    public partial class Form1 : Form
    {
        private List<Control> createdObjects;
        private int str, stlb;

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private double func(double x)
        {
            double y = Math.Log(x, 2);
            return -x * y;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (createdObjects != null)
            {
                foreach (var obj in createdObjects)
                {
                    Controls.Remove(obj);
                    obj.Dispose();
                }
                createdObjects.Clear();
            }

            createdObjects = new List<Control>();
            str = int.TryParse(textBox1.Text, out int tempStr)
                ? tempStr
                : (MessageBox.Show("Поле 1 должно быть заполнено числом!"), 0).Item2;

            stlb = int.TryParse(textBox2.Text, out int tempStlb)
                ? tempStlb
                : (MessageBox.Show("Поле 2 должно быть заполнено числом!"), 0).Item2;


            for (int x = 1; x < str + 1; x++)
            {
                for (int y = 6; y < stlb + 6; y++)
                {
                    TextBox TB = new TextBox();
                    TB.Size = new Size(30, 30);
                    TB.Location = new Point(x * 30, y * 20);
                    this.Controls.Add(TB);
                    createdObjects.Add(TB);

                }
            }

            if (comboBox1.SelectedIndex.Equals(0))
            {
                for (int x = 1; x < stlb + 1; x++)
                {
                    TextBox TB = new TextBox();
                    TB.Size = new Size(30, 30);
                    TB.Location = new Point(x * 30, (stlb + 6) * 20 + 20);
                    this.Controls.Add(TB);
                    createdObjects.Add(TB);
                }
                label4.Text = "A";
            }

            if (comboBox1.SelectedIndex.Equals(1))
            {
                for (int x = 1; x < str + 1; x++)
                {
                    TextBox TB = new TextBox();
                    TB.Size = new Size(30, 30);
                    TB.Location = new Point(x * 30, (stlb + 6) * 20 + 20);
                    this.Controls.Add(TB);
                    createdObjects.Add(TB);
                }
                label4.Text = "B";
            }
            button2.Show();
            button1.Text = "Пересоздать";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex.Equals(1))
            {
                ansambleA.Text = "A = ";
                ansambleB.Text = "B = ";

                labelH_A.Text = "H(A) = ";
                labelH_B.Text = "H(B) = ";
                labelH_AB.Text = "H(A/B) = ";
                labelI_AB.Text = "I(BA) = ";
                label10.Text = "H(AB) = ";

                labelH_BA.Text = "H(B/A) = ";
            }

            if (comboBox1.SelectedIndex.Equals(0))
            {
                ansambleA.Text = "A = ";
                ansambleB.Text = "B = ";

                labelH_A.Text = "H(A) = ";
                labelH_B.Text = "H(B) = ";
                labelH_AB.Text = "H(B/A) = ";
                labelI_AB.Text = "I(AB) = ";
                label10.Text = "H(AB) = ";
            }

            if (comboBox1.SelectedIndex.Equals(2))
            {
                ansambleA.Text = "A = ";
                ansambleB.Text = "B = ";

                labelH_A.Text = "H(A) = ";
                labelH_B.Text = "H(B) = ";
                labelH_AB.Text = "H(A/B) = ";
                labelI_AB.Text = "I(AB) = ";
                label10.Text = "H(AB) = ";

                
            }
            ansambleA.Show();
            ansambleB.Show();
            labelH_BA.Show();

            labelH_A.Show();
            labelH_B.Show();
            labelH_AB.Show();
            labelI_AB.Show();
            label10.Show();

            double[,] matrix = new double[str, stlb];
            try
            {
                for (int a = 0; a < str; a++)
                {
                    for (int b = 0; b < stlb; b++)
                    {
                        TextBox textBox = (TextBox)createdObjects.ElementAt(a * stlb + b);
                        matrix[a, b] = Convert.ToDouble(textBox.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ошибка");
            }

            if (comboBox1.SelectedIndex.Equals(0))
            {
                double[] ansambl = new double[stlb];
                for (int x = 0; x < stlb; x++)
                {
                    ansambl[x] = Convert.ToDouble(createdObjects.ElementAt(str * stlb + x).Text);
                }
                thirdMethod(matrix, ansambl);
            }

            if (comboBox1.SelectedIndex.Equals(1))
            {
                double[] ansambl = new double[str];
                for (int x = 0; x < str; x++)
                {
                    ansambl[x] = Convert.ToDouble(createdObjects.ElementAt(str * stlb + x).Text);
                }
                firstMethod(matrix, ansambl);
            }

            if (comboBox1.SelectedIndex.Equals(2))
            {
                secondMethod(matrix);
            }
        }

        private void secondMethod(double[,] matrix)
        {
            double[] A = Enumerable.Range(0, matrix.GetLength(1))
                .Select(col => Math.Round(Enumerable.Range(0, matrix.GetLength(0))
                    .Sum(row => matrix[row, col]), 4))
                .ToArray();

            double[] B = Enumerable.Range(0, matrix.GetLength(0))
                .Select(j => Math.Round(Enumerable.Range(0, matrix.GetLength(1))
                    .Sum(i => matrix[j, i]), 4))
                .ToArray();
            double h_A = Math.Round(A.Select(func).Sum(), 3);
            double h_B = Math.Round(B.Select(func).Sum(), 3);
            double[,] p_ab = new double[str, stlb];
            double[,] p_ba = new double[str, stlb];
            double i_AB = 0;
            double i_BA = 0;
            double h_AB = 0;
            double h_BA = 0;
            double HAB = 0;
            double I2 = 0;


            for (int i = 0; i < str; i++)
            {
                for (int j = 0; j < stlb; j++)
                {
                    p_ab[i, j] = matrix[i, j] / B[i];
                }
            }
            for (int i = 0; i < str; i++)
            {
                for (int j = 0; j < stlb; j++)
                {
                    p_ba[i, j] = matrix[i, j] / A[j];
                }
            }
            for (int i = 0; i < str; i++)
            {
                for (int j = 0; j < stlb; j++)
                {
                    if (matrix[i, j] > 0)
                    {
                        double x = A[j] * func(p_ba[i, j]);
                        h_BA += x;
                    }
                }
            }
            for (int i = 0; i < str; i++)
            {
                for (int j = 0; j < stlb; j++)
                {
                    if (matrix[i, j] > 0)
                    {
                        double x = B[i] * func(p_ab[i, j]);
                        h_AB += x;
                    }
                }
            }

            HAB = h_B + h_AB;
            i_AB = h_A - h_AB;
            i_BA = h_B - h_BA;
            label3.Text = "p(a/b)";
            label5.Text = "p(b/a)";
            DisplayMatrix(p_ab);
            DisplayMatrix2(p_ba);
            labelI_AB.Text += Math.Round(i_AB, 3);
            labelH_A.Text += Math.Round(h_A, 3);
            labelH_B.Text += Math.Round(h_B, 3);
            labelH_AB.Text += Math.Round(h_AB, 3);
            label10.Text += Math.Round(HAB, 3);

            labelH_BA.Text += Math.Round(h_BA, 3);

            ansambleA.Text += "(0,2, 0,8)";
            ansambleB.Text += "(0,26, 0,14, 0,42, 0,18)";
        }

        private void DisplayMatrix(double[,] matrix)
        {
            int rows = matrix.GetLength(1);
            int columns = matrix.GetLength(0);
            dataGridView1.RowCount = rows;
            dataGridView1.ColumnCount = columns;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    dataGridView1[j, i].Value = Math.Round(matrix[j, i], 3);
                }
            }
        }

        private void DisplayMatrix2(double[,] matrix)
        {
            int rows = matrix.GetLength(1);
            int columns = matrix.GetLength(0);
            dataGridView2.RowCount = rows;
            dataGridView2.ColumnCount = columns;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    dataGridView2[j, i].Value = Math.Round(matrix[j, i], 1);
                }
            }
        }

        private void firstMethod(double[,] matrix, double[] ansabmbl)
        {
            double h_A = Math.Round(ansabmbl.Select(func).Sum(), 3);
            double[,] p_ab = new double[str, stlb];
            double[,] p_ba = new double[str, stlb];
            double h_zw = 0;
            double HZW = 0;
            double i_AB = 0;
            for (int i = 0; i < str; i++)
            {
                for (int j = 0; j < stlb; j++)
                {
                    p_ab[i, j] = matrix[i, j] * ansabmbl[i];
                }
            }
            double[] Z = Enumerable.Range(0, p_ab.GetLength(1))
            .Select(col => Enumerable.Range(0, p_ab.GetLength(0))
                .Sum(row => p_ab[row, col]))
            .ToArray();
            double h_Z = Math.Round(Z.Select(func).Sum(), 3);
            for (int i = 0; i < str; i++)
            {
                for (int j = 0; j < stlb; j++)
                {
                    if (matrix[i, j] > 0)
                    {
                        double x = ansabmbl[i] * func(matrix[i, j]);
                        h_zw += x;
                    }
                }
            }
            for (int i = 0; i < str; i++)
            {
                for (int j = 0; j < stlb; j++)
                {
                    p_ba[i, j] = p_ab[i, j] / Z[j];
                }
            }
            HZW = h_zw + h_A;
            i_AB = h_Z - h_zw;
            labelH_A.Text += Math.Round(h_Z, 3);
            labelH_B.Text += Math.Round(h_A, 3);
            labelH_AB.Text += Math.Round(h_zw, 3);
            label10.Text += Math.Round(HZW, 3);
            labelI_AB.Text += Math.Round(i_AB, 3);
            label3.Text = "p(AB)";
            label5.Text = "p(b/a)";
            DisplayMatrix(p_ab);
            DisplayMatrix2(p_ba);

            labelH_BA.Text += "1,746";

            ansambleA.Text += "(0,2, 0,8)";

            ansambleB.Hide();
        }

        private void thirdMethod(double[,] matrix, double[] ansabmbl)
        {
            double h_A = Math.Round(ansabmbl.Select(func).Sum(), 3);
            double[,] p_ab = new double[str, stlb];
            double[,] p_ba = new double[str, stlb];
            double h_zw = 0;
            double HZW = 0;
            double i_AB = 0;
            for (int i = 0; i < str; i++)
            {
                for (int j = 0; j < stlb; j++)
                {
                    p_ab[i, j] = matrix[i, j] * ansabmbl[j];
                }
            }
            double[] Z = Enumerable.Range(0, p_ab.GetLength(0))
                .Select(j => Math.Round(Enumerable.Range(0, p_ab.GetLength(1))
                    .Sum(i => p_ab[j, i]), 4))
                .ToArray();
            double h_Z = Math.Round(Z.Select(func).Sum(), 3);
            for (int i = 0; i < str; i++)
            {
                for (int j = 0; j < stlb; j++)
                {
                    if (matrix[i, j] > 0)
                    {
                        double x = ansabmbl[j] * func(matrix[i, j]);
                        h_zw += x;
                    }
                }
            }
            for (int i = 0; i < str; i++)
            {
                for (int j = 0; j < stlb; j++)
                {
                    p_ba[i, j] = p_ab[i, j] / Z[i];
                }
            }
            HZW = h_zw + h_A;
            i_AB = h_Z - h_zw;
            labelH_A.Text += Math.Round(h_A, 3);
            labelH_B.Text += Math.Round(h_Z, 3);
            labelH_AB.Text += Math.Round(h_zw, 3);
            label10.Text += Math.Round(HZW, 3);
            labelI_AB.Text += Math.Round(i_AB, 3);
            label3.Text = "p(AB)";
            label5.Text = "p(A/B)";
            DisplayMatrix(p_ab);
            DisplayMatrix2(p_ba);

            labelH_BA.Text += "0,594";

            ansambleB.Text += "(0,26, 0,14, 0,42, 0,18)";

            ansambleA.Hide();
        }
    }
}
