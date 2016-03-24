namespace anonymous
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.matrix_combobox = new System.Windows.Forms.ComboBox();
            this.matrix_textBox = new System.Windows.Forms.TextBox();
            this.matrix_button = new System.Windows.Forms.Button();
            this.rightpart_button = new System.Windows.Forms.Button();
            this.matrix_label = new System.Windows.Forms.Label();
            this.matrixformat_label = new System.Windows.Forms.Label();
            this.preconditioner_label = new System.Windows.Forms.Label();
            this.preconditioner_comboBox = new System.Windows.Forms.ComboBox();
            this.rightpart_textBox = new System.Windows.Forms.TextBox();
            this.rightpart_label = new System.Windows.Forms.Label();
            this.initial_button = new System.Windows.Forms.Button();
            this.solver_label = new System.Windows.Forms.Label();
            this.solver_comboBox = new System.Windows.Forms.ComboBox();
            this.initial_label = new System.Windows.Forms.Label();
            this.initial_textBox = new System.Windows.Forms.TextBox();
            this.solve_button = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.eps_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.eps_label = new System.Windows.Forms.Label();
            this.maxiter_label = new System.Windows.Forms.Label();
            this.maxiter_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.initial_checkBox = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.save_label = new System.Windows.Forms.Label();
            this.save_button = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.conv_checkBox2 = new System.Windows.Forms.CheckBox();
            this.conv_checkBox1 = new System.Windows.Forms.CheckBox();
            this.conv_textBox2 = new System.Windows.Forms.TextBox();
            this.conv_textBox1 = new System.Windows.Forms.TextBox();
            this.conv_button3 = new System.Windows.Forms.Button();
            this.conv_button2 = new System.Windows.Forms.Button();
            this.conv_button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eps_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxiter_numericUpDown)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // matrix_combobox
            // 
            this.matrix_combobox.BackColor = System.Drawing.Color.White;
            this.matrix_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.matrix_combobox.FormattingEnabled = true;
            this.matrix_combobox.Location = new System.Drawing.Point(337, 32);
            this.matrix_combobox.Margin = new System.Windows.Forms.Padding(4);
            this.matrix_combobox.Name = "matrix_combobox";
            this.matrix_combobox.Size = new System.Drawing.Size(195, 24);
            this.matrix_combobox.TabIndex = 0;
            this.matrix_combobox.SelectedIndexChanged += new System.EventHandler(this.matrix_comboBox_SelectedIndexChanged);
            // 
            // matrix_textBox
            // 
            this.matrix_textBox.BackColor = System.Drawing.Color.White;
            this.matrix_textBox.Location = new System.Drawing.Point(35, 217);
            this.matrix_textBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.matrix_textBox.Name = "matrix_textBox";
            this.matrix_textBox.Size = new System.Drawing.Size(391, 22);
            this.matrix_textBox.TabIndex = 1;
            // 
            // matrix_button
            // 
            this.matrix_button.Location = new System.Drawing.Point(457, 214);
            this.matrix_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.matrix_button.Name = "matrix_button";
            this.matrix_button.Size = new System.Drawing.Size(75, 26);
            this.matrix_button.TabIndex = 2;
            this.matrix_button.Text = "Обзор";
            this.matrix_button.UseVisualStyleBackColor = true;
            this.matrix_button.Click += new System.EventHandler(this.matrix_button_Click);
            // 
            // rightpart_button
            // 
            this.rightpart_button.Location = new System.Drawing.Point(457, 319);
            this.rightpart_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rightpart_button.Name = "rightpart_button";
            this.rightpart_button.Size = new System.Drawing.Size(75, 26);
            this.rightpart_button.TabIndex = 3;
            this.rightpart_button.Text = "Обзор";
            this.rightpart_button.UseVisualStyleBackColor = true;
            this.rightpart_button.Click += new System.EventHandler(this.rightpart_button_Click);
            // 
            // matrix_label
            // 
            this.matrix_label.AutoSize = true;
            this.matrix_label.Location = new System.Drawing.Point(32, 170);
            this.matrix_label.Name = "matrix_label";
            this.matrix_label.Size = new System.Drawing.Size(192, 17);
            this.matrix_label.TabIndex = 4;
            this.matrix_label.Text = "Выберите файл с матрицей";
            // 
            // matrixformat_label
            // 
            this.matrixformat_label.AutoSize = true;
            this.matrixformat_label.Location = new System.Drawing.Point(32, 34);
            this.matrixformat_label.Name = "matrixformat_label";
            this.matrixformat_label.Size = new System.Drawing.Size(195, 17);
            this.matrixformat_label.TabIndex = 5;
            this.matrixformat_label.Text = "Выберите формат хранения";
            // 
            // preconditioner_label
            // 
            this.preconditioner_label.AutoSize = true;
            this.preconditioner_label.Location = new System.Drawing.Point(32, 34);
            this.preconditioner_label.Name = "preconditioner_label";
            this.preconditioner_label.Size = new System.Drawing.Size(224, 17);
            this.preconditioner_label.TabIndex = 6;
            this.preconditioner_label.Text = "Выберите предобуславливатель";
            // 
            // preconditioner_comboBox
            // 
            this.preconditioner_comboBox.BackColor = System.Drawing.Color.White;
            this.preconditioner_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.preconditioner_comboBox.FormattingEnabled = true;
            this.preconditioner_comboBox.Location = new System.Drawing.Point(337, 32);
            this.preconditioner_comboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.preconditioner_comboBox.Name = "preconditioner_comboBox";
            this.preconditioner_comboBox.Size = new System.Drawing.Size(195, 24);
            this.preconditioner_comboBox.TabIndex = 7;
            this.preconditioner_comboBox.SelectedIndexChanged += new System.EventHandler(this.preconditioner_comboBox_SelectedIndexChanged);
            // 
            // rightpart_textBox
            // 
            this.rightpart_textBox.BackColor = System.Drawing.Color.White;
            this.rightpart_textBox.Location = new System.Drawing.Point(35, 321);
            this.rightpart_textBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rightpart_textBox.Name = "rightpart_textBox";
            this.rightpart_textBox.Size = new System.Drawing.Size(391, 22);
            this.rightpart_textBox.TabIndex = 8;
            // 
            // rightpart_label
            // 
            this.rightpart_label.AutoSize = true;
            this.rightpart_label.Location = new System.Drawing.Point(32, 271);
            this.rightpart_label.Name = "rightpart_label";
            this.rightpart_label.Size = new System.Drawing.Size(226, 17);
            this.rightpart_label.TabIndex = 9;
            this.rightpart_label.Text = "Выберите файл с правой частью";
            // 
            // initial_button
            // 
            this.initial_button.Location = new System.Drawing.Point(457, 319);
            this.initial_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.initial_button.Name = "initial_button";
            this.initial_button.Size = new System.Drawing.Size(75, 26);
            this.initial_button.TabIndex = 10;
            this.initial_button.Text = "Обзор";
            this.initial_button.UseVisualStyleBackColor = true;
            this.initial_button.Click += new System.EventHandler(this.initial_button_Click);
            // 
            // solver_label
            // 
            this.solver_label.AutoSize = true;
            this.solver_label.Location = new System.Drawing.Point(32, 86);
            this.solver_label.Name = "solver_label";
            this.solver_label.Size = new System.Drawing.Size(143, 17);
            this.solver_label.TabIndex = 11;
            this.solver_label.Text = "Выберите решатель";
            // 
            // solver_comboBox
            // 
            this.solver_comboBox.BackColor = System.Drawing.Color.White;
            this.solver_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.solver_comboBox.FormattingEnabled = true;
            this.solver_comboBox.Location = new System.Drawing.Point(337, 82);
            this.solver_comboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.solver_comboBox.Name = "solver_comboBox";
            this.solver_comboBox.Size = new System.Drawing.Size(195, 24);
            this.solver_comboBox.TabIndex = 12;
            this.solver_comboBox.SelectedIndexChanged += new System.EventHandler(this.solver_comboBox_SelectedIndexChanged);
            // 
            // initial_label
            // 
            this.initial_label.AutoSize = true;
            this.initial_label.Location = new System.Drawing.Point(32, 271);
            this.initial_label.Name = "initial_label";
            this.initial_label.Size = new System.Drawing.Size(304, 17);
            this.initial_label.TabIndex = 13;
            this.initial_label.Text = "Выберите файл с начальным приближением";
            // 
            // initial_textBox
            // 
            this.initial_textBox.BackColor = System.Drawing.Color.White;
            this.initial_textBox.Location = new System.Drawing.Point(35, 321);
            this.initial_textBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.initial_textBox.Name = "initial_textBox";
            this.initial_textBox.Size = new System.Drawing.Size(391, 22);
            this.initial_textBox.TabIndex = 14;
            // 
            // solve_button
            // 
            this.solve_button.Location = new System.Drawing.Point(457, 383);
            this.solve_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.solve_button.Name = "solve_button";
            this.solve_button.Size = new System.Drawing.Size(75, 26);
            this.solve_button.TabIndex = 15;
            this.solve_button.Text = "Решить";
            this.solve_button.UseVisualStyleBackColor = true;
            this.solve_button.Click += new System.EventHandler(this.solve_button_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(564, 679);
            this.tabControl1.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.matrixformat_label);
            this.tabPage1.Controls.Add(this.matrix_combobox);
            this.tabPage1.Controls.Add(this.matrix_label);
            this.tabPage1.Controls.Add(this.matrix_textBox);
            this.tabPage1.Controls.Add(this.matrix_button);
            this.tabPage1.Controls.Add(this.rightpart_button);
            this.tabPage1.Controls.Add(this.rightpart_textBox);
            this.tabPage1.Controls.Add(this.rightpart_label);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(556, 650);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Матрица";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 387);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(316, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Что-то непонятно? Используйте Справку! (F1)";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.richTextBox1);
            this.tabPage2.Controls.Add(this.eps_numericUpDown);
            this.tabPage2.Controls.Add(this.eps_label);
            this.tabPage2.Controls.Add(this.maxiter_label);
            this.tabPage2.Controls.Add(this.maxiter_numericUpDown);
            this.tabPage2.Controls.Add(this.initial_checkBox);
            this.tabPage2.Controls.Add(this.preconditioner_label);
            this.tabPage2.Controls.Add(this.solve_button);
            this.tabPage2.Controls.Add(this.preconditioner_comboBox);
            this.tabPage2.Controls.Add(this.initial_textBox);
            this.tabPage2.Controls.Add(this.solver_label);
            this.tabPage2.Controls.Add(this.initial_label);
            this.tabPage2.Controls.Add(this.initial_button);
            this.tabPage2.Controls.Add(this.solver_comboBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(556, 650);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Решатель";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.Location = new System.Drawing.Point(8, 442);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(540, 201);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            this.richTextBox1.WordWrap = false;
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // eps_numericUpDown
            // 
            this.eps_numericUpDown.Location = new System.Drawing.Point(337, 186);
            this.eps_numericUpDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.eps_numericUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.eps_numericUpDown.Name = "eps_numericUpDown";
            this.eps_numericUpDown.Size = new System.Drawing.Size(91, 22);
            this.eps_numericUpDown.TabIndex = 21;
            this.eps_numericUpDown.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // eps_label
            // 
            this.eps_label.AutoSize = true;
            this.eps_label.Location = new System.Drawing.Point(32, 188);
            this.eps_label.Name = "eps_label";
            this.eps_label.Size = new System.Drawing.Size(130, 17);
            this.eps_label.TabIndex = 20;
            this.eps_label.Text = "Порядок точности";
            // 
            // maxiter_label
            // 
            this.maxiter_label.AutoSize = true;
            this.maxiter_label.Location = new System.Drawing.Point(32, 139);
            this.maxiter_label.Name = "maxiter_label";
            this.maxiter_label.Size = new System.Drawing.Size(215, 17);
            this.maxiter_label.TabIndex = 19;
            this.maxiter_label.Text = "Максимальное число итераций";
            // 
            // maxiter_numericUpDown
            // 
            this.maxiter_numericUpDown.Location = new System.Drawing.Point(337, 134);
            this.maxiter_numericUpDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.maxiter_numericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.maxiter_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxiter_numericUpDown.Name = "maxiter_numericUpDown";
            this.maxiter_numericUpDown.Size = new System.Drawing.Size(91, 22);
            this.maxiter_numericUpDown.TabIndex = 18;
            this.maxiter_numericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // initial_checkBox
            // 
            this.initial_checkBox.AutoSize = true;
            this.initial_checkBox.Location = new System.Drawing.Point(35, 388);
            this.initial_checkBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.initial_checkBox.Name = "initial_checkBox";
            this.initial_checkBox.Size = new System.Drawing.Size(254, 21);
            this.initial_checkBox.TabIndex = 17;
            this.initial_checkBox.Text = "Нулевое начальное приближение";
            this.initial_checkBox.UseVisualStyleBackColor = true;
            this.initial_checkBox.CheckedChanged += new System.EventHandler(this.initial_checkBox_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.zedGraphControl1);
            this.tabPage3.Controls.Add(this.save_label);
            this.tabPage3.Controls.Add(this.save_button);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Size = new System.Drawing.Size(556, 650);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Вывод";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.zedGraphControl1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.zedGraphControl1.IsEnableHZoom = false;
            this.zedGraphControl1.IsEnableVZoom = false;
            this.zedGraphControl1.IsEnableWheelZoom = false;
            this.zedGraphControl1.IsShowPointValues = true;
            this.zedGraphControl1.Location = new System.Drawing.Point(3, 237);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(5);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(550, 411);
            this.zedGraphControl1.TabIndex = 3;
            this.zedGraphControl1.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.zedGraphControl1_ContextMenuBuilder);
            // 
            // save_label
            // 
            this.save_label.AutoSize = true;
            this.save_label.Location = new System.Drawing.Point(32, 34);
            this.save_label.Name = "save_label";
            this.save_label.Size = new System.Drawing.Size(168, 17);
            this.save_label.TabIndex = 1;
            this.save_label.Text = "Сохранить ответ в файл";
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(337, 32);
            this.save_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(100, 28);
            this.save_button.TabIndex = 0;
            this.save_button.Text = "Сохранить";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Controls.Add(this.label1);
            this.tabPage4.Controls.Add(this.conv_checkBox2);
            this.tabPage4.Controls.Add(this.conv_checkBox1);
            this.tabPage4.Controls.Add(this.conv_textBox2);
            this.tabPage4.Controls.Add(this.conv_textBox1);
            this.tabPage4.Controls.Add(this.conv_button3);
            this.tabPage4.Controls.Add(this.conv_button2);
            this.tabPage4.Controls.Add(this.conv_button1);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage4.Size = new System.Drawing.Size(556, 650);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Конвертер";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Конвертированный файл";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Исходный файл";
            // 
            // conv_checkBox2
            // 
            this.conv_checkBox2.AutoSize = true;
            this.conv_checkBox2.Location = new System.Drawing.Point(32, 278);
            this.conv_checkBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.conv_checkBox2.Name = "conv_checkBox2";
            this.conv_checkBox2.Size = new System.Drawing.Size(123, 21);
            this.conv_checkBox2.TabIndex = 6;
            this.conv_checkBox2.Text = "Наш -> общий";
            this.conv_checkBox2.UseVisualStyleBackColor = true;
            this.conv_checkBox2.Click += new System.EventHandler(this.conv_checkBox2_Click);
            // 
            // conv_checkBox1
            // 
            this.conv_checkBox1.AutoSize = true;
            this.conv_checkBox1.Checked = true;
            this.conv_checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.conv_checkBox1.Location = new System.Drawing.Point(32, 255);
            this.conv_checkBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.conv_checkBox1.Name = "conv_checkBox1";
            this.conv_checkBox1.Size = new System.Drawing.Size(124, 21);
            this.conv_checkBox1.TabIndex = 5;
            this.conv_checkBox1.Text = "Общий -> наш";
            this.conv_checkBox1.UseVisualStyleBackColor = true;
            this.conv_checkBox1.Click += new System.EventHandler(this.conv_checkBox1_Click);
            // 
            // conv_textBox2
            // 
            this.conv_textBox2.Location = new System.Drawing.Point(32, 188);
            this.conv_textBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.conv_textBox2.Name = "conv_textBox2";
            this.conv_textBox2.Size = new System.Drawing.Size(391, 22);
            this.conv_textBox2.TabIndex = 4;
            this.conv_textBox2.TextChanged += new System.EventHandler(this.conv_textBox2_TextChanged);
            // 
            // conv_textBox1
            // 
            this.conv_textBox1.Location = new System.Drawing.Point(32, 86);
            this.conv_textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.conv_textBox1.Name = "conv_textBox1";
            this.conv_textBox1.Size = new System.Drawing.Size(391, 22);
            this.conv_textBox1.TabIndex = 3;
            this.conv_textBox1.TextChanged += new System.EventHandler(this.conv_textBox1_TextChanged);
            // 
            // conv_button3
            // 
            this.conv_button3.Enabled = false;
            this.conv_button3.Location = new System.Drawing.Point(387, 255);
            this.conv_button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.conv_button3.Name = "conv_button3";
            this.conv_button3.Size = new System.Drawing.Size(149, 28);
            this.conv_button3.TabIndex = 2;
            this.conv_button3.Text = "Конвертировать";
            this.conv_button3.UseVisualStyleBackColor = true;
            this.conv_button3.Click += new System.EventHandler(this.conv_button3_Click);
            // 
            // conv_button2
            // 
            this.conv_button2.Location = new System.Drawing.Point(461, 186);
            this.conv_button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.conv_button2.Name = "conv_button2";
            this.conv_button2.Size = new System.Drawing.Size(75, 26);
            this.conv_button2.TabIndex = 1;
            this.conv_button2.Text = "Обзор";
            this.conv_button2.UseVisualStyleBackColor = true;
            this.conv_button2.Click += new System.EventHandler(this.conv_button2_Click);
            // 
            // conv_button1
            // 
            this.conv_button1.Location = new System.Drawing.Point(461, 84);
            this.conv_button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.conv_button1.Name = "conv_button1";
            this.conv_button1.Size = new System.Drawing.Size(75, 26);
            this.conv_button1.TabIndex = 0;
            this.conv_button1.Text = "Обзор";
            this.conv_button1.UseVisualStyleBackColor = true;
            this.conv_button1.Click += new System.EventHandler(this.conv_button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 679);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Решатель 2000";
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.Form1_HelpRequested);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eps_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxiter_numericUpDown)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox matrix_combobox;
        private System.Windows.Forms.Button matrix_button;
        private System.Windows.Forms.Button rightpart_button;
        private System.Windows.Forms.Label matrix_label;
        private System.Windows.Forms.Label matrixformat_label;
        private System.Windows.Forms.Label preconditioner_label;
        private System.Windows.Forms.ComboBox preconditioner_comboBox;
        private System.Windows.Forms.TextBox rightpart_textBox;
        private System.Windows.Forms.Label rightpart_label;
        private System.Windows.Forms.Button initial_button;
        private System.Windows.Forms.Label solver_label;
        private System.Windows.Forms.ComboBox solver_comboBox;
        private System.Windows.Forms.TextBox matrix_textBox;
        private System.Windows.Forms.Label initial_label;
        private System.Windows.Forms.TextBox initial_textBox;
        private System.Windows.Forms.Button solve_button;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox initial_checkBox;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label maxiter_label;
        private System.Windows.Forms.NumericUpDown maxiter_numericUpDown;
        private System.Windows.Forms.NumericUpDown eps_numericUpDown;
        private System.Windows.Forms.Label eps_label;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.Label save_label;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button conv_button3;
        private System.Windows.Forms.Button conv_button2;
        private System.Windows.Forms.Button conv_button1;
        private System.Windows.Forms.TextBox conv_textBox1;
        private System.Windows.Forms.TextBox conv_textBox2;
        private System.Windows.Forms.CheckBox conv_checkBox2;
        private System.Windows.Forms.CheckBox conv_checkBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}

