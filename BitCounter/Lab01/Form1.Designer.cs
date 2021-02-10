namespace Lab01
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
            this.UI_gb_FileDetails = new System.Windows.Forms.GroupBox();
            this.UI_Btn_SelectFile = new System.Windows.Forms.Button();
            this.UI_tBx_FileSize = new System.Windows.Forms.TextBox();
            this.UI_tbx_FileName = new System.Windows.Forms.TextBox();
            this.UI_LBL_FileSize = new System.Windows.Forms.Label();
            this.UI_LBL_FileName = new System.Windows.Forms.Label();
            this.UI_gb_1sPercent = new System.Windows.Forms.GroupBox();
            this.UI_tbx_PecentStatus = new System.Windows.Forms.TextBox();
            this.UI_pbar_1sPercent = new System.Windows.Forms.ProgressBar();
            this.UI_tbx_ByteCount = new System.Windows.Forms.GroupBox();
            this.UI_lv_Btyes = new System.Windows.Forms.ListView();
            this.UI_lv_byteValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UI_lv_Count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UI_btn_reset = new System.Windows.Forms.Button();
            this.UI_gb_FileDetails.SuspendLayout();
            this.UI_gb_1sPercent.SuspendLayout();
            this.UI_tbx_ByteCount.SuspendLayout();
            this.SuspendLayout();
            // 
            // UI_gb_FileDetails
            // 
            this.UI_gb_FileDetails.Controls.Add(this.UI_btn_reset);
            this.UI_gb_FileDetails.Controls.Add(this.UI_Btn_SelectFile);
            this.UI_gb_FileDetails.Controls.Add(this.UI_tBx_FileSize);
            this.UI_gb_FileDetails.Controls.Add(this.UI_tbx_FileName);
            this.UI_gb_FileDetails.Controls.Add(this.UI_LBL_FileSize);
            this.UI_gb_FileDetails.Controls.Add(this.UI_LBL_FileName);
            this.UI_gb_FileDetails.Location = new System.Drawing.Point(13, 13);
            this.UI_gb_FileDetails.Name = "UI_gb_FileDetails";
            this.UI_gb_FileDetails.Size = new System.Drawing.Size(430, 100);
            this.UI_gb_FileDetails.TabIndex = 0;
            this.UI_gb_FileDetails.TabStop = false;
            this.UI_gb_FileDetails.Text = "File Details";
            // 
            // UI_Btn_SelectFile
            // 
            this.UI_Btn_SelectFile.Location = new System.Drawing.Point(349, 72);
            this.UI_Btn_SelectFile.Name = "UI_Btn_SelectFile";
            this.UI_Btn_SelectFile.Size = new System.Drawing.Size(75, 23);
            this.UI_Btn_SelectFile.TabIndex = 4;
            this.UI_Btn_SelectFile.Text = "Select File";
            this.UI_Btn_SelectFile.UseVisualStyleBackColor = true;
            // 
            // UI_tBx_FileSize
            // 
            this.UI_tBx_FileSize.Location = new System.Drawing.Point(71, 46);
            this.UI_tBx_FileSize.Name = "UI_tBx_FileSize";
            this.UI_tBx_FileSize.ReadOnly = true;
            this.UI_tBx_FileSize.Size = new System.Drawing.Size(353, 20);
            this.UI_tBx_FileSize.TabIndex = 3;
            // 
            // UI_tbx_FileName
            // 
            this.UI_tbx_FileName.Location = new System.Drawing.Point(71, 19);
            this.UI_tbx_FileName.Name = "UI_tbx_FileName";
            this.UI_tbx_FileName.ReadOnly = true;
            this.UI_tbx_FileName.Size = new System.Drawing.Size(353, 20);
            this.UI_tbx_FileName.TabIndex = 2;
            // 
            // UI_LBL_FileSize
            // 
            this.UI_LBL_FileSize.AutoSize = true;
            this.UI_LBL_FileSize.Location = new System.Drawing.Point(7, 49);
            this.UI_LBL_FileSize.Name = "UI_LBL_FileSize";
            this.UI_LBL_FileSize.Size = new System.Drawing.Size(52, 13);
            this.UI_LBL_FileSize.TabIndex = 1;
            this.UI_LBL_FileSize.Text = "File Size: ";
            // 
            // UI_LBL_FileName
            // 
            this.UI_LBL_FileName.AutoSize = true;
            this.UI_LBL_FileName.Location = new System.Drawing.Point(7, 22);
            this.UI_LBL_FileName.Name = "UI_LBL_FileName";
            this.UI_LBL_FileName.Size = new System.Drawing.Size(57, 13);
            this.UI_LBL_FileName.TabIndex = 0;
            this.UI_LBL_FileName.Text = "File Name:";
            // 
            // UI_gb_1sPercent
            // 
            this.UI_gb_1sPercent.Controls.Add(this.UI_tbx_PecentStatus);
            this.UI_gb_1sPercent.Controls.Add(this.UI_pbar_1sPercent);
            this.UI_gb_1sPercent.Location = new System.Drawing.Point(13, 119);
            this.UI_gb_1sPercent.Name = "UI_gb_1sPercent";
            this.UI_gb_1sPercent.Size = new System.Drawing.Size(430, 82);
            this.UI_gb_1sPercent.TabIndex = 1;
            this.UI_gb_1sPercent.TabStop = false;
            this.UI_gb_1sPercent.Text = "1s Percentage";
            // 
            // UI_tbx_PecentStatus
            // 
            this.UI_tbx_PecentStatus.Location = new System.Drawing.Point(7, 50);
            this.UI_tbx_PecentStatus.Name = "UI_tbx_PecentStatus";
            this.UI_tbx_PecentStatus.ReadOnly = true;
            this.UI_tbx_PecentStatus.Size = new System.Drawing.Size(417, 20);
            this.UI_tbx_PecentStatus.TabIndex = 1;
            this.UI_tbx_PecentStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UI_pbar_1sPercent
            // 
            this.UI_pbar_1sPercent.Location = new System.Drawing.Point(7, 20);
            this.UI_pbar_1sPercent.Maximum = 0;
            this.UI_pbar_1sPercent.Name = "UI_pbar_1sPercent";
            this.UI_pbar_1sPercent.Size = new System.Drawing.Size(417, 23);
            this.UI_pbar_1sPercent.Step = 1;
            this.UI_pbar_1sPercent.TabIndex = 0;
            // 
            // UI_tbx_ByteCount
            // 
            this.UI_tbx_ByteCount.Controls.Add(this.UI_lv_Btyes);
            this.UI_tbx_ByteCount.Location = new System.Drawing.Point(13, 207);
            this.UI_tbx_ByteCount.Name = "UI_tbx_ByteCount";
            this.UI_tbx_ByteCount.Size = new System.Drawing.Size(430, 206);
            this.UI_tbx_ByteCount.TabIndex = 2;
            this.UI_tbx_ByteCount.TabStop = false;
            this.UI_tbx_ByteCount.Text = "Byte Counts";
            // 
            // UI_lv_Btyes
            // 
            this.UI_lv_Btyes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.UI_lv_byteValue,
            this.UI_lv_Count});
            this.UI_lv_Btyes.GridLines = true;
            this.UI_lv_Btyes.HideSelection = false;
            this.UI_lv_Btyes.Location = new System.Drawing.Point(7, 20);
            this.UI_lv_Btyes.Name = "UI_lv_Btyes";
            this.UI_lv_Btyes.Size = new System.Drawing.Size(417, 186);
            this.UI_lv_Btyes.TabIndex = 0;
            this.UI_lv_Btyes.UseCompatibleStateImageBehavior = false;
            this.UI_lv_Btyes.View = System.Windows.Forms.View.Details;
            // 
            // UI_lv_byteValue
            // 
            this.UI_lv_byteValue.Text = "Byte Value";
            this.UI_lv_byteValue.Width = 98;
            // 
            // UI_lv_Count
            // 
            this.UI_lv_Count.Text = "Count";
            this.UI_lv_Count.Width = 314;
            // 
            // UI_btn_reset
            // 
            this.UI_btn_reset.Location = new System.Drawing.Point(268, 73);
            this.UI_btn_reset.Name = "UI_btn_reset";
            this.UI_btn_reset.Size = new System.Drawing.Size(75, 23);
            this.UI_btn_reset.TabIndex = 5;
            this.UI_btn_reset.Text = "Reset";
            this.UI_btn_reset.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 425);
            this.Controls.Add(this.UI_tbx_ByteCount);
            this.Controls.Add(this.UI_gb_1sPercent);
            this.Controls.Add(this.UI_gb_FileDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "FileThingy";
            this.UI_gb_FileDetails.ResumeLayout(false);
            this.UI_gb_FileDetails.PerformLayout();
            this.UI_gb_1sPercent.ResumeLayout(false);
            this.UI_gb_1sPercent.PerformLayout();
            this.UI_tbx_ByteCount.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox UI_gb_FileDetails;
        private System.Windows.Forms.Button UI_Btn_SelectFile;
        private System.Windows.Forms.TextBox UI_tBx_FileSize;
        private System.Windows.Forms.TextBox UI_tbx_FileName;
        private System.Windows.Forms.Label UI_LBL_FileSize;
        private System.Windows.Forms.Label UI_LBL_FileName;
        private System.Windows.Forms.GroupBox UI_gb_1sPercent;
        private System.Windows.Forms.TextBox UI_tbx_PecentStatus;
        private System.Windows.Forms.ProgressBar UI_pbar_1sPercent;
        private System.Windows.Forms.GroupBox UI_tbx_ByteCount;
        private System.Windows.Forms.ListView UI_lv_Btyes;
        private System.Windows.Forms.ColumnHeader UI_lv_byteValue;
        private System.Windows.Forms.ColumnHeader UI_lv_Count;
        private System.Windows.Forms.Button UI_btn_reset;
    }
}

