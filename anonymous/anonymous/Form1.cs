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
            string[] preconditioner = { "Нет предобуславливателя", "Диагональный", "Разложение Холесского", "LU", "LUsq" };
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

            OFD.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            OFD.RestoreDirectory = true;

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                initial_textBox.Text = OFD.FileName;   //Путь файла с начальным приближением

                Data.initialPath = initial_textBox.Text;
            }
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();

            SFD.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            SFD.RestoreDirectory = true;

            if (Data.result != null)
            {
                if (SFD.ShowDialog() == DialogResult.OK) InputOutput.OutputVector(SFD.FileName, Data.result);
            }
            else
            {
                MessageBox.Show("Кажется что-то пошло не так.\nВ векторе ответа нет данных. :(", "Опаньки...", MessageBoxButtons.OK);
            }
        }

        private void solve_button_Click(object sender, EventArgs e)
        {
            //Запуск решения
                   
            if (checkinput())
            {
                richTextBox1.Clear();
                Data.richtextbox = richTextBox1;

                Vector Initial;
                ISolver solver;
                double eps;

                eps = 1.0;

                for (int i = 0; i < eps_numericUpDown.Value; i++)
                {
                    eps /= 10.0;
                }

                switch (Data.matrixformat)
                {
                    case 0: //Плотная
                        {
                            Slae<DenseMatrix> SLAE = new Slae<DenseMatrix>();
                            SLAE.Matrix = new DenseMatrix(Data.matrixPath);
                            SLAE.RightPart = new Vector(Data.rightpartPath);
        
                            if (SLAE.Matrix.getMatrix().N == 0) break;

                            if (initial_checkBox.Checked) Initial = new Vector(SLAE.Matrix.getMatrix().N);
                            else Initial = new Vector(Data.initialPath);

                            switch (Data.preconditioner)
                            {
                                case 0: //Нет предобуславливателя
                                    {
                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    solver = new MSG();
                                                    Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                        }
                                            case 1: //ЛОС
                                                {
                                                    solver = new LOS();
                                                    Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case 1: //Диагональный
                                    {
                                        break;
                                    }
                                case 2: //LLT
                                    {
                                        break;
                                    }
                                case 3: //LU
                                    {
                                        break;
                                    }
                                case 4: //LUsq
                                    {
                                        break;
                                    }
                            }
                            break;
                        }
                    case 1: //Профильная
                        {
                            Slae<ProfileMatrix> SLAE = new Slae<ProfileMatrix>();
                            SLAE.Matrix = new ProfileMatrix(Data.matrixPath);
                            SLAE.RightPart = new Vector(Data.rightpartPath);

                            if (SLAE.Matrix.getMatrix().N == 0) break;

                            if (initial_checkBox.Checked) Initial = new Vector(SLAE.Matrix.getMatrix().N);
                            else Initial = new Vector(Data.initialPath);                            

                            switch (Data.preconditioner)
                            {
                                case 0: //Нет предобуславливателя
                                    {
                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    solver = new MSG();
                                                    Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                            case 1: //ЛОС
                                                {
                                                    solver = new LOS();
                                                    Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case 1: //Диагональный
                                    {                                        
                                        IPreconditioner<ProfileMatrix> preconditioner = new ProfilePreconditioner();
                                        preconditioner.createDiag(SLAE);
                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    solver = new MSG();
                                                    Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                            case 1: //ЛОС
                                                {
                                                    //solver = new LOS();
                                                    //Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case 2: //LLT
                                    {
                                        IPreconditioner<ProfileMatrix> preconditioner = new ProfilePreconditioner();
                                        preconditioner.createLLT(SLAE);
                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    solver = new MSG();
                                                    Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                            case 1: //ЛОС
                                                {
                                                    //solver = new LOS();
                                                    //Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case 3: //LU
                                    {
                                        IPreconditioner<ProfileMatrix> preconditioner = new ProfilePreconditioner();
                                        preconditioner.createLU(SLAE);
                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    //solver = new MSG();
                                                    //Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                            case 1: //ЛОС
                                                {
                                                    solver = new LOS();
                                                    Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case 4: //LUsq
                                    {
                                        IPreconditioner<ProfileMatrix> preconditioner = new ProfilePreconditioner();
                                        preconditioner.createLUsq(SLAE);
                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    //solver = new MSG();
                                                    //Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                            case 1: //ЛОС
                                                {
                                                    solver = new LOS();
                                                    Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                            }
                            break;
                        }
                    case 2: //Ленточная
                        {
                            break;
                        }
                    case 3: //Диагональная
                        {
                            Slae<DiagonalMatrix> SLAE = new Slae<DiagonalMatrix>();
                            SLAE.Matrix = new DiagonalMatrix(Data.matrixPath);
                            SLAE.RightPart = new Vector(Data.rightpartPath);

                            if (SLAE.Matrix.getMatrix().N == 0) break;

                            if (initial_checkBox.Checked) Initial = new Vector(SLAE.Matrix.getMatrix().N);
                            else Initial = new Vector(Data.initialPath);

                            switch (Data.preconditioner)
                            {
                                case 0: //Нет предобуславливателя
                                    {
                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    solver = new MSG();
                                                    Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                            case 1: //ЛОС
                                                {
                                                    solver = new LOS();
                                                    Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case 1: //Диагональный
                                    {
                                        break;
                                    }
                                case 2: //LLT
                                    {
                                        break;
                                    }
                                case 3: //LU
                                    {
                                        break;
                                    }
                                case 4: //LUsq
                                    {
                                        break;
                                    }
                            }
                            break;
                        }
                    case 4: //Разреженая
                        {
                            Slae<DisperseMatrix> SLAE = new Slae<DisperseMatrix>();
                            SLAE.Matrix = new DisperseMatrix(Data.matrixPath);
                            SLAE.RightPart = new Vector(Data.rightpartPath);

                            if (SLAE.Matrix.getMatrix().N == 0) break;

                            if (initial_checkBox.Checked) Initial = new Vector(SLAE.Matrix.getMatrix().N);
                            else Initial = new Vector(Data.initialPath);

                            switch (Data.preconditioner)
                            {
                                case 0: //Нет предобуславливателя
                                    {
                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    solver = new MSG();
                                                    Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                            case 1: //ЛОС
                                                {
                                                    solver = new LOS();
                                                    Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case 1: //Диагональный
                                    {
                                        break;
                                    }
                                case 2: //LLT
                                    {
                                        break;
                                    }
                                case 3: //LU
                                    {
                                        break;
                                    }
                                case 4: //LUsq
                                    {
                                        break;
                                    }
                            }
                            break;
                        }
                }

                //tabControl1.SelectedIndex = 2; <-- Закомменчено пока не готова вкладка "Вывод"
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

            //Проверка работоспособности прямого и обратого хода.
            /*
                      { 0, 4, 5 }
            MatrixA = { 0, 4, 5 }
                      { 1, 20, 70 }
            */

            /*Профильная матрица*/

            /*
            //LLT-разложение
            Data.preconditioner = 2;
            int n = 3;
            int[] ia = { 0, 0, 1, 3 };
            int[] ja = { 0, 1 };
            double[] al = { 0, 4, 5 };
            double[] au = { 0, 4, 5 };
            double[] di = { 1, 20, 70 };
                                
            IMatrix<ProfileMatrix> MatrixA = new ProfileMatrix(au, al, di, ia, n);
            IMatrix<ProfileMatrix> PMatrixA;
            IPreconditioner<ProfileMatrix> preconditioner = new ProfilePreconditioner();

            preconditioner.createLLT(MatrixA, out PMatrixA);
            Vector f = new Vector(3, new double[3] { 13, 55, 224 });//x={1,2,3}
            Vector tempX = PMatrixA.getMatrix().DirectProgress(f);
            Vector x = PMatrixA.getMatrix().ReverseProgress(tempX);
            */

            /*
            //LU-разложение
            Data.preconditioner = 3;
            int n = 3;
            int[] ia = { 0, 0, 1, 3 };
            int[] ja = { 0, 1 };
            double[] al = { 0, 4, 5 };
            double[] au = { 0, 4, 5 };
            double[] di = { 1, 20, 70 };
                                
            IMatrix<ProfileMatrix> MatrixA = new ProfileMatrix(au, al, di, ia, n);
            IMatrix<ProfileMatrix> PMatrixA;
            IPreconditioner<ProfileMatrix> preconditioner = new ProfilePreconditioner();

            preconditioner.createLU(MatrixA, out PMatrixA);
            Vector f = new Vector(3, new double[3] { 13, 55, 224 });//x={1,2,3}
            Vector tempX = PMatrixA.getMatrix().DirectProgress(f);
            Vector x = PMatrixA.getMatrix().ReverseProgress(tempX);
            */

            /*
            //LUsq-разложение
            Data.preconditioner = 4;
            int n = 3;
            int[] ia = { 0, 0, 1, 3 };
            int[] ja = { 0, 1 };
            double[] al = { 0, 4, 5 };
            double[] au = { 0, 4, 5 };
            double[] di = { 1, 20, 70 };
                                
            IMatrix<ProfileMatrix> MatrixA = new ProfileMatrix(au, al, di, ia, n);
            IMatrix<ProfileMatrix> PMatrixA;
            IPreconditioner<ProfileMatrix> preconditioner = new ProfilePreconditioner();

            preconditioner.createLUsq(MatrixA, out PMatrixA);
            Vector f = new Vector(3, new double[3] { 13, 55, 224 });//x={1,2,3}
            Vector tempX = PMatrixA.getMatrix().DirectProgress(f);
            Vector x = PMatrixA.getMatrix().ReverseProgress(tempX);
            */

            /*Разреженная матрица*/

            /*
            //LU-разложение
            Data.preconditioner = 3;
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

            /*Плотная матрица*/

            /*
            //LU-разложение
            Data.preconditioner = 3;
            int n = 3;
            //double[,] Plot = { { 1, 4, 5 }, { 4, 20, 32 }, { 5, 32, 70 } };
            double[,] Plot = { { 1, 0, 4 }, { 0, 20, 5 }, { 4, 5, 70 } };

            Slae<DenseMatrix> Slae = new Slae<DenseMatrix>();
            Slae.Matrix = new DenseMatrix(Plot, n);

            IPreconditioner<DenseMatrix> preconditioner = new DensePreconditioner();
            preconditioner.createLU(Slae);

            //Vector f = new Vector(n, new double[3] { 24, 140, 279 });//x={1,2,3}
            Vector f = new Vector(n, new double[3] { 13, 55, 224 });//x={1,2,3}
            Vector tempX = Slae.PMatrix.getMatrix().DirectProgress(f);
            Vector x = Slae.PMatrix.getMatrix().ReverseProgress(tempX);
            */
        }

        private bool checkinput()
        {
            if (Data.matrixPath == null)
            {
                MessageBox.Show("Кажется вы забыли ввести данные.\nЗадайте файл с матрицей на вкладке \"Матрица\".", "Опаньки...", MessageBoxButtons.OK);
                tabControl1.SelectedIndex = 0;
                return false;
            }

            if ((Data.solver == 0) || (Data.solver == 1))
            {
                if (Data.rightpartPath == null)
                {
                    MessageBox.Show("Кажется вы забыли ввести данные.\nЗадайте файл с вектором правой части на вкладке \"Матрица\".", "Опаньки...", MessageBoxButtons.OK);
                    tabControl1.SelectedIndex = 0;
                    return false;
                }

                if ((Data.initialPath == null) && (initial_checkBox.Checked == false))
                {
                    MessageBox.Show("Кажется вы забыли ввести данные.\nЗадайте файл с начальным приближением, или отметьте пункт \"Нулевое начальное приближение\" на вкладке \"Решатель\".", "Опаньки...", MessageBoxButtons.OK);
                    tabControl1.SelectedIndex = 1;
                    return false;
                }
            }
            return true;
        }

        private void Form1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            HelpForm Help = new HelpForm();
            Help.Show();
        }
    }

    public static class Data
    {
        public static int matrixformat;         //Выбранный формат матрицы
        public static string matrixPath;        //Путь файла с матрицей
        public static string rightpartPath;     //Путь файла с вектором правой части
        public static string initialPath;       //Путь файла с начальным приближением
        public static int preconditioner;       //Выбранный предобуславливатель
        public static int solver;               //Выбранный решатель
        public static Vector result;            //Вектор с ответом
        public static RichTextBox richtextbox;  
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
