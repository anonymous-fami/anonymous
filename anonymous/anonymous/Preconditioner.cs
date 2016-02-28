﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    interface IPreconditioner<T>//интерфейс не наследует iMatrix, было убрано чтобы избежать реализацию интерфейса предка.
    {
        string Type { get; }
        //диагональное предобуславливание
        void createDiag(T matrix, out T out_m); //out будет убрано, добавлено чисто для теста.
        //LU разложение
        void createLU(IMatrix<T> matrix, out IMatrix<T> out_matrix);
        //LLT разложение
        void createLLT(T matrix, out T out_l);
        //LU(sq) разложение
        void createLUsq(T matrix, out T out_l, out T out_u);
    }
    #region Профильный формат(1)

    class ProfilePreconditioner : IPreconditioner<ProfileMatrix>
    {
        string type;

        public ProfilePreconditioner()
        {
            type = "Профильный";
        }
        public ProfilePreconditioner(string txt)
        {
            type = txt;
        }
        //возвращение предобуславливателя.
        public string Type
        {
            get
            {
                return type;
            }
        }


        public void createDiag(ProfileMatrix matrix, out ProfileMatrix out_m)
        {

            int[] ia = new int[matrix.N+1];
            for (int i = 0; i <matrix.N+1; i++)
            {
                ia[i] = 1;
            }
            //т.к. все кроме диагонали 0
            int size_au_al = 0;
            double[] al = new double[size_au_al];
            out_m = new ProfileMatrix(al, al, matrix.Di, ia, matrix.N+1);

            
        }


        public void createLLT(ProfileMatrix matrix, out ProfileMatrix out_l)
        {
            throw new NotImplementedException();
        }



        public void createLU(IMatrix<ProfileMatrix> b, out IMatrix<ProfileMatrix> out_matrix)
        {
            int i, j; //индексы в полной матрице
            int ii;  //индекс в массиве ia
            int k;  //счетчик
            int jbeg, jend, beg, end; //вспомогательные переменые
            int j0; //первый ненулевой элемент в строке
            int i0; //первый ненулевой элемент в столбце
            int ial, iau; //индексы элементов в массивах al и au
            double tl, tu, tdi; //накопительные переменные, используются в циклах
            ProfileMatrix a = b.getMatrix();
            int n = a.N;
            double[] au = a.Au;
            double[] al = a.Al;
            double[] di = a.Di;
            int[] ia = a.Ia;

            for (i = 1; i < n; i++)     //пробегаем все строки матрицы
            {
                tdi = 0;
                j0 = i - ia[i + 1] + ia[i];   //первый ненулевой элемент в строке i
                for (ii = ia[i]; ii < ia[i + 1]; ii++)    //пробегаем по ненулевым элементиам i-ой строки
                {
                    j = ii - ia[i] + j0;           //находим индекс очередного элемента профиля в полной матрице
                    jbeg = ia[j];
                    jend = ia[j + 1];
                    if (jbeg < jend)        //если в j-ом столбце есть элементы
                    {
                        i0 = j - jend + jbeg;//индекс первого ненулевого элемента в столбце j
                        if (i0 < j0)//находим с какого элемента умножение строки на cтолбец
                            beg = j0;//даст ненулевой результат
                        else
                            beg = i0;
                        if (j < i - 1)  //находим по какой элемент
                            end = j;
                        else
                            end = i - 1;
                        tl = 0;
                        tu = 0;
                        for (k = 0; k < end - beg; k++) //вычисляем сумму в формуле для Lij
                        {
                            //a.ia[i] - начало i-ой строки в массиве ia
                            //beg - i0(beg - j0) - кол-во элементов ia, которые следует пропустить
                            //так как кол-во элементов в строке и столбце может быть разным. k - смещение
                            ial = ia[i] + beg - j0 + k;
                            iau = ia[j] + beg - i0 + k;
                            tl += al[ial] * au[iau];
                            tu += al[iau] * au[ial];
                        }
                        al[ii] -= tl;
                        au[ii] -= tu;
                    }
                    al[ii] = al[ii] / di[j];
                    tdi += al[ii] * au[ii];       // вычисление диагонали
                }
                di[i] -= tdi;
            }
            out_matrix = new ProfileMatrix(au, al, di, ia, n);
        }

        public void createLUsq(ProfileMatrix matrix, out ProfileMatrix out_l, out ProfileMatrix out_u)  //внимание работает не правильно!
        {
            int i, j,k,ind, num_elem_i, num_elem_j, ind1, ind2;
            double sum_, sum_l, sum_u;

            bool start_with_zero = false;
            // здесь смотрим как задана нумерация в массиве ia, с нуля или с единицы.
            if (matrix.Ia[0] == 0) start_with_zero = true;

            int[] ia=new int[matrix.N+1];
            Array.Copy(matrix.Ia, ia, matrix.N+1); //нужно быть внимательным при копировании массива, иначе можно испортить входные данные.

            if (!start_with_zero)
            for (i=0; i<matrix.N+1; i++)
            {
                ia[i]--;
            }

            out_l = null;
            out_u = null;
            for (i=0; i< matrix.N; i++)
            {
                num_elem_i = ia[i + 1] - ia[i]; //количество элементов в i строке/столбце
                ind = ia[i]; //индекс в массиве al
               

                sum_ = 0; //сумма для диагонали
                for (j=i-num_elem_i; j< i; j++)
                {
                    sum_l = 0;
                    sum_u = 0;
                    num_elem_j = ia[j + 1] - ia[j]; //количество элементов в j строке
                    k = Math.Max(i - num_elem_i, j - num_elem_j);

                    ind1 = ia[i] - (i - num_elem_i) + k;
                    ind2 = ia[j] - (j - num_elem_j) + k;
                    k = j - 1 - k + ind1;
                    for (ind1=ind1; ind1< k; ind1++)
                    {
                        sum_l += matrix.Al[ind1] * matrix.Au[ind2];
                        sum_u += matrix.Au[ind1] * matrix.Al[ind2];
                        ind2++;
                    }
                    matrix.Al[ind] = (matrix.Al[ind] - sum_l) / matrix.Di[j];
                    matrix.Au[ind] = (matrix.Au[ind] - sum_u) / matrix.Di[j];
                    sum_ += matrix.Al[ind] * matrix.Au[ind];
                    ind++;
                }
                matrix.Di[i] = Math.Sqrt(matrix.Di[i] - sum_);
            }
        }
    }
    #endregion
    #region Разреженный формат(4)
    #endregion
    #region Ленточный формат(2)
    #endregion
    #region Плотный формат(0)


    //    public void Create(PlotMatrix matrix, out PlotMatrix out_m)
    //    {
    //       
    //        out_m = new PlotMatrix(matrix.N, matrix.M);
    //        out_m.setmatrix(matrix);

    //        int i, j;
    //        for (i=0; i<out_m.N; i++)
    //            for (j = 0; j < out_m.M; j++)
    //            {
    //                if (i != j)
    //                    out_m.Mas[i, j] = 0;
    //            }
    //       
    //    }
    //}
    #endregion
}
