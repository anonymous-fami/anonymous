using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public class DisperseSLAE : ISLAE<DisperseMatrix>
    {
        public IMatrix<DisperseMatrix> MakeDisperse(double[] au, double[] al, double[] di, int[] ia, int[] ja, int n)
        {
            return new DisperseMatrix(au, al, di, ia,ja, n);
        }
        public Vector MakeVector(int size, double[] values)
        {
            return new Vector(size, values);
        }
    }
}
