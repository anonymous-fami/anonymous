using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace anonymous
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            int type;

            InitializeComponent();

            /////////////////////////
            IMatrix A = new Matrix();
            A.test();
            //////////////////////// 

            /*
            A.Input();
            A.LU();
            A.Solve();
            A.Output();
            */             
        }
    }

    interface IMatrix
    {
        IVector Multiply(IVector x);
        IVector TMultiply(IVector x);
        void test();
    }

    interface IVector
    {
        /*
        static double operator *(IVector v1, IVector v2);
        static IVector operator +(IVector v1, IVector v2);
        static IVector operator *(IVector v1, double v2);
        static IVector operator *(double v1, IVector v2);
        */
        double Norm();
    }
    public static class InputOutput
    {
        //Ввод плотной матрицы
        public static void InputMatrix(int Flag, string FileName,out double[,] Plot) { Plot = new double[10,10]; }
        //Ввод матрицы в профильном формате
        public static void InputMatrix(int Flag, string FileName, out int[] ia, out double[] gg, out double[] diag)
        {
            gg = new double[10];
            diag = new double[10];
            ia = new int[10];
        }
        //Ввод матрицы в разреженном формате
        public static void InputMatrix(int Flag, string FileName, out int[] ia, out int[] ja, out double[] gg, out double[] diag)
        {
            gg = new double[10];
            diag = new double[10];
            ia = new int[10];
            ja = new int[10];
        }
        //Ввод матрицы в диагональном формате
        //void InputMatrix(int Flag, string FileName, double[] gg, double[] diag);
        //Ввод матрицы в ленточном формате
        public static void InputMatrix(int Flag, string FileName, out double[] gg, out double[] diag)
        {
            gg = new double[10];
            diag = new double[10];
        }

        //ввод вектора правой части
        public static void InputRightPart(string FileName, out double[] pr)
        {
            pr = new double[10];
        }
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
        IVector Solve(ISLAE slae, IVector initial, IIterationLogger logger, double eps, int maxiter);
        string Name { get; }
        IPreconditioner Preconditioner { get; set; }
    }

    class Matrix : IMatrix // Реализация интерфейса IMatrix
    {
        

        public IVector Multiply(IVector x)
        {
            return null;
        }

        public IVector TMultiply(IVector x)
        {
            return null;
        }

        public void test()
        {
            MessageBox.Show("test msg");
        }
    }

    class Vector : IVector // Реализация интерфейса IVector
    {
        public double Norm()
        {
            return 0;
        }

        public static double operator *(Vector v1, Vector v2)
        {
            return 0;
        }

        public static IVector operator +(Vector v1, Vector v2)
        {
            return null;
        }

        public static IVector operator *(Vector v1, double v2)
        {
            return null;
        }

        public static IVector operator *(double v1, Vector v2)
        {
            return null;
        }
    }

    class SLAE : ISLAE // Реализация интерфейса ISLAE
    {
        public IMatrix Matrix
        {
            get;
            set;
        }

        public IVector RightPart
        {
            get;
            set;
        }
    }

    class IterationLogger : IIterationLogger // Реализация интерфейса IIterationLogger
    {
        public int IterationNumber
        {
            get;
            set;
        }

        public IVector Residual
        {
            get;
            set;
        }

        public IVector CurrentSolution
        {
            get;
            set;
        }

        public ISolver CurrentSolver
        {
            get;
            set;
        }
    }

    /*
    class Preconditioner : IPreconditioner // Реализация интерфейса IPreconditioner
    {
        public string Name
        {
            get;
        }

        public void Create(IMatrix matrix)
        {
            
        }
    }
    */

    /*
    class Solver : ISolver // Реализация интерфейса ISolver
    {
        public IVector Solve(ISLAE slae, IVector initial, IIterationLogger logger, double eps, int maxiter)
        {
            return null;
        }

        public string Name
        {
            get;
        }

        public IPreconditioner Preconditioner
        {
            set;
        }
    }
    */
}
