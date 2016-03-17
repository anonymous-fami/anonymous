﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace anonymous
{
    class BSGstab: ISolver
    {
        public Vector Solve(Slae<ProfileMatrix> SLAE, Vector Initial, int maxiter, double eps)
        {
            double pr, pr1, iner_mr, alpha, betta, residual;
            Vector Az, Mr;

            Vector r = new Vector(SLAE.RightPart.Differ(SLAE.Matrix.Multiply(Initial)));

            Vector result = new Vector(Initial);

            residual = r.Norm() / SLAE.RightPart.Norm();
            Vector z = new Vector(r);
            Vector s = new Vector(r);
            Vector p = new Vector(r);
            pr1 = p.Scalar(r);
            for (int iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
            {
                Az = SLAE.Matrix.Multiply(z);
                alpha = pr1 / Az.Scalar(s);
                result = result.Sum(z.Mult(alpha));
                r = r.Differ(Az.Mult(alpha));
                p = p.Differ(SLAE.Matrix.TMultiply(s).Mult(alpha));
                pr = p.Scalar(r);
                betta = pr / pr1;
                pr1 = pr;
                z = r.Sum(z.Mult(betta));
                s = p.Sum(s.Mult(betta));
                residual = r.Norm() / SLAE.RightPart.Norm();
                if (!InputOutput.OutputIterationToForm(iterNum, residual))
                    MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
            }
            return result;
        }

        public Vector Solve(Slae<DenseMatrix> SLAE, Vector Initial, int maxiter, double eps)
        {
            double pr, pr1, iner_mr, alpha, betta, residual;
            Vector Az, Mr;

            Vector r = new Vector(SLAE.RightPart.Differ(SLAE.Matrix.Multiply(Initial)));

            Vector result = new Vector(Initial);

            residual = r.Norm() / SLAE.RightPart.Norm();
            Vector z = new Vector(r);
            Vector s = new Vector(r);
            Vector p = new Vector(r);
            pr1 = p.Scalar(r);
            for (int iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
            {
                Az = SLAE.Matrix.Multiply(z);
                alpha = pr1 / Az.Scalar(s);
                result = result.Sum(z.Mult(alpha));
                r = r.Differ(Az.Mult(alpha));
                p = p.Differ(SLAE.Matrix.TMultiply(s).Mult(alpha));
                pr = p.Scalar(r);
                betta = pr / pr1;
                pr1 = pr;
                z = r.Sum(z.Mult(betta));
                s = p.Sum(s.Mult(betta));
                residual = r.Norm() / SLAE.RightPart.Norm();
                if (!InputOutput.OutputIterationToForm(iterNum, residual))
                    MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
            }
            return result;
        }

        public Vector Solve(Slae<DisperseMatrix> SLAE, Vector Initial, int maxiter, double eps)
        {
            double pr, pr1, iner_mr, alpha, betta, residual;
            Vector Az, Mr;

            Vector r = new Vector(SLAE.RightPart.Differ(SLAE.Matrix.Multiply(Initial)));

            Vector result = new Vector(Initial);

            residual = r.Norm() / SLAE.RightPart.Norm();
            Vector z = new Vector(r);
            Vector s = new Vector(r);
            Vector p = new Vector(r);
            pr1 = p.Scalar(r);
            for (int iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
            {
                Az = SLAE.Matrix.Multiply(z);
                alpha = pr1 / Az.Scalar(s);
                result = result.Sum(z.Mult(alpha));
                r = r.Differ(Az.Mult(alpha));
                p = p.Differ(SLAE.Matrix.TMultiply(s).Mult(alpha));
                pr = p.Scalar(r);
                betta = pr / pr1;
                pr1 = pr;
                z = r.Sum(z.Mult(betta));
                s = p.Sum(s.Mult(betta));
                residual = r.Norm() / SLAE.RightPart.Norm();
                if (!InputOutput.OutputIterationToForm(iterNum, residual))
                    MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
            }
            return result;
        }

        public Vector Solve(Slae<DiagonalMatrix> SLAE, Vector Initial, int maxiter, double eps)
        {
            double pr, pr1, iner_mr, alpha, betta, residual;
            Vector Az, Mr;

            Vector r = new Vector(SLAE.RightPart.Differ(SLAE.Matrix.Multiply(Initial)));

            Vector result = new Vector(Initial);

            residual = r.Norm() / SLAE.RightPart.Norm();
            Vector z = new Vector(r);
            Vector s = new Vector(r);
            Vector p = new Vector(r);
            pr1 = p.Scalar(r);
            for (int iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
            {
                Az = SLAE.Matrix.Multiply(z);
                alpha = pr1 / Az.Scalar(s);
                result = result.Sum(z.Mult(alpha));
                r = r.Differ(Az.Mult(alpha));
                p = p.Differ(SLAE.Matrix.TMultiply(s).Mult(alpha));
                pr = p.Scalar(r);
                betta = pr / pr1;
                pr1 = pr;
                z = r.Sum(z.Mult(betta));
                s = p.Sum(s.Mult(betta));
                residual = r.Norm() / SLAE.RightPart.Norm();
                if (!InputOutput.OutputIterationToForm(iterNum, residual))
                    MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
            }
            return result;
        }
    }
}
