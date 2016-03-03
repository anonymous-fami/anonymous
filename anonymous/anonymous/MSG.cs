using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public class MSG: Isolver
    {
        public Vector Solve(ProfileMatrix ProfMatr, Vector RightPart, Vector Initial, int maxiter, double eps)
        {
            Vector r = RightPart.Differ(ProfMatr.Multiply(Initial));
            Vector z = r;
            Vector result = Initial;
            for(int iterNum = 0; iterNum < maxiter && r.Norm()/RightPart.Norm() >= eps; iterNum++)
            {
                double iner_r = r.Scalar(r);
                Vector Az = ProfMatr.Multiply(z);
                double alpha = r.Scalar(r) / Az.Scalar(z);
                result = result.Sum(z.Mult(alpha));
                r = r.Differ(Az.Mult(alpha));
                double betta = r.Scalar(r) / iner_r;
                z = r.Sum(z.Mult(betta));
            }
            return result;
        }
    }

}