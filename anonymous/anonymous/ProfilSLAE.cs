using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public class ProfilSLAE : ISLAE <ProfileMatrix>
    {
        public IMatrix <ProfileMatrix> MakeProfile(double[] au, double[] al, double[] di, int[] ia, int n)
        {
            return new ProfileMatrix(out au, out al, out di, out ia, out n);
        }
        public Vector MakeVector(int size, double[] values)
        {
            return new Vector(size, values);
        }
    }
}
