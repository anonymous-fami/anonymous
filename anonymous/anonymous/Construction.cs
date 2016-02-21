using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public class Construction : IConstruction
    {
        public IMatrix MakeProfile(double[] au, double[] al, double[] di, int[] ia, int size_au_al, int size_di, int size_ia)
        {
            return new ProfileMatrix(au, al, di, ia, size_au_al, size_di, size_ia);
        }
        public Vector MakeVector(int size, double[] values)
        {
            return new Vector(size, values);
        }
    }
}
