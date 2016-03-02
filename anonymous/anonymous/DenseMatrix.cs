using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    //Плотный формат  хранения матрицы
    public class DenseMatrix : IMatrix<DenseMatrix>
    {
        private double[,] Plot;
        private int n;

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


        //public DenseMatrix(out double[,] Plot, out int n)//конструктор, получает данные на входе
        //{
        //    this.Plot = Plot;
        //    this.n = n;
        //} 

        public DenseMatrix(double[,] Plot, int n)//конструктор, получает данные на входе
        {
            this.Plot = Plot;
            this.n = n;
        } 

        public DenseMatrix(string FilePath)//конструктор, считывает данные из файла
        {
            InputOutput.InputMatrix(out this.n, FilePath, out this.Plot);
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
        public bool setMatrix(DenseMatrix matrix)
        {
            this.Plot = matrix.Plot;
            this.n = matrix.n;
            return true;
        }
        public DenseMatrix getMatrix()
        {
            return this;
        }

    }
}