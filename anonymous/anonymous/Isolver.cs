using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public interface ISolver
    {        
        Vector Solve(Slae<DenseMatrix> SLAE, Vector Initial, int maxiter, double eps);
        Vector Solve(Slae<ProfileMatrix> SLAE, Vector Initial, int maxiter, double eps);
        Vector Solve(Slae<DiagonalMatrix> SLAE, Vector Initial, int maxiter, double eps);
        Vector Solve(Slae<DisperseMatrix> SLAE, Vector Initial, int maxiter, double eps);
        void set_autotest(bool flag);      
    }
}
