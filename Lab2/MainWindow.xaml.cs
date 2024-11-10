using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ShannonFanoApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnEncodeButtonClick(object sender, RoutedEventArgs e)
        {
            // Получаем введенное сообщение
            string message = MessageTextBox.Text;

            if (string.IsNullOrEmpty(message))
            {
                MessageBox.Show("Пожалуйста, введите сообщение для кодирования.");
                return;
            }

            // Разбиваем сообщение на символы и вычисляем вероятности
            var frequencies = CalculateFrequencies(message);
            var probabilities = frequencies.Select(f => new SymbolProbability(f.Key, f.Value / (double)message.Length)).ToList();

            Dictionary<string, string> codes;

            // Выбираем алгоритм кодирования в зависимости от выбранной радиокнопки
            if (ShannonFanoRadioButton.IsChecked == true)
            {
                // Запускаем кодирование Шеннона-Фано
                codes = ShannonFano(probabilities);
            }
            else
            {
                // Запускаем кодирование Хаффмана
                codes = Huffman(probabilities);
            }

            // Формируем строку с результатами
            string result = "Коды символов:\n";
            foreach (var code in codes)
            {
                result += $"{code.Key}: {code.Value}\n";
            }

            // Вычисляем среднюю длину и дисперсию
            var averageLength = CalculateAverageLength(codes, probabilities);
            var variance = CalculateVariance(codes, probabilities, averageLength);

            // Отображаем результат
            ResultTextBlock.Text = result;
            AverageLengthTextBlock.Text = $"Средняя длина кода: {averageLength:F4}";
            VarianceTextBlock.Text = $"Дисперсия: {variance:F4}";
        }

        // Функция для подсчета частоты символов
        private Dictionary<char, int> CalculateFrequencies(string message)
        {
            var frequencies = new Dictionary<char, int>();

            foreach (char symbol in message)
            {
                if (frequencies.ContainsKey(symbol))
                {
                    frequencies[symbol]++;
                }
                else
                {
                    frequencies[symbol] = 1;
                }
            }

            return frequencies;
        }

        // Алгоритм Шеннона-Фано
        private Dictionary<string, string> ShannonFano(List<SymbolProbability> probabilities)
        {
            probabilities = probabilities.OrderByDescending(x => x.Probability).ToList();
            var codes = new Dictionary<string, string>();

            void Encode(List<SymbolProbability> symbolList, string code)
            {
                if (symbolList.Count == 1)
                {
                    codes[symbolList[0].Symbol.ToString()] = code;
                    return;
                }

                double totalProbability = symbolList.Sum(x => x.Probability);
                double cumulativeProb = 0;
                int splitIndex = 0;

                for (int i = 0; i < symbolList.Count; i++)
                {
                    cumulativeProb += symbolList[i].Probability;
                    if (cumulativeProb >= totalProbability / 2)
                    {
                        splitIndex = i + 1;
                        break;
                    }
                }

                Encode(symbolList.Take(splitIndex).ToList(), code + "0");
                Encode(symbolList.Skip(splitIndex).ToList(), code + "1");
            }

            Encode(probabilities, "");
            return codes;
        }

        // Алгоритм Хаффмана
        private Dictionary<string, string> Huffman(List<SymbolProbability> probabilities)
        {
            var priorityQueue = new PriorityQueue<HuffmanNode, double>();

            foreach (var prob in probabilities)
            {
                priorityQueue.Enqueue(new HuffmanNode(prob.Symbol.ToString(), prob.Probability), prob.Probability);
            }

            while (priorityQueue.Count > 1)
            {
                var left = priorityQueue.Dequeue();
                var right = priorityQueue.Dequeue();

                var mergedNode = new HuffmanNode(left.Symbol + right.Symbol, left.Probability + right.Probability)
                {
                    Left = left,
                    Right = right
                };

                priorityQueue.Enqueue(mergedNode, mergedNode.Probability);
            }

            var codes = new Dictionary<string, string>();
            GenerateCodes(priorityQueue.Dequeue(), "", codes);

            return codes;
        }

        private void GenerateCodes(HuffmanNode node, string code, Dictionary<string, string> codes)
        {
            if (node == null)
                return;

            if (node.Left == null && node.Right == null)
            {
                codes[node.Symbol] = code;
                return;
            }

            GenerateCodes(node.Left, code + "0", codes);
            GenerateCodes(node.Right, code + "1", codes);
        }

        // Расчет средней длины кода
        private double CalculateAverageLength(Dictionary<string, string> codes, List<SymbolProbability> probabilities)
        {
            double avgLength = 0;
            foreach (var code in codes)
            {
                var symbol = code.Key[0];
                var probability = probabilities.First(p => p.Symbol == symbol).Probability;
                avgLength += probability * code.Value.Length;
            }
            return avgLength;
        }

        // Расчет дисперсии
        private double CalculateVariance(Dictionary<string, string> codes, List<SymbolProbability> probabilities, double averageLength)
        {
            double variance = 0;
            foreach (var code in codes)
            {
                var symbol = code.Key[0];
                var probability = probabilities.First(p => p.Symbol == symbol).Probability;
                variance += probability * Math.Pow(code.Value.Length - averageLength, 2);
            }
            return variance;
        }
    }

    public class SymbolProbability
    {
        public char Symbol { get; }
        public double Probability { get; }

        public SymbolProbability(char symbol, double probability)
        {
            Symbol = symbol;
            Probability = probability;
        }
    }

    public class HuffmanNode
    {
        public string Symbol { get; set; }
        public double Probability { get; set; }
        public HuffmanNode Left { get; set; }
        public HuffmanNode Right { get; set; }

        public HuffmanNode(string symbol, double probability)
        {
            Symbol = symbol;
            Probability = probability;
        }
    }
}
