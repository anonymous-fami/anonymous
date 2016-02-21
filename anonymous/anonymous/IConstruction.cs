using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public interface IConstruction
    {
        IMatrix MakeProfile(double[] au, double[] al, double[] di, int[] ia, int size_au_al, int size_di, int size_ia);
        Vector MakeVector(int size, double[] values);
    }
}
