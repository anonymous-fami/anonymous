using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public interface ISolver
    {
        Vector Solve(ProfileSLAE SLAE, Vector Initial, int maxiter, double eps);
    }
}
