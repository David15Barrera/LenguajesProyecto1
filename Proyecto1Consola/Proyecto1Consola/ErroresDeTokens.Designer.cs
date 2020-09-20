namespace Proyecto1Consola
{
    partial class ErroresDeTokens
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErroresDeTokens));
            this.grilla_error = new System.Windows.Forms.DataGridView();
            this.numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.err = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grilla_error)).BeginInit();
            this.SuspendLayout();
            // 
            // grilla_error
            // 
            this.grilla_error.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grilla_error.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numero,
            this.err});
            this.grilla_error.Location = new System.Drawing.Point(0, 22);
            this.grilla_error.Name = "grilla_error";
            this.grilla_error.Size = new System.Drawing.Size(713, 115);
            this.grilla_error.TabIndex = 0;
            this.grilla_error.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grilla_error_CellContentClick);
            // 
            // numero
            // 
            this.numero.HeaderText = "Fila";
            this.numero.Name = "numero";
            // 
            // err
            // 
            this.err.HeaderText = "Errores de compilacion";
            this.err.MinimumWidth = 10;
            this.err.Name = "err";
            this.err.Width = 600;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(337, 161);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Guardar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ErroresDeTokens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Proyecto1Consola.Properties.Resources.Fondo1;
            this.ClientSize = new System.Drawing.Size(725, 215);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.grilla_error);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ErroresDeTokens";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Muestra de Errores";
            this.Load += new System.EventHandler(this.FormMostrarErrores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grilla_error)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView grilla_error;
        private System.Windows.Forms.DataGridViewTextBoxColumn numero;
        private System.Windows.Forms.DataGridViewTextBoxColumn err;
        private System.Windows.Forms.Button button1;
    }
}