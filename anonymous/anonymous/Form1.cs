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

            matrix_textBox.ReadOnly = true;
            rightpart_textBox.ReadOnly = true;
            initial_textBox.ReadOnly = true;

            //Формат матрицы
            string[] matrixformats = { "Плотный", "Профильный", "Ленточный", "Диагональный", "Разреженный" };

            matrix_combobox.Items.AddRange(matrixformats);
            matrix_combobox.SelectedItem = matrix_combobox.Items[0];

            //Предобуславливатель
            string[] preconditioner = { "Нет предобуславливателя", "Диагональный", "Разложение Холесского", "LU", "LU(sq)" };
            preconditioner_comboBox.Items.AddRange(preconditioner);
            preconditioner_comboBox.SelectedItem = preconditioner_comboBox.Items[0];

            //Решатель
            string[] solver = { "МСГ", "ЛОС" ,"$$$"};
            solver_comboBox.Items.AddRange(solver);
            solver_comboBox.SelectedItem = solver_comboBox.Items[0];
        }

        private void matrix_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.matrixformat = matrix_combobox.SelectedIndex;   //Формат матрицы           
        }

        private void preconditioner_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.preconditioner = preconditioner_comboBox.SelectedIndex;  //Предобуславливатель
        }

        private void solver_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((solver_comboBox.SelectedIndex == 0) || (solver_comboBox.SelectedIndex == 1))
            {
                if (!initial_checkBox.Checked) initial_button.Enabled = true;
                initial_label.Enabled = true;
                initial_textBox.Enabled = true;
                initial_checkBox.Enabled = true;              
            }
            else
            {
                initial_button.Enabled = false;
                initial_label.Enabled = false;
                initial_textBox.Enabled = false;
                initial_checkBox.Enabled = false;
            }

            Data.solver = solver_comboBox.SelectedIndex;  //Решатель
        }


        private void initial_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (initial_checkBox.Checked)
            {
                initial_button.Enabled = false;
            }
            else
            {
                initial_button.Enabled = true;
            }
        }

        private void matrix_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();

            //OFD.InitialDirectory = "c:\\";
            OFD.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            OFD.RestoreDirectory = true;

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                matrix_textBox.Text = OFD.FileName;   //Путь файла с матрицей

                Data.matrixPath = matrix_textBox.Text;
            }
        }

        private void rightpart_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();

            //OFD.InitialDirectory = "c:\\";
            OFD.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            OFD.RestoreDirectory = true;

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                rightpart_textBox.Text = OFD.FileName;   //Путь файла правой части

                Data.rightpartPath = rightpart_textBox.Text;
            }
        }

        private void initial_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();

            //OFD.InitialDirectory = "c:\\";
            OFD.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            OFD.RestoreDirectory = true;

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                initial_textBox.Text = OFD.FileName;   //Путь файла с начальным приближением

                Data.initialPath = initial_textBox.Text;
            }
        }

        private void Solvebutton_Click(object sender, EventArgs e)
        {
            //Запуск решения
            tabControl1.SelectedIndex = 2;

            Vector RightPart, Initial, Result;
            ISolver solver;
            double eps;

            if ((Data.matrixPath == null) || (Data.rightpartPath == null))
            {
                MessageBox.Show("Кажется вы забыли ввести данные. \nЗадайте файл с матрицей и вектором правой части на вкладке \"Матрица\".", "Опаньки...", MessageBoxButtons.OK);
                tabControl1.SelectedIndex = 0;
            }
            else
            {
                RightPart = new Vector(Data.rightpartPath);
                eps = 1.0;

                for (int i = 0; i < eps_numericUpDown.Value; i++)
                {
                    eps /= 10.0;
                }

            switch (Data.matrixformat)
            {
                case 0:
                    {
                        //Плотная
                            switch (Data.preconditioner)
                        {
                            case 0:
                                {
                                        //нет
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
                            case 2:
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
                            case 3:
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
                                case 4:
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

                            if (initial_checkBox.Checked)
                            {
                                Initial = new Vector(Matrix.getMatrix().N);
                            }
                            else
                            {
                                Initial = new Vector(Data.initialPath);
                            }

                        switch (Data.preconditioner)
                        {
                            case 0:
                                    {
                                        //нет
                                        switch (Data.solver)
                                        {
                                            case 0:
                                {
                                                    solver = new MSG();
                                                    Result = solver.Solve(Matrix, RightPart, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    solver = new LOS();
                                                    Result = solver.Solve(Matrix, RightPart, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case 1:
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
                                case 2:
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
                                case 3:
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
                                case 4:
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
            }

            /*
            IMatrix<ProfileMatrix> Matrix = new ProfileMatrix(Data.matrixPath);
            IMatrix<ProfileMatrix> PMatrix;

            Vector RightPart = new Vector(Data.rightpartPath);
            Vector Initial = new Vector(Data.initialPath);
            Vector Result;

            IPreconditioner<ProfileMatrix> Preconditioner = new ProfilePreconditioner();
            Preconditioner.createDiag(Matrix, out PMatrix);

            ISolver solver = new MSG();
            Result = solver.Solve(Matrix,RightPart,Initial,100,1e-16);
            */
            int a = 0; // Точка останова


            //Проверка работоспособности предобуславливателя.

            //int n = 3;
            //int[] ia = { 0, 0, 1, 3 };
            //double[] al = { 4, 5, 47 };
            //double[] au = { 7, 8, 50 };
            //double[] di = { 1, 32, 103 };

            //int n = 3;
            //int[] ia = { 0, 0, 1, 3 };
            //double[] al = { 4, 5, 32 };
            //double[] au = { 4, 5, 32 };
            //double[] di = { 1, 20, 70 };

            //int n = 3;
            //int[] ia = { 0, 0, 0, 2 };
            //double[] al = { 4, 5};
            //double[] au = { 4, 25};
            //double[] di = { 1, 1, 70 };

            //int n = 3;
            //int[] ia = { 0, 0, 0, 2 };
            //double[] al = { 4, 5 };
            //double[] au = { 4, 5 };
            //double[] di = { 1, 20, 70 };

            //IMatrix<ProfileMatrix> A = new ProfileMatrix(au, al, di, ia, n);
            //IMatrix<ProfileMatrix> B;
            //IPreconditioner<ProfileMatrix> P = new ProfilePreconditioner();

            //P.createDiag(A, out B);
            //P.createLLT(A, out B);
            //P.createLU(A, out B);

            //Проверка работоспособности прямого и обратого хода.

            /*
            Data.preconditioner = 3;
            double[] al = { 0, 4, 0.25 };
            double[] au = { 0, 4, 5 };
            double[] di = { 1, 20, 52.75 };
            int[] ja = { 0, 1 };
            int[] ia = { 0, 0, 1, 3 };
            int n = 3;
            ProfileMatrix A = new ProfileMatrix(au, al, di, ia, n);
            Vector f = new Vector(3, new double[3] { 13, 55, 224 });//x={1,2,3}
            Vector tempX = A.DirectProgress(f);
            Vector x = A.ReverseProgress(tempX);
            */

            /*
            double[] al = { 4, 0.25 };
            double[] au = { 4, 5 };
            double[] di = { 1, 20, 52.75 };
            int[] ja = { 0, 1 };
            int[] ia = { 0, 0, 0, 2 };
            int n = 3;
            DisperseMatrix A = new DisperseMatrix(au, al, di, ia, ja, n);
            Vector f = new Vector(3, new double[3] { 13, 55, 224 });//x={1,2,3}
            Vector tempX = A.DirectProgress(f);
            Vector x = A.ReverseProgress(tempX);
            */
        }        
    }

    public static class Data
    {
        public static int matrixformat;     //Выбранный формат матрицы
        public static string matrixPath;    //Путь файла с матрицей
        public static string rightpartPath; //Путь файла с вектором правой части
        public static string initialPath;   //Путь файла с начальным приближением
        public static int preconditioner;   //Выбранный предобуславливатель
        public static int solver;           //Выбранный решатель
    }
    
    /*
    interface IIterationLogger
    {
        int IterationNumber { get; set; }
        IVector Residual { get; set; }
        IVector CurrentSolution { get; set; }
        ISolver CurrentSolver { get; set; }
    }
    */

    /*
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
    */
}
