using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace anonymous
{
    public static class InputOutput
    {
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
                int i, j;
                double[] ia;
                string[] lines = System.IO.File.ReadAllLines(FileName);
                n = Int32.Parse(lines[0]);
                Plot = new double[n, n];
                for (i = 0; i < n; i++)
                {
                    lines[i + 1] = lines[i + 1].Trim();
                    lines[i + 1] = lines[i + 1].Replace('\t', ' ');
                    lines[i + 1] = lines[i + 1].Replace("  ", " ");
                    lines[i + 1] = lines[i + 1].Replace('.', ',');
                    ia = lines[i + 1].Split(' ').Select(nn => Convert.ToDouble(nn)).ToArray();
                    for (j = 0; j < n; j++)
                    {
                        Plot[i, j] = ia[j];
                    }
                }
                return true;
            }
            catch (Exception error)
            {
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
                lines[1] = lines[1].Trim();
                lines[1] = lines[1].Replace('\t', ' ');
                lines[1] = lines[1].Replace("  ", " ");
                ia = lines[1].Split(' ').Select(nn => Convert.ToInt32(nn)).ToArray();
                if (ia[0] == 1) for (int i = 0; i <= n; i++)ia[i]--;
                int k = ia[n];
                al = new double[k];
                lines[2] = lines[2].Trim();
                lines[2] = lines[2].Replace('\t', ' ');
                lines[2] = lines[2].Replace("  ", " ");
                lines[2] = lines[2].Replace('.', ',');
                al = lines[2].Split(' ').Select(nn => Convert.ToDouble(nn)).ToArray();
                au = new double[k];
                lines[3] = lines[3].Trim();
                lines[3] = lines[3].Replace('\t', ' ');
                lines[3] = lines[3].Replace("  ", " ");
                lines[3] = lines[3].Replace('.', ',');
                au = lines[3].Split(' ').Select(nn => Convert.ToDouble(nn)).ToArray();
                diag = new double[n];
                lines[4] = lines[4].Trim();
                lines[4] = lines[4].Replace('\t', ' ');
                lines[4] = lines[4].Replace("  ", " ");
                lines[4] = lines[4].Replace('.', ',');
                diag = lines[4].Split(' ').Select(nn => Convert.ToDouble(nn)).ToArray();
                return true;
            }
            catch (Exception error)
            {
                n = 0;
                ia = new int[0];
                al = new double[0];
                au = new double[0];
                diag = new double[0];
                return false;
            }
        }

        //временная замена ввода для профильного формата
        /*public static bool InputMatrix_res(out int n, string FileName, out int[] ia, out double[] al, out double[] au, out double[] diag)
        {
            try
            {
                bool nul = true;
                string line = null;
                StreamReader str = new StreamReader(FileName);
                n = Convert.ToInt32(str.ReadLine());

                ia = new int[n + 1];
                int ch = str.Read();
                ia[0] = ch - 48;
                if (ia[0] == 1) { nul = false; ia[0] -= 1; }
                ch = str.Read();
                for (int i = 1; i <= n && ch != -1;)
                {
                    line += Convert.ToChar(ch);
                    ch = str.Read();
                    if (ch == ' ' || ch == '\n' || ch == -1)
                    {
                        ia[i] = Convert.ToInt32(line);
                        if (nul == false) ia[i] -= 1;
                        i++;
                        line = null;
                        do
                        {
                            ch = str.Read();
                        } while ((ch == ' ' || ch == '\n' || ch == '\r') && ch != -1);
                    }
                }


                int m = ia[n];
                al = new double[m];
                for (int i = 0; i < m && ch != -1;)
                {
                    line += Convert.ToChar(ch);
                    ch = str.Read();
                    if (ch == ' ' || ch == '\n' || ch == -1)
                    {
                        line = line.Replace('.', ',');
                        al[i] = Convert.ToDouble(line);
                        i++;
                        line = null;
                        do
                        {
                            ch = str.Read();
                        } while ((ch == ' ' || ch == '\n' || ch == '\r') && ch != -1);
                    }
                }
                m = ia[n];

                au = new double[m];
                for (int i = 0; i < m && ch != -1;)
                {
                    line += Convert.ToChar(ch);
                    ch = str.Read();
                    if (ch == ' ' || ch == '\n' || ch == -1)
                    {
                        line = line.Replace('.', ',');
                        au[i] = Convert.ToDouble(line);
                        i++;
                        line = null;
                        do
                        {
                            ch = str.Read();
                        } while ((ch == ' ' || ch == '\n' || ch == '\r') && ch != -1);
                    }
                }

                diag = new double[n];
                for (int i = 0; i < n && ch != -1;)
                {
                    line += Convert.ToChar(ch);
                    ch = str.Read();
                    if (ch == ' ' || ch == '\n' || ch == -1)
                    {
                        line = line.Replace('.', ',');
                        diag[i] = Convert.ToDouble(line);
                        i++;
                        line = null;
                        do
                        {
                            ch = str.Read();
                        } while ((ch == ' ' || ch == '\n') && ch != -1);
                    }
                }
                str.Close();
                return true;
            }
            catch (Exception error)
            {
                n = 0;
                ia = new int[0];
                al = new double[0];
                au = new double[0];
                diag = new double[0];
                return false;
            }
        }*/

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
                lines[1] = lines[1].Trim();
                lines[1] = lines[1].Replace('\t', ' ');
                lines[1] = lines[1].Replace("  ", " ");
                ia = lines[1].Split(' ').Select(nn => Convert.ToInt32(nn)).ToArray();
                if (ia[0] == 1) for (int i = 0; i <= n; i++) ia[i]--;
                int k = ia[n];
                ja = new int[k];
                lines[2] = lines[2].Trim();
                lines[2] = lines[2].Replace('\t', ' ');
                lines[2] = lines[2].Replace("  ", " ");
                ia = lines[2].Split(' ').Select(nn => Convert.ToInt32(nn)).ToArray();
                al = new double[k];
                lines[3] = lines[3].Trim();
                lines[3] = lines[3].Replace('\t', ' ');
                lines[3] = lines[3].Replace("  ", " ");
                lines[3] = lines[3].Replace('.', ',');
                al = lines[3].Split(' ').Select(nn => Convert.ToDouble(nn)).ToArray();
                au = new double[k];
                lines[4] = lines[4].Trim();
                lines[4] = lines[4].Replace('\t', ' ');
                lines[4] = lines[4].Replace("  ", " ");
                lines[4] = lines[4].Replace('.', ',');
                au = lines[4].Split(' ').Select(nn => Convert.ToDouble(nn)).ToArray();
                diag = new double[n];
                lines[5] = lines[5].Trim();
                lines[5] = lines[5].Replace('\t', ' ');
                lines[5] = lines[5].Replace("  ", " ");
                lines[5] = lines[5].Replace('.', ',');
                diag = lines[5].Split(' ').Select(nn => Convert.ToDouble(nn)).ToArray();
                return true;
            }
            catch (Exception error)
            {
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
        // ig - хранит номера ненулевых диагоналей
        // gl - массив элементов нижнего треугольника
        // gu - массив элементов верхнего треугольника
        public static bool InputMatrix(string FileName, out int n, out int m,  out int[] ig, out double[,] gl, out double[,] gu, out double[] diag)
        {
            try
            {
                int ch,j,i;
                string line = null;
                StreamReader str = new StreamReader(FileName);
                n = Convert.ToInt32(str.ReadLine());
                m = Convert.ToInt32(str.ReadLine());
                ig = new int[m];

                ch = str.Read();
                for (i = 0; i < m && ch != -1;)
                {
                    line += Convert.ToChar(ch);
                    ch = str.Read();
                    if (ch == ' ' || ch == '\n' || ch == -1)
                    {
                        ig[i] = Convert.ToInt32(line);
                        i++;
                        line = null;
                        do
                        {
                            ch = str.Read();
                        } while ((ch == ' ' || ch == '\n') && ch != -1);
                    }
                }

                gl = new double[m, n - ig[0]];
                for (j = 0; j < m; j++)
                {
                    for (i = 0; i < n - ig[j] && ch != -1;)
                    {
                        line += Convert.ToChar(ch);
                        ch = str.Read();
                        if (ch == ' ' || ch == '\n' || ch == '\t' || ch == -1)
                        {
                            line = line.Replace('.', ',');
                            gl[j, i] = Convert.ToDouble(line);
                            i++;
                            line = null;
                            do
                            {
                                ch = str.Read();
                            } while ((ch == ' ' || ch == '\n' || ch == '\t') && ch != -1);
                        }
                    }
                    for (i = n - ig[j]; i < n - ig[0]; i++)
                        gl[j, i] = 0.0;
                }

                gu = new double[m, n - ig[0]];
                for (j = 0; j < m; j++)
                {
                    for (i = 0; i < n - ig[j] && ch != -1;)
                    {
                        line += Convert.ToChar(ch);
                        ch = str.Read();
                        if (ch == ' ' || ch == '\n' || ch == '\t' || ch == -1)
                        {
                            line = line.Replace('.', ',');
                            gu[j, i] = Convert.ToDouble(line);
                            i++;
                            line = null;
                            do
                            {
                                ch = str.Read();
                            } while ((ch == ' ' || ch == '\n' || ch == '\t') && ch != -1);
                        }
                    }
                    for (i = n - ig[j]; i < n - ig[0]; i++)
                        gu[j, i] = 0.0;
                }

                diag = new double[n];
                for (i = 0; i < n && ch != -1;)
                {
                    line += Convert.ToChar(ch);
                    ch = str.Read();
                    if (ch == ' ' || ch == '\n' || ch == '\t' || ch == -1)
                    {
                        line = line.Replace('.', ',');
                        diag[i] = Convert.ToDouble(line);
                        i++;
                        line = null;
                        do
                        {
                            ch = str.Read();
                        } while ((ch == ' ' || ch == '\n' || ch == '\t') && ch != -1);
                    }
                }

                str.Close();
                return true;
            }
            catch (Exception error)
            {
                n = 0;
                m = 0;
                ig = new int[0];
                gu = new double[0, 0];
                gl = new double[0, 0];
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
                    lines[numberLine] = lines[numberLine].Trim();
                    lines[numberLine] = lines[numberLine].Replace('\t', ' ');
                    lines[numberLine] = lines[numberLine].Replace("  ", " ");
                    lines[numberLine] = lines[numberLine].Replace('.', ',');
                    double[] row = lines[numberLine].Split(' ').Select(nn => Convert.ToDouble(nn)).ToArray();
                    for(int j=0;j<k;j++)
                    {
                        au[i,j] = row[j];
                    }
                }
                al = new double[n, k];

                for (int i = 0; i < n; i++, numberLine++)
                {
                    lines[numberLine] = lines[numberLine].Trim();
                    lines[numberLine] = lines[numberLine].Replace('\t', ' ');
                    lines[numberLine] = lines[numberLine].Replace("  ", " ");
                    lines[numberLine] = lines[numberLine].Replace('.', ',');
                    double[] row = lines[numberLine].Split(' ').Select(nn => Convert.ToDouble(nn)).ToArray();
                    for (int j = 0; j < k; j++)
                    {
                        al[i, j] = row[j];
                    }
                }

                diag = new double[n];
                lines[numberLine] = lines[numberLine].Trim();
                lines[numberLine] = lines[numberLine].Replace('\t', ' ');
                lines[numberLine] = lines[numberLine].Replace("  ", " ");
                lines[numberLine] = lines[numberLine].Replace('.', ',');
                diag = lines[numberLine].Split(' ').Select(nn => Convert.ToDouble(nn)).ToArray();
                return true;

            }
            catch (Exception error)
            {
                n = 0;
                m = 0;
                al = new double[0, 0];
                au = new double[0, 0];
                diag = new double[0];
                return false;
            }
        }

        //
        //Ввод вектора
        // Возвращает false - если возникла любая ошибка, true - если все данные корректно считались
        // в файле FileName в первой строке указывается длина вектора, во второй строке весь вектор.
        public static bool InputVector(out int n, string FileName, out double[] vector)
        {
            try
            {
                string line = null;
                StreamReader str = new StreamReader(FileName);
                n = Convert.ToInt32(str.ReadLine());

                vector = new double[n];
                int ch = str.Read();
                for (int i = 0; i < n && ch != -1;)
                {
                    line += Convert.ToChar(ch);
                    ch = str.Read();
                    if(ch == ' ' || ch == '\n' || ch == '\t' || ch == -1)
                    {
                        line = line.Replace('.', ',');
                        vector[i] = Convert.ToDouble(line);
                        i++;
                        line = null;
                        do
                        {
                            ch = str.Read();
                        } while ((ch == ' ' || ch == '\n' || ch == '\t') && ch != -1);
                    }
                }
                str.Close();
                return true;
            }
            catch (Exception error)
            {
                n = 0;
                vector = new double[0];
                return false;
            }
        }

        public static bool OutputVector(string filename, double[] vector)
        {
            try
            {
                StreamWriter str = new StreamWriter(filename);
                for (int i = 0; i < vector.Length; i++)
                {
                    str.WriteLine(vector[i]);
                }
                str.Close();
                return true;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка!", MessageBoxButtons.OK);              
                return false;
            }
        }

        public static bool OutputVector(string filename, Vector vector)
        {
            try
            {
                StreamWriter str = new StreamWriter(filename);
                for (int i = 0; i < vector.SIZE; i++)
                {
                    str.WriteLine(vector.VALUES[i]);
                }
                str.Close();
                return true;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка!", MessageBoxButtons.OK);
                return false;
            }
        }

        public static bool OutputIteration(string filename, int number_it, double residual, double[] vector)
        {
            try
            {
                StreamWriter str = new StreamWriter(filename);
                str.WriteLine("Iteration: " + number_it + " Residual: " + residual);
                for (int i = 0; i < vector.Length; i++)
                {
                    str.Write(vector[i]); str.Write(" ");
                }
                str.Close();
                return true;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка!", MessageBoxButtons.OK);
                return false;
            }
        }

        public static bool OutputIterationToForm(int number_it, double residual)
        {
            try
            {
                number_it++;
                Data.richtextbox.Text = Data.richtextbox.Text + "Итерация: " + number_it + " Норма: " + residual + "\n";
                return true;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка!", MessageBoxButtons.OK);
                return false;
            }
        }
    }
}
