using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace anonymous
{
    public class GaussZeidel : ISolver
    {
        public Vector Solve(Slae<DenseMatrix> SLAE, Vector Initial, int maxiter, double eps)
        {
            int iterNum, vec_index;
            double residual, diff, ML, MU;

            Vector result = new Vector(Initial);
            residual = SLAE.RightPart.Differ(SLAE.Matrix.Multiply(result)).Norm() / SLAE.RightPart.Norm();

            for (iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
            {
                for (vec_index = 0; vec_index < result.size; vec_index++)
                {
                    ML = SLAE.Matrix.MultiplyL(vec_index, result);
                    MU = SLAE.Matrix.MultiplyU(vec_index, result);
                    diff = SLAE.RightPart.values[vec_index] - ML - MU;
                    result.values[vec_index] += diff / SLAE.Matrix.get_di(vec_index);
                }
                residual = SLAE.RightPart.Differ(SLAE.Matrix.Multiply(result)).Norm() / SLAE.RightPart.Norm();
                if (!InputOutput.OutputIterationToForm(iterNum, residual, maxiter, false))
                    MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
            }
            if (!InputOutput.OutputIterationToForm(iterNum - 1, residual, maxiter, true))
                MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
            return result;
        }

        public Vector Solve(Slae<ProfileMatrix> SLAE, Vector Initial, int maxiter, double eps)
        {
            int iterNum, vec_index;
            double residual, diff, ML, MU;

            Vector result = new Vector(Initial);

            residual = SLAE.RightPart.Differ(SLAE.Matrix.Multiply(result)).Norm() / SLAE.RightPart.Norm();

            for (iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
            {
                for (vec_index = 0; vec_index < result.size; vec_index++)
                {
                    ML = SLAE.Matrix.MultiplyL(vec_index, result);
                    MU = SLAE.Matrix.MultiplyU(vec_index, result);
                    diff = SLAE.RightPart.values[vec_index] - ML - MU;
                    result.values[vec_index] += diff / SLAE.Matrix.get_di(vec_index);
                }
                residual = SLAE.RightPart.Differ(SLAE.Matrix.Multiply(result)).Norm() / SLAE.RightPart.Norm();
                if (!InputOutput.OutputIterationToForm(iterNum, residual, maxiter, false))
                    MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
            }
            if (!InputOutput.OutputIterationToForm(iterNum - 1, residual, maxiter, true))
                MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
            return result;
        }

        public Vector Solve(Slae<DiagonalMatrix> SLAE, Vector Initial, int maxiter, double eps)
        {
            int iterNum, vec_index;
            double residual, diff, ML, MU;

            Vector result = new Vector(Initial);
            residual = SLAE.RightPart.Differ(SLAE.Matrix.Multiply(result)).Norm() / SLAE.RightPart.Norm();

            for (iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
            {
                for (vec_index = 0; vec_index < result.size; vec_index++)
                {
                    ML = SLAE.Matrix.MultiplyL(vec_index, result);
                    MU = SLAE.Matrix.MultiplyU(vec_index, result);
                    diff = SLAE.RightPart.values[vec_index] - ML - MU;
                    result.values[vec_index] += diff / SLAE.Matrix.get_di(vec_index);
                }
                residual = SLAE.RightPart.Differ(SLAE.Matrix.Multiply(result)).Norm() / SLAE.RightPart.Norm();
                if (!InputOutput.OutputIterationToForm(iterNum, residual, maxiter, false))
                    MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
            }
            if (!InputOutput.OutputIterationToForm(iterNum - 1, residual, maxiter, true))
                MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
            return result;
        }

        public Vector Solve(Slae<DisperseMatrix> SLAE, Vector Initial, int maxiter, double eps)
        {
            int iterNum, vec_index;
            double residual, diff, ML, MU;

            Vector result = new Vector(Initial);
            residual = SLAE.RightPart.Differ(SLAE.Matrix.Multiply(result)).Norm() / SLAE.RightPart.Norm();

            for (iterNum = 0; iterNum < maxiter && residual >= eps; iterNum++)
            {
                for (vec_index = 0; vec_index < result.size; vec_index++)
                {
                    ML = SLAE.Matrix.MultiplyL(vec_index, result);
                    MU = SLAE.Matrix.MultiplyU(vec_index, result);
                    diff = SLAE.RightPart.values[vec_index] - ML - MU;
                    result.values[vec_index] += diff / SLAE.Matrix.get_di(vec_index);
                }
                residual = SLAE.RightPart.Differ(SLAE.Matrix.Multiply(result)).Norm() / SLAE.RightPart.Norm();
                if (!InputOutput.OutputIterationToForm(iterNum, residual, maxiter, false))
                    MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
            }
            if (!InputOutput.OutputIterationToForm(iterNum - 1, residual, maxiter, true))
                MessageBox.Show("Ошибка при выводе данных на форму.", "Опаньки...", MessageBoxButtons.OK);
            return result;
        }
    }
}

