using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Kod
{
    public partial class Form1 : Form
    {
        private string textVariable;
        private bool check = false;

        private string str;
        private double[] P1;
        private string[] Res;

        public Form1()
        {
            InitializeComponent();
            resDataGridView.Columns.Add("Column3", "Шеннон-Фано");
            resDataGridView.Columns.Add("Column4", "Хаффман");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textVariable != TextBox.Text)
                check = false;

            if (!check)
                str = TextBox.Text.ToLower();
            else
                str = textVariable.ToLower();

            // Расчет вероятностей
            Dictionary<char, double> probabilities = CalculateProbabilities(str);

            // Шеннон-Фано
            var (shannonCodes, lFan) = CalculateShannonFano(probabilities);

            // Энтропия
            double hFan = CalculateEntropy(probabilities.Values.ToArray());
            double dFan = (lFan - hFan) / lFan;

            // Хаффман
            var (huffmanCodes, lHof) = CalculateHuffman(probabilities);
            double dHof = (lHof - hFan) / lHof;

            // Обновляем UI
            PopulateDataGridView(probabilities, shannonCodes, huffmanCodes);

            ShIlabel.Text = "l = " + Math.Round(lFan, 4);
            ShHlabel.Text = "H = " + Math.Round(hFan, 4);
            ShDlabel.Text = "D = " + Math.Round(dFan, 4);

            HafILabel.Text = "l = " + Math.Round(lHof, 4);
            HafHlabel.Text = "H = " + Math.Round(hFan, 4);
            HafDlabel.Text = "D = " + Math.Round(dHof, 4);
        }

        private Dictionary<char, double> CalculateProbabilities(string text)
        {
            Dictionary<char, int> chars = new Dictionary<char, int>();
            foreach (char c in text)
            {
                if (!chars.ContainsKey(c))
                    chars[c] = 1;
                else
                    chars[c]++;
            }

            return chars.ToDictionary(
                kvp => kvp.Key,
                kvp => (double)kvp.Value / text.Length
            );
        }

        private void PopulateDataGridView(Dictionary<char, double> probabilities, string[] shannonCodes, List<string> huffmanCodes)
        {
            resDataGridView.Rows.Clear();
            int i = 0;
            foreach (var pair in probabilities)
            {
                resDataGridView.Rows.Add();
                resDataGridView[0, i].Value = pair.Key;
                resDataGridView[1, i].Value = Math.Round(pair.Value, 4);
                resDataGridView[2, i].Value = shannonCodes[i];
                resDataGridView[3, i].Value = huffmanCodes[i];
                i++;
            }
        }

        private double CalculateEntropy(double[] probabilities)
        {
            return probabilities.Sum(p => f(p));
        }

        private (string[], double) CalculateShannonFano(Dictionary<char, double> probabilities)
        {
            ShannonFanoEncoder shannonFanoEncoder = new ShannonFanoEncoder(probabilities.Values.ToArray());
            string[] codes = shannonFanoEncoder.Encode();

            double avgLength = probabilities.Values
                .Zip(codes, (prob, code) => prob * code.Length)
                .Sum();

            return (codes, avgLength);
        }

        private (List<string>, double) CalculateHuffman(Dictionary<char, double> probabilities)
        {
            HuffmanTree huffmanTree = new HuffmanTree();
            huffmanTree.Build(probabilities);

            List<string> codes = huffmanTree.ReturnAlphabet();
            double avgLength = probabilities.Values
                .Zip(codes, (prob, code) => prob * code.Length)
                .Sum();

            return (codes, avgLength);
        }


        private double f(double x)
        {
            return -x * Math.Log(x, 2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            check = true;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string fileText = File.ReadAllText(filePath);
                textVariable = fileText;
                TextBox.Text = textVariable;
            }
        }
    }
}
