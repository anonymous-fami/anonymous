using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

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
            string[] matrixformats = { "Плотный", "Профильный", "Диагональный", "Разреженный" };
            
            matrix_combobox.Items.AddRange(matrixformats);
            matrix_combobox.SelectedItem = matrix_combobox.Items[0];

            //Предобуславливатель
            string[] preconditioner = { "Нет предобуславливателя", "Диагональный", "Разложение Холеccкого", "LU", "LUsq" };
            preconditioner_comboBox.Items.AddRange(preconditioner);
            preconditioner_comboBox.SelectedItem = preconditioner_comboBox.Items[0];

            //Решатель
            string[] solver = { "МСГ", "ЛОС", "BSG Стабилизированный", "Метод Гаусса-Зейделя"};
            solver_comboBox.Items.AddRange(solver);
            solver_comboBox.SelectedItem = solver_comboBox.Items[0];

            setupGraph();
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
            if ((solver_comboBox.SelectedIndex == 0) || (solver_comboBox.SelectedIndex == 1) || (solver_comboBox.SelectedIndex == 2) || (solver_comboBox.SelectedIndex == 3))
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
                MessageBox.Show("Кажется что-то пошло не так.\nВ векторе ответа нет данных. :(", "Опаньки...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void solve_button_Click(object sender, EventArgs e)
        {
            //Запуск решения
                   
            bool success;

            if (checkinput())
            {
                success = true;
                richTextBox1.Clear();
                Data.richtextbox = richTextBox1;

                Data.ZedGraph.GraphPane.CurveList.Clear();
                InputOutput.points.Clear();

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
        
                            if (initial_checkBox.Checked) Initial = new Vector(SLAE.Matrix.getMatrix().N);
                            else Initial = new Vector(Data.initialPath);

                            if ((SLAE.Matrix.getMatrix().N == 0) || (SLAE.RightPart.SIZE == 0) || (Initial.SIZE == 0) || (SLAE.Matrix.getMatrix().N != SLAE.RightPart.SIZE) || (SLAE.Matrix.getMatrix().N != Initial.SIZE))
                            {
                                MessageBox.Show("Размерность матрицы/вектора правой части/вектора приближения равна нулю.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                success = false;
                                break;
                            }

                            if ((SLAE.Matrix.getMatrix().N != SLAE.RightPart.SIZE) || (SLAE.Matrix.getMatrix().N != Initial.SIZE))
                            {
                                MessageBox.Show("Размерности матрицы/вектора правой части/вектора приближения не совпадают.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                success = false;
                                break;
                            }

                            switch (Data.preconditioner)
                            {
                                case 0: //Нет предобуславливателя
                                    {
                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    if (!SLAE.Matrix.CheckSymmetry())
                                                    {
                                                        MessageBox.Show("Для выбранного решателя ваша матрица должна быть симметричной.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        success = false;
                                                        break;
                                                    }
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
                                            case 2: //БСГ стаб
                                                {                                                    
                                                    solver = new BSGstab();
                                                    Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                            case 3: //Гаусс-Зейдель
                                                {
                                                    solver = new GaussZeidel();
                                                    Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case 1: //Диагональный
                                    {
                                        IPreconditioner<DenseMatrix> preconditioner = new DensePreconditioner();

                                        if (!preconditioner.createDiag(SLAE))
                                        {
                                            success = false;
                                            break;
                                        }

                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    if (!SLAE.Matrix.CheckSymmetry())
                                                    {
                                                        MessageBox.Show("Для выбранного решателя ваша матрица должна быть симметричной.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        success = false;
                                                        break;
                                                    }
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
                                case 2: //LLT
                                    {
                                        if (!SLAE.Matrix.CheckSymmetry())
                                        {
                                            MessageBox.Show("Для выбранного предобуславливателя ваша матрица должна быть симметричной.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            success = false;
                                            break;
                                        }

                                        IPreconditioner<DenseMatrix> preconditioner = new DensePreconditioner();

                                        if (!preconditioner.createLLT(SLAE))
                                        {
                                            success = false;
                                            break;
                                        }

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
                                case 3: //LU
                                    {
                                        IPreconditioner<DenseMatrix> preconditioner = new DensePreconditioner();

                                        if (!preconditioner.createLU(SLAE))
                                        {
                                            success = false;
                                            break;
                                        }

                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    if (!SLAE.Matrix.CheckSymmetry())
                                                    {
                                                        MessageBox.Show("Для выбранного решателя ваша матрица должна быть симметричной.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        success = false;
                                                        break;
                                                    }
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
                                case 4: //LUsq
                                    {
                                        IPreconditioner<DenseMatrix> preconditioner = new DensePreconditioner();

                                        if (!preconditioner.createLUsq(SLAE))
                                        {
                                            success = false;
                                            break;
                                        }

                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    if (!SLAE.Matrix.CheckSymmetry())
                                                    {
                                                        MessageBox.Show("Для выбранного решателя ваша матрица должна быть симметричной.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        success = false;
                                                        break;
                                                    }
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
                            }
                            break;
                        }
                    case 1: //Профильная
                        {
                            Slae<ProfileMatrix> SLAE = new Slae<ProfileMatrix>();
                            SLAE.Matrix = new ProfileMatrix(Data.matrixPath);
                            SLAE.RightPart = new Vector(Data.rightpartPath);

                            if (initial_checkBox.Checked) Initial = new Vector(SLAE.Matrix.getMatrix().N);
                            else Initial = new Vector(Data.initialPath);                            

                            if ((SLAE.Matrix.getMatrix().N == 0) || (SLAE.RightPart.SIZE == 0) || (Initial.SIZE == 0) || (SLAE.Matrix.getMatrix().N != SLAE.RightPart.SIZE) || (SLAE.Matrix.getMatrix().N != Initial.SIZE))
                            {
                                MessageBox.Show("Размерность матрицы/вектора правой части/вектора приближения равна нулю.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                success = false;
                                break;
                            }

                            if ((SLAE.Matrix.getMatrix().N != SLAE.RightPart.SIZE) || (SLAE.Matrix.getMatrix().N != Initial.SIZE))
                            {
                                MessageBox.Show("Размерности матрицы/вектора правой части/вектора приближения не совпадают.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                success = false;
                                break;
                            }

                            switch (Data.preconditioner)
                            {
                                case 0: //Нет предобуславливателя
                                    {
                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    if (!SLAE.Matrix.CheckSymmetry())
                                                    {
                                                        MessageBox.Show("Для выбранного решателя ваша матрица должна быть симметричной.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        success = false;
                                                        break;
                                                    }
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
                                            case 2: //БСГ стаб
                                                {
                                                    solver = new BSGstab();
                                                    Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                            case 3: //Гаусс-Зейдель
                                                {
                                                    solver = new GaussZeidel();
                                                    Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case 1: //Диагональный
                                    {                                        
                                        IPreconditioner<ProfileMatrix> preconditioner = new ProfilePreconditioner();

                                        if (!preconditioner.createDiag(SLAE))
                                        {
                                            success = false;
                                            break;
                                        }

                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    if (!SLAE.Matrix.CheckSymmetry())
                                                    {
                                                        MessageBox.Show("Для выбранного решателя ваша матрица должна быть симметричной.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        success = false;
                                                        break;
                                                    }
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
                                case 2: //LLT
                                    {
                                        if (!SLAE.Matrix.CheckSymmetry())
                                        {
                                            MessageBox.Show("Для выбранного предобуславливателя ваша матрица должна быть симметричной.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            success = false;
                                            break;
                                        }

                                        IPreconditioner<ProfileMatrix> preconditioner = new ProfilePreconditioner();

                                        if (!preconditioner.createLLT(SLAE))
                                        {
                                            success = false;
                                            break;
                                        }

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
                                                    //if (MessageBox.Show("Ваша матрица должна быть положительно определённой.\nЭто так?\nЕсли не уверены, исользуйте решатель МСГ.", "Обратите внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No) break;
                                                    solver = new LOS();
                                                    Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case 3: //LU
                                    {
                                        IPreconditioner<ProfileMatrix> preconditioner = new ProfilePreconditioner();

                                        if (!preconditioner.createLU(SLAE))
                                        {
                                            success = false;
                                            break;
                                        }

                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    if (!SLAE.Matrix.CheckSymmetry())
                                                    {
                                                        MessageBox.Show("Для выбранного решателя ваша матрица должна быть симметричной.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        success = false;
                                                        break;
                                                    }
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
                                case 4: //LUsq
                                    {
                                        IPreconditioner<ProfileMatrix> preconditioner = new ProfilePreconditioner();

                                        if (!preconditioner.createLUsq(SLAE))
                                        {
                                            success = false;
                                            break;
                                        }

                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    if (!SLAE.Matrix.CheckSymmetry())
                                                    {
                                                        MessageBox.Show("Для выбранного решателя ваша матрица должна быть симметричной.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        success = false;
                                                        break;
                                                    }
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
                            }
                            break;
                        }
                    case 2: //Диагональная
                        {
                            Slae<DiagonalMatrix> SLAE = new Slae<DiagonalMatrix>();
                            SLAE.Matrix = new DiagonalMatrix(Data.matrixPath);
                            SLAE.RightPart = new Vector(Data.rightpartPath);

                            if (initial_checkBox.Checked) Initial = new Vector(SLAE.Matrix.getMatrix().N);
                            else Initial = new Vector(Data.initialPath);

                            if ((SLAE.Matrix.getMatrix().N == 0) || (SLAE.RightPart.SIZE == 0) || (Initial.SIZE == 0) || (SLAE.Matrix.getMatrix().N != SLAE.RightPart.SIZE) || (SLAE.Matrix.getMatrix().N != Initial.SIZE))
                            {
                                MessageBox.Show("Размерность матрицы/вектора правой части/вектора приближения равна нулю.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                success = false;
                                break;
                            }

                            if ((SLAE.Matrix.getMatrix().N != SLAE.RightPart.SIZE) || (SLAE.Matrix.getMatrix().N != Initial.SIZE))
                            {
                                MessageBox.Show("Размерности матрицы/вектора правой части/вектора приближения не совпадают.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                success = false;
                                break;
                            }

                            switch (Data.preconditioner)
                            {
                                case 0: //Нет предобуславливателя
                                    {
                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    if (!SLAE.Matrix.CheckSymmetry())
                                                    {
                                                        MessageBox.Show("Для выбранного решателя ваша матрица должна быть симметричной.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        success = false;
                                                        break;
                                                    }
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
                                            case 2: //БСГ стаб
                                                {
                                                    solver = new BSGstab();
                                                    Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                            case 3: //Гаусс-Зейдель
                                                {
                                                    solver = new GaussZeidel();
                                                    Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case 1: //Диагональный
                                    {
                                        IPreconditioner<DiagonalMatrix> preconditioner = new DiagonalPreconditioner();

                                        if (!preconditioner.createDiag(SLAE))
                                        {
                                            success = false;
                                            break;
                                        }

                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    if (!SLAE.Matrix.CheckSymmetry())
                                                    {
                                                        MessageBox.Show("Для выбранного решателя ваша матрица должна быть симметричной.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        success = false;
                                                        break;
                                                    }
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
                                case 2: //LLT
                                    {
                                        if (!SLAE.Matrix.CheckSymmetry())
                                        {
                                            MessageBox.Show("Для выбранного предобуславливателя ваша матрица должна быть симметричной.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            success = false;
                                            break;
                                        }

                                        IPreconditioner<DiagonalMatrix> preconditioner = new DiagonalPreconditioner();
                                        
                                        if (!preconditioner.createLLT(SLAE))
                                        {
                                            success = false;
                                            break;
                                        }

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
                                case 3: //LU
                                    {
                                        IPreconditioner<DiagonalMatrix> preconditioner = new DiagonalPreconditioner();

                                        if (!preconditioner.createLU(SLAE))
                                        {
                                            success = false;
                                            break;
                                        }

                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    if (!SLAE.Matrix.CheckSymmetry())
                                                    {
                                                        MessageBox.Show("Для выбранного решателя ваша матрица должна быть симметричной.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        success = false;
                                                        break;
                                                    }
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
                                case 4: //LUsq
                                    {
                                        IPreconditioner<DiagonalMatrix> preconditioner = new DiagonalPreconditioner();

                                        if (!preconditioner.createLUsq(SLAE))
                                        {
                                            success = false;
                                            break;
                                        }

                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    if (!SLAE.Matrix.CheckSymmetry())
                                                    {
                                                        MessageBox.Show("Для выбранного решателя ваша матрица должна быть симметричной.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        success = false;
                                                        break;
                                                    }
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
                            }
                            break;
                        }
                    case 3: //Разреженая
                        {
                            Slae<DisperseMatrix> SLAE = new Slae<DisperseMatrix>();
                            SLAE.Matrix = new DisperseMatrix(Data.matrixPath);
                            SLAE.RightPart = new Vector(Data.rightpartPath);

                            if (initial_checkBox.Checked) Initial = new Vector(SLAE.Matrix.getMatrix().N);
                            else Initial = new Vector(Data.initialPath);

                            if ((SLAE.Matrix.getMatrix().N == 0) || (SLAE.RightPart.SIZE == 0) || (Initial.SIZE == 0) || (SLAE.Matrix.getMatrix().N != SLAE.RightPart.SIZE) || (SLAE.Matrix.getMatrix().N != Initial.SIZE))
                            {
                                MessageBox.Show("Размерность матрицы/вектора правой части/вектора приближения равна нулю.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                success = false;
                                break;
                            }

                            if ((SLAE.Matrix.getMatrix().N != SLAE.RightPart.SIZE) || (SLAE.Matrix.getMatrix().N != Initial.SIZE))
                            {
                                MessageBox.Show("Размерности матрицы/вектора правой части/вектора приближения не совпадают.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                success = false;
                                break;
                            }

                            switch (Data.preconditioner)
                            {
                                case 0: //Нет предобуславливателя
                                    {
                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    if (!SLAE.Matrix.CheckSymmetry())
                                                    {
                                                        MessageBox.Show("Для выбранного решателя ваша матрица должна быть симметричной.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        success = false;
                                                        break;
                                                    }
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
                                            case 2: //БСГ стаб
                                                {
                                                    solver = new LOS();
                                                    Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                            case 3: //Гаусс-Зейдель
                                                {
                                                    solver = new GaussZeidel();
                                                    Data.result = solver.Solve(SLAE, Initial, (int)maxiter_numericUpDown.Value, eps);
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case 1: //Диагональный
                                    {
                                        IPreconditioner<DisperseMatrix> preconditioner = new DispersePreconditioner();

                                        if (!preconditioner.createDiag(SLAE))
                                        {
                                            success = false;
                                            break;
                                        }

                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    if (!SLAE.Matrix.CheckSymmetry())
                                                    {
                                                        MessageBox.Show("Для выбранного решателя ваша матрица должна быть симметричной.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        success = false;
                                                        break;
                                                    }
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
                                case 2: //LLT
                                    {
                                        if (!SLAE.Matrix.CheckSymmetry())
                                        {
                                            MessageBox.Show("Для выбранного предобуславливателя ваша матрица должна быть симметричной.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            success = false;
                                            break;
                                        }

                                        IPreconditioner<DisperseMatrix> preconditioner = new DispersePreconditioner();

                                        if (!preconditioner.createLLT(SLAE))
                                        {
                                            success = false;
                                            break;
                                        }

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
                                case 3: //LU
                                    {
                                        IPreconditioner<DisperseMatrix> preconditioner = new DispersePreconditioner();

                                        if (!preconditioner.createLU(SLAE))
                                        {
                                            success = false;
                                            break;
                                        }

                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    if (!SLAE.Matrix.CheckSymmetry())
                                                    {
                                                        MessageBox.Show("Для выбранного решателя ваша матрица должна быть симметричной.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        success = false;
                                                        break;
                                                    }
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
                                case 4: //LUsq
                                    {
                                        IPreconditioner<DisperseMatrix> preconditioner = new DispersePreconditioner();

                                        if (!preconditioner.createLUsq(SLAE))
                                        {
                                            success = false;
                                            break;
                                        }
            
                                        switch (Data.solver)
                                        {
                                            case 0: //МСГ
                                                {
                                                    if (!SLAE.Matrix.CheckSymmetry())
                                                    {
                                                        MessageBox.Show("Для выбранного решателя ваша матрица должна быть симметричной.", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        success = false;
                                                        break;
                                                    }
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
                            }
                            break;
                        }
                }
            }
            else success = false;

            if (success)
                MessageBox.Show("Сохраните ответ в файл на вкладке \"Вывод\".", "Решение завершено!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("В процессе решения возникла ошибка.", "Решение не завершено!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool checkinput()
        {
            if (Data.matrixPath == null)
            {
                MessageBox.Show("Кажется вы забыли ввести данные.\nЗадайте файл с матрицей на вкладке \"Матрица\".", "Опаньки...", MessageBoxButtons.OK);
                tabControl1.SelectedIndex = 0;
                return false;
            }

            if ((Data.solver == 0) || (Data.solver == 1) || (Data.solver == 2) || (Data.solver == 3))
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

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void setupGraph()
        {
            Data.ZedGraph = zedGraphControl1;
            Data.ZedGraph.GraphPane.Title.Text = "График нормы";
            Data.ZedGraph.GraphPane.YAxis.Type = AxisType.Log;
            Data.ZedGraph.GraphPane.YAxis.Title.Text = "Норма";
            Data.ZedGraph.GraphPane.XAxis.Title.Text = "Итерация";
        }

        private void zedGraphControl1_ContextMenuBuilder(ZedGraphControl sender, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
        {
            menuStrip.Items[0].Text = "Копировать";
            menuStrip.Items[1].Text = "Сохранить изображение как";
            menuStrip.Items[2].Enabled = false;
            menuStrip.Items[3].Text = "Печать";
            menuStrip.Items[4].Text = "Показывать значения в узлах";
            menuStrip.Items[5].Enabled = false;
            menuStrip.Items[6].Enabled = false;
            menuStrip.Items[7].Enabled = false;
        }

        private void conv_button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();

            OFD.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            OFD.RestoreDirectory = true;

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                conv_textBox1.Text = OFD.FileName;   //Путь файла с матрицей

                Data.convert_enter_file = conv_textBox1.Text;
            }
            if (conv_textBox1.TextLength != 0 && conv_textBox2.TextLength != 0)
                conv_button3.Enabled = true;
            else conv_button3.Enabled = false;
        }

        private void conv_button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();

            OFD.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            OFD.RestoreDirectory = true;

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                conv_textBox2.Text = OFD.FileName;   //Путь файла с матрицей

                Data.convert_exit_file = conv_textBox2.Text;
            }
            if (conv_textBox1.TextLength != 0 && conv_textBox2.TextLength != 0)
                conv_button3.Enabled = true;
            else conv_button3.Enabled = false;
        }

        private void conv_button3_Click(object sender, EventArgs e)
        {
            //конвертирование
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = Application.StartupPath + @"\Конвертер.exe";
            p.StartInfo.Arguments = Data.convert_state + " " + Data.convert_enter_file + " " + Data.convert_exit_file;
            p.Start();
        }

        private void conv_checkBox1_Click(object sender, EventArgs e)
        {
            conv_checkBox2.Checked = false;
            conv_checkBox2.CheckState = unchecked(conv_checkBox2.CheckState);

            conv_checkBox1.Checked = true;
            conv_checkBox1.CheckState = checked(conv_checkBox1.CheckState);

            Data.convert_state = 1;
        }

        private void conv_checkBox2_Click(object sender, EventArgs e)
        {
            conv_checkBox1.Checked = false;
            conv_checkBox1.CheckState = unchecked(conv_checkBox1.CheckState);

            conv_checkBox2.Checked = true;
            conv_checkBox2.CheckState = checked(conv_checkBox2.CheckState);

            Data.convert_state = 2;
        }

        private void conv_textBox1_TextChanged(object sender, EventArgs e)
        {
            if (conv_textBox1.TextLength != 0 && conv_textBox2.TextLength != 0)
                conv_button3.Enabled = true;
            else conv_button3.Enabled = false;
            Data.convert_enter_file = conv_textBox1.Text;
        }

        private void conv_textBox2_TextChanged(object sender, EventArgs e)
        {
            if (conv_textBox1.TextLength != 0 && conv_textBox2.TextLength != 0)
                conv_button3.Enabled = true;
            else conv_button3.Enabled = false;
            Data.convert_exit_file = conv_textBox2.Text;
        }

        private void matrix_label_Click(object sender, EventArgs e)
        {

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
        public static ZedGraphControl ZedGraph;
        public static string convert_enter_file;    //Путь файла для конвертирования
        public static string convert_exit_file;     //Путь файла куда конвертировать
        public static int convert_state = 1;    //режим работы конвертер
    }    
}
