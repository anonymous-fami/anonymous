﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace anonymous
{
    public class DisperseMatrix : IMatrix<DisperseMatrix>
    {
        private double[] al;
        private double[] au;
        private double[] di;
        private int[] ia;
        private int[] ja;
        private int n;

        public DisperseMatrix(string FilePath) //Конструктор, считывает данные из файла
        {
            if (!InputOutput.InputMatrix(FilePath, out this.n, out this.ia, out this.ja, out this.al, out this.au, out this.di))
                MessageBox.Show("Ошибка ввода матрицы.\nИспользуйте справку (F1).", "Опаньки...", MessageBoxButtons.OK);
        }

        public DisperseMatrix(double[] au, double[] al, double[] di, int[] ia, int[] ja, int n) //Конструктор, получает данные на вход
        {
            this.au = au;
            this.al = al;
            this.di = di;
            this.ia = ia;
            this.ja = ja;
            this.n = n;
        }

        public DisperseMatrix(DisperseMatrix Original) //Конструктор копий 
        {
            this.n = Original.n;

            this.ia = new int[this.n + 1];
            Array.Copy(Original.ia, this.ia, this.n + 1);

            this.ja = new int[this.ia[this.n]];
            Array.Copy(Original.ja, this.ja, this.ia[this.n]);

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
                for (int j = ia[i]; j<ia[i+1]; j++)
                {
                    res.values[i] += al[j] * x.values[ja[j]];
                    res.values[ja[j]] += au[j] * x.values[i];
                }
            return res;
        }

        public double MultiplyL(int index,Vector x)//функция для Гаусс-Зейделя, нижний треугольник
        {
            double res=0;
            for (int j = ia[index]; j < ia[index + 1]; j++)
                res += al[j] * x.values[ja[j]];
  
            return res;
        }

        public double MultiplyU(int index, Vector x)//функция для Гаусс-Зейделя, верхний треугольник
        {
            double[] values_res = new double[x.size];
            var res = new Vector(x.size, values_res);

            for (int i = 0; i < n; i++)
                res.values[i] = di[i] * x.values[i];

            for (int i = 0; i < n; i++)
                for (int j = ia[i]; j < ia[i + 1]; j++)
                {
                    res.values[ja[j]] += au[j] * x.values[i];
                }

            return res.values[index];
        }
       
        public Vector TMultiply(Vector x)//умножение транспонированной матрицы на вектор
        {
            double[] values_res = new double[x.size];
            var res = new Vector(x.size, values_res);

            for (int i = 0; i < n; i++)
                res.values[i] = di[i] * x.values[i];

            for (int i = 0; i < n; i++)
                for (int j = ia[i]; j < ia[i + 1]; j++)
                {
                    res.values[i] += au[j] * x.values[ja[j]];
                    res.values[ja[j]] += al[j] * x.values[i];
                }
            return res;
        }

        public Vector DirectProgress(Vector f)      //Ly=f
        {
            int i, j, k, kol_str;
            double s;
            double[] temp = new double[n];
            k = 0;

            try
            {
                for (i = 0; i < n; i++)
                {
                    s = 0;
                    kol_str = ia[i + 1] - ia[i];
                    for (j = 0; j < kol_str; j++)
                    {
                        s += al[k] * temp[ja[k]];
                        k++;
                    }
                    if (Data.preconditioner == 3)
                        temp[i] = (f.values[i] - s);
                    else
                        if (di[i] == 0)
                        throw new Exception("DirectProgress: Нулевой элемент на диагонале. Деление на ноль.");
                    else
                        temp[i] = (f.values[i] - s) / di[i];
                }
                return new Vector(n, temp);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка.", MessageBoxButtons.OK);
                return null;
            }
        }

        public Vector ReverseProgress(Vector y)     //Ux=y
        {
            double[] x = new double[n];
            double[] temp = new double[n];
            int i, j, k, kol_str;
            for (i = 0; i < n; i++) temp[i] = y.values[i];
            k = ia[n] - 1;

            try
            {
                for (i = n - 1; i >= 0; i--)
                {
                    if (di[i] == 0)
                        throw new Exception("ReverseProgress: Нулевой элемент на диагонале. Деление на ноль.");
                    else
                        x[i] = temp[i] / di[i];
                    kol_str = ia[i + 1] - ia[i];
                    for (j = 0; j < kol_str; j++)
                    {
                        temp[ja[k]] -= au[k] * x[i];
                        k--;
                    }
                }
                if (di[0] == 0)
                    throw new Exception("ReverseProgress: Нулевой элемент на диагонале. Деление на ноль.");
                else
                    x[0] = temp[0] / di[0];
                return new Vector(n, x);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка.", MessageBoxButtons.OK);
                return null;
            }
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
            return di[index];
        }

        public void setMatrix(DisperseMatrix matrix)
        {
            this.al = matrix.al;
            this.au = matrix.au;
            this.di = matrix.di;
            this.ia = matrix.ia;
            this.ja = matrix.ja;
            this.n = matrix.n;
        }

        public DisperseMatrix getMatrix()
        {
            return this;
        }

        public bool CheckSymmetry()
        {
            for (int i = 0; i < ia[n]; i++)
                if (al[i] != au[i]) return false;
            return true;
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

        public int[] JA
        {
            get { return ja; }
            set { ja = value; }
        }

        public int N
        {
            get { return n; }
            set { n = value; }
        }

    }
}
