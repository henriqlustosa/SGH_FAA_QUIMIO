namespace FAA_SGH
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lbEntrada = new System.Windows.Forms.Label();
            this.btImprimir = new System.Windows.Forms.Button();
            this.lbError = new System.Windows.Forms.Label();
            this.txtFAA = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbEntrada
            // 
            this.lbEntrada.AutoSize = true;
            this.lbEntrada.Location = new System.Drawing.Point(177, 83);
            this.lbEntrada.Name = "lbEntrada";
            this.lbEntrada.Size = new System.Drawing.Size(118, 13);
            this.lbEntrada.TabIndex = 0;
            this.lbEntrada.Text = "Entre com o nº da FAA:";
            // 
            // btImprimir
            // 
            this.btImprimir.Location = new System.Drawing.Point(296, 215);
            this.btImprimir.Name = "btImprimir";
            this.btImprimir.Size = new System.Drawing.Size(75, 23);
            this.btImprimir.TabIndex = 1;
            this.btImprimir.Text = "Imprimir";
            this.btImprimir.UseVisualStyleBackColor = true;
            this.btImprimir.Click += new System.EventHandler(this.btImprimir_Click);
            // 
            // lbError
            // 
            this.lbError.AutoSize = true;
            this.lbError.ForeColor = System.Drawing.Color.Red;
            this.lbError.Location = new System.Drawing.Point(177, 156);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(35, 13);
            this.lbError.TabIndex = 2;
            this.lbError.Text = "label1";
            // 
            // txtFAA
            // 
            this.txtFAA.Location = new System.Drawing.Point(346, 83);
            this.txtFAA.Name = "txtFAA";
            this.txtFAA.Size = new System.Drawing.Size(100, 20);
            this.txtFAA.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 300);
            this.Controls.Add(this.txtFAA);
            this.Controls.Add(this.lbError);
            this.Controls.Add(this.btImprimir);
            this.Controls.Add(this.lbEntrada);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "  Impressão de FAA";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbEntrada;
        private System.Windows.Forms.Button btImprimir;
        private System.Windows.Forms.Label lbError;
        private System.Windows.Forms.TextBox txtFAA;
    }
}

