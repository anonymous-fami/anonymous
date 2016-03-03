using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public class LOS: ISolver
    {
        public Vector Solve(IMatrix<ProfileMatrix> ProfMatr, Vector RightPart, Vector Initial, int maxiter, double eps)
        {
            double iner_p, alpha, betta;
            Vector Ar;

            Vector r = RightPart.Differ(ProfMatr.Multiply(Initial));
            Vector z = r;
            Vector p = ProfMatr.Multiply(z);
            Vector result = Initial;

            for (int iterNum = 0; iterNum < maxiter && r.Scalar(r) >= eps; iterNum++)
            {
                iner_p = p.Scalar(p);
                alpha = p.Scalar(r) / iner_p;
                result = result.Sum(z.Mult(alpha));
                r = r.Differ(p.Mult(alpha));
                Ar = ProfMatr.Multiply(r);
                betta = -p.Scalar(Ar) / iner_p;
                z = r.Sum(z.Mult(betta));
                p = Ar.Sum(p.Mult(betta));
            }
            return result;
        }
    }
}
