namespace TILab1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.createMatrix = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.labelH_A = new System.Windows.Forms.Label();
            this.labelH_B = new System.Windows.Forms.Label();
            this.labelH_AB = new System.Windows.Forms.Label();
            this.labelI_AB = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ansambleA = new System.Windows.Forms.Label();
            this.ansambleB = new System.Windows.Forms.Label();
            this.labelH_BA = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(27, 105);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(48, 26);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "3";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(86, 106);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(48, 26);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "3";
            // 
            // createMatrix
            // 
            this.createMatrix.Location = new System.Drawing.Point(165, 105);
            this.createMatrix.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.createMatrix.Name = "createMatrix";
            this.createMatrix.Size = new System.Drawing.Size(177, 31);
            this.createMatrix.TabIndex = 2;
            this.createMatrix.Text = "Создать матрицу";
            this.createMatrix.UseVisualStyleBackColor = true;
            this.createMatrix.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Дана матрица условных вероятностей p(Bj/Ai) и ансамбль А",
            "Дана матрица условных вероятностей p(Ai/Bj) и ансамбль Б",
            "Дана матрица совместных вероятностей p(AiBj)"});
            this.comboBox1.Location = new System.Drawing.Point(32, 43);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(1080, 28);
            this.comboBox1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Тип задачи";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Размер матрицы";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(351, 106);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 31);
            this.button2.TabIndex = 6;
            this.button2.Text = "Решить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // labelH_A
            // 
            this.labelH_A.AutoSize = true;
            this.labelH_A.Location = new System.Drawing.Point(24, 395);
            this.labelH_A.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelH_A.Name = "labelH_A";
            this.labelH_A.Size = new System.Drawing.Size(59, 20);
            this.labelH_A.TabIndex = 7;
            this.labelH_A.Text = "H(A) = ";
            this.labelH_A.Visible = false;
            // 
            // labelH_B
            // 
            this.labelH_B.AutoSize = true;
            this.labelH_B.Location = new System.Drawing.Point(24, 435);
            this.labelH_B.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelH_B.Name = "labelH_B";
            this.labelH_B.Size = new System.Drawing.Size(59, 20);
            this.labelH_B.TabIndex = 8;
            this.labelH_B.Text = "H(B) = ";
            this.labelH_B.Visible = false;
            // 
            // labelH_AB
            // 
            this.labelH_AB.AutoSize = true;
            this.labelH_AB.Location = new System.Drawing.Point(160, 395);
            this.labelH_AB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelH_AB.Name = "labelH_AB";
            this.labelH_AB.Size = new System.Drawing.Size(70, 20);
            this.labelH_AB.TabIndex = 9;
            this.labelH_AB.Text = "H(AB) = ";
            this.labelH_AB.Visible = false;
            // 
            // labelI_AB
            // 
            this.labelI_AB.AutoSize = true;
            this.labelI_AB.Location = new System.Drawing.Point(160, 512);
            this.labelI_AB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelI_AB.Name = "labelI_AB";
            this.labelI_AB.Size = new System.Drawing.Size(63, 20);
            this.labelI_AB.TabIndex = 10;
            this.labelI_AB.Text = "I(AB) = ";
            this.labelI_AB.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(160, 435);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 20);
            this.label10.TabIndex = 11;
            this.label10.Text = "H(B/A) = ";
            this.label10.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 278);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 20);
            this.label4.TabIndex = 13;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(18, 602);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.Size = new System.Drawing.Size(645, 231);
            this.dataGridView1.TabIndex = 14;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(722, 602);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 62;
            this.dataGridView2.Size = new System.Drawing.Size(608, 231);
            this.dataGridView2.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 577);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 20);
            this.label3.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(747, 577);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 20);
            this.label5.TabIndex = 17;
            // 
            // ansambleA
            // 
            this.ansambleA.AutoSize = true;
            this.ansambleA.Location = new System.Drawing.Point(24, 323);
            this.ansambleA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ansambleA.Name = "ansambleA";
            this.ansambleA.Size = new System.Drawing.Size(37, 20);
            this.ansambleA.TabIndex = 18;
            this.ansambleA.Text = "A = ";
            this.ansambleA.Visible = false;
            // 
            // ansambleB
            // 
            this.ansambleB.AutoSize = true;
            this.ansambleB.Location = new System.Drawing.Point(24, 358);
            this.ansambleB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ansambleB.Name = "ansambleB";
            this.ansambleB.Size = new System.Drawing.Size(37, 20);
            this.ansambleB.TabIndex = 19;
            this.ansambleB.Text = "B = ";
            this.ansambleB.Visible = false;
            this.ansambleB.Click += new System.EventHandler(this.ansambleB_Click);
            // 
            // labelH_BA
            // 
            this.labelH_BA.AutoSize = true;
            this.labelH_BA.Location = new System.Drawing.Point(160, 477);
            this.labelH_BA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelH_BA.Name = "labelH_BA";
            this.labelH_BA.Size = new System.Drawing.Size(74, 20);
            this.labelH_BA.TabIndex = 20;
            this.labelH_BA.Text = "H(A/B) = ";
            this.labelH_BA.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1347, 855);
            this.Controls.Add(this.labelH_BA);
            this.Controls.Add(this.ansambleB);
            this.Controls.Add(this.ansambleA);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.labelI_AB);
            this.Controls.Add(this.labelH_AB);
            this.Controls.Add(this.labelH_B);
            this.Controls.Add(this.labelH_A);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.createMatrix);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Lab TI 1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button createMatrix;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelH_A;
        private System.Windows.Forms.Label labelH_B;
        private System.Windows.Forms.Label labelH_AB;
        private System.Windows.Forms.Label labelI_AB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label ansambleA;
        private System.Windows.Forms.Label ansambleB;
        private System.Windows.Forms.Label labelH_BA;
    }
}

