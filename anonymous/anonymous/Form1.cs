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

<<<<<<< HEAD
            comboBox1.Items.AddRange(new string[] { "Плотный", "Профильный", "Ленточный", "Диагональный", "Разреженный" });
            comboBox1.SelectedItem = comboBox1.Items[0];
            IMatrix A = new ProfileMatrix();
 
=======
            string[] formats = { "Плотный", "Профильный", "Ленточный", "Диагональный", "Разреженный" };

            comboBox1.Items.AddRange(formats);
            comboBox1.SelectedItem = comboBox1.Items[0];           
>>>>>>> a0f84a8a96df7b911bdcc0ec4aa8bb9e7e7a636f
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            InputOutput.formattype = comboBox1.SelectedIndex;
<<<<<<< HEAD
            IMatrix A = new ProfileMatrix();
           
=======
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
            //Запуск решения
>>>>>>> a0f84a8a96df7b911bdcc0ec4aa8bb9e7e7a636f
        }
    }

   /* interface IMatrix
    {
        IVector Multiply(IVector x); //перемножение матрицы на вектор
        IVector TMultiply(IVector x);//перемножает транспонированную матрицу на вектор
                                     //   IMatrix SumMatr();
        void test();
    }*/

   /* interface IVector
    {
        double Norm(Vector x);
        // double Scalar();
        // double SumVec(IVector x, IVector y);
        //IVector aMultVec(double a,IVector x);

    }*/

  /*  interface ISLAE
    {
        IMatrix Matrix { get; set; }
        IVector RightPart { get; set; }
    }*/

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
  
  /*  class Matrix : IMatrix // Реализация интерфейса IMatrix
    {

        public Matrix()//конструктор
        {
            switch (InputOutput.formattype)//formattype-тип формата хранения матрицы 
            {
                case 0://плотный
                    break;
                case 1://профильный

                    double[] di, au, al;
                    int[] ia;
                    int n;//размерность

                    break;
                case 2://ленточный
                    break;
                case 3://диагональный
                    break;
                case 4://разреженный строчно-столбцовый
                    break;

            }
        }
        public IVector Multiply(IVector x)
        {
            IVector res;
            switch (InputOutput.formattype)//formattype-тип формата хранения матрицы 
            {
                case 0://плотный
                    break;
                case 1://профильный


                    for (int i = 0; i < n; i++)
                        res[i] = di[i] * x[i];

                    for (int i = 0; i < n; i++)
                        for (int j = ia[i], k = i - (ia[i + 1] - ia[i]); j < ia[i + 1]; j++, k++)
                        {
                            res[i] += al[j] * x[k];
                            res[k] += au[j] * x[i];
                        }

                    break;

                case 2://ленточный
                    break;
                case 3://диагональный
                    break;
                case 4://разреженный строчно-столбцовый
                    break;


            }
            throw new NotImplementedException();
            return res;
        }

        public IVector TMultiply(IVector x)
        {
            switch (InputOutput.formattype)//formattype-тип формата хранения матрицы 
            {
                case 0://плотный
                    break;
                case 1://профильный

                    for (int i = 0; i < n; i++)
                        res[i] = di[i] * b[i];

                    for (int i = 0; i < n; i++)
                        for (int j = ia[i], k = i - (ia[i + 1] - ia[i]); j < ia[i + 1]; j++, k++)
                        {
                            res[i] += au[j] * b[k];
                            res[k] += al[j] * b[i];
                        }

                    break;
                case 2://ленточный
                    break;
                case 3://диагональный
                    break;
                case 4://разреженный строчно-столбцовый
                    break;

            }
            throw new NotImplementedException();
        }

        public void test()
        {
            MessageBox.Show(InputOutput.formattype.ToString());
        }


    }

    class Vector : IVector // Реализация интерфейса IVector
    {

        public Vector()
        {
            double[] vec;
        }
        public double Norm(Vector x)
        {
            double norm = 0;
            for (int i = 0; i < n; i++)
                norm += x[i] * x[i];
            norm = Math.Sqrt(norm);
            return norm;
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


    }*/

  /*  class SLAE : ISLAE // Реализация интерфейса ISLAE
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
    }*/

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
