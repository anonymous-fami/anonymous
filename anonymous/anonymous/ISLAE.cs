using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public interface ISLAE
    {
        IMatrix MakeProfile(double[] au, double[] al, double[] di, int[] ia, int n);
        Vector MakeVector(int size, double[] values);
    }
}
