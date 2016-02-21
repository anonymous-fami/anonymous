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

            string[] formats = { "Плотный", "Профильный", "Ленточный", "Диагональный", "Разреженный" };

            comboBox1.Items.AddRange(formats);
            comboBox1.SelectedItem = comboBox1.Items[0];

            //для предобуславливателя
            string[] formats_preconditioner = { "Диагональный", "Разложение Холесского" };
            comboBox3.Items.AddRange(formats_preconditioner);
            comboBox3.SelectedItem = comboBox3.Items[0];
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            InputOutput.formattype = comboBox1.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();

            OFD.InitialDirectory = "c:\\";
            OFD.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            OFD.RestoreDirectory = true;

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = OFD.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();

            OFD.InitialDirectory = "c:\\";
            OFD.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            OFD.RestoreDirectory = true;

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = OFD.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            preconditioner_profil pp = new preconditioner_profil(comboBox3.Text);

            //зададим какую-нибудь плотную матрицу
            int razm = 9;
            //
            int[] ia = { 1, 1, 1, 2, 4, 6, 9, 12, 15, 19 };
            //
            double[] di = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            //
            double[] al = { 1, 1, 0, 1, 1, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1, 0, 1, 0 };

            int size_al = ia[razm] - 1;


            //
            ProfileMatrix m = new ProfileMatrix(al, al, di, ia, size_al, razm, razm + 1);
            ProfileMatrix new_m;
            //
           // preconditioner_profil pr = new preconditioner_profil("Диагональный");
            pp.Create(m, out new_m);
        }
    }

    //interface IMatrix
    //{
    //    IVector Multiply(IVector x);
    //    IVector TMultiply(IVector x);
    //    void test();
    //}

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



    interface ISolver
    {
        IVector Solve(ISLAE slae, IVector initial, IIterationLogger logger, double eps, int maxiter);
        string Name { get; }
      //  IPreconditioner Preconditioner { get; set; }  временно убрано, по всем вопросам к Тонхоноеву А.А. (amashtay)
    }

    //class Matrix : IMatrix // Реализация интерфейса IMatrix
    //{      
    //    public IVector Multiply(IVector x)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IVector TMultiply(IVector x)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void test()
    //    {
    //        MessageBox.Show(InputOutput.formattype.ToString());
    //    }
    //}

    //class Vector : IVector // Реализация интерфейса IVector
    //{
    //    public double Norm()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public static double operator *(Vector v1, Vector v2)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public static IVector operator +(Vector v1, Vector v2)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public static IVector operator *(Vector v1, double v2)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public static IVector operator *(double v1, Vector v2)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

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
