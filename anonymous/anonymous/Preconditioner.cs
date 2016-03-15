using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace anonymous
{
    interface IPreconditioner<T>//интерфейс не наследует iMatrix, было убрано чтобы избежать реализацию интерфейса предка.
    {
        //диагональное предобуславливание
        void createDiag(Slae<T> slae);
        //LU разложение
        void createLU(Slae<T> slae);
        //LLT разложение
        void createLLT(Slae<T> slae);
        //LU(sq) разложение
        void createLUsq(Slae<T> slae);
    }
    #region Профильный формат(1)

    class ProfilePreconditioner : IPreconditioner<ProfileMatrix>
    {
        public void createDiag(Slae<ProfileMatrix> Slae)
        {
            ProfileMatrix temp = new ProfileMatrix(Slae.Matrix.getMatrix());

            int[] ia = new int[temp.N+1];
            
            for (int i = 0; i <temp.N+1; i++)
            {
                ia[i] = 0;  // подразумевает что ia[0] начинается  с 0, как и во всем проекте.
            }
            //т.к. все кроме диагонали 0
            double[] al = null;
            Slae.PMatrix = new ProfileMatrix(al, al, temp.DI, ia, temp.N);                   
        }

        public void createLLT(Slae<ProfileMatrix> Slae)
        {
            ProfileMatrix temp = new ProfileMatrix(Slae.Matrix.getMatrix());

            //обработка исключений
            try
            {
                if (temp.DI[0] <= 0)
                    throw new Exception("LLt: Первый диагональный элемент меньше или равен нулю.");

                double sumDi, sumL;
                int length, k, stolb, temp1, j ;
                for (int i = 0; i < temp.N; i++)
                {
                    sumDi = 0;
                    for (j = temp.IA[i]; j < temp.IA[i + 1]; j++)
                    {
                        length = temp.IA[i + 1] - temp.IA[i];   //колличество элементов в строке
                        stolb = i - (temp.IA[i + 1] - j);       //номер столбца вычисляемого элемента
                        sumL = 0;
                        if (temp.IA[stolb + 1] - temp.IA[stolb] != 0)
                            for (k = 0; k < j - temp.IA[i]; k++)
                            {
                                temp1 = i - length + k - stolb + temp.IA[stolb + 1] - temp.IA[stolb];
                                sumL += temp.AL[temp.IA[i] + k] * temp.AL[temp.IA[stolb] + temp1];    // L[i,j]=1/L[j,j](A[i,j]-(SUM(L[i,k]*L[j,k]),K=1 to j-1))
                            }
                        if(temp.DI[stolb]==0)
                        {
                            throw new Exception("LLt: Деление на ноль.");
                        }
                        temp.AL[j] = (temp.AL[j] - sumL) / temp.DI[stolb];
                        temp.AU[j] = temp.AL[j]; //добавлено для заполнения верхнего треугольника, если что уберите.
                        sumDi += temp.AL[j] * temp.AL[j];
                    }
                    if(temp.DI[i] < sumDi)
                    {
                        throw new Exception("LLt: Извлечение корня из отрицательного числа.");
                    }
                    temp.DI[i] = Math.Sqrt(temp.DI[i] - sumDi);
                }
                Slae.PMatrix = temp;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка Предобуславливателя.", MessageBoxButtons.OK);
                Slae.PMatrix = null;
            }
            #region old

            /*int[] ia = new int[matrix.N + 1];
            for (int i = 0; i < matrix.N + 1; i++)
            {
                ia[i] = matrix.IA[i];
            }
            double[] L = new double[ia[matrix.N] - 1];
            double[] Di = new double[matrix.N];
            double epsForFirstElem = 1e-5;
            if(Math.Abs(matrix.DI[0])< epsForFirstElem)
            {
                for (int i = 0; i < matrix.N; i++)
                {
                    Di[i] = matrix.DI[i] + epsForFirstElem;
                }
            }
            else
            {
                for (int i = 0; i < matrix.N; i++)
                {
                    Di[i] = matrix.DI[i];
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
                    L[j] = (matrix.AL[j] - sumL) / Di[stolb] ;
                    sumDi += L[j] * L[j];
                }
                Di[i] = Math.Sqrt(Di[i] - sumDi);
            }
            */
            #endregion
        }

        public void createLU(Slae<ProfileMatrix> Slae)
        {
            int i, j, ii, k, jbeg, jend, beg, end, j0, i0, ial, iau;
            double tl, tu, tdi;
            ProfileMatrix temp = new ProfileMatrix(Slae.Matrix.getMatrix());
            //обработка исключений
            try
            {
                foreach (double x in temp.DI)
                {
                    if (x == 0)
                        throw new Exception("Элемент на диагонали нулевой. Деление на ноль.");
                }
                for (i = 1; i < temp.N; i++)     //пробегаем все строки матрицы
                {
                    tdi = 0;
                    j0 = i - temp.IA[i + 1] + temp.IA[i];   //первый ненулевой элемент в строке i
                    for (ii = temp.IA[i]; ii < temp.IA[i + 1]; ii++)    //пробегаем по ненулевым элементиам i-ой строки
                    {
                        j = ii - temp.IA[i] + j0;           //находим индекс очередного элемента профиля в полной матрице
                        jbeg = temp.IA[j];
                        jend = temp.IA[j + 1];
                        if (jbeg < jend)        //если в j-ом столбце есть элементы
                        {
                            i0 = j - jend + jbeg;//индекс первого ненулевого элемента в столбце j
                            if (i0 < j0) beg = j0; //находим с какого элемента умножение строки на cтолбец даст ненулевой результат
                            else beg = i0;
                            if (j < i - 1) end = j; //находим по какой элемент
                            else end = i - 1;
                            tl = 0;
                            tu = 0;
                            for (k = 0; k < end - beg; k++) //вычисляем сумму в формуле для Lij
                            {
                                //a.ia[i] - начало i-ой строки в массиве ia
                                //beg - i0(beg - j0) - кол-во элементов ia, которые следует пропустить
                                //так как кол-во элементов в строке и столбце может быть разным. k - смещение
                                ial = temp.IA[i] + beg - j0 + k;
                                iau = temp.IA[j] + beg - i0 + k;
                                tl += temp.AL[ial] * temp.AU[iau];
                                tu += temp.AL[iau] * temp.AU[ial];
                            }
                            temp.AL[ii] -= tl;
                            temp.AU[ii] -= tu;
                        }
                        temp.AL[ii] = temp.AL[ii] / temp.DI[j];
                        tdi += temp.AL[ii] * temp.AU[ii];       // вычисление диагонали
                    }
                    temp.DI[i] -= tdi;
                }
                Slae.PMatrix = temp;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка Предобуславливателя.", MessageBoxButtons.OK);
                Slae.PMatrix = null;
            }
        }

        public void createLUsq(Slae<ProfileMatrix> Slae)  
        {
            ProfileMatrix temp = new ProfileMatrix(Slae.Matrix.getMatrix());           
            int i0, i1, j0, j1, i, j, mi, mj, kol_i, kol_j, kol_r;
            double sumDiag, sumL, sumU;         
            //обработка исключений
            try
            {
                foreach (double x in temp.DI)
                {
                    if (x==0)
                    throw new Exception("Элемент на диагонали нулевой. Деление на ноль.");
                }               
                for (i = 0; i < temp.N; i++)
                {
                    i0 = temp.IA[i];
                    i1 = temp.IA[i + 1];
                    j = i - (i1 - i0);
                    sumDiag = 0;

                    for (int ind = i0; ind < i1; ind++, j++)
                    {
                        sumL = 0;
                        sumU = 0;
                        j0 = temp.IA[j];
                        j1 = temp.IA[j + 1];
                        mi = i0;
                        mj = j0;
                        kol_i = ind - i0;
                        kol_j = j1 - j0;
                        kol_r = kol_i - kol_j;
                        if (kol_r < 0) mj -= kol_r;
                        else mi += kol_r;
                        for (; mi < ind; ++mi, ++mj)
                        {
                            sumL += temp.AL[mi] * temp.AU[mj];
                            sumU += temp.AU[mi] * temp.AL[mj];
                        }
                        temp.AL[ind] = (temp.AL[ind] - sumL) / temp.DI[j];
                        temp.AU[ind] = (temp.AU[ind] - sumU) / temp.DI[j];
                        sumDiag += temp.AL[ind] * temp.AU[ind];
                    }
                    //необходимо добавить какой-нибудь экспешн.
                    if ((temp.DI[i] - sumDiag)<0)
                    {
                        throw new Exception("Извлечение корня из отрицательного числа.");
                    }
                    else
                    {
                        temp.DI[i] = Math.Sqrt(temp.DI[i] - sumDiag);
                       Slae.PMatrix = temp;
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка Предобуславливателя.", MessageBoxButtons.OK);
                Slae.PMatrix = null;
            }            
        }
    }
    #endregion
    #region Разреженный формат(4)
    class DispersePreconditioner : IPreconditioner<DisperseMatrix>
    {
        public void createDiag(Slae<DisperseMatrix> Slae)
        {
            DisperseMatrix temp = new DisperseMatrix(Slae.Matrix.getMatrix());
            int[] ia = new int[temp.N + 1];
            int[] ja = new int[temp.N + 1];
            for (int i = 0; i < temp.N + 1; i++)
            {
                ia[i] = 0;  // подразумевает что ia[0] начинается  с 0, как и во всем проекте.
                ja[i] = 0;
            }
            //т.к. все кроме диагонали 0
            double[] al = null;
            Slae.PMatrix = new DisperseMatrix(al, al, temp.DI, ia, ja, temp.N);
        }

        public void createLLT(Slae<DisperseMatrix> Slae)
        {

            throw new NotImplementedException();
        }

        public void createLU(Slae<DisperseMatrix> Slae)
        {
            int i, j, p, m, k = 0;
            double s = 0;
            DisperseMatrix temp = new DisperseMatrix(Slae.Matrix.getMatrix());
            //обработка исключений
            try
            {
                foreach (double x in temp.DI)
                {
                    if (x == 0)
                        throw new Exception("Элемент на диагонали нулевой. Деление на ноль.");
                }
                for (i = 0; i < temp.N; i++)
                {
                    for (j = 0; j < temp.IA[i + 1] - temp.IA[i]; j++, k++)
                    {
                        for (m = j, s = 0, p = temp.IA[temp.JA[k]]; m > 0 && p < temp.IA[temp.JA[k] + 1]; )
                            if (temp.JA[p] == temp.JA[k - m])
                                s += temp.AL[k - m] * temp.AU[p++];
                            else
                                if (temp.JA[p] > temp.JA[k - m]) k--;
                                else p++;
                        temp.AL[k] = (temp.AL[k] - s) / temp.DI[temp.JA[k]];
                        temp.AU[k] -= s;
                    }
                    for (m = k - j, s = 0; m < k; m++)
                        s += temp.AL[m] * temp.AU[m];
                    temp.DI[i] = temp.DI[i] - s;
                }
                Slae.PMatrix = temp;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка Предобуславливателя.", MessageBoxButtons.OK);
                Slae.PMatrix = null;

            }
        }

        public void createLUsq(Slae<DisperseMatrix> Slae)
        {
            DisperseMatrix temp = new DisperseMatrix(Slae.Matrix.getMatrix());

            //int i0, i1, j0, j1, i, j, mi, mj, kol_i, kol_j, kol_r;

            double sumDiag, sumL, sumU;
            int iaEnd;
            int i, j, k, k1, i0, i1, j0, j1;


            //обработка исключений
            try
            {
                foreach (double x in temp.DI)
                {
                    if (x == 0)
                        throw new Exception("Элемент на диагонали нулевой. Деление на ноль.");
                }

                iaEnd = temp.IA[temp.N];

                //LU(sq)
                for (k = 1, k1 = 0; k <= temp.N; k++, k1++)
                {

                    i0 = temp.IA[k1];
                    i1 = temp.IA[k];

                    sumDiag = 0;

                    for (int ind = i0; ind < i1; ind++)
                    {
                        sumL = 0;
                        sumU = 0;

                        j0 = temp.IA[temp.JA[ind]];
                        j1 = temp.IA[temp.JA[ind] + 1];


                        for (i = i0; i < ind; i++)
                            for (j = j0; j0 < j1; j++)
                            {
                                if (temp.JA[i] == temp.JA[j])
                                {
                                    sumL += temp.AL[i] * temp.AU[j];
                                    sumU += temp.AU[i] * temp.AL[j];
                                    j0++;
                                }
                            }


                        temp.AL[ind] = (temp.AL[ind] - sumL) / temp.DI[ind];
                        temp.AU[ind] = (temp.AU[ind] - sumU) / temp.DI[ind];
                        sumDiag += temp.AL[ind] * temp.AU[ind];
                    }


                    if ((temp.DI[k1] - sumDiag) < 0)
                    {
                        throw new Exception("Извлечение корня из отрицательного числа.");
                    }
                    else
                    {
                        temp.DI[k1] = Math.Sqrt(temp.DI[k1] - sumDiag);
                        Slae.PMatrix = temp;
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка Предобуславливателя.", MessageBoxButtons.OK);
                Slae.PMatrix = null;
             
            }
            


        }
    }
    #endregion
    #region Ленточный формат(2)

    #endregion
    #region Плотный формат(0)
    class DensePreconditioner : IPreconditioner<DenseMatrix>
    {
        public void createDiag(Slae<DenseMatrix> Slae)
        {
            DenseMatrix temp = new DenseMatrix(Slae.Matrix.getMatrix());
            for (int i = 0; i < temp.N; i++)
                for (int j = 0; j < i; j++)
                {
                    temp.PLOT[i, j] = 0;
                    temp.PLOT[j, i] = 0;
                }
            Slae.PMatrix = new DenseMatrix(temp.PLOT, temp.N);
        }

        public void createLLT(Slae<DenseMatrix> Slae)
        {
            throw new NotImplementedException();
        }

        public void createLU(Slae<DenseMatrix> Slae)
        {
            DenseMatrix temp = new DenseMatrix(Slae.Matrix.getMatrix());
            double td, tl, tu;
            int i, k, j;
            try
            {
                for (i=0;i<temp.N;i++)
                {
                    if (temp.PLOT[i,i] == 0)
                        throw new Exception("Элемент на диагонали нулевой. Деление на ноль.");
                }
                for (i = 0; i < temp.N; i++) //проходим по всем строкам
                {
                    td = 0;
                    for (j = 0; j < i; j++) //проходим по всем столбцам
                    {
                        tl = 0;
                        tu = 0;
                        for (k = 0; k < j; k++)
                        {
                            tl += temp.PLOT[k, j] * temp.PLOT[i, k];
                            tu += temp.PLOT[j, k] * temp.PLOT[k, i];
                        }
                        temp.PLOT[i, j] = (temp.PLOT[i, j] - tl) / temp.PLOT[j, j];
                        temp.PLOT[j, i] = (temp.PLOT[j, i] - tu);
                        td += temp.PLOT[i, j] * temp.PLOT[j, i];
                    }
                    temp.PLOT[i, i] -= td;
                }
                Slae.PMatrix = temp;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка Предобуславливателя.", MessageBoxButtons.OK);
                Slae.PMatrix = null;
            }
        }

        public void createLUsq(Slae<DenseMatrix> Slae)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
    #region Диагональный формат(3)
    class DiagonalPreconditioner : IPreconditioner<DiagonalMatrix>
    {
        public void createDiag(Slae<DiagonalMatrix> Slae)
        {
            throw new NotImplementedException();
        }

        public void createLLT(Slae<DiagonalMatrix> Slae)
        {
            throw new NotImplementedException();
        }

        public void createLU(Slae<DiagonalMatrix> Slae)
        {
            throw new NotImplementedException();
        }

        public void createLUsq(Slae<DiagonalMatrix> Slae)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
