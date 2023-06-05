
namespace CG_2
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
            this.Screen = new System.Windows.Forms.PictureBox();
            this.Render = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Screen)).BeginInit();
            this.SuspendLayout();
            // 
            // Screen
            // 
            this.Screen.Location = new System.Drawing.Point(104, 61);
            this.Screen.Name = "Screen";
            this.Screen.Size = new System.Drawing.Size(684, 377);
            this.Screen.TabIndex = 0;
            this.Screen.TabStop = false;
            // 
            // Render
            // 
            this.Render.Location = new System.Drawing.Point(668, 22);
            this.Render.Name = "Render";
            this.Render.Size = new System.Drawing.Size(75, 23);
            this.Render.TabIndex = 1;
            this.Render.Text = "RENDER";
            this.Render.UseVisualStyleBackColor = true;
            this.Render.Click += new System.EventHandler(this.Render_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Render);
            this.Controls.Add(this.Screen);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Screen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Screen;
        private System.Windows.Forms.Button Render;
    }
}

