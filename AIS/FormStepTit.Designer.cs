namespace AIS
{
    partial class FormStepTit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStepTit));
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonAnswer = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chartGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonNIter = new System.Windows.Forms.Button();
            this.numericUpDownN = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonInitalGeneration = new System.Windows.Forms.Button();
            this.buttonBestLeader = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.buttonEndCondition = new System.Windows.Forms.Button();
            this.buttonNewGeneraton = new System.Windows.Forms.Button();
            this.buttonResult = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonSavePictures = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AllowUserToResizeColumns = false;
            this.dataGridView3.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView3.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView3.Location = new System.Drawing.Point(11, 30);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView3.Size = new System.Drawing.Size(331, 135);
            this.dataGridView3.TabIndex = 31;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "Характеристика";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 195;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn2.HeaderText = "Значение";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 136;
            // 
            // buttonAnswer
            // 
            this.buttonAnswer.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonAnswer.Enabled = false;
            this.buttonAnswer.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonAnswer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAnswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAnswer.Location = new System.Drawing.Point(12, 591);
            this.buttonAnswer.Name = "buttonAnswer";
            this.buttonAnswer.Size = new System.Drawing.Size(89, 53);
            this.buttonAnswer.TabIndex = 34;
            this.buttonAnswer.Text = "Получить ответ";
            this.buttonAnswer.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(388, 474);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 170);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Информация о текущей популяции";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(385, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(354, 30);
            this.label1.TabIndex = 36;
            this.label1.Text = "График изменения средней (зеленый) и наилучшей (синий) \r\nприспособленности популя" +
    "ции:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(755, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(377, 15);
            this.label2.TabIndex = 37;
            this.label2.Text = "Графическое изображение популяции и линий уровня функции ";
            // 
            // chartGraph
            // 
            chartArea1.AxisX.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.SharpTriangle;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.AxisX.Title = "Итерация";
            chartArea1.AxisX.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.AxisY.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.SharpTriangle;
            chartArea1.AxisY.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea1.AxisY.Title = "f";
            chartArea1.AxisY.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Times New Roman", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.InnerPlotPosition.Auto = false;
            chartArea1.InnerPlotPosition.Height = 81F;
            chartArea1.InnerPlotPosition.Width = 81.7F;
            chartArea1.InnerPlotPosition.X = 10F;
            chartArea1.InnerPlotPosition.Y = 7F;
            chartArea1.Name = "ChartArea1";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 96F;
            chartArea1.Position.Width = 94F;
            chartArea1.Position.X = 5F;
            chartArea1.Position.Y = 3F;
            this.chartGraph.ChartAreas.Add(chartArea1);
            this.chartGraph.Location = new System.Drawing.Point(388, 48);
            this.chartGraph.Name = "chartGraph";
            this.chartGraph.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.RoyalBlue;
            series1.Legend = "Legend1";
            series1.Name = "Series0";
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Green;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartGraph.Series.Add(series1);
            this.chartGraph.Series.Add(series2);
            this.chartGraph.Size = new System.Drawing.Size(348, 410);
            this.chartGraph.TabIndex = 38;
            this.chartGraph.Text = "chartGraph";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(155, 599);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 39;
            // 
            // buttonNIter
            // 
            this.buttonNIter.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonNIter.Enabled = false;
            this.buttonNIter.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonNIter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNIter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonNIter.Location = new System.Drawing.Point(209, 591);
            this.buttonNIter.Name = "buttonNIter";
            this.buttonNIter.Size = new System.Drawing.Size(155, 30);
            this.buttonNIter.TabIndex = 40;
            this.buttonNIter.Text = "Выполнить N итераций";
            this.buttonNIter.UseVisualStyleBackColor = false;
            this.buttonNIter.Click += new System.EventHandler(this.buttonNIter_Click);
            // 
            // numericUpDownN
            // 
            this.numericUpDownN.Location = new System.Drawing.Point(318, 624);
            this.numericUpDownN.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownN.Name = "numericUpDownN";
            this.numericUpDownN.Size = new System.Drawing.Size(46, 20);
            this.numericUpDownN.TabIndex = 41;
            this.numericUpDownN.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(289, 627);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 42;
            this.label4.Text = "N = ";
            // 
            // buttonInitalGeneration
            // 
            this.buttonInitalGeneration.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonInitalGeneration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonInitalGeneration.Location = new System.Drawing.Point(31, 48);
            this.buttonInitalGeneration.Name = "buttonInitalGeneration";
            this.buttonInitalGeneration.Size = new System.Drawing.Size(95, 53);
            this.buttonInitalGeneration.TabIndex = 43;
            this.buttonInitalGeneration.Text = "Генерация начальной популяции";
            this.buttonInitalGeneration.UseVisualStyleBackColor = true;
            this.buttonInitalGeneration.Click += new System.EventHandler(this.buttonInitilize_Click);
            // 
            // buttonBestLeader
            // 
            this.buttonBestLeader.Enabled = false;
            this.buttonBestLeader.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonBestLeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBestLeader.Location = new System.Drawing.Point(31, 122);
            this.buttonBestLeader.Name = "buttonBestLeader";
            this.buttonBestLeader.Size = new System.Drawing.Size(333, 88);
            this.buttonBestLeader.TabIndex = 44;
            this.buttonBestLeader.Text = "Движение стаи и нахождение наилучших достигнутых положений синиц и вожака (стохас" +
    "тические уравнения)";
            this.buttonBestLeader.UseVisualStyleBackColor = true;
            this.buttonBestLeader.Click += new System.EventHandler(this.buttonBestLeader_Click);
            // 
            // button6
            // 
            this.button6.Enabled = false;
            this.button6.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Location = new System.Drawing.Point(269, 255);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(95, 88);
            this.button6.TabIndex = 49;
            this.button6.Text = "Проверка заполнения матрицы памяти";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Enabled = false;
            this.button7.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Location = new System.Drawing.Point(150, 255);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(95, 88);
            this.button7.TabIndex = 48;
            this.button7.Text = "Нахождение нового положения вожака";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Enabled = false;
            this.button8.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Location = new System.Drawing.Point(31, 255);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(95, 88);
            this.button8.TabIndex = 47;
            this.button8.Text = "Реализация перелетов остальных членов стаи";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // buttonEndCondition
            // 
            this.buttonEndCondition.Enabled = false;
            this.buttonEndCondition.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonEndCondition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEndCondition.Location = new System.Drawing.Point(269, 394);
            this.buttonEndCondition.Name = "buttonEndCondition";
            this.buttonEndCondition.Size = new System.Drawing.Size(95, 64);
            this.buttonEndCondition.TabIndex = 51;
            this.buttonEndCondition.Text = "Проверка условий завершения";
            this.buttonEndCondition.UseVisualStyleBackColor = true;
            this.buttonEndCondition.Click += new System.EventHandler(this.buttonEndCondition_Click);
            // 
            // buttonNewGeneraton
            // 
            this.buttonNewGeneraton.Enabled = false;
            this.buttonNewGeneraton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonNewGeneraton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNewGeneraton.Location = new System.Drawing.Point(31, 394);
            this.buttonNewGeneraton.Name = "buttonNewGeneraton";
            this.buttonNewGeneraton.Size = new System.Drawing.Size(95, 64);
            this.buttonNewGeneraton.TabIndex = 50;
            this.buttonNewGeneraton.Text = "Генерация новой стаи";
            this.buttonNewGeneraton.UseVisualStyleBackColor = true;
            this.buttonNewGeneraton.Click += new System.EventHandler(this.buttonNewGeneraton_Click);
            // 
            // buttonResult
            // 
            this.buttonResult.Enabled = false;
            this.buttonResult.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonResult.Location = new System.Drawing.Point(269, 500);
            this.buttonResult.Name = "buttonResult";
            this.buttonResult.Size = new System.Drawing.Size(95, 63);
            this.buttonResult.TabIndex = 52;
            this.buttonResult.Text = "Выбор наилучшего решения";
            this.buttonResult.UseVisualStyleBackColor = true;
            this.buttonResult.Click += new System.EventHandler(this.button11_Click);
            // 
            // buttonSavePictures
            // 
            this.buttonSavePictures.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.buttonSavePictures.FlatAppearance.MouseDownBackColor = System.Drawing.Color.OliveDrab;
            this.buttonSavePictures.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.buttonSavePictures.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSavePictures.Image = global::AIS.Properties.Resources.Save;
            this.buttonSavePictures.Location = new System.Drawing.Point(1213, 12);
            this.buttonSavePictures.Name = "buttonSavePictures";
            this.buttonSavePictures.Size = new System.Drawing.Size(34, 36);
            this.buttonSavePictures.TabIndex = 53;
            this.toolTip1.SetToolTip(this.buttonSavePictures, "Сохранить график приспособленности и изображение популяции на текущей итерации");
            this.buttonSavePictures.UseVisualStyleBackColor = true;
            this.buttonSavePictures.Click += new System.EventHandler(this.buttonSavePictures_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox3.Image = global::AIS.Properties.Resources.SchoolColor_Steps;
            this.pictureBox3.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.InitialImage")));
            this.pictureBox3.Location = new System.Drawing.Point(758, 563);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(490, 81);
            this.pictureBox3.TabIndex = 32;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox2.Location = new System.Drawing.Point(758, 68);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(490, 490);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(358, 573);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // FormStepTit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1262, 653);
            this.Controls.Add(this.buttonSavePictures);
            this.Controls.Add(this.buttonResult);
            this.Controls.Add(this.buttonEndCondition);
            this.Controls.Add(this.buttonNewGeneraton);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.buttonBestLeader);
            this.Controls.Add(this.buttonInitalGeneration);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownN);
            this.Controls.Add(this.buttonNIter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chartGraph);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonAnswer);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormStepTit";
            this.Text = "Метод стаи синиц. Работа по шагам";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button buttonAnswer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartGraph;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonNIter;
        private System.Windows.Forms.NumericUpDown numericUpDownN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Button buttonInitalGeneration;
        private System.Windows.Forms.Button buttonBestLeader;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button buttonEndCondition;
        private System.Windows.Forms.Button buttonNewGeneraton;
        private System.Windows.Forms.Button buttonResult;
        private System.Windows.Forms.Button buttonSavePictures;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}