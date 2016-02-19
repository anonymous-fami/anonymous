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
            InitializeComponent();

            comboBox1.Items.AddRange(new string[] {"Плотный", "Профильный", "Ленточный", "Диагональный", "Разреженный"});
            comboBox1.SelectedItem = comboBox1.Items[0];

            /////////////////////////
            //IMatrix A = new Matrix();
            //A.test();
            //////////////////////// 

            /*
            A.Input();
            A.LU();
            A.Solve();
            A.Output();
            */             
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            InputOutput.formattype = comboBox1.SelectedIndex;

            IMatrix A = new Matrix();
            A.test();
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

    interface IPreconditioner //: IMatrix  
        //если интерфейс наследуется от iMatrix, то класс, реализующий наследника должен реализовывать интерфейс класса родитиля..
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
            throw new NotImplementedException();
        }

        public IVector TMultiply(IVector x)
        {
            throw new NotImplementedException();
        }

        public void test()
        {
            MessageBox.Show(InputOutput.formattype.ToString());
        }
    }

    class Vector : IVector // Реализация интерфейса IVector
    {
        public double Norm()
        {
            throw new NotImplementedException();
        }

        public static double operator *(Vector v1, Vector v2)
        {
            throw new NotImplementedException();
        }

        public static IVector operator +(Vector v1, Vector v2)
        {
            throw new NotImplementedException();
        }

        public static IVector operator *(Vector v1, double v2)
        {
            throw new NotImplementedException();
        }

        public static IVector operator *(double v1, Vector v2)
        {
            throw new NotImplementedException();
        }
    }

    class SLAE : ISLAE // Реализация интерфейса ISLAE
    {
        public IMatrix Matrix
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IVector RightPart
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }

    class IterationLogger : IIterationLogger // Реализация интерфейса IIterationLogger
    {
        public int IterationNumber
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IVector Residual
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IVector CurrentSolution
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public ISolver CurrentSolver
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }


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
