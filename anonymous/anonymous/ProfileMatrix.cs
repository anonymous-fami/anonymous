using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace anonymous
{
    public class ProfileMatrix : IMatrix
    {
        public double[] al;
        public double[] au;
        public double[] di;
        public int[] ia;
        public int n;

    

        public ProfileMatrix(double[] au, double[] al, double[] di, int[] ia, int n)//конструктор
        {
            this.au = au;
            this.al = al;
            this.di = di;
            this.ia = ia;
            this.n = n;
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

        public IMatrix Sum(ProfileMatrix B)//сумма матриц
        {
            double[] al_res=new double[ia[n+1]-1];
            double[] au_res=new double[ia[n + 1] - 1];
            double[] di_res=new double[n];
            for (int i = 0; i < ia[n + 1] - 1; i++)
            {
                al_res[i] = 0;
                au_res[i] = 0;

            }

            for (int i = 0; i < n; i++)
                di_res[i] = 0;

            var res = new ProfileMatrix(au_res, al_res, di_res, ia, n);

            for (int i = 0; i < ia[n + 1] - 1; i++)
            {
                res.al[i] += this.al[i];
                res.al[i] += B.al[i];
                res.au[i] += this.au[i];
                res.au[i] += B.au[i];
            }

            for (int i = 0; i < n; i++)
            {
                res.di[i] += this.di[i];
                res.di[i] += B.di[i];
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
    }
}
