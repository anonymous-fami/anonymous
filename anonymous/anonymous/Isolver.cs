using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public interface ISolver
    {
        Vector Solve(IMatrix<ProfileMatrix> ProfMatr, Vector RightPart, Vector Initial, int maxiter, double eps); 
    }
}
