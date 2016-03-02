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

            textBox1.ReadOnly = true;
            textBox1.ReadOnly = true;
            textBox3.ReadOnly = true;

            //Формат матрицы
            string[] matrixformats = { "Плотный", "Профильный", "Ленточный", "Диагональный", "Разреженный" };

            comboBox1.Items.AddRange(matrixformats);
            comboBox1.SelectedItem = comboBox1.Items[0];

            //Предобуславливатель
            string[] preconditioner = { "Нет предобуславливателя", "Диагональный", "Разложение Холесского", "LU", "LU(sq)" };
            comboBox2.Items.AddRange(preconditioner);
            comboBox2.SelectedItem = comboBox2.Items[0];

            //Решатель
            string[] solver = { "МСГ", "ЛОС" ,"$$$"};
            comboBox3.Items.AddRange(solver);
            comboBox3.SelectedItem = comboBox3.Items[0];
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.matrixformat = comboBox1.SelectedIndex;   //Формат матрицы           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.preconditioner = comboBox2.SelectedIndex;  //Предобуславливатель
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBox3.SelectedIndex == 0) || (comboBox3.SelectedIndex == 1))
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }

            Data.solver = comboBox3.SelectedIndex;  //Решатель
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();

            //OFD.InitialDirectory = "c:\\";
            OFD.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            OFD.RestoreDirectory = true;

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = OFD.FileName;   //Путь файла с матрицей

                Data.matrixPath = textBox1.Text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();

            //OFD.InitialDirectory = "c:\\";
            OFD.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            OFD.RestoreDirectory = true;

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = OFD.FileName;   //Путь файла правой части

                Data.vectorPath = textBox2.Text;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();

            //OFD.InitialDirectory = "c:\\";
            OFD.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            OFD.RestoreDirectory = true;

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = OFD.FileName;   //Путь файла с начальным приближением

                Data.initialPath = textBox3.Text;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Запуск решения
            /*
            switch (Data.matrixformat)
            {
                case 0:
                    {
                        //Плотная
                        switch(Data.preconditioner)
                        {
                            case 0:
                                {
                                    //Diag
                                    switch (Data.solver)
                                    {
                                        case 0:
                                            {
                                                //МСГ 
                                                break;
                                            }
                                        case 1:
                                            {
                                                //ЛОС
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    //LU
                                    switch (Data.solver)
                                    {
                                        case 0:
                                            {
                                                //МСГ 
                                                break;
                                            }
                                        case 1:
                                            {
                                                //ЛОС
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    //LLT
                                    switch (Data.solver)
                                    {
                                        case 0:
                                            {
                                                //МСГ 
                                                break;
                                            }
                                        case 1:
                                            {
                                                //ЛОС
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    //LUsq
                                    switch (Data.solver)
                                    {
                                        case 0:
                                            {
                                                //МСГ 
                                                break;
                                            }
                                        case 1:
                                            {
                                                //ЛОС
                                                break;
                                            }
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case 1:
                    {
                        //Профильная
                        IMatrix<ProfileMatrix> Matrix = new ProfileMatrix(Data.matrixPath);
                        switch (Data.preconditioner)
                        {
                            case 0:
                                {
                                    //Diag
                                    switch (Data.solver)
                                    {
                                        case 0:
                                            {
                                                //МСГ
                                                ProfileMatrix A1 = new ProfileMatrix(Matrix.getMatrix());
                                                break;
                                            }
                                        case 1:
                                            {
                                                //ЛОС
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    //LU
                                    switch (Data.solver)
                                    {
                                        case 0:
                                            {
                                                //МСГ 
                                                break;
                                            }
                                        case 1:
                                            {
                                                //ЛОС                                                
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    //LLT
                                    switch (Data.solver)
                                    {
                                        case 0:
                                            {
                                                //МСГ 
                                                break;
                                            }
                                        case 1:
                                            {
                                                //ЛОС
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    //LUsq
                                    switch (Data.solver)
                                    {
                                        case 0:
                                            {
                                                //МСГ 
                                                break;
                                            }
                                        case 1:
                                            {
                                                //ЛОС
                                                break;
                                            }
                                    }
                                    break;
                                }
                        }
                        break;
                    }
            }
            */

            /*
            IMatrix<ProfileMatrix> Matrix = new ProfileMatrix(Data.matrixPath);
            ProfileMatrix MatrixCopy = new ProfileMatrix(Matrix.getMatrix());

            MatrixCopy.AU[0] = 888;
            MatrixCopy.N = 888;

            int a = 0;
            */

            //Проверка работоспособности предобуславливателя.



            //double[] al = { 4, 5, 47 };
            //double[] au = { 7, 8, 50 };
            //double[] di = { 1, 32, 103 };
            //int[] ia = { 0, 0, 1, 3 };
            //int n = 3;
            double[] al = { 4, 5, 32 };
            double[] au = { 4, 5, 32 };
            double[] di = { 1, 20, 70 };
            int[] ia = { 0, 0, 1, 3 };
            int n = 3;

            IMatrix<ProfileMatrix> A = new ProfileMatrix(au, al, di, ia, n);
            IMatrix<ProfileMatrix> B;
            IPreconditioner<ProfileMatrix> P = new ProfilePreconditioner();
            
            P.createDiag(A, out B);
            P.createLLT(A, out B);
            //P.createLU(A, out B);

        }        
    }

    public static class Data
    {
        public static int matrixformat;     //Выбранный формат матрицы
        public static string matrixPath;    //Путь файла с матрицей
        public static string vectorPath;    //Путь файла с вектором
        public static string initialPath;   //Путь файла с начальным приближением
        public static int preconditioner;   //Выбранный предобуславливатель
        public static int solver;           //Выбранный решатель
    }
   
    interface IVector
    {
        double Norm();
        Vector Sum(Vector B);
        Vector Mult(double A);
        Vector Scalar(Vector A);
        // double Scalar();
        // double SumVec(IVector x, IVector y);
        //IVector aMultVec(double a,IVector x);

    }
    

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

   
    interface ISolver
    {
        IVector Solve(ISLAE<Type> slae, IVector initial, IIterationLogger logger, double eps, int maxiter);
        string Name { get; }
        //IPreconditioner Preconditioner { get; set; }
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
