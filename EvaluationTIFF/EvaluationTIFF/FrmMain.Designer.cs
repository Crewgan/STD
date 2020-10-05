namespace EvaluationTIFF
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
            this.tbxData = new System.Windows.Forms.TextBox();
            this.btnOpenfile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbxData
            // 
            this.tbxData.Location = new System.Drawing.Point(12, 41);
            this.tbxData.Multiline = true;
            this.tbxData.Name = "tbxData";
            this.tbxData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxData.Size = new System.Drawing.Size(364, 343);
            this.tbxData.TabIndex = 4;
            // 
            // btnOpenfile
            // 
            this.btnOpenfile.Location = new System.Drawing.Point(12, 12);
            this.btnOpenfile.Name = "btnOpenfile";
            this.btnOpenfile.Size = new System.Drawing.Size(75, 23);
            this.btnOpenfile.TabIndex = 3;
            this.btnOpenfile.Text = "Open file";
            this.btnOpenfile.UseVisualStyleBackColor = true;
            this.btnOpenfile.Click += new System.EventHandler(this.BtnOpenfile_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 396);
            this.Controls.Add(this.tbxData);
            this.Controls.Add(this.btnOpenfile);
            this.Name = "FrmMain";
            this.Text = "TIFF";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxData;
        private System.Windows.Forms.Button btnOpenfile;
    }
}

