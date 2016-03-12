using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace anonymous
{
    //Плотный формат  хранения матрицы
    public class DenseMatrix : IMatrix<DenseMatrix>
    {
        private double[,] Plot;
        private int n;

        public DenseMatrix(double[,] Plot, int n)//конструктор, получает данные на входе
        {
            this.Plot = Plot;
            this.n = n;
        }

        public DenseMatrix(string FilePath)//конструктор, считывает данные из файла
        {
            if (!InputOutput.InputMatrix(FilePath, out this.n, out this.Plot))
                MessageBox.Show("Ошибка ввода матрицы.", "Опаньки...", MessageBoxButtons.OK);
        }

        public DenseMatrix(DenseMatrix Original) //Конструктор копий 
        {
            this.n = Original.n;
            this.Plot = new double[Original.n, Original.n];
            Array.Copy(Original.Plot, this.Plot, this.n * this.n);
        }

        public Vector Multiply(Vector x)//умножение матрицы на вектор
        {
            double[] values_res = new double[x.size];
            var res = new Vector(x.size, values_res);

            //инциализация вектора
            for (int i = 0; i < x.size; i++)
                res.values[i] = Plot[i, 0] * x.values[0];

            for (int i = 0; i < n; i++)
                for (int j = 1; j < n; j++)
                {
                    res.values[i] += Plot[i, j] * x.values[j];
                }
            return res;
        }

        public Vector TMultiply(Vector x)//умножение транспонированной матрицы на вектор
        {
            double[] values_res = new double[x.size];
            var res = new Vector(x.size, values_res);

            //инциализация вектора
            for (int i = 0; i < x.size; i++)
                res.values[i] = Plot[0, i] * x.values[0];

            for (int i = 0; i < n; i++)
                for (int j = 1; j < n; j++)
                {
                    res.values[i] += Plot[i, j] * x.values[i];
                }
            return res;
        }

        public Vector DirectProgress(Vector f)      //Ly=f
        {
            int i, j;
            double s;
            double[] temp = new double[n];
            for (i = 0; i < n; i++)
            {
                s = 0;
                for (j = 0; j < i; j++)
                    s += Plot[i, j] * temp[j];
                if (Data.preconditioner == 3)
                    temp[i] = (f.values[i] - s);
                else
                    if (Plot[i, i] == 0)
                    throw new Exception("DirectProgress: Деление на ноль.");
                else
                    temp[i] = (f.values[i] - s) / Plot[i, i];
            }
            return new Vector(n, temp);
        }

        public Vector ReverseProgress(Vector y)     //Ux=y
        {
            double[] x = new double[n];
            double[] temp = new double[n];
            int i, j, k;
            for (i = 0; i < n; i++) temp[i] = y.values[i];
            for (i = n - 1; i >= 0; i--)
            {
                if (Plot[i, i] == 0)
                    throw new Exception("ReverseProgress: Деление на ноль.");
                else
                    x[i] = temp[i] / Plot[i, i];
                for (j = i; j >= 0; j--)
                    temp[j] -= Plot[j, i] * x[i];
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

        public void setMatrix(DenseMatrix matrix)
        {
            this.Plot = matrix.Plot;
            this.n = matrix.n;
        }

        public DenseMatrix getMatrix()
        {
            return this;
        }

        public double[,] PLOT
        {
            get { return Plot; }
            set { Plot = value; }
        }

        public int N
        {
            get { return n; }
            set { n = value; }
        }
    }
}