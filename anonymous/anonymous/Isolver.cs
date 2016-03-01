using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public interface Isolver
    {
        public Vector Solve(ProfileMatrix ProfMatr, Vector RightPart, Vector Initial, int maxiter, double eps); 
    }
}
