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

            //Формат матрицы
            string[] matrixformats = { "Плотный", "Профильный", "Ленточный", "Диагональный", "Разреженный" };

            comboBox1.Items.AddRange(matrixformats);
            comboBox1.SelectedItem = comboBox1.Items[0];

            //Предобуславливатель
            string[] preconditioner = { "Нет предобуславливателя", "Диагональный", "Разложение Холесского", "LU", "LU(sq)"};
            comboBox2.Items.AddRange(preconditioner);
            comboBox2.SelectedItem = comboBox2.Items[0];

            //Решатель
            string[] solver = { "МСГ" };
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
            Data.solver = comboBox3.SelectedIndex;  //Решатель
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();

            OFD.InitialDirectory = "c:\\";
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

            OFD.InitialDirectory = "c:\\";
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
            //Запуск решения

            /*
            double[] au;
            double[] al;
            double[] di;
            int[] ia;
            int n;
            IMatrix<ProfileMatrix> A = new ProfileMatrix(out au, out al, out di,out ia,out n);
            Vector V = new Vector();  
            */

            //Проверка работоспособности предобуславливателя.

            double[] al = { 4, 5, 47};
            double[] au = { 7, 8, 50};
            double[] di = { 1, 32, 103 };
            int[] ia= { 1, 1, 2, 4};
            int n = 3;
            ProfileMatrix A = new ProfileMatrix(au, al, di, ia, n);
            IPreconditioner<ProfileMatrix> P = new ProfilePreconditioner("Профильный");
            
            ProfileMatrix out_res, l_res, u_res;
            P.createDiag(A, out out_res);
            P.createLUsq(A, out l_res, out u_res);
        }        
    }

    public static class Data
    {
        public static int matrixformat;     //Выбранный формат матрицы
        public static string matrixPath;    //Путь файла с матрицей
        public static string vectorPath;    //Путь файла с вектором
        public static int preconditioner;   //Выбранный предобуславливатель
        public static int solver;           //Выбранный решатель
    }
   
    interface IVector
    {
        double Norm(Vector x);
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

    //убрано! по всем вопросам к Тонхоноеву А.А. (amashtay)
    /*
    interface IPreconditioner : IMatrix <ProfileMatrix>
    {
        string Name { get; }
        void Create(IMatrix<ProfileMatrix> matrix);
    }
    */
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
