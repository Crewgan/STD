namespace Ex3
{
    partial class FrmMain
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpenfile = new System.Windows.Forms.Button();
            this.tbxData = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOpenfile
            // 
            this.btnOpenfile.Location = new System.Drawing.Point(12, 12);
            this.btnOpenfile.Name = "btnOpenfile";
            this.btnOpenfile.Size = new System.Drawing.Size(75, 23);
            this.btnOpenfile.TabIndex = 0;
            this.btnOpenfile.Text = "Open file";
            this.btnOpenfile.UseVisualStyleBackColor = true;
            this.btnOpenfile.Click += new System.EventHandler(this.BtnOpenfile_Click);
            // 
            // tbxData
            // 
            this.tbxData.Location = new System.Drawing.Point(12, 41);
            this.tbxData.Multiline = true;
            this.tbxData.Name = "tbxData";
            this.tbxData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxData.Size = new System.Drawing.Size(360, 207);
            this.tbxData.TabIndex = 2;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 260);
            this.Controls.Add(this.tbxData);
            this.Controls.Add(this.btnOpenfile);
            this.Name = "FrmMain";
            this.Text = "Zip reader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenfile;
        private System.Windows.Forms.TextBox tbxData;
    }
}

