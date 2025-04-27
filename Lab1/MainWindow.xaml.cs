using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace lab1
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<ObservableCollection<MatrixCell>> matrixJointData;
        private ObservableCollection<ObservableCollection<MatrixCell>> matrixCondWgivenZData;
        private ObservableCollection<ObservableCollection<MatrixCell>> matrixCondZgivenWData;
        private ObservableCollection<MatrixCell> ensembleZData;
        private ObservableCollection<MatrixCell> ensembleWData;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            InitializeMatrixInputs(2, 4); // Инициализация матриц
            SetDefaultValues(); // Устанавливаем значения из задачи №9 для p(Zi, Wj)
        }

        private void InitializeMatrixInputs(int rows, int cols)
        {
            // Инициализация p(Zi, Wj)
            matrixJointData = new ObservableCollection<ObservableCollection<MatrixCell>>();
            for (int i = 0; i < rows; i++)
            {
                var row = new ObservableCollection<MatrixCell>();
                for (int j = 0; j < cols; j++)
                {
                    row.Add(new MatrixCell { Value = "0" });
                }
                matrixJointData.Add(row);
            }
            MatrixJointInput.ItemsSource = matrixJointData;

            // Инициализация p(Wj | Zi)
            matrixCondWgivenZData = new ObservableCollection<ObservableCollection<MatrixCell>>();
            for (int i = 0; i < rows; i++)
            {
                var row = new ObservableCollection<MatrixCell>();
                for (int j = 0; j < cols; j++)
                {
                    row.Add(new MatrixCell { Value = "0" });
                }
                matrixCondWgivenZData.Add(row);
            }
            MatrixCondWgivenZInput.ItemsSource = matrixCondWgivenZData;

            // Инициализация p(Zi | Wj)
            matrixCondZgivenWData = new ObservableCollection<ObservableCollection<MatrixCell>>();
            for (int i = 0; i < rows; i++)
            {
                var row = new ObservableCollection<MatrixCell>();
                for (int j = 0; j < cols; j++)
                {
                    row.Add(new MatrixCell { Value = "0" });
                }
                matrixCondZgivenWData.Add(row);
            }
            MatrixCondZgivenWInput.ItemsSource = matrixCondZgivenWData;

            // Инициализация ансамбля p(Zi)
            ensembleZData = new ObservableCollection<MatrixCell>();
            for (int i = 0; i < rows; i++)
            {
                ensembleZData.Add(new MatrixCell { Value = "0" });
            }
            EnsembleZInput.ItemsSource = ensembleZData;

            // Инициализация ансамбля p(Wj)
            ensembleWData = new ObservableCollection<MatrixCell>();
            for (int j = 0; j < cols; j++)
            {
                ensembleWData.Add(new MatrixCell { Value = "0" });
            }
            EnsembleWInput.ItemsSource = ensembleWData;
        }

        private void SetDefaultValues()
        {
            // Значения из задачи №9 для p(Zi, Wj)
            var defaultMatrix = new double[,]
            {
                { 0.32, 0.10, 0.16, 0.02 },
                { 0.08, 0.20, 0.04, 0.08 }
            };
            var defaultEnsembleW = new double[] { 0.40, 0.30, 0.20, 0.10 };

            for (int i = 0; i < matrixJointData.Count; i++)
            {
                for (int j = 0; j < matrixJointData[i].Count; j++)
                {
                    matrixJointData[i][j].Value = defaultMatrix[i, j].ToString("0.00");
                }
            }

            for (int j = 0; j < ensembleWData.Count; j++)
            {
                ensembleWData[j].Value = defaultEnsembleW[j].ToString("0.00");
            }
        }

        private void UpdateMatrixButton_Click(object sender, RoutedEventArgs e)
        {
            int rows = int.Parse((RowsComboBox.SelectedItem as ComboBoxItem).Content.ToString());
            int cols = int.Parse((ColsComboBox.SelectedItem as ComboBoxItem).Content.ToString());
            InitializeMatrixInputs(rows, cols);
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int rows = matrixJointData.Count;
                int cols = matrixJointData[0].Count;

                // Парсим все матрицы
                double[,] matrixJoint = new double[rows, cols];
                double[,] matrixCondWgivenZ = new double[rows, cols];
                double[,] matrixCondZgivenW = new double[rows, cols];
                double[] ensembleZ = new double[rows];
                double[] ensembleW = new double[cols];

                bool isJointFilled = false;
                bool isCondWgivenZFilled = false;
                bool isCondZgivenWFilled = false;

                // p(Zi, Wj)
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        matrixJoint[i, j] = double.Parse(matrixJointData[i][j].Value);
                        if (matrixJoint[i, j] != 0) isJointFilled = true;
                    }
                }

                // p(Wj | Zi)
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        matrixCondWgivenZ[i, j] = double.Parse(matrixCondWgivenZData[i][j].Value);
                        if (matrixCondWgivenZ[i, j] != 0) isCondWgivenZFilled = true;
                    }
                }

                // p(Zi | Wj)
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        matrixCondZgivenW[i, j] = double.Parse(matrixCondZgivenWData[i][j].Value);
                        if (matrixCondZgivenW[i, j] != 0) isCondZgivenWFilled = true;
                    }
                }

                // Ансамбль p(Zi)
                for (int i = 0; i < rows; i++)
                {
                    ensembleZ[i] = double.Parse(ensembleZData[i].Value);
                }

                // Ансамбль p(Wj)
                for (int j = 0; j < cols; j++)
                {
                    ensembleW[j] = double.Parse(ensembleWData[j].Value);
                }

                // Проверка, что хотя бы одна матрица заполнена
                if (!isJointFilled && !isCondWgivenZFilled && !isCondZgivenWFilled)
                {
                    throw new Exception("Хотя бы одна матрица должна быть заполнена (содержать ненулевые значения).");
                }

                // Определяем тип задачи на основе заполненной матрицы
                int taskType = -1;
                double[,] inputMatrix = null;
                double[] inputEnsemble = null;

                if (isJointFilled)
                {
                    taskType = 0;
                    inputMatrix = matrixJoint;
                }
                else if (isCondWgivenZFilled)
                {
                    taskType = 1;
                    inputMatrix = matrixCondWgivenZ;
                    inputEnsemble = ensembleZ;
                }
                else if (isCondZgivenWFilled)
                {
                    taskType = 2;
                    inputMatrix = matrixCondZgivenW;
                    inputEnsemble = ensembleW;
                }

                // Проверяем корректность введённых данных
                ValidateInput(inputMatrix, inputEnsemble, taskType, rows, cols);

                // Решаем задачу
                var result = SolveTask(inputMatrix, inputEnsemble, taskType, rows, cols);
                ResultsTextBlock.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ValidateInput(double[,] matrix, double[] ensemble, int taskType, int rows, int cols)
        {
            // Проверяем, что все элементы неотрицательны
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] < 0)
                        throw new Exception("Вероятности не могут быть отрицательными.");
                }
            }

            if (taskType != 0) // Ансамбль проверяем только если он используется
            {
                for (int j = 0; j < ensemble.Length; j++)
                {
                    if (ensemble[j] < 0)
                        throw new Exception("Вероятности ансамбля не могут быть отрицательными.");
                }
            }

            // Проверяем суммы в зависимости от типа задачи
            if (taskType == 0) // p(Zi, Wj)
            {
                double sum = 0;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        sum += matrix[i, j];
                    }
                }
                if (Math.Abs(sum - 1.0) > 0.05)
                    throw new Exception($"Сумма элементов p(Zi, Wj) должна быть равна 1. Текущая сумма: {sum}");
            }
            else if (taskType == 1) // p(Wj | Zi)
            {
                for (int i = 0; i < rows; i++)
                {
                    double rowSum = 0;
                    for (int j = 0; j < cols; j++)
                    {
                        rowSum += matrix[i, j];
                    }
                    if (Math.Abs(rowSum - 1.0) > 0.05)
                        throw new Exception("Сумма по строкам p(Wj | Zi) должна быть равна 1.");
                }
                double ensembleSum = ensemble.Sum();
                if (Math.Abs(ensembleSum - 1.0) > 0.05)
                    throw new Exception("Сумма элементов p(Zi) должна быть равна 1.");
            }
            else if (taskType == 2) // p(Zi | Wj)
            {
                for (int j = 0; j < cols; j++)
                {
                    double colSum = 0;
                    for (int i = 0; i < rows; i++)
                    {
                        colSum += matrix[i, j];
                    }
                    if (Math.Abs(colSum - 1.0) > 0.05)
                        throw new Exception("Сумма по столбцам p(Zi | Wj) должна быть равна 1.");
                }
                double ensembleSum = ensemble.Sum();
                if (Math.Abs(ensembleSum - 1.0) > 0.05)
                    throw new Exception("Сумма элементов p(Wj) должна быть равна 1.");
            }
        }

        private double Log2(double n)
        {
            return Math.Log(n) / Math.Log(2);
        }

        private string SolveTask(double[,] inputMatrix, double[] inputEnsemble, int taskType, int rows, int cols)
        {
            double[,] p_Zi_Wj = new double[rows, cols]; // Совместные вероятности

            double[] p_Zi = new double[rows];
            double[] p_Wj = new double[cols];

            double[,] p_Wj_given_Zi = new double[rows, cols];
            double[,] p_Zi_given_Wj = new double[rows, cols];

            // Шаг 1: Определяем p(Zi, Wj) в зависимости от типа задачи
            if (taskType == 0) // Дана p(Zi, Wj)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        p_Zi_Wj[i, j] = inputMatrix[i, j];
                    }
                }
            }
            else if (taskType == 1) // Дана p(Wj | Zi) и p(Zi)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        p_Zi_Wj[i, j] = inputMatrix[i, j] * inputEnsemble[i];
                    }
                }
            }
            else if (taskType == 2) // Дана p(Zi | Wj) и p(Wj)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        p_Zi_Wj[i, j] = inputMatrix[i, j] * inputEnsemble[j];
                    }
                }
            }

            // Шаг 2: Находим p(Zi) и p(Wj)
            for (int i = 0; i < rows; i++)
            {
                p_Zi[i] = 0;
                for (int j = 0; j < cols; j++)
                {
                    p_Zi[i] += p_Zi_Wj[i, j];
                }
            }

            for (int j = 0; j < cols; j++)
            {
                p_Wj[j] = 0;
                for (int i = 0; i < rows; i++)
                {
                    p_Wj[j] += p_Zi_Wj[i, j];
                }
            }

            // Шаг 3: Находим p(Wj | Zi) и p(Zi | Wj)
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (p_Zi[i] > 0)
                        p_Wj_given_Zi[i, j] = p_Zi_Wj[i, j] / p_Zi[i];
                    else
                        p_Wj_given_Zi[i, j] = 0;

                    if (p_Wj[j] > 0)
                        p_Zi_given_Wj[i, j] = p_Zi_Wj[i, j] / p_Wj[j];
                    else
                        p_Zi_given_Wj[i, j] = 0;
                }
            }

            // Шаг 4: Вычисляем энтропии
            double H_Z = 0;
            for (int i = 0; i < rows; i++)
            {
                if (p_Zi[i] > 0)
                    H_Z -= p_Zi[i] * Log2(p_Zi[i]);
            }

            double H_W = 0;
            for (int j = 0; j < cols; j++)
            {
                if (p_Wj[j] > 0)
                    H_W -= p_Wj[j] * Log2(p_Wj[j]);
            }

            double H_ZW = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (p_Zi_Wj[i, j] > 0)
                        H_ZW -= p_Zi_Wj[i, j] * Log2(p_Zi_Wj[i, j]);
                }
            }

            double H_W_Z = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (p_Zi_Wj[i, j] > 0 && p_Zi_given_Wj[i, j] > 0)
                        H_W_Z -= p_Zi_Wj[i, j] * Log2(p_Zi_given_Wj[i, j]);
                }
            }

            double H_Z_W = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (p_Zi_Wj[i, j] > 0 && p_Wj_given_Zi[i, j] > 0)
                        H_Z_W -= p_Zi_Wj[i, j] * Log2(p_Wj_given_Zi[i, j]);
                }
            }

            // Шаг 5: Вычисляем I(Z;W)
            double I_ZW = H_Z - H_W_Z;

            // Формируем вывод
            StringBuilder result = new StringBuilder();

            result.AppendLine("Матрица p(Zi, Wj):");
            for (int i = 0; i < rows; i++)
            {
                var row = new List<string>();
                for (int j = 0; j < cols; j++)
                {
                    row.Add(p_Zi_Wj[i, j].ToString("0.00"));
                }
                result.AppendLine(string.Join("\t", row));
            }

            result.AppendLine("\nМатрица p(Wj | Zi):");
            for (int i = 0; i < rows; i++)
            {
                var row = new List<string>();
                for (int j = 0; j < cols; j++)
                {
                    row.Add(p_Wj_given_Zi[i, j].ToString("0.00"));
                }
                result.AppendLine(string.Join("\t", row));
            }

            result.AppendLine("\nМатрица p(Zi | Wj):");
            for (int i = 0; i < rows; i++)
            {
                var row = new List<string>();
                for (int j = 0; j < cols; j++)
                {
                    row.Add(p_Zi_given_Wj[i, j].ToString("0.00"));
                }
                result.AppendLine(string.Join("\t", row));
            }

            result.AppendLine("\nАнсамбль p(Zi):");
            result.AppendLine(string.Join("\t", p_Zi.Select(p => p.ToString("0.00"))));

            result.AppendLine("\nАнсамбль p(Wj):");
            result.AppendLine(string.Join("\t", p_Wj.Select(p => p.ToString("0.00"))));

            result.AppendLine("\nЭнтропии:");
            result.AppendLine($"H(Z) = {H_Z.ToString("0.00")}");
            result.AppendLine($"H(W) = {H_W.ToString("0.00")}");
            result.AppendLine($"H(Z, W) = {H_ZW.ToString("0.00")}");
            result.AppendLine($"H_W(Z) = {H_W_Z.ToString("0.00")}");
            result.AppendLine($"H_Z(W) = {H_Z_W.ToString("0.00")}");
            result.AppendLine($"I(Z;W) = {I_ZW.ToString("0.00")}");

            return result.ToString();
        }
    }

    public class MatrixCell : INotifyPropertyChanged
    {
        private string value;

        public string Value
        {
            get => value;
            set
            {
                this.value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}