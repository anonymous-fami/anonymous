using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public class SLAE : ISLAE
    {
        public IMatrix MakeProfile(double[] au, double[] al, double[] di, int[] ia, int n)
        {
            return new ProfileMatrix(au, al, di, ia, n);
        }
        public Vector MakeVector(int size, double[] values)
        {
            return new Vector(size, values);
        }
    }
}
