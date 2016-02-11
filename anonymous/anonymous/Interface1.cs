using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    interface Interface1
    {
        interface IMatrix
        {
            bool InitializeFrom(IMatrix matr);

            public delegate void ProcessElement(int i, int j, double d);
            void Run(ProcessElement fun);//Пробежаться по всем ненулевым элементам матрицы

            //IVector Multiply(IVector x);
            //IVector TMultiply(IVector x);

            //IVector LMult(IVector x,bool UseDiagonal);//L*x , Если false, то диагональ нулевая
            //IVector UMult(IVector x, bool UseDiagonal);//U*x, Если false, то диагональ нулевая

            //IVector LSolve(IVector x, bool UseDiagonal);//L-1*x, Если false, то диагональ единички
            //IVector USolve(IVector x, bool UseDiagonal);//U-1*x, Если false, то диагональ единички

            //IVector LtSolve(IVector x, bool UseDiagonal);//Lt-1*x Если false, то диагональ единички
            //IVector UtSolve(IVector x, bool UseDiagonal);//Ut-1*x Если false, то диагональ единички
            IVector Diagonal { get; }
            int Size { get; }
        }
        public static class MatrixAssistant
        {
            public IMatrix CreateMatrix(string name, object[] parameters);
            IVector Multiply(this IMatrix matr, IVector x)
            {
                IVector res = x.Clone() as IVector;
                res.Nullify();
                matr.Run((int i, int j, double d) => { res[i] += x[j] * d; });
                return res;
            }
            IVector TMultiply(this IMatrix matr, IVector x)
            {
                IVector res = x.Clone() as IVector;
                res.Nullify();
                matr.Run((int i, int j, double d) => { res[j] += x[i] * d; });
                return res;
            }
        }
        class Vector : IVector
        {
            double[] v;

            public Vector(double[] vec)
            {
                v = vec;
            }
            public double this[int i]
            {
                get
                {
                    return v[i];
                }
                set
                {
                    v[i] = value;
                }
            }

            public double Norm
            {
                get
                {
                    double N = 0;
                    for (int i = 0; i < v.Length; i++)
                    {
                        N += v[i] * v[i];
                    }
                    return Math.Sqrt(N);
                }
            }

            public int Size
            {
                get { return v.Length; }
            }

            public void Nullify()
            {
                for (int i = 0; i < v.Length; i++) v[i] = 0;
            }
            public object Clone()
            {
                return new Vector(v.Clone() as double[]);
            }
        }

        class RowColumnSparseMatrix : IMatrix
        {
            int[] ia;
            int[] ja;
            Vector al;
            Vector di;
            int size;
            RowColumnSparseMatrix(int sz, int[] _ia, int[] _ja,
                double[] _al, double[] _di)
            {
                size = sz;
                ia = _ia; ja = _ja; al = new Vector(_al); di = new Vector(_di);
            }

            public void Run(IMatrix.ProcessElement processor)
            {
                for (int i = 0; i < size; i++)
                {
                    processor(i, i, di[i]);
                    for (int jaddr = ia[i]; jaddr < ia[i + 1]; jaddr++)
                    {
                        processor(i, ja[jaddr], al[jaddr]);
                        processor(ja[jaddr], i, al[jaddr]);
                    }
                }
            }

            public IVector Diagonal
            {
                get { return di; }
            }

            public int Size
            {
                get { return size; }
            }
        }



        interface IVector : ICloneable
        {
            double this[int i] { get; set; }
            double Norm();
            void Nullify();
            int Size { get; }
        }

        static class VectorAssistant
        {
            static double operator *(IVector v1, IVector v2)
            {
                if (v1.Size != v2.Size) throw new ArgumentException("Bad function arguments");
                double res = 0;
                for (int i = 0; i < v1.Size; i++) res += v1[i] * v2[i];
                return res;
            }
            static IVector operator +(IVector v1, IVector v2);
            static IVector operator *(IVector v1, double v2);
            static IVector operator *(double v1, IVector v2);
        }

        interface ISLAE
        {
            IMatrix Matrix { get; set; }
            IVector RightPart { get; set; }
        }

        interface IIterationLogger
        {
            int IterationNumber { get; set; }
            IVector Residual { get; set; }
            IVector CurrentSolution { get; set; }
            ISolver CurrentSolver { get; set; }
        }

        interface IPreconditioner : IMatrix
        {
            string Name { get; }
            void Create(IMatrix matrix);
        }

        interface ISolver
        {
            IVector Solve(ISLAE slae, IVector initial,
                            IIterationLogger logger, double eps, int maxiter);
            string Name { get; }
            IPreconditioner Preconditioner { set; }
        }

    }
}
