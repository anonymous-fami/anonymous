using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    interface IPreconditioner<T>//интерфейс не наследует iMatrix, было убрано чтобы избежать реализацию интерфейса предка.
    {
        string Name { get; }
        void Create(T matrix, out T out_m); //out будет убрано, добавлено чисто для теста.
    }

    class preconditioner_profil : IPreconditioner<ProfileMatrix>
    {
        string name;
        public preconditioner_profil()
        {
            name = "Диагональный";
        }
        public preconditioner_profil(string txt)
        {
            name = txt;
        }
        //возвращение предобуславливателя.
        public string Name
        {
            get
            {
                return name;
            }
        }
        public void Create(ProfileMatrix matrix, out ProfileMatrix out_m)
        {
            //switch (this.Name)

            //НАРУШЕНИЕ ИНКАПСУЛЯЦИИ? попросить описать как параметры(свойства) внутреннее представление класса ProfileMatrix
            int[] ia = matrix.ia;

            for (int i = 0; i < matrix.size_ia; i++)
            {
                ia[i] = 1;
            }

            //т.к. все кроме диагонали 0
            int size_au_al = 0;
            double[] al = new double[size_au_al];

            out_m = new ProfileMatrix(al, al, matrix.di, ia, size_au_al, matrix.size_di, matrix.size_ia);


        }
    }
}
