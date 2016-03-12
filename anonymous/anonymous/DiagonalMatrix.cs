using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace anonymous
{
    public class DiagonalMatrix : IMatrix<DiagonalMatrix>
    {
        private double[,] al;
        private double[,] au;
        private double[] di;
        private int[] ia;
        private int n;//размерность матрицы
        private int nd;//число ненулевых побочных диагоналей

        public DiagonalMatrix(string FilePath) //Конструктор, считывает данные из файла
        {
            //Ввод диагональной матрицы не готов.            
            if (!InputOutput.InputMatrix(FilePath, out this.n, out this.nd, out this.ia, out this.al, out this.au, out this.di))
                MessageBox.Show("Ошибка ввода матрицы.", "Опаньки...", MessageBoxButtons.OK);
        }

        public DiagonalMatrix(double[,] au, double[,] al, double[] di, int[] ia, int n, int nd) //Конструктор, получает данные на вход
        {
            this.au = au;
            this.al = al;
            this.di = di;
            this.ia = ia;
            this.n = n;
            this.nd = nd;
        }

        public DiagonalMatrix(DiagonalMatrix Original) //Конструктор копий 
        {
            this.n = Original.n;

            this.nd = Original.nd;

            this.ia = new int[Original.nd];
            Array.Copy(Original.ia, this.ia, Original.nd);

            this.al = new double[this.nd, this.n-1];
            Array.Copy(Original.al, this.al, this.nd*(this.n - 1));
        
            this.au = new double[this.nd, this.n-1];
            Array.Copy(Original.au, this.au, (this.nd)*( this.n - 1));

            this.di = new double[this.n];
            Array.Copy(Original.di, this.di, this.n);
        }

        public Vector Multiply(Vector x)//умножение матрицы на вектор
        {
            double[] values_res = new double[x.size];
            var res = new Vector(x.size, values_res);

            for (int i = 0; i < N; i++)
                res.values[i] = di[i] * x.values[i];

            for (int i = 0, ir = 0; i < ND; i++)
                for (int j = 0; j < N - ia[i]; j++)
                {
                    ir = j + ia[i];
                    res.values[ir] += al[i,j] * x.values[j];
                    res.values[j] += au[i,j] * x.values[ir];
                }
            return res;
        }

        public Vector TMultiply(Vector x)//умножение транспонированной матрицы на вектор
        {
            double[] values_res = new double[x.size];
            var res = new Vector(x.size, values_res);

            for (int i = 0; i < N; i++)
                res.values[i] = di[i] * x.values[i];

            for (int i = 0, ir = 0; i < ND; i++)
                for (int j = 0; j < N - ia[i]; j++)
                {
                    ir = j + ia[i];
                    res.values[ir] += au[i,j] * x.values[j];
                    res.values[j] += al[i,j] * x.values[ir];
                }
            return res;
        }

        public Vector DirectProgress(Vector f)      //Ly=f
        {
            throw new NotImplementedException();
        }

        public Vector ReverseProgress(Vector y)     //Ux=y
        {
            throw new NotImplementedException();
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

        public void setMatrix(DiagonalMatrix matrix)
        {
            this.al = matrix.al;
            this.au = matrix.au;
            this.di = matrix.di;
            this.ia = matrix.ia;
            this.n = matrix.n;
            this.nd = matrix.nd;
        }

        public DiagonalMatrix getMatrix()
        {
            return this;
        }

        public double[,] AL
        {
            get { return al; }
            set { al = value; }
        }

        public double[,] AU
        {
            get { return au; }
            set { au = value; }
        }

        public double[] DI
        {
            get { return di; }
            set { di = value; }
        }

        public int[] IA
        {
            get { return ia; }
            set { ia = value; }
        }
  
        public int N
        {
            get { return n; }
            set { n = value; }
        }

        public int ND
        {
            get { return nd; }
            set { nd = value; }
        }
    }
}
