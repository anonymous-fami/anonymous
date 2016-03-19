using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace anonymous
{
    interface IPreconditioner<T>
    {
        //диагональное предобуславливание
        bool createDiag(Slae<T> slae);
        //LU разложение
        bool createLU(Slae<T> slae);
        //LLT разложение
        bool createLLT(Slae<T> slae);
        //LU(sq) разложение
        bool createLUsq(Slae<T> slae);
    }
    #region Профильный формат

    class ProfilePreconditioner : IPreconditioner<ProfileMatrix>
    {
        public bool createDiag(Slae<ProfileMatrix> Slae)
        {
            ProfileMatrix temp = new ProfileMatrix(Slae.Matrix.getMatrix());

            int[] ia = new int[temp.N + 1];

            for (int i = 0; i < temp.N + 1; i++)
            {
                ia[i] = 0;  // подразумевает что ia[0] начинается  с 0, как и во всем проекте.
            }
            //т.к. все кроме диагонали 0
            double[] al = null;
            Slae.PMatrix = new ProfileMatrix(al, al, temp.DI, ia, temp.N);

            return true;
        }

        public bool createLLT(Slae<ProfileMatrix> Slae)
        {
            ProfileMatrix temp = new ProfileMatrix(Slae.Matrix.getMatrix());


                //обработка исключений
                try
                {

                if (temp.DI[0] <= 0)
                        throw new Exception("LLt: Первый диагональный элемент меньше или равен нулю.");

                    double sumDi, sumL;
                    int length, k, stolb, temp1, j;
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
                            if (temp.DI[stolb] == 0)
                            {
                                throw new Exception("LLt: Деление на ноль.");
                            }
                            temp.AL[j] = (temp.AL[j] - sumL) / temp.DI[stolb];
                            temp.AU[j] = temp.AL[j]; //добавлено для заполнения верхнего треугольника, если что уберите.
                            sumDi += temp.AL[j] * temp.AL[j];
                        }
                        if (temp.DI[i] < sumDi)
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
                    return false;
                }
                return true;
            
        }

        public bool createLU(Slae<ProfileMatrix> Slae)
        {
            int i, j, ii, k, jbeg, jend, beg, end, j0, i0, ial, iau;
            double tl, tu, tdi;
            ProfileMatrix temp = new ProfileMatrix(Slae.Matrix.getMatrix());
            //обработка исключений
            try
            {
                //foreach (double x in temp.DI)
                //{
                //    if (x == 0)
                //        throw new Exception("Элемент на диагонали нулевой. Деление на ноль.");
                //}
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
                        if (temp.DI[j]==0)
                        {
                            throw new Exception("Элемент на диагонали нулевой. Деление на ноль.");
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
                return false;
            }
            return true;
        }

        public bool createLUsq(Slae<ProfileMatrix> Slae)
        {
            ProfileMatrix temp = new ProfileMatrix(Slae.Matrix.getMatrix());
            int i0, i1, j0, j1, i, j, mi, mj, kol_i, kol_j, kol_r, ind;
            double sumDiag, sumL, sumU;
            //обработка исключений
            try
            {
                //foreach (double x in temp.DI)
                //{
                //    if (x == 0)
                //        throw new Exception("Элемент на диагонали нулевой. Деление на ноль.");
                //}
                for (i = 0; i < temp.N; i++)
                {
                    i0 = temp.IA[i];
                    i1 = temp.IA[i + 1];
                    j = i - (i1 - i0);
                    sumDiag = 0;

                    for (ind = i0; ind < i1; ind++, j++)
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
                        if (temp.DI[j]==0)
                        {
                            throw new Exception("Элемент на диагонали нулевой. Деление на ноль.");
                        }
                        else
                        {
                            temp.AL[ind] = (temp.AL[ind] - sumL) / temp.DI[j];
                            temp.AU[ind] = (temp.AU[ind] - sumU) / temp.DI[j];
                            sumDiag += temp.AL[ind] * temp.AU[ind];
                        }

                    }

                    if ((temp.DI[i] - sumDiag) < 0)
                    {
                        throw new Exception("Извлечение корня из отрицательного числа.");
                    }
                    else
                    {
                        temp.DI[i] = Math.Sqrt(temp.DI[i] - sumDiag);

                    }
                }
                Slae.PMatrix = temp;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка Предобуславливателя.", MessageBoxButtons.OK);
                Slae.PMatrix = null;
                return false;
            }
            return true;
        }
    }
    #endregion
    #region Разреженный формат
    class DispersePreconditioner : IPreconditioner<DisperseMatrix>
    {
        public bool createDiag(Slae<DisperseMatrix> Slae)
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
            return true;
        }

        public bool createLLT(Slae<DisperseMatrix> Slae)
        {
            throw new NotImplementedException();
        }

        public bool createLU(Slae<DisperseMatrix> Slae)
        {
            int i, j, p, m, k = 0;
            double s = 0;
            DisperseMatrix temp = new DisperseMatrix(Slae.Matrix.getMatrix());
            //обработка исключений
            try
            {
                //foreach (double x in temp.DI)
                //{
                //    if (x == 0)
                //        throw new Exception("Элемент на диагонали нулевой. Деление на ноль.");
                //}
                for (i = 0; i < temp.N; i++)
                {
                    for (j = 0; j < temp.IA[i + 1] - temp.IA[i]; j++, k++)
                    {
                        for (m = j, s = 0, p = temp.IA[temp.JA[k]]; m > 0 && p < temp.IA[temp.JA[k] + 1];)
                            if (temp.JA[p] == temp.JA[k - m])
                                s += temp.AL[k - m] * temp.AU[p++];
                            else
                            if (temp.JA[p] > temp.JA[k - m]) k--;
                            else p++;
                        if (temp.DI[temp.JA[k]]==0)
                        {
                            throw new Exception("Элемент на диагонали нулевой. Деление на ноль.");
                        }
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
                return false;
            }
            return true;
        }


        public bool createLUsq(Slae<DisperseMatrix> Slae)
        {
            DisperseMatrix temp = new DisperseMatrix(Slae.Matrix.getMatrix());

            double sumDiag, sumL, sumU;
            int iaEnd;
            int i, j, k, k1, i0, i1, j0, j1, ind;


            //обработка исключений
            try
            {
               
                //foreach (double x in temp.DI)
                //{
                //    if (x == 0)
                //        throw new Exception("Элемент на диагонали нулевой. Деление на ноль.");
                //}

                iaEnd = temp.IA[temp.N];

                //LU(sq)
                for (k = 1, k1 = 0; k <= temp.N; k++, k1++)
                {

                    i0 = temp.IA[k1];
                    i1 = temp.IA[k];

                    sumDiag = 0;

                    for (ind = i0; ind < i1; ind++)
                    {
                        sumL = 0;
                        sumU = 0;

                        j0 = temp.IA[temp.JA[ind]];
                        j1 = temp.IA[temp.JA[ind] + 1];



                        for (i = i0; i < ind; i++)
                            for (j = j0; j0 < j1; j++)
                            {
                                if ((temp.JA[i] == temp.JA[j]))
                                {
                                    sumL += temp.AL[i] * temp.AU[j];
                                    sumU += temp.AU[i] * temp.AL[j];
                                }
                                j0++;
                            }
                        if (temp.DI[temp.JA[ind]] == 0)
                        {
                            throw new Exception("Элемент на диагонали нулевой. Деление на ноль.");
                        }
                        else
                        {
                            temp.AL[ind] = (temp.AL[ind] - sumL) / temp.DI[temp.JA[ind]];
                            temp.AU[ind] = (temp.AU[ind] - sumU) / temp.DI[temp.JA[ind]];

                            sumDiag += temp.AL[ind] * temp.AU[ind];
                        }
                    }


                    if ((temp.DI[k1] - sumDiag) < 0)
                    {
                        throw new Exception("Извлечение корня из отрицательного числа.");
                    }
                    else
                    {
                        temp.DI[k1] = Math.Sqrt(temp.DI[k1] - sumDiag);
                    }
                }
                Slae.PMatrix = temp;
 
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка Предобуславливателя.", MessageBoxButtons.OK);
                Slae.PMatrix = null;
                return false;
            }
            return true;

        }
    }
    #endregion
    #region Плотный формат
    class DensePreconditioner : IPreconditioner<DenseMatrix>
    {
        public bool createDiag(Slae<DenseMatrix> Slae)
        {
            DenseMatrix temp = new DenseMatrix(Slae.Matrix.getMatrix());
            for (int i = 0; i < temp.N; i++)
                for (int j = 0; j < i; j++)
                {
                    temp.PLOT[i, j] = 0;
                    temp.PLOT[j, i] = 0;
                }
            Slae.PMatrix = new DenseMatrix(temp.PLOT, temp.N);
            return true;
        }

        public bool createLLT(Slae<DenseMatrix> Slae)
        {
            throw new NotImplementedException();
        }

        public bool createLU(Slae<DenseMatrix> Slae)
        {
            DenseMatrix temp = new DenseMatrix(Slae.Matrix.getMatrix());
            double td, tl, tu;
            int i, k, j;
            try
            {
                //for (i = 0; i < temp.N; i++)
                //{
                //    if (temp.PLOT[i, i] == 0)
                //        throw new Exception("Элемент на диагонали нулевой. Деление на ноль.");
                //}
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
                        if (temp.PLOT[j, j] == 0)
                        {
                            throw new Exception("Элемент на диагонали нулевой. Деление на ноль.");
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
                return false;
            }
            return true;
        }

        public bool createLUsq(Slae<DenseMatrix> Slae)
        {
            int i, j, k;
            double sumDiag, sumL, sumU;
            DenseMatrix temp = new DenseMatrix(Slae.Matrix.getMatrix());

            try
            {
                //for (i = 0; i < temp.N; i++)
                //{
                //    if (temp.PLOT[i, i] == 0)
                //        throw new Exception("Элемент на диагонали нулевой. Деление на ноль.");
                //}
                for (i = 0; i < temp.N; i++) 
                {
                    sumDiag = 0;
                    for (j = 0; j < i; j++) 
                    {
                        sumL = 0;
                        sumU = 0;
                        for (k = 0; k < j; k++)
                        {
                            sumL += temp.PLOT[k, j] * temp.PLOT[i, k];
                            sumU += temp.PLOT[j, k] * temp.PLOT[k, i];
                        }

                        if (temp.PLOT[j,j]==0)
                        {
                            throw new Exception("Элемент на диагонали нулевой. Деление на ноль.");
                        }
                        else
                        {
                            temp.PLOT[i, j] = (temp.PLOT[i, j] - sumL) / temp.PLOT[j, j];
                            temp.PLOT[j, i] = (temp.PLOT[j, i] - sumU) / temp.PLOT[j, j];
                            sumDiag += temp.PLOT[i, j] * temp.PLOT[j, i];
                        }

                    }

                    if ((temp.PLOT[i,i] - sumDiag) < 0)
                    {
                        throw new Exception("Извлечение корня из отрицательного числа.");
                    }
                    else
                    {
                        temp.PLOT[i,i] = Math.Sqrt(temp.PLOT[i, i] - sumDiag);
                    }
                }

                Slae.PMatrix = temp;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка Предобуславливателя.", MessageBoxButtons.OK);
                Slae.PMatrix = null;
                return false;
            }
            return true;

            throw new NotImplementedException();
        }
    }
    #endregion
    #region Диагональный формат
    class DiagonalPreconditioner : IPreconditioner<DiagonalMatrix>
    {
        public bool createDiag(Slae<DiagonalMatrix> Slae)
        {
            DiagonalMatrix temp = new DiagonalMatrix(Slae.Matrix.getMatrix());
            for (int i = 0; i < temp.N - temp.IA[0]; i++)
                for (int j = 0; j < temp.ND; j++)
                {
                    temp.AU[i, j] = 0;
                    temp.AL[i, j] = 0;
                }
            Slae.PMatrix = new DiagonalMatrix(temp.AU, temp.AL, temp.DI, temp.IA, temp.N, temp.ND);
            return true;
        }

        public bool createLLT(Slae<DiagonalMatrix> Slae)
        {
            throw new NotImplementedException();
        }

        public bool createLU(Slae<DiagonalMatrix> Slae)
        {
            DiagonalMatrix temp = new DiagonalMatrix(Slae.Matrix.getMatrix());
            double td, tl, tu;
            int i, k, j;
            try
            {
                //for (i = 0; i < temp.N; i++)
                //{
                //    if (temp.DI[i] == 0)
                //        throw new Exception("Элемент на диагонали нулевой. Деление на ноль.");
                //}
                for (i = 0; i < temp.N - temp.IA[0]; i++) //проходим по всем столбцам
                {
                    td = 0;
                    for (j = 0; j < temp.ND && temp.IA[j] + i < temp.N; j++) //проходим по всем даигоналям
                    {
                        tl = 0;
                        tu = 0;
                        for (k = 0; k + 1 < temp.ND && k < i; k++)
                        {
                            if (i >= temp.IA[k])
                            {
                                tl += temp.AL[k + 1, i - temp.IA[k]] * temp.AU[k, i - temp.IA[k]];
                                tu += temp.AL[k, i - temp.IA[k]] * temp.AU[k + 1, i - temp.IA[k]];
                            }
                        }
                        if (temp.DI[i]==0)
                        {
                            throw new Exception("Элемент на диагонали нулевой. Деление на ноль.");
                        }
                        temp.AL[j, i] = (temp.AL[j, i] - tl) / temp.DI[i];
                        temp.AU[j, i] = (temp.AU[j, i] - tu);
                    }
                    for (k = 0; k < temp.ND && i >= temp.IA[k] - 1; k++)
                        td += temp.AL[k, i - temp.IA[k] + 1] * temp.AU[k, i - temp.IA[k] + 1];
                    temp.DI[i + temp.IA[0]] -= td;
                }
                Slae.PMatrix = temp;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка Предобуславливателя.", MessageBoxButtons.OK);
                Slae.PMatrix = null;
                return false;
            }
            return true;
        }

        public bool createLUsq(Slae<DiagonalMatrix> Slae)
        {
            DiagonalMatrix temp = new DiagonalMatrix(Slae.Matrix.getMatrix());
            double sumDiag, sumL, sumU;
            int i, k, j;
            try
            {
                for (i = 0; i < temp.N - temp.IA[0]; i++)
                {
                    sumDiag = 0;
                    for (j = 0; j < temp.ND && temp.IA[j] + i < temp.N; j++)
                    {
                        sumL = 0;
                        sumU = 0;
                        for (k = 0; k + 1 < temp.ND && k < i; k++)
                        {
                            if (i >= temp.IA[k])
                            {
                                sumL += temp.AL[k + 1, i - temp.IA[k]] * temp.AU[k, i - temp.IA[k]];
                                sumU += temp.AL[k, i - temp.IA[k]] * temp.AU[k + 1, i - temp.IA[k]];
                            }
                        }
                        if (temp.DI[i] == 0)
                        {
                            throw new Exception("Элемент на диагонали нулевой. Деление на ноль.");
                        }
                        else
                        {
                            temp.AL[j, i] = (temp.AL[j, i] - sumL) / temp.DI[i];
                            temp.AU[j, i] = (temp.AU[j, i] - sumU) / temp.DI[i];
                        }
                    }
                    for (k = 0; k < temp.ND && i >= temp.IA[k] - 1; k++)
                        sumDiag += temp.AL[k, i - temp.IA[k] + 1] * temp.AU[k, i - temp.IA[k] + 1];

                    if ((temp.DI[i+temp.IA[0]] - sumDiag) < 0)
                    {
                        throw new Exception("Извлечение корня из отрицательного числа.");
                    }
                    else
                    {
                        temp.DI[i + temp.IA[0]] = Math.Sqrt(temp.DI[i + temp.IA[0]] - sumDiag);
                    }
                }                
                Slae.PMatrix = temp;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка Предобуславливателя.", MessageBoxButtons.OK);
                Slae.PMatrix = null;
                return false;
            }
            return true;
        }
    }
    #endregion
}
