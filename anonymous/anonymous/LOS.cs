using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace anonymous
{
    public class LOS: ISolver<ProfileMatrix>
    {
        public Vector Solve(Slae<ProfileMatrix> SLAE, Vector Initial, int maxiter, double eps)
        {
            double iner_p, alpha, betta, residual;
            Vector Ar;

            Vector r = new Vector(SLAE.RightPart.Differ(SLAE.Matrix.Multiply(Initial)));
            Vector z = new Vector(r);
            Vector p = new Vector(SLAE.Matrix.Multiply(z));
            Vector result = new Vector(Initial);

            residual = r.Scalar(r);
            for (int iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
            {
                iner_p = p.Scalar(p);
                alpha = p.Scalar(r) / iner_p;
                result = result.Sum(z.Mult(alpha));
                r = r.Differ(p.Mult(alpha));
                Ar = SLAE.Matrix.Multiply(r);
                betta = -p.Scalar(Ar) / iner_p;
                z = r.Sum(z.Mult(betta));
                p = Ar.Sum(p.Mult(betta));

                residual = r.Scalar(r);
                if (!InputOutput.OutputIterationToForm(iterNum, residual))
                    MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
            }
            return result;
        }
    }
}
