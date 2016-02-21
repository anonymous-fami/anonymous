using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    interface IPreconditioner//интерфейс не наследует iMatrix, было убрано чтобы избежать реализацию интерфейса предка.
    {
        string Name { get; }
        void Create(IMatrix matrix);
    }

    class profile_preconditioner: IPreconditioner
    {
        string name;
        public profile_preconditioner()
        {
            name = "Диагональный";
        }
        public profile_preconditioner(string txt)
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
        public void Create(IMatrix matrix)
        {

        }
    }
}
