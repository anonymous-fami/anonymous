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
        void createLU(T matrix, out T out_l, out T out_u);
        //LLT разложение
        void createLLT(T matrix, out T out_l);
        //LU(sq) разложение
        //void createLUsq();
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

            throw new NotImplementedException();
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



        public void createLU(ProfileMatrix matrix, out ProfileMatrix out_l, out ProfileMatrix out_u)
        {            
            throw new NotImplementedException();
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
