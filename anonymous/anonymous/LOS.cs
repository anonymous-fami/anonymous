using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public class LOS: Isolver
    {
        public Vector Solve(ProfileMatrix ProfMatr, Vector RightPart, Vector Initial, int maxiter, double eps)
        {
            Vector r = RightPart.Differ(ProfMatr.Multiply(Initial));
            Vector z = r;
            Vector p = ProfMatr.Multiply(z);
            Vector result = Initial;
            for (int iterNum = 0; iterNum < maxiter && r.Scalar(r) >= eps; iterNum++)
            {
                double iner_p = p.Scalar(p);
                double alpha = p.Scalar(r) / iner_p;
                result = result.Sum(z.Mult(alpha));
                r = r.Differ(p.Mult(alpha));
                Vector Ar = ProfMatr.Multiply(r);
                double betta = -p.Scalar(Ar) / iner_p;
                z = r.Sum(z.Mult(betta));
                p = Ar.Sum(p.Mult(betta));
            }
            return result;
        }
    }
}
