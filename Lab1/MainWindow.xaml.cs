    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    namespace Lab1
    {
        public partial class MainWindow : Window
        {
        private ObservableCollection<double[]> MatrixData; // Объявление MatrixData

        public MainWindow()
            {
                InitializeComponent();
            }

            private void MatrixSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                if (MatrixSizeComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    string sizeText = selectedItem.Content.ToString();
                    int size = int.Parse(sizeText.Substring(0, 1));

                    MatrixData = new ObservableCollection<double[]>();
                    MatrixDataGrid.ItemsSource = MatrixData;
                    MatrixDataGrid.Columns.Clear();

                    for (int i = 0; i < size; i++)
                    {
                        MatrixDataGrid.Columns.Add(new DataGridTextColumn
                        {
                            Header = $"Колонка {i + 1}",
                            Binding = new Binding($"[{i}]"),
                            IsReadOnly = false
                        });
                    }

                    for (int i = 0; i < size; i++)
                    {
                        MatrixData.Add(new double[size]);
                    }
                }
            }

            private double[,] GetMatrixFromDataGrid()
            {
                int size = MatrixData.Count;
                double[,] matrix = new double[size, size];

                for (int i = 0; i < size; i++)
                {
                    var row = MatrixData[i];
                    for (int j = 0; j < row.Length; j++)
                    {
                        matrix[i, j] = row[j];
                    }
                }
                return matrix;

            }


            private void CalculateAllMetrics_Click(object sender, RoutedEventArgs e)
            {
                double[,] matrix = GetMatrixFromDataGrid();

                // Расчёт энтропий и других метрик
                double entropy = CalculateEntropy(matrix);
                double[] marginalX = CalculateMarginalProbabilitiesX(matrix);
                double[] marginalY = CalculateMarginalProbabilitiesY(matrix);
                double conditionalEntropyBA = CalculateConditionalEntropy(matrix, marginalY);
                double conditionalEntropyAB = CalculateConditionalEntropy(matrix, marginalX);
                double jointEntropy = CalculateJointEntropy(matrix);
                double mutualInformation = CalculateMutualInformation(marginalX, marginalY, matrix);

                double[,] jointProbabilities = CalculateJointProbabilities(matrix);
                double[,] conditionalProbabilities = CalculateConditionalProbabilities(matrix, marginalY);

                ResultsTextBlock.Text = $"Энтропия H(A): {entropy:F4}\n" +
                                        $"Условная энтропия H(B|A): {conditionalEntropyBA:F4}\n" +
                                        $"Условная энтропия H(A|B): {conditionalEntropyAB:F4}\n" +
                                        $"Совместная энтропия H(A,B): {jointEntropy:F4}\n" +
                                        $"Взаимная информация I(A,B): {mutualInformation:F4}\n" +
                                        "Совместные вероятности p(ai, bj):\n" + FormatMatrix(jointProbabilities) +
                                        "\nУсловные вероятности p(ai|bj):\n" + FormatMatrix(conditionalProbabilities);
            }

            private double CalculateEntropy(double[,] matrix)
            {
                double entropy = 0;
                foreach (var p in matrix)
                {
                    if (p > 0)
                        entropy -= p * Math.Log2(p);
                }
                return entropy;
            }

            private double[] CalculateMarginalProbabilitiesX(double[,] matrix)
            {
                int rows = matrix.GetLength(0);
                int cols = matrix.GetLength(1);
                double[] marginalX = new double[rows];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        marginalX[i] += matrix[i, j];
                    }
                }
                return marginalX;
            }

            private double[] CalculateMarginalProbabilitiesY(double[,] matrix)
            {
                int rows = matrix.GetLength(0);
                int cols = matrix.GetLength(1);
                double[] marginalY = new double[cols];

                for (int j = 0; j < cols; j++)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        marginalY[j] += matrix[i, j];
                    }
                }
                return marginalY;
            }

            private double CalculateConditionalEntropy(double[,] matrix, double[] marginal)
            {
                double conditionalEntropy = 0;
                int rows = matrix.GetLength(0);
                int cols = matrix.GetLength(1);

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (matrix[i, j] > 0 && marginal[j] > 0)
                        {
                            conditionalEntropy -= matrix[i, j] * Math.Log2(matrix[i, j] / marginal[j]);
                        }
                    }
                }
                return conditionalEntropy;
            }

            private double CalculateJointEntropy(double[,] matrix)
            {
                return CalculateEntropy(matrix);
            }

            private double CalculateMutualInformation(double[] marginalX, double[] marginalY, double[,] matrix)
            {
                double mutualInformation = 0;
                int rows = matrix.GetLength(0);
                int cols = matrix.GetLength(1);

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (matrix[i, j] > 0)
                        {
                            mutualInformation += matrix[i, j] * Math.Log2(matrix[i, j] / (marginalX[i] * marginalY[j]));
                        }
                    }
                }
                return mutualInformation;
            }

            private double[,] CalculateJointProbabilities(double[,] matrix)
            {
                double total = 0;
                foreach (var p in matrix)
                {
                    total += p;
                }

                int rows = matrix.GetLength(0);
                int cols = matrix.GetLength(1);
                double[,] jointProbabilities = new double[rows, cols];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        jointProbabilities[i, j] = matrix[i, j] / total;
                    }
                }
                return jointProbabilities;
            }

            private double[,] CalculateConditionalProbabilities(double[,] matrix, double[] marginalY)
            {
                int rows = matrix.GetLength(0);
                int cols = matrix.GetLength(1);
                double[,] conditionalProbabilities = new double[rows, cols];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        conditionalProbabilities[i, j] = marginalY[j] > 0 ? matrix[i, j] / marginalY[j] : 0;
                    }
                }
                return conditionalProbabilities;
            }


            private string FormatMatrix(double[,] matrix)
            {
                int rows = matrix.GetLength(0);
                int cols = matrix.GetLength(1);
                string formatted = "";
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        formatted += $"{matrix[i, j]:F4}\t";
                    }
                    formatted += "\n";
                }
                return formatted;
            }
        }
    }
