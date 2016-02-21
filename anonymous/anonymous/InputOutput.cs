using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace anonymous
{
    public class InputOutput
    {
<<<<<<< HEAD

            public static int formattype;
            //Ввод плотной матрицы
            public static void InputMatrix(int Flag, string FileName, out double[,] Plot) { Plot = new double[10, 10]; }
            //Ввод матрицы в профильном формате
            public static void InputMatrix(int Flag, string FileName, out int[] ia, out double[] gg, out double[] diag)
            {
                gg = new double[10];
                diag = new double[10];
                ia = new int[10];
            }
            //Ввод матрицы в разреженном формате
            public static void InputMatrix(int Flag, string FileName, out int[] ia, out int[] ja, out double[] gg, out double[] diag)
            {
                gg = new double[10];
                diag = new double[10];
                ia = new int[10];
                ja = new int[10];
            }
            //Ввод матрицы в диагональном формате
            //void InputMatrix(int Flag, string FileName, double[] gg, double[] diag);
            //Ввод матрицы в ленточном формате
            public static void InputMatrix(int Flag, string FileName, out double[] gg, out double[] diag)
            {
                gg = new double[10];
                diag = new double[10];
            }

            //ввод вектора правой части
            public static void InputRightPart(string FileName, out double[] pr)
            {
                pr = new double[10];
            }

=======
        public static int formattype;

        //
        //Ввод плотной матрицы
        // n - размерность матрицы
        // FileName - файл
        // Plot - матрица
        // Возвращает false - если возникла любая ошибка, true - если все данные корректно считались
        //
        public static bool InputMatrix(out int n, string FileName, out double[,] Plot)
         {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(FileName);
                n = Int32.Parse(lines[0]);
                Plot = new double[n, n];
                for (int i = 0; i < n; i++)
                {
                    double[] ia = lines[i + 1].Split(' ').Select(nn => Convert.ToDouble(nn)).ToArray();
                    for (int j = 0; j < n; j++)
                    {
                        Plot[i, j] = ia[j];
                    }
                }
                return true;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка!", MessageBoxButtons.OK);
                n = 0;
                Plot = new double[0, 0];
                return false;
            }
        }

        //
        //Ввод матрицы в профильном формате
        // n - размерность матрицы
        // FileName - файл
        // ia - индексный массив
        // al - массив элементов нижнего треугольника
        // au - массив элементов верхнего треугольника
        // diag - массив элементов диагонали
        // Возвращает false - если возникла любая ошибка, true - если все данные корректно считались
        //
        public static bool InputMatrix(out int n, string FileName, out int[] ia, out double[] al, out double[] au,  out double[] diag)
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(FileName);
                n = Int32.Parse(lines[0]);
                ia = new int[n + 1];
                ia = lines[1].Split(' ').Select(nn => Convert.ToInt32(nn)).ToArray();
                int k = ia[n] - 1;
                al = new double[k];
                al = lines[2].Split(' ').Select(nn => Convert.ToDouble(nn)).ToArray();
                au = new double[k];
                au = lines[3].Split(' ').Select(nn => Convert.ToDouble(nn)).ToArray();
                diag = new double[n];
                diag = lines[4].Split(' ').Select(nn => Convert.ToDouble(nn)).ToArray();
                return true;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка!", MessageBoxButtons.OK);
                n = 0;
                ia = new int[0];
                al = new double[0];
                au = new double[0];
                diag = new double[0];
                return false;
            }
        }

        //
        //Ввод матрицы в разреженном формате
        // n - размерность матрицы
        // FileName - файл
        // ia - индексный массив
        // ja - массив номеров столбцов
        // al - массив элементов нижнего треугольника
        // au - массив элементов верхнего треугольника
        // diag - массив элементов диагонали
        // Возвращает false - если возникла любая ошибка, true - если все данные корректно считались
        //
        public static bool InputMatrix(out int n, string FileName, out int[] ia, out int[] ja, out double[] al, out double[] au, out double[] diag)
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(FileName);
                n = Int32.Parse(lines[0]);
                ia = new int[n + 1];
                ia = lines[1].Split(' ').Select(nn => Convert.ToInt32(nn)).ToArray();
                int k = ia[n] - 1;
                ja = new int[k];
                ia = lines[2].Split(' ').Select(nn => Convert.ToInt32(nn)).ToArray();
                al = new double[k];
                al = lines[3].Split(' ').Select(nn => Convert.ToDouble(nn)).ToArray();
                au = new double[k];
                au = lines[4].Split(' ').Select(nn => Convert.ToDouble(nn)).ToArray();
                diag = new double[n];
                diag = lines[5].Split(' ').Select(nn => Convert.ToDouble(nn)).ToArray();
                return true;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка!", MessageBoxButtons.OK);
                n = 0;
                al = new double[0];
                au = new double[0];
                diag = new double[0];
                ia = new int[0];
                ja = new int[0];
                return false;
            }
        }

        //
        //Ввод матрицы в диагональном формате
        // n - размерность матрицы
        // m - количество ненулевых диагоналей
        // Возвращает false - если возникла любая ошибка, true - если все данные корректно считались
        // Пока не доделано
        //
        public static bool InputMatrix(out int n, out int m, string FileName, double[] gg, double[] diag)
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(FileName);
                n = Int32.Parse(lines[0]);
                m = Int32.Parse(lines[1]);
                gg = new double[10];
                diag = new double[10];
                return true;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка!", MessageBoxButtons.OK);
                n = 0;
                m = 0;
                gg = new double[0];
                diag = new double[0];
                return false;
            }
        }

        //
        //Ввод матрицы в ленточном формате
        // n - размерность матрицы
        // m - ширина ленты
        // al - массив для нижнего треугольника
        // au - массив для верхнего треугольника
        // Возвращает false - если возникла любая ошибка, true - если все данные корректно считались
        //
        public static bool InputMatrix(out int n, out int m, string FileName, out double[,] al, out double[,] au, out double[] diag)
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(FileName);
                n = Int32.Parse(lines[0]);
                m = Int32.Parse(lines[1]);
                int k = (m - 1) / 2;
                au = new double[n, k];
                int numberLine = 2;
                for (int i = 0; i < n; i++,numberLine++)
                {
                    double[] row = lines[numberLine].Split(' ').Select(nn => Convert.ToDouble(nn)).ToArray();
                    for(int j=0;j<k;j++)
                    {
                        au[i,j] = row[j];
                    }
                }
                al = new double[n, k];

                for (int i = 0; i < n; i++, numberLine++)
                {
                    double[] row = lines[numberLine].Split(' ').Select(nn => Convert.ToDouble(nn)).ToArray();
                    for (int j = 0; j < k; j++)
                    {
                        al[i, j] = row[j];
                    }
                }

                diag = new double[n];
                diag = lines[numberLine].Split(' ').Select(nn => Convert.ToDouble(nn)).ToArray();
                return true;

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка!", MessageBoxButtons.OK);
                n = 0;
                m = 0;
                al = new double[0, 0];
                au = new double[0, 0];
                diag = new double[0];
                return false;
            }
        }

        //
        //Ввод вектора правой части
        // Возвращает false - если возникла любая ошибка, true - если все данные корректно считались
        //
        public static bool InputRightPart(out int n, string FileName, out double[] pr)
        {
            try
            {

                n = 10;
                pr = new double[10];
                return true;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка!", MessageBoxButtons.OK);
                n = 0;
                pr = new double[0];
                return false;
            }
        }
>>>>>>> a0f84a8a96df7b911bdcc0ec4aa8bb9e7e7a636f
    }
}
