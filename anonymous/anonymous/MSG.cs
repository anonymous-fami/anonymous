using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    public class MSG: ISolver
    {
        public Vector Solve(IMatrix<ProfileMatrix> ProfMatr, Vector RightPart, Vector Initial, int maxiter, double eps)
        {
            double iner_r, alpha, betta, residual;
            Vector Az;

            Vector r = new Vector(RightPart.Differ(ProfMatr.Multiply(Initial)));
            Vector z = new Vector(r);
            Vector result = new Vector(Initial);

            residual = r.Norm() / RightPart.Norm();
            for (int iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
            {
                iner_r = r.Scalar(r);
                Az = ProfMatr.Multiply(z);
                alpha = r.Scalar(r) / Az.Scalar(z);
                result = result.Sum(z.Mult(alpha));
                r = r.Differ(Az.Mult(alpha));
                betta = r.Scalar(r) / iner_r;
                z = r.Sum(z.Mult(betta));

                residual = r.Norm() / RightPart.Norm();
                InputOutput.OutputIterationToForm(iterNum, residual);               
            }
            return result;
        }
    }

}