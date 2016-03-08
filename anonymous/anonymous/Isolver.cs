using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public interface ISolver<T>
    {
        Vector Solve(Slae<T> SLAE, Vector Initial, int maxiter, double eps);
    }
}
