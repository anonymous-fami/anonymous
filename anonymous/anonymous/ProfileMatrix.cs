using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace anonymous
{
    public class ProfileMatrix : IMatrix <ProfileMatrix>
    {
        private double[] al; 
        private double[] au;
        private double[] di;
        private int[] ia;
        private int n;

        public ProfileMatrix(string FilePath) //Конструктор, считывает данные из файла
        {
            InputOutput.InputMatrix(out this.n, FilePath, out this.ia, out this.al, out this.au, out this.di);
        }

        public ProfileMatrix(double[] au, double[] al, double[] di, int[] ia, int n) //Конструктор, получает данные на вход
        {
            this.au = au;
            this.al = al;
            this.di = di;
            this.ia = ia;
            this.n = n;
        }

        public ProfileMatrix(ProfileMatrix Original) //Конструктор копий 
        {
            this.n = Original.n;

            this.ia = new int[Original.n + 1];
            Array.Copy(Original.ia, this.ia, Original.n + 1);
    
            this.al = new double[this.ia[this.n]];
            Array.Copy(Original.al, this.al, this.ia[this.n]);

            this.au = new double[this.ia[this.n]];
            Array.Copy(Original.au, this.au, this.ia[this.n]);

            this.di = new double[this.n];
            Array.Copy(Original.di, this.di, this.n);
        }

        public Vector Multiply(Vector x)//умножение матрицы на вектор
        {
            double[] values_res = new double[x.size];
            var res = new Vector(x.size, values_res);

            for (int i = 0; i < n; i++)
                  res.values[i] = di[i] * x.values[i];

            for (int i = 0; i < n; i++)
                for (int j = ia[i], k = i - (ia[i + 1] - ia[i]); j < ia[i + 1]; j++, k++)
                {
                    res.values[i] += al[j] * x.values[k];
                    res.values[k] += au[j] * x.values[i];
                }
            return res;
        }

        public Vector TMultiply(Vector x)//умножение транспонированной матрицы на вектор
        {
            double[] values_res = new double[x.size];
            var res = new Vector(x.size, values_res);

            for (int i = 0; i < n; i++)
                res.values[i] = di[i] * x.values[i];

            for (int i = 0; i < n; i++)
                for (int j = ia[i], k = i - (ia[i + 1] - ia[i]); j < ia[i + 1]; j++, k++)
                {
                    res.values[i] += au[j] * x.values[k];
                    res.values[k] += al[j] * x.values[i];
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

            for (int i=0; i<x.size;i++)
            {
                Ax_F.values[i] = Ax.values[i]-F.values[i];
            }
            return res=Ax_F.Norm();
        }

        public double rel_discrepancy(Vector x, Vector F)//относительная невязка
        {
            double res;
            double norm_F = F.Norm();
            double norm_Ax_F = this.abs_discrepancy(x, F);
            return res = norm_Ax_F / norm_F;
        }

        public void setMatrix(ProfileMatrix matrix)
        {
            this.al = matrix.al;
            this.au = matrix.au;
            this.di = matrix.di;
            this.ia = matrix.ia;
            this.n = matrix.n;          
        }

        public ProfileMatrix getMatrix()
        {
             return this;    
        }

        public double[] AL
        {
            get { return al; }
            set { al = value; }
        }

        public double[] AU
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
    }
}
