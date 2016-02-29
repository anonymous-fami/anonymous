using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public class DiagonalSLAE : ISLAE<DiagonalMatrix>
    {
        public IMatrix<DiagonalMatrix> MakeProfile(double[,] au, double[,] al, double[] di, int[] ia, int[] ja, int n, int nd)
        {
            return new DiagonalMatrix(au, al, di, ia, ja, n, nd);
        }
        public Vector MakeVector(int size, double[] values)
        {
            return new Vector(size, values);
        }
    }
}
