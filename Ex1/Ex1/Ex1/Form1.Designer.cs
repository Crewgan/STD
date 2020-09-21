namespace Ex1
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
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.pcbImage = new System.Windows.Forms.PictureBox();
            this.btnRevertImage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSizeBitsOfPicture = new System.Windows.Forms.Label();
            this.lblBitByPixel = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblSizeBits = new System.Windows.Forms.Label();
            this.lblSignature = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pcbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(33, 12);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "Select file";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // pcbImage
            // 
            this.pcbImage.Location = new System.Drawing.Point(33, 41);
            this.pcbImage.Name = "pcbImage";
            this.pcbImage.Size = new System.Drawing.Size(484, 513);
            this.pcbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbImage.TabIndex = 1;
            this.pcbImage.TabStop = false;
            // 
            // btnRevertImage
            // 
            this.btnRevertImage.Location = new System.Drawing.Point(114, 12);
            this.btnRevertImage.Name = "btnRevertImage";
            this.btnRevertImage.Size = new System.Drawing.Size(75, 23);
            this.btnRevertImage.TabIndex = 2;
            this.btnRevertImage.Text = "Invert";
            this.btnRevertImage.UseVisualStyleBackColor = true;
            this.btnRevertImage.Click += new System.EventHandler(this.btnRevertImage_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(523, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Signature :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(523, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Taille en octets :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(523, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Largeur :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(523, 308);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Hauteur :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(523, 376);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 26);
            this.label5.TabIndex = 7;
            this.label5.Text = "nombres de \r\nbits par pixel :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(523, 444);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 26);
            this.label6.TabIndex = 8;
            this.label6.Text = "Taille en octets des \r\ndonnées de l\'image :";
            // 
            // lblSizeBitsOfPicture
            // 
            this.lblSizeBitsOfPicture.AutoSize = true;
            this.lblSizeBitsOfPicture.Location = new System.Drawing.Point(647, 444);
            this.lblSizeBitsOfPicture.Name = "lblSizeBitsOfPicture";
            this.lblSizeBitsOfPicture.Size = new System.Drawing.Size(0, 13);
            this.lblSizeBitsOfPicture.TabIndex = 14;
            // 
            // lblBitByPixel
            // 
            this.lblBitByPixel.AutoSize = true;
            this.lblBitByPixel.Location = new System.Drawing.Point(647, 376);
            this.lblBitByPixel.Name = "lblBitByPixel";
            this.lblBitByPixel.Size = new System.Drawing.Size(0, 13);
            this.lblBitByPixel.TabIndex = 13;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(647, 308);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(0, 13);
            this.lblHeight.TabIndex = 12;
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(647, 240);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(0, 13);
            this.lblWidth.TabIndex = 11;
            // 
            // lblSizeBits
            // 
            this.lblSizeBits.AutoSize = true;
            this.lblSizeBits.Location = new System.Drawing.Point(647, 172);
            this.lblSizeBits.Name = "lblSizeBits";
            this.lblSizeBits.Size = new System.Drawing.Size(0, 13);
            this.lblSizeBits.TabIndex = 10;
            // 
            // lblSignature
            // 
            this.lblSignature.AutoSize = true;
            this.lblSignature.Location = new System.Drawing.Point(647, 104);
            this.lblSignature.Name = "lblSignature";
            this.lblSignature.Size = new System.Drawing.Size(0, 13);
            this.lblSignature.TabIndex = 9;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 561);
            this.Controls.Add(this.lblSizeBitsOfPicture);
            this.Controls.Add(this.lblBitByPixel);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.lblWidth);
            this.Controls.Add(this.lblSizeBits);
            this.Controls.Add(this.lblSignature);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRevertImage);
            this.Controls.Add(this.pcbImage);
            this.Controls.Add(this.btnOpenFile);
            this.Name = "FrmMain";
            this.Text = "Exercice 01";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.PictureBox pcbImage;
        private System.Windows.Forms.Button btnRevertImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSizeBitsOfPicture;
        private System.Windows.Forms.Label lblBitByPixel;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblSizeBits;
        private System.Windows.Forms.Label lblSignature;
    }
}

