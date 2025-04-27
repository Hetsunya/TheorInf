using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace HammingCodeWPF
{
    public partial class MainWindow : Window
    {
        private int ERROR_VAL = -1;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region HAMMING_CODE
        private BitArray Code(string inMessage)
        {
            if (!IsValidBinaryString(inMessage))
                throw new ArgumentException("Входное сообщение должно содержать только 0 и 1.");

            var messageArray = new BitArray(inMessage.Length);
            for (int i = 0; i < inMessage.Length; i++)
                messageArray[i] = inMessage[i] == '1';

            int messageInd = 0;
            int retInd = 0;
            int controlIndex = 1;
            var retArray = new BitArray(messageArray.Length + (int)Math.Ceiling(Math.Log(messageArray.Length + 1, 2)));

            while (messageInd < messageArray.Length)
            {
                if (retInd + 1 == controlIndex)
                {
                    retInd++;
                    controlIndex *= 2;
                    continue;
                }
                retArray.Set(retInd, messageArray.Get(messageInd));
                messageInd++;
                retInd++;
            }

            retInd = 0;
            controlIndex = 1;
            while (controlIndex <= retArray.Length)
            {
                int c = controlIndex - 1;
                int counter = 0;

                for (int i = c; i < retArray.Length; i += 2 * controlIndex)
                {
                    for (int j = 0; j < controlIndex && i + j < retArray.Length; j++)
                    {
                        if (retArray[i + j])
                            counter++;
                    }
                }

                retArray[controlIndex - 1] = counter % 2 != 0;
                controlIndex *= 2;
            }
            return retArray;
        }

        private BitArray Decode(string inMessage)
        {
            if (!IsValidBinaryString(inMessage))
                throw new ArgumentException("Входное сообщение должно содержать только 0 и 1.");

            var codedArray = new BitArray(inMessage.Length);
            for (int i = 0; i < inMessage.Length; i++)
                codedArray[i] = inMessage[i] == '1';

            var decodedArray = new BitArray((int)(codedArray.Count - Math.Ceiling(Math.Log(codedArray.Count, 2))));
            int count = 0;

            for (int i = 0; i < codedArray.Length; i++)
            {
                if (IsPowerOfTwo(i + 1))
                    continue;
                if (count < decodedArray.Length)
                    decodedArray[count++] = codedArray[i];
            }

            string strDecodedArray = BitArrayToString(decodedArray);
            var checkArray = Code(strDecodedArray);
            byte[] failBits = new byte[(int)Math.Ceiling(Math.Log(codedArray.Count, 2))];
            count = 0;
            bool isMistake = false;

            for (int i = 0; i < failBits.Length; i++)
            {
                int pos = (int)Math.Pow(2, i) - 1;
                if (pos < codedArray.Length && pos < checkArray.Length &&
                    codedArray[pos] != checkArray[pos])
                {
                    failBits[count++] = (byte)(Math.Pow(2, i));
                    isMistake = true;
                }
            }

            if (isMistake)
            {
                int mistakeIndex = 0;
                for (int i = 0; i < count; i++)
                    mistakeIndex += failBits[i];
                mistakeIndex--;

                if (mistakeIndex >= 0 && mistakeIndex < codedArray.Length)
                {
                    codedArray[mistakeIndex] = !codedArray[mistakeIndex];
                    ERROR_VAL = mistakeIndex;
                }
                else
                {
                    throw new InvalidOperationException("Ошибка за пределами диапазона закодированного сообщения.");
                }

                count = 0;
                decodedArray = new BitArray((int)(codedArray.Count - Math.Ceiling(Math.Log(codedArray.Count, 2))));
                for (int i = 0; i < codedArray.Length; i++)
                {
                    if (IsPowerOfTwo(i + 1))
                        continue;
                    if (count < decodedArray.Length)
                        decodedArray[count++] = codedArray[i];
                }
            }
            return decodedArray;
        }
        #endregion

        private void EncodeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClearOutput();
                if (string.IsNullOrWhiteSpace(InputTextBox.Text))
                {
                    LogMessage("Ошибка: Введите двоичную строку.", Brushes.Red);
                    return;
                }

                BitArray code = Code(InputTextBox.Text);
                string result = BitArrayToString(code);
                ResultLabel.Content = "Закодированная строка:";
                DisplayBitsWithColor(code, true);
                LogMessage($"Кодирование успешно. Длина результата: {result.Length} бит.", Brushes.Green);
            }
            catch (ArgumentException ex)
            {
                LogMessage($"Ошибка ввода: {ex.Message}", Brushes.Red);
            }
            catch (Exception ex)
            {
                LogMessage($"Ошибка: {ex.Message}", Brushes.Red);
            }
        }

        private void DecodeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClearOutput();
                ERROR_VAL = -1;
                BitArray decoded = Decode(InputTextBox.Text);

                if (ERROR_VAL == -1)
                {
                    ResultLabel.Content = "Декодированная строка:";
                    DisplayBitsWithColor(decoded, false);
                    LogMessage("В коде ошибок не обнаружено.", Brushes.Green);
                }
                else
                {
                    char[] corrected = InputTextBox.Text.ToCharArray();
                    corrected[ERROR_VAL] = corrected[ERROR_VAL] == '0' ? '1' : '0';
                    string correctedString = new string(corrected);

                    ResultLabel.Content = $"Ошибка в бите {ERROR_VAL + 1}. Исправленная строка:";
                    DisplayBitsWithColor(new BitArray(correctedString.Length), true, correctedString);
                    LogMessage($"Обнаружена и исправлена ошибка в бите {ERROR_VAL + 1}.", new SolidColorBrush(Color.FromRgb(255, 165, 0))); // Мягкий оранжевый
                }
            }
            catch (ArgumentException ex)
            {
                LogMessage($"Ошибка ввода: {ex.Message}", Brushes.Red);
            }
            catch (Exception ex)
            {
                LogMessage($"Ошибка: {ex.Message}", Brushes.Red);
            }
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            StatusTextBlock.Text = $"Символов: {InputTextBox.Text.Length}";
        }

        private bool IsValidBinaryString(string input)
        {
            return !string.IsNullOrEmpty(input) && input.All(c => c == '0' || c == '1');
        }

        private bool IsPowerOfTwo(int n)
        {
            return (n & (n - 1)) == 0 && n != 0;
        }

        private string BitArrayToString(BitArray bits)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bits.Length; i++)
                sb.Append(bits[i] ? "1" : "0");
            return sb.ToString();
        }

        private void DisplayBitsWithColor(BitArray bits, bool isEncoded, string customString = null)
        {
            BitDisplayTextBlock.Inlines.Clear();
            string bitString = customString ?? BitArrayToString(bits);

            for (int i = 0; i < bitString.Length; i++)
            {
                Brush color;
                if (isEncoded && IsPowerOfTwo(i + 1))
                    color = Brushes.Red; // Контрольные биты
                else
                    color = Brushes.Blue; // Информационные биты

                Run bitRun = new Run(bitString[i].ToString()) { Foreground = color };
                BitDisplayTextBlock.Inlines.Add(bitRun);
            }
        }

        private void LogMessage(string message, Brush color)
        {
            Run logRun = new Run($"{DateTime.Now:HH:mm:ss}: {message}\n") { Foreground = color };
            Paragraph paragraph = new Paragraph(logRun);
            FlowDocument document = LogTextBox.Document ?? new FlowDocument();
            document.Blocks.Add(paragraph);
            LogTextBox.Document = document;
            LogTextBox.ScrollToEnd();
        }

        private void ClearOutput()
        {
            ResultLabel.Content = "";
            BitDisplayTextBlock.Inlines.Clear();
        }
    }
}