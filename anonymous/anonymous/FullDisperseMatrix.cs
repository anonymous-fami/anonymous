using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace anonymous
{
   public class FullDisperseMatrix : IMatrix<FullDisperseMatrix>
    {
        public struct elem
        {
            public int row;//номер строки
            public int col;//номер столбца
            public double val;//элемент
        }

        public List<elem> matr = new List<elem>();//элементы матрицы
        public int n;//размерность

        public FullDisperseMatrix(string FilePath) //Конструктор, считывает данные из файла
        {
            if (!InputOutput.InputMatrix(FilePath, out this.n, out this.matr))
                MessageBox.Show("Ошибка ввода матрицы.\nИспользуйте справку (F1).", "Опаньки...", MessageBoxButtons.OK);
        }

        public Vector Multiply(Vector x)//умножение матрицы на вектор
        {
            double[] values_res = new double[x.size];
            var res = new Vector(x.size, values_res);

            for (int i = 0; i < matr.Count; i++)
            {
                if (matr[i].row == matr[i].col)
                {
                    res.values[matr[i].row] += matr[i].val * x.values[matr[i].row];
                }
                else
                {
                    res.values[matr[i].col] += matr[i].val * x.values[matr[i].row];
                    res.values[matr[i].row] += matr[i].val * x.values[matr[i].col];
                }
            }

                return res;
        }

        public double MultiplyL(int index, Vector x)//функция для Гаусс-Зейделя, нижний треугольник
        {
            double res = 0;
            for (int i = 0; i < matr.Count; i++)
            {
                if (matr[i].row == index)
                {
                    for (; matr[i].row != matr[i].col; i++)
                    {
                        res += matr[i].val * x.values[matr[i].col];
                    }
                    break;
                }
                
            }

                return res;
        }

        public double MultiplyU(int index, Vector x)//функция для Гаусс-Зейделя, верхний треугольник
        {
            double res = 0;

            for (int i = 0; i < matr.Count; i++)
            {
                if (matr[i].row == index && matr[i].row == matr[i].col)
                {
                    res += matr[i].val * x.values[matr[i].row];

                    for (int j = index + 1; j < this.n; j++)
                    {
                        for (i = i + 1; matr[i].row != matr[i].col; i++)
                        {
                            if (matr[i].row == j && matr[i].col == index)
                                res += matr[i].val * x.values[matr[i].row];
                        }
                    }
                }
            }

                return res;
        }

        public Vector TMultiply(Vector x)//умножение транспонированной матрицы на вектор
        {
            double[] values_res = new double[x.size];
            var res = new Vector(x.size, values_res);

            for (int i = 0; i < matr.Count; i++)
            {
                if (matr[i].row == matr[i].col)
                {
                    res.values[matr[i].row] += matr[i].val * x.values[matr[i].row];
                }
                else
                {
                    res.values[matr[i].col] += matr[i].val * x.values[matr[i].row];
                    res.values[matr[i].row] += matr[i].val * x.values[matr[i].col];
                }
            }

            return res;
        }

        public Vector DirectProgress(Vector f)      //Ly=f
        {
            int i, j;
            double s;
            double[] temp = new double[n];
            for (i = 0; i < matr.Count; i++)
            {
                s = 0;
                for (; matr[i].col != matr[i].row; i++)
                {
                    s += matr[i].val * temp[matr[i].col];
                }
                if (matr[i].val == 0)
                    throw new Exception("DirectProgress: Нулевой элемент на диагонале. Деление на ноль.");
                else
                    temp[matr[i].col] = (f.values[i] - s) / matr[i].val;
            }
            return new Vector(n, temp);
        }

        public Vector ReverseProgress(Vector y)     //Ux=y
        {
            double[] x = new double[n];
            double[] temp = new double[n];
            int i, j;

            for (i = 0; i < n; i++) temp[i] = y.values[i];
            for (i = matr.Count - 1; i >= 0; i--)
            {
                if (matr[i].val == 0)
                    throw new Exception("ReverseProgress: Нулевой элемент на диагонале. Деление на ноль.");
                else
                    x[matr[i].col] = temp[matr[i].col] / matr[i].val;
                for (; matr[i].col != matr[i].row; i--)
                    temp[matr[i].col] -= matr[i].val * x[matr[i].row];
            }
                return new Vector(n, x);
        }

        public double abs_discrepancy(Vector x, Vector F)//абсолютная невязка
        {
            double res;
            double[] values_Ax = new double[x.size];
            double[] values_Ax_F = new double[x.size];
            var Ax = new Vector(x.size, values_Ax);
            Ax = Multiply(x);

            var Ax_F = new Vector(x.size, values_Ax_F);

            for (int i = 0; i < x.size; i++)
            {
                Ax_F.values[i] = Ax.values[i] - F.values[i];
            }
            return res = Ax_F.Norm();
        }

        public double rel_discrepancy(Vector x, Vector F)//относительная невязка
        {
            double res;
            double norm_F = F.Norm();
            double norm_Ax_F = this.abs_discrepancy(x, F);
            return res = norm_Ax_F / norm_F;
        }

        public double get_di(int index)
        {
            for (int i = 0; i < matr.Count; i++)
            {
                if (matr[i].row == index && matr[i].col == index)
                    return matr[i].val;
            }
            return 0;
        }

        public void setMatrix(FullDisperseMatrix matrix)
        {
            this.matr = matrix.matr;
            this.n = matrix.n;
        }

        public FullDisperseMatrix getMatrix()
        {
            return this;
        }

        public bool CheckSymmetry()
        {
            return true;
        }

        public int N
        {
            get { return n; }
            set { n = value; }
        }
    }
}
