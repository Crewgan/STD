namespace ExIkea
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
            this.components = new System.ComponentModel.Container();
            this.tmrDisplay = new System.Windows.Forms.Timer(this.components);
            this.pcbStore = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcbStore)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrDisplay
            // 
            this.tmrDisplay.Enabled = true;
            this.tmrDisplay.Interval = 10;
            this.tmrDisplay.Tick += new System.EventHandler(this.Display_Tick);
            // 
            // pcbStore
            // 
            this.pcbStore.Location = new System.Drawing.Point(12, 12);
            this.pcbStore.Name = "pcbStore";
            this.pcbStore.Size = new System.Drawing.Size(812, 536);
            this.pcbStore.TabIndex = 0;
            this.pcbStore.TabStop = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 560);
            this.Controls.Add(this.pcbStore);
            this.Name = "FrmMain";
            this.Text = "Ikea";
            ((System.ComponentModel.ISupportInitialize)(this.pcbStore)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrDisplay;
        private System.Windows.Forms.PictureBox pcbStore;
    }
}

