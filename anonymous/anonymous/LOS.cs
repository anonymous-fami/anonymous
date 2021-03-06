﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace anonymous
{
    public class LOS: ISolver
    {
        bool autotest = false;

        public Vector Solve(Slae<DenseMatrix> SLAE, Vector Initial, int maxiter, double eps)
        {
            int iterNum;
            double iner_p, alpha, betta, residual;
            Vector Ar;

            Vector result = new Vector(Initial);


            if (Data.preconditioner == 0)//нет предобуславливания
            {
                Vector r = new Vector(SLAE.RightPart.Differ(SLAE.Matrix.Multiply(Initial)));
                Vector z = new Vector(r);
                Vector p = new Vector(SLAE.Matrix.Multiply(z));

                residual = Math.Sqrt(r.Scalar(r)) / SLAE.RightPart.Norm();

                for (iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
                {
                    iner_p = p.Scalar(p);
                    alpha = p.Scalar(r) / iner_p;
                    result = result.Sum(z.Mult(alpha));
                    r = r.Differ(p.Mult(alpha));
                    Ar = SLAE.Matrix.Multiply(r);
                    betta = -p.Scalar(Ar) / iner_p;
                    z = r.Sum(z.Mult(betta));
                    p = Ar.Sum(p.Mult(betta));

                    residual = Math.Sqrt(r.Scalar(r)) / SLAE.RightPart.Norm();
                    if (!autotest)
                    {
                        if (!InputOutput.OutputIterationToForm(iterNum, residual, maxiter, false))
                            MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
                    }
                }
                if (!autotest)
                {
                    if (!InputOutput.OutputIterationToForm(iterNum - 1, residual, maxiter, true))
                        MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
                }
            }
            else
                if (Data.preconditioner == 1 || Data.preconditioner == 2 || Data.preconditioner == 3 || Data.preconditioner == 4)//LU или LU(sq)-предобуславливание
                {
                    Vector fAx = new Vector(SLAE.RightPart.Differ(SLAE.Matrix.Multiply(result)));
                    Vector r = new Vector(SLAE.PMatrix.DirectProgress(fAx));
                    Vector z = new Vector(SLAE.PMatrix.ReverseProgress(r));
                    Vector p = new Vector(SLAE.PMatrix.DirectProgress(SLAE.Matrix.Multiply(z)));

                    if ((r == null) || (z == null) || (p == null)) return null;

                    residual = Math.Sqrt(r.Scalar(r)) / SLAE.RightPart.Norm();

                    for (iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
                    {
                        iner_p = p.Scalar(p);
                        alpha = p.Scalar(r) / iner_p;
                        result = result.Sum(z.Mult(alpha));
                        r = r.Differ(p.Mult(alpha));
                        Ar = SLAE.PMatrix.DirectProgress(SLAE.Matrix.Multiply(SLAE.PMatrix.ReverseProgress(r)));//L^-1*A*U^-1*r(k)
                        betta = -p.Scalar(Ar) / iner_p;
                        z = SLAE.PMatrix.ReverseProgress(r).Sum(z.Mult(betta));
                        p = Ar.Sum(p.Mult(betta));

                        residual = Math.Sqrt(r.Scalar(r)) / SLAE.RightPart.Norm();
                        if (!autotest)
                        {
                            if (!InputOutput.OutputIterationToForm(iterNum, residual, maxiter, false))
                                MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
                        }
                    }
                    if (!autotest)
                    {
                        if (!InputOutput.OutputIterationToForm(iterNum - 1, residual, maxiter, true))
                            MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
                    }
                }
                return result;
        }

        public Vector Solve(Slae<ProfileMatrix> SLAE, Vector Initial, int maxiter, double eps)
        {
            int iterNum;
            double iner_p, alpha, betta, residual;
            Vector Ar;

            Vector result = new Vector(Initial);

            
            if(Data.preconditioner == 0)//нет предобуславливания
            {
                Vector r = new Vector(SLAE.RightPart.Differ(SLAE.Matrix.Multiply(Initial)));
                Vector z = new Vector(r);
                Vector p = new Vector(SLAE.Matrix.Multiply(z));

                residual = Math.Sqrt(r.Scalar(r)) / SLAE.RightPart.Norm();

                for (iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
                {
                    iner_p = p.Scalar(p);
                    alpha = p.Scalar(r) / iner_p;
                    result = result.Sum(z.Mult(alpha));
                    r = r.Differ(p.Mult(alpha));
                    Ar = SLAE.Matrix.Multiply(r);
                    betta = -p.Scalar(Ar) / iner_p;
                    z = r.Sum(z.Mult(betta));
                    p = Ar.Sum(p.Mult(betta));

                    residual = Math.Sqrt(r.Scalar(r)) / SLAE.RightPart.Norm();
                    if (!autotest)
                    {
                        if (!InputOutput.OutputIterationToForm(iterNum, residual, maxiter, false))
                            MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
                    }
                }
                if (!autotest)
                {
                    if (!InputOutput.OutputIterationToForm(iterNum - 1, residual, maxiter, true))
                        MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
                }
            }
            else
                if(Data.preconditioner == 1 || Data.preconditioner == 2 || Data.preconditioner == 3 || Data.preconditioner == 4)//LU или LU(sq)-предобуславливание
                {
                    Vector fAx = new Vector(SLAE.RightPart.Differ(SLAE.Matrix.Multiply(result)));
                    Vector r = new Vector(SLAE.PMatrix.DirectProgress(fAx));
                    Vector z = new Vector(SLAE.PMatrix.ReverseProgress(r));
                    Vector p = new Vector(SLAE.PMatrix.DirectProgress(SLAE.Matrix.Multiply(z)));

                    residual = Math.Sqrt(r.Scalar(r)) / SLAE.RightPart.Norm();

                    for (iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
                    {
                        iner_p = p.Scalar(p);
                        alpha = p.Scalar(r) / iner_p;
                        result = result.Sum(z.Mult(alpha));
                        r = r.Differ(p.Mult(alpha));
                        Ar = SLAE.PMatrix.DirectProgress(SLAE.Matrix.Multiply(SLAE.PMatrix.ReverseProgress(r)));//L^-1*A*U^-1*r(k)
                        betta = -p.Scalar(Ar) / iner_p;
                        z = SLAE.PMatrix.ReverseProgress(r).Sum(z.Mult(betta));
                        p = Ar.Sum(p.Mult(betta));

                        residual = Math.Sqrt(r.Scalar(r)) / SLAE.RightPart.Norm();
                        if (!autotest)
                        {
                            if (!InputOutput.OutputIterationToForm(iterNum, residual, maxiter, false))
                                MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
                        }
                    }
                    if (!autotest)
                    {
                        if (!InputOutput.OutputIterationToForm(iterNum - 1, residual, maxiter, true))
                            MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
                    }
                }
                return result;
        }

        public Vector Solve(Slae<DiagonalMatrix> SLAE, Vector Initial, int maxiter, double eps)
        {
            int iterNum;
            double iner_p, alpha, betta, residual;
            Vector Ar;

            Vector result = new Vector(Initial);


            if (Data.preconditioner == 0)//нет предобуславливания
            {
                Vector r = new Vector(SLAE.RightPart.Differ(SLAE.Matrix.Multiply(Initial)));
                Vector z = new Vector(r);
                Vector p = new Vector(SLAE.Matrix.Multiply(z));

                residual = Math.Sqrt(r.Scalar(r)) / SLAE.RightPart.Norm();

                for (iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
                {
                    iner_p = p.Scalar(p);
                    alpha = p.Scalar(r) / iner_p;
                    result = result.Sum(z.Mult(alpha));
                    r = r.Differ(p.Mult(alpha));
                    Ar = SLAE.Matrix.Multiply(r);
                    betta = -p.Scalar(Ar) / iner_p;
                    z = r.Sum(z.Mult(betta));
                    p = Ar.Sum(p.Mult(betta));

                    residual = Math.Sqrt(r.Scalar(r)) / SLAE.RightPart.Norm();
                    if (!autotest)
                    {
                        if (!InputOutput.OutputIterationToForm(iterNum, residual, maxiter, false))
                            MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
                    }
                }
                if (!autotest)
                {
                    if (!InputOutput.OutputIterationToForm(iterNum - 1, residual, maxiter, true))
                        MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
                }
            }
            else
                if (Data.preconditioner == 1 || Data.preconditioner == 2 || Data.preconditioner == 3 || Data.preconditioner == 4)//LU или LU(sq)-предобуславливание
                {
                    Vector fAx = new Vector(SLAE.RightPart.Differ(SLAE.Matrix.Multiply(result)));
                    Vector r = new Vector(SLAE.PMatrix.DirectProgress(fAx));
                    Vector z = new Vector(SLAE.PMatrix.ReverseProgress(r));
                    Vector p = new Vector(SLAE.PMatrix.DirectProgress(SLAE.Matrix.Multiply(z)));

                    residual = Math.Sqrt(r.Scalar(r)) / SLAE.RightPart.Norm();

                    for (iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
                    {
                        iner_p = p.Scalar(p);
                        alpha = p.Scalar(r) / iner_p;
                        result = result.Sum(z.Mult(alpha));
                        r = r.Differ(p.Mult(alpha));
                        Ar = SLAE.PMatrix.DirectProgress(SLAE.Matrix.Multiply(SLAE.PMatrix.ReverseProgress(r)));//L^-1*A*U^-1*r(k)
                        betta = -p.Scalar(Ar) / iner_p;
                        z = SLAE.PMatrix.ReverseProgress(r).Sum(z.Mult(betta));
                        p = Ar.Sum(p.Mult(betta));

                        residual = Math.Sqrt(r.Scalar(r)) / SLAE.RightPart.Norm();
                        if (!autotest)
                        {
                            if (!InputOutput.OutputIterationToForm(iterNum, residual, maxiter, false))
                                MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
                        }
                    }
                    if (!autotest)
                    {
                        if (!InputOutput.OutputIterationToForm(iterNum - 1, residual, maxiter, true))
                            MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
                    }
                }
                return result;
        }

        public Vector Solve(Slae<DisperseMatrix> SLAE, Vector Initial, int maxiter, double eps)
        {
            int iterNum;
            double iner_p, alpha, betta, residual;
            Vector Ar;

            Vector result = new Vector(Initial);


            if (Data.preconditioner == 0)//нет предобуславливания
            {
                Vector r = new Vector(SLAE.RightPart.Differ(SLAE.Matrix.Multiply(Initial)));
                Vector z = new Vector(r);
                Vector p = new Vector(SLAE.Matrix.Multiply(z));

                residual = Math.Sqrt(r.Scalar(r)) / SLAE.RightPart.Norm();

                for (iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
                {
                    iner_p = p.Scalar(p);
                    alpha = p.Scalar(r) / iner_p;
                    result = result.Sum(z.Mult(alpha));
                    r = r.Differ(p.Mult(alpha));
                    Ar = SLAE.Matrix.Multiply(r);
                    betta = -p.Scalar(Ar) / iner_p;
                    z = r.Sum(z.Mult(betta));
                    p = Ar.Sum(p.Mult(betta));

                    residual = Math.Sqrt(r.Scalar(r)) / SLAE.RightPart.Norm();
                    if (!autotest)
                    {
                        if (!InputOutput.OutputIterationToForm(iterNum, residual, maxiter, false))
                            MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
                    }
                }
                if (!autotest)
                {
                    if (!InputOutput.OutputIterationToForm(iterNum - 1, residual, maxiter, true))
                        MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
                }
            }
            else
                if (Data.preconditioner == 1 || Data.preconditioner == 2 || Data.preconditioner == 3 || Data.preconditioner == 4)//LU или LU(sq)-предобуславливание
                {
                    Vector fAx = new Vector(SLAE.RightPart.Differ(SLAE.Matrix.Multiply(result)));
                    Vector r = new Vector(SLAE.PMatrix.DirectProgress(fAx));
                    Vector z = new Vector(SLAE.PMatrix.ReverseProgress(r));
                    Vector p = new Vector(SLAE.PMatrix.DirectProgress(SLAE.Matrix.Multiply(z)));

                    if ((r == null) || (z == null) || (p == null)) return null;

                    residual = Math.Sqrt(r.Scalar(r)) / SLAE.RightPart.Norm();

                    for (iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
                    {
                        iner_p = p.Scalar(p);
                        alpha = p.Scalar(r) / iner_p;
                        result = result.Sum(z.Mult(alpha));
                        r = r.Differ(p.Mult(alpha));
                        Ar = SLAE.PMatrix.DirectProgress(SLAE.Matrix.Multiply(SLAE.PMatrix.ReverseProgress(r)));//L^-1*A*U^-1*r(k)
                        betta = -p.Scalar(Ar) / iner_p;
                        z = SLAE.PMatrix.ReverseProgress(r).Sum(z.Mult(betta));
                        p = Ar.Sum(p.Mult(betta));

                        residual = Math.Sqrt(r.Scalar(r)) / SLAE.RightPart.Norm();
                        if (!autotest)
                        {
                            if (!InputOutput.OutputIterationToForm(iterNum, residual, maxiter, false))
                                MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
                        }
                    }
                    if (!autotest)
                    {
                        if (!InputOutput.OutputIterationToForm(iterNum - 1, residual, maxiter, true))
                            MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
                    }
                }
                return result;
        }

        public void set_autotest(bool flag)
        {
            autotest = flag;
        }
    }
}
