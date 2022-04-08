namespace Task
{
    partial class MainForm
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
            this.BtnMock = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnMock
            // 
            this.BtnMock.Location = new System.Drawing.Point(206, 164);
            this.BtnMock.Name = "BtnMock";
            this.BtnMock.Size = new System.Drawing.Size(259, 136);
            this.BtnMock.TabIndex = 0;
            this.BtnMock.Text = "Mock";
            this.BtnMock.UseVisualStyleBackColor = true;
            this.BtnMock.Click += new System.EventHandler(this.BtnMock_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 508);
            this.Controls.Add(this.BtnMock);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnMock;
    }
}

