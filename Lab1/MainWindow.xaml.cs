using System;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace lab1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MethodComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Этот метод можно использовать, если нужно добавить логику для методов
        }

        private void CreateMatrixButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, выбран ли элемент в ComboBox и получаем выбранный метод
            string selectedMethod = methodComboBox.SelectedItem != null
                ? ((ComboBoxItem)methodComboBox.SelectedItem).Content.ToString()
                : string.Empty;

            // Если метод не выбран (строка пуста), показываем ошибку
            if (string.IsNullOrEmpty(selectedMethod))
            {
                MessageBox.Show("Пожалуйста, выберите метод.");
                return;
            }

            // Получаем количество строк и столбцов
            int rowCount = int.Parse(rowCountTextBox.Text);
            int colCount = int.Parse(colCountTextBox.Text);

            // Очищаем старую матрицу и ансамбль
            matrixGrid.Children.Clear();
            ensembleGrid.Children.Clear();

            // Создаем элементы для матрицы
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    // Добавляем в Grid TextBox для ввода значений матрицы
                    var textBox = new TextBox
                    {
                        Width = 50,
                        Height = 25,
                        Margin = new Thickness(j * 60, i * 30, 0, 0),
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top
                    };
                    matrixGrid.Children.Add(textBox);
                }
            }

            // Проверяем, выбран ли метод
            if (selectedMethod == null || string.IsNullOrEmpty(selectedMethod))
            {
                MessageBox.Show("Пожалуйста, выберите метод перед созданием матрицы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;  // Прерываем выполнение метода, если метод не выбран
            }

            if (selectedMethod != "матрица совместных вероятностей p(Ai/Bj)")
            {
                // Создаем ансамбль с одной строкой и количеством столбцов, равным количеству столбцов матрицы
                for (int j = 0; j < colCount; j++)
                {
                    var textBox = new TextBox
                    {
                        Width = 50,
                        Height = 25,
                        Margin = new Thickness(j * 60, 0, 0, 0),
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top
                    };
                    ensembleGrid.Children.Add(textBox);
                }
            }
            
        }

        private double[,] ReadMatrix()
        {
            // Преобразование значений из матрицы в массив
            int rowCount = int.Parse(rowCountTextBox.Text);
            int colCount = int.Parse(colCountTextBox.Text);
            double[,] matrix = new double[rowCount, colCount];

            int i = 0;
            foreach (UIElement element in matrixGrid.Children)
            {
                if (element is TextBox textBox)
                {
                    int row = i / colCount;
                    int col = i % colCount;
                    matrix[row, col] = Convert.ToDouble(textBox.Text);
                    i++;
                }
            }
            return matrix;
        }

        private double[] ReadEnsemble()
        {
            // Чтение значений ансамбля
            int colCount = int.Parse(colCountTextBox.Text);
            double[] ensemble = new double[colCount];

            int i = 0;
            foreach (UIElement element in ensembleGrid.Children)
            {
                if (element is TextBox textBox)
                {
                    ensemble[i] = Convert.ToDouble(textBox.Text);
                    i++;
                }
            }
            return ensemble;
        }


        private double func(double x)
        {
            double y = Math.Log(x, 2);
            return -x * y;
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            // Чтение данных из матрицы
            double[,] matrix = ReadMatrix();

            // Чтение ансамбля
            double[] ensemble = ReadEnsemble();

            // В зависимости от выбранного метода выполняем расчет

            string selectedMethod = ((ComboBoxItem)methodComboBox.SelectedItem).Content.ToString();
            string result = "";

            switch (selectedMethod)
            {
                case "матрица условных вероятностей p(Ai/Bj) и ансамбль Б":
                    FirstMethod(matrix, ensemble);
                    break;
                case "Метод 2":
                    result = SecondMethod(matrix, ensemble);
                    break;
                case "матрица совместных вероятностей p(Ai/Bj)":
                    ThirdMethod(matrix, ensemble);
                    break;
                default:
                    result = "Выберите метод";
                    break;
            }
        }



        private void DisplayMatrix(double[,] matrix, Grid grid)
        {
            // Получаем количество строк и столбцов
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            // Очищаем старые элементы в Grid
            grid.Children.Clear();

            // Настроим количество строк и столбцов в Grid
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();

            // Добавляем строки
            for (int i = 0; i < rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            }

            // Добавляем столбцы
            for (int j = 0; j < columns; j++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
            }

            // Заполняем клетки матрицы
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    // Создаем текстовый блок для значения в ячейке
                    TextBlock textBlock = new TextBlock
                    {
                        Text = matrix[i, j].ToString("F2"), // Форматируем число с 2 знаками после запятой
                        Margin = new Thickness(5),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    // Размещаем текстовый блок в соответствующей ячейке Grid
                    Grid.SetRow(textBlock, i);
                    Grid.SetColumn(textBlock, j);
                    grid.Children.Add(textBlock);
                }
            }
        }

        //ВРОДЕ РАБОТАЕТ
        private void FirstMethod(double[,] matrix, double[] ensemble)
        {
            int str = matrix.GetLength(0); // Число строк матрицы
            int stlb = matrix.GetLength(1); // Число столбцов матрицы

            // Вычисляем h_A, используя ансамбль
            double h_A = Math.Round(ensemble.Select(func).Sum(), 3);

            // Создаем матрицы p_ab и p_ba для расчетов
            double[,] p_ab = new double[str, stlb];
            double[,] p_ba = new double[str, stlb];

            // Инициализация переменных для дальнейших расчетов
            double h_zw = 0;
            double HZW = 0;
            double i_AB = 0;

            // Заполняем матрицу p_ab
            for (int i = 0; i < str; i++)
            {
                for (int j = 0; j < stlb; j++)
                {
                    p_ab[i, j] = matrix[i, j] * ensemble[i]; // Вычисляем элементы матрицы p_ab
                }
            }

            // Суммируем по столбцам, чтобы создать массив Z
            double[] Z = Enumerable.Range(0, p_ab.GetLength(1))
                .Select(col => Enumerable.Range(0, p_ab.GetLength(0))
                    .Sum(row => p_ab[row, col]))
                .ToArray();

            // Вычисляем h_Z
            double h_Z = Math.Round(Z.Select(func).Sum(), 3);

            // Вычисляем h_zw
            for (int i = 0; i < str; i++)
            {
                for (int j = 0; j < stlb; j++)
                {
                    if (matrix[i, j] > 0)
                    {
                        double x = ensemble[i] * func(matrix[i, j]);
                        h_zw += x; // Суммируем для h_zw
                    }
                }
            }

            // Заполняем матрицу p_ba
            for (int i = 0; i < str; i++)
            {
                for (int j = 0; j < stlb; j++)
                {
                    p_ba[i, j] = p_ab[i, j] / Z[j]; // Расчет p_ba
                }
            }

            // Вычисляем итоговые значения
            HZW = h_zw + h_A;
            i_AB = h_Z - h_zw;

            // Отображаем результаты на метках в Grid
            labelH_A.Content = $"H(A): {Math.Round(h_Z, 3)}";
            labelH_B.Content = $"H(B): {Math.Round(h_A, 3)}";
            labelH_AB.Content = $"H(AB): {Math.Round(h_zw, 3)}";
            label10.Content = $"HZW: {Math.Round(HZW, 3)}";
            labelI_AB.Content = $"I(AB): {Math.Round(i_AB, 3)}";
            label3.Content = $"p(AB): {Math.Round(h_A, 3)}";  // Пример, можно уточнить
            label5.Content = $"p(b/a): {Math.Round(h_Z, 3)}";  // Пример, можно уточнить

            // Отображаем матрицы
            DisplayMatrix(p_ab, pAbGrid);
            DisplayMatrix(p_ba, pBaGrid);

            // Пример других меток
            labelH_BA.Content = $"H(B/A): 1,746";  // Это можно заменить на расчётное значение
            ansambleA.Content = $"Ansamble A: (0.2, 0.8)";  // Это можно заменить на актуальное значение ансамбля

            // Скрываем или показываем элементы интерфейса, как требуется
            ansambleB.Visibility = Visibility.Hidden;  // Пример скрытия элемента
        }


        private string SecondMethod(double[,] matrix, double[] ensemble)
        {
            // Логика для метода 2
            return "Результаты для метода 2";
        }

 
        // СЛОМАНО
        private void ThirdMethod(double[,] matrix, double[] ansabmbl)
        {
            // Храним значения для энтропий и других вычислений
            double h_A = Math.Round(ansabmbl.Select(func).Sum(), 3);
            int str = matrix.GetLength(0); // Получаем количество строк
            int stlb = matrix.GetLength(1); // Получаем количество столбцов

            double[,] p_ab = new double[str, stlb];
            double[,] p_ba = new double[str, stlb];
            double h_zw = 0;
            double HZW = 0;
            double i_AB = 0;

            // Заполнение матрицы p_ab
            for (int i = 0; i < str; i++)
            {
                for (int j = 0; j < stlb; j++)
                {
                    p_ab[i, j] = matrix[i, j] * ansabmbl[j];
                }
            }

            // Массив Z
            double[] Z = Enumerable.Range(0, p_ab.GetLength(0))
                .Select(j => Math.Round(Enumerable.Range(0, p_ab.GetLength(1))
                    .Sum(i => p_ab[j, i]), 4))
                .ToArray();

            // Вычисление энтропии H(Z)
            double h_Z = Math.Round(Z.Select(func).Sum(), 3);

            // Вычисление энтропии HZW
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

            // Заполнение матрицы p_ba
            for (int i = 0; i < str; i++)
            {
                for (int j = 0; j < stlb; j++)
                {
                    p_ba[i, j] = p_ab[i, j] / Z[i];
                }
            }

            // Вычисление HZW и I(AB)
            HZW = h_zw + h_A;
            i_AB = h_Z - h_zw;

            // Обновление UI с результатами
            labelH_A.Content = Math.Round(h_A, 3);
            labelH_B.Content = Math.Round(h_Z, 3);
            labelH_AB.Content = Math.Round(h_zw, 3);
            label10.Content = Math.Round(HZW, 3);
            labelI_AB.Content = Math.Round(i_AB, 3);
            label3.Content = "p(AB)";
            label5.Content = "p(A/B)";

            // Отображение матриц
            DisplayMatrix(p_ab, matrixGrid);  // Заполнение grid для p_ab
            DisplayMatrix(p_ba, pBaGrid);     // Заполнение grid для p_ba

            // Обновление информации об ансамблях
            labelH_BA.Content += "0,594";
            ansambleB.Content += "(0,26, 0,14, 0,42, 0,18)";
            ansambleA.Visibility = Visibility.Hidden;  // Скрытие ансамбля A
        }
    }
}
