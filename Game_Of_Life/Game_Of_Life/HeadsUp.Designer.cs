namespace Game_Of_Life
{
    partial class HeadsUp
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
            this.button1 = new System.Windows.Forms.Button();
            this.LivingCell = new System.Windows.Forms.Label();
            this.Generation = new System.Windows.Forms.Label();
            this.Counting = new System.Windows.Forms.Label();
            this.Rows = new System.Windows.Forms.Label();
            this.Columns = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(678, 99);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // LivingCell
            // 
            this.LivingCell.AutoSize = true;
            this.LivingCell.Location = new System.Drawing.Point(23, 13);
            this.LivingCell.Name = "LivingCell";
            this.LivingCell.Size = new System.Drawing.Size(72, 13);
            this.LivingCell.TabIndex = 1;
            this.LivingCell.Text = "Living Cells=0";
            // 
            // Generation
            // 
            this.Generation.AutoSize = true;
            this.Generation.Location = new System.Drawing.Point(26, 49);
            this.Generation.Name = "Generation";
            this.Generation.Size = new System.Drawing.Size(77, 13);
            this.Generation.TabIndex = 2;
            this.Generation.Text = "Generation = 0";
            // 
            // Counting
            // 
            this.Counting.AutoSize = true;
            this.Counting.Location = new System.Drawing.Point(26, 82);
            this.Counting.Name = "Counting";
            this.Counting.Size = new System.Drawing.Size(115, 13);
            this.Counting.TabIndex = 3;
            this.Counting.Text = "Boundary Style = Finite";
            // 
            // Rows
            // 
            this.Rows.AutoSize = true;
            this.Rows.Location = new System.Drawing.Point(230, 13);
            this.Rows.Name = "Rows";
            this.Rows.Size = new System.Drawing.Size(110, 13);
            this.Rows.TabIndex = 4;
            this.Rows.Text = "Number of Rows = 20";
            // 
            // Columns
            // 
            this.Columns.AutoSize = true;
            this.Columns.Location = new System.Drawing.Point(233, 49);
            this.Columns.Name = "Columns";
            this.Columns.Size = new System.Drawing.Size(123, 13);
            this.Columns.TabIndex = 5;
            this.Columns.Text = "Number of Columns = 20";
            // 
            // HeadsUp
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 134);
            this.Controls.Add(this.Columns);
            this.Controls.Add(this.Rows);
            this.Controls.Add(this.Counting);
            this.Controls.Add(this.Generation);
            this.Controls.Add(this.LivingCell);
            this.Controls.Add(this.button1);
            this.Name = "HeadsUp";
            this.Text = "Heads Up Display";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label LivingCell;
        private System.Windows.Forms.Label Generation;
        private System.Windows.Forms.Label Counting;
        private System.Windows.Forms.Label Rows;
        private System.Windows.Forms.Label Columns;
    }
}