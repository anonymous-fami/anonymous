using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    interface IMatrix<T>
    {
        int setmatrix(T t); //устанавливаем значение матрицы
        T getmatrix(); //получаем значение матрицы
       // void printmatrix(); //вывод на экран
    }

    public class Plot_matrix : IMatrix<Plot_matrix>
    {
        private double[,] mas;
        private int n, m;

        //конструктор
        public Plot_matrix()
        {
            n = 1;
            m = 1;
            mas = new double[n, m];
        }
        //конструктор с параметрами(лучше использовать его)
        public Plot_matrix(int nn, int mm)
        {
            n = nn;
            m = mm;
            mas = new double[n, m];
        }
        public Plot_matrix(int nn)
        {
            n = nn;
            m = nn;
            mas = new double[n, m];
        }

        //описание параметров
        //количество строк матрицы (описание параметра) 
        public int N
        {
            get
            {
                return n;
            }

        }
        //количество столбцов матрицы (описание параметра)
        public int M
        {
            get
            {
                return m;
            }

        }
        //описание непосредственно элементов матрицы (описание параметра)
        public double[,] Mas
        {
            get
            {
                return mas;
            }
            set
            {
                mas = value;
            }
        }

        //реализация интерфейса
        public int setmatrix(Plot_matrix t)
        {
            int i, j;
            if ((this.n != t.N) || (this.m != t.M))
            {
                return 0;
            }
            else
            {
                for (i = 0; i < t.N; i++)
                    for (j = 0; j < t.M; j++)
                    {
                        this.Mas[i, j] = t.Mas[i, j];
                    }

            }
            return 1;

        }
        public Plot_matrix getmatrix()
        {
            Plot_matrix pm = new Plot_matrix(this.n, this.m);
            pm.Mas = this.Mas;
            return pm;
        }
        //public void printmatrix()
        //{
        //    int i, j;
        //    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~");
        //    for (i = 0; i < n; i++)
        //    {
        //        for (j = 0; j < n; j++)
        //        {
        //            Console.Write(mas[i, j] + " ");
        //        }
        //        Console.WriteLine();
        //    }
        //    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~");
        //}
    }
}
