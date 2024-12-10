using System;
using System.Collections.Generic;
using System.Linq;

namespace Kod
{
    public class ShannonFanoEncoder
    {
        private double[] probabilities;
        private string[] codes;

        public ShannonFanoEncoder(double[] probabilities)
        {
            this.probabilities = probabilities;
            this.codes = new string[probabilities.Length];
        }

        public string[] Encode()
        {
            Fano(0, probabilities.Length - 1);
            return codes;
        }

        private void Fano(int left, int right)
        {
            if (left < right)
            {
                int middle = FindSplitPoint(left, right);

                for (int i = left; i <= right; i++)
                {
                    codes[i] = codes[i] ?? ""; // Инициализация строки
                    codes[i] += (i <= middle) ? "1" : "0";
                }

                Fano(left, middle);
                Fano(middle + 1, right);
            }
        }

        private int FindSplitPoint(int left, int right)
        {
            double leftSum = probabilities[left];
            double rightSum = probabilities.Skip(left + 1).Take(right - left).Sum();

            int splitIndex = left;
            double minDifference = Math.Abs(leftSum - rightSum);

            for (int i = left + 1; i <= right; i++)
            {
                leftSum += probabilities[i];
                rightSum -= probabilities[i];

                double currentDifference = Math.Abs(leftSum - rightSum);
                if (currentDifference < minDifference)
                {
                    minDifference = currentDifference;
                    splitIndex = i;
                }
            }

            return splitIndex;
        }
    }
}
