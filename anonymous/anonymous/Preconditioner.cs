using System;
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
                                                // void createLU(IMatrix<T> matrix, out IMatrix<T> out_matrix);
        void createLU(IMatrix<T> matrix, out IMatrix<T> out_matrix);
        //LLT разложение
        void createLLT(T matrix, out T out_l);
        //LU(sq) разложение
        void createLUsq(T matrix, out T out_matrix);
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
            int[] ia = new int[matrix.N + 1];
            for (int i = 0; i < matrix.N + 1; i++)
            {
                ia[i] = matrix.Ia[i];
            }
            double[] L = new double[ia[matrix.N] - 1];
            double[] Di = new double[matrix.N];
            double epsForFirstElem = 1e-5;
            if(Math.Abs(matrix.Di[0])< epsForFirstElem)
            {
                for (int i = 0; i < matrix.N; i++)
                {
                    Di[i] = matrix.Di[i] + epsForFirstElem;
                }
            }
            else
            {
                for (int i = 0; i < matrix.N; i++)
                {
                    Di[i] = matrix.Di[i];
                }
            }
            for (int i = 0; i < matrix.N; i++)
            {
                double sumDi = 0;
                for (int j = ia[i]; j < ia[i + 1]; j++)
                {
                    int length = ia[i + 1] - ia[i];
                    int k = 0;
                    int stolb = i - (ia[i + 1] - j);
                    double sumL = 0;
                    for (k = 0; k < j - ia[i]; k++) 
                    {
                        //int temp = i - length + k;
                        //int temp2 = stolb - (ia[stolb + 1] - ia[stolb]);
                        //if (stolb + k - (ia[stolb + 1] - ia[stolb]) - (i - length + k) > 0)
                        //   continue;
                        int temp1 = 0;
                        while (stolb + temp1 - (ia[stolb + 1] - ia[stolb]) != (i - length + k)
                            && (ia[stolb + 1] - ia[stolb]) != 0)
                        {
                            temp1++;
                            continue;
                        }
                        if (ia[stolb + 1] - ia[stolb] == 0) continue;
                        sumL += L[ia[i] + k] * L[ia[stolb] + temp1];    // L[i,j]=1/L[j,j](A[i,j]-(SUM(L[i,k]*L[j,k]),K=1 to j-1))
                    }
                    L[j] = (matrix.Al[j] - sumL) / Di[stolb] ;
                    sumDi += L[j] * L[j];
                }
                Di[i] = Math.Sqrt(Di[i] - sumDi);
            }

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
            bool start_with_zero = false;
            
            // здесь смотрим как задана нумерация в массиве ia, с нуля или с единицы.
            if (ia[0] == 0) start_with_zero = true;
            if (!start_with_zero)
            for (i = 0; i < n + 1; i++)
            {
                ia[i]--;
            }

            //данный код подразумевает, что ia[0]=0 а не 1.


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

        public void createLUsq(ProfileMatrix matrix, out ProfileMatrix out_matrix)  //внимание работает не правильно!
        {
            int i0, i1, j0, j1, i, j, mi, mj, kol_i, kol_j, kol_r;
            double sumDiag, sumL, sumU;


            bool start_with_zero = false;
            // здесь смотрим как задана нумерация в массиве ia, с нуля или с единицы.
            if (matrix.Ia[0] == 0) start_with_zero = true;

            //нужно быть внимательным при копировании массива, иначе можно испортить входные данные.
            int n = matrix.N;
            int[] ia=new int[matrix.N+1];
            Array.Copy(matrix.Ia, ia, matrix.N+1); 

            double[] al = new double[ia[n] - 1];
            Array.Copy(matrix.Al, al, ia[n] - 1);

            double[] au = new double[ia[n] - 1];
            Array.Copy(matrix.Au, au, ia[n] - 1);

            double[] di = new double[n];
            Array.Copy(matrix.Di, di, n);


            //Если ia[0]=1, делаем его равным 0
            if (!start_with_zero)
            for (i=0; i<matrix.N+1; i++)
            {
                ia[i]--;
            }

           
            //LU(sq)

            for (i = 0; i < n; i++)
            {
                i0 = ia[i];
                i1 = ia[i + 1];
                j = i - (i1 - i0);
                sumDiag = 0;
                for (int m = i0; m < i1; m++, j++)
                {
                    sumL = 0;
                    sumU = 0;

                    j0 = ia[j];
                    j1 = ia[j + 1];

                    mi = i0;
                    mj = j0;

                    kol_i = m - i0;
                    kol_j = j1 - j0;
                    kol_r = kol_i - kol_j;
                    if (kol_r < 0) mj -= kol_r;
                    else mi += kol_r;



                    for (mi = mi; mi < m; mi++, mj++)
                    {
                        sumL += al[mi] * au[mj];
                        sumU += au[mi] * al[mj];
                    }


                    al[m] = (al[m] - sumL)/di[j];
                    au[m] = (au[m] - sumU) / di[j];
                    sumDiag += al[m] * au[m];
                }

                //необходимо добавить какой-нибудь экспешн.
                di[i] = Math.Sqrt(di[i] - sumDiag);


            }

            //делаем снова ia[0]=1, если необходимо
            if (!start_with_zero)
                for (i = 0; i < matrix.N + 1; i++)
        {
                    ia[i]++;
                }


            out_matrix = new ProfileMatrix(au, al,di, ia,n);

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
