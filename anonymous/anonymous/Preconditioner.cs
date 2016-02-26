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
