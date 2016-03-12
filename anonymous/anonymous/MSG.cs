using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace anonymous
{
    public class MSG: ISolver
    {
        public Vector Solve(Slae<DenseMatrix> SLAE, Vector Initial, int maxiter, double eps)
        {
            double iner_r, alpha, betta, residual;
            Vector Az;

            Vector r = new Vector(SLAE.RightPart.Differ(SLAE.Matrix.Multiply(Initial)));
            Vector z = new Vector(r);
            Vector result = new Vector(Initial);

            residual = r.Norm() / SLAE.RightPart.Norm();
            for (int iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
            {
                iner_r = r.Scalar(r);
                Az = SLAE.Matrix.Multiply(z);
                alpha = r.Scalar(r) / Az.Scalar(z);
                result = result.Sum(z.Mult(alpha));
                r = r.Differ(Az.Mult(alpha));
                betta = r.Scalar(r) / iner_r;
                z = r.Sum(z.Mult(betta));

                residual = r.Norm() / SLAE.RightPart.Norm();
                if (!InputOutput.OutputIterationToForm(iterNum, residual))
                    MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
            }
            return result;
        }

        public Vector Solve(Slae<ProfileMatrix> SLAE, Vector Initial, int maxiter, double eps)//////////////
        {

            double iner_r,iner_mr, alpha, betta, residual;
            Vector Az, Mr;

            Vector r = new Vector(SLAE.RightPart.Differ(SLAE.Matrix.Multiply(Initial)));
            
            Vector result = new Vector(Initial);

            residual = r.Norm() / SLAE.RightPart.Norm();
            if (Data.preconditioner == 0)//нет предобуславливания
            {
                Vector z = new Vector(r);
                for (int iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
                {
                    iner_r = r.Scalar(r);
                    Az = SLAE.Matrix.Multiply(z);
                    alpha = r.Scalar(r) / Az.Scalar(z);
                    result = result.Sum(z.Mult(alpha));
                    r = r.Differ(Az.Mult(alpha));
                    betta = r.Scalar(r) / iner_r;
                    z = r.Sum(z.Mult(betta));

                    residual = r.Norm() / SLAE.RightPart.Norm();
                    if (!InputOutput.OutputIterationToForm(iterNum, residual))
                        MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
                }
            }
            else
            if (Data.preconditioner == 1 || Data.preconditioner == 2)//диагональное предобуславливание или Неполное разложение Холесского
            {
                Vector z = new Vector(SLAE.PMatrix.ReverseProgress(SLAE.PMatrix.DirectProgress(r)));
                for (int iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
                {
                    Mr = SLAE.PMatrix.ReverseProgress(SLAE.PMatrix.DirectProgress(r));//M^-1*r(k-1)
                    Az = SLAE.Matrix.Multiply(z);//A*z(k-1)
                    alpha = Mr.Scalar(r) / Az.Scalar(z);
                    result = result.Sum(z.Mult(alpha));
                    iner_mr = Mr.Scalar(r);//(M^-1*r(k-1),r(k-1))
                    r = r.Differ(Az.Mult(alpha));
                    Mr = SLAE.PMatrix.ReverseProgress(SLAE.PMatrix.DirectProgress(r));
                    betta = Mr.Scalar(r) / iner_mr;
                    z = Mr.Sum(z.Mult(betta));

                    residual = r.Norm() / SLAE.RightPart.Norm();
                    if (!InputOutput.OutputIterationToForm(iterNum, residual))
                        MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
                }
            }
                return result;
        }

        public Vector Solve(Slae<DiagonalMatrix> SLAE, Vector Initial, int maxiter, double eps)
        {
            double iner_r, alpha, betta, residual;
            Vector Az;

            Vector r = new Vector(SLAE.RightPart.Differ(SLAE.Matrix.Multiply(Initial)));
            Vector z = new Vector(r);
            Vector result = new Vector(Initial);

            residual = r.Norm() / SLAE.RightPart.Norm();
            for (int iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
            {
                iner_r = r.Scalar(r);
                Az = SLAE.Matrix.Multiply(z);
                alpha = r.Scalar(r) / Az.Scalar(z);
                result = result.Sum(z.Mult(alpha));
                r = r.Differ(Az.Mult(alpha));
                betta = r.Scalar(r) / iner_r;
                z = r.Sum(z.Mult(betta));

                residual = r.Norm() / SLAE.RightPart.Norm();
                if (!InputOutput.OutputIterationToForm(iterNum, residual))
                    MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
            }
            return result;
        }

        public Vector Solve(Slae<DisperseMatrix> SLAE, Vector Initial, int maxiter, double eps)
        {
            double iner_r, alpha, betta, residual;
            Vector Az;

            Vector r = new Vector(SLAE.RightPart.Differ(SLAE.Matrix.Multiply(Initial)));
            Vector z = new Vector(r);
            Vector result = new Vector(Initial);

            residual = r.Norm() / SLAE.RightPart.Norm();
            for (int iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
            {
                iner_r = r.Scalar(r);
                Az = SLAE.Matrix.Multiply(z);
                alpha = r.Scalar(r) / Az.Scalar(z);
                result = result.Sum(z.Mult(alpha));
                r = r.Differ(Az.Mult(alpha));
                betta = r.Scalar(r) / iner_r;
                z = r.Sum(z.Mult(betta));

                residual = r.Norm() / SLAE.RightPart.Norm();
                if (!InputOutput.OutputIterationToForm(iterNum, residual))
                    MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
            }
            return result;
        }
    }

}