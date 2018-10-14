namespace ContentFileFind
{
    partial class FindFileForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txtFilepath = new System.Windows.Forms.TextBox();
            this.selectFile = new System.Windows.Forms.Button();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.txt_KeyValue = new System.Windows.Forms.TextBox();
            this.btn_SearchKeyValue = new System.Windows.Forms.Button();
            this.btn_ClearResult = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFilepath
            // 
            this.txtFilepath.Location = new System.Drawing.Point(11, 5);
            this.txtFilepath.Name = "txtFilepath";
            this.txtFilepath.Size = new System.Drawing.Size(293, 21);
            this.txtFilepath.TabIndex = 1;
            // 
            // selectFile
            // 
            this.selectFile.Location = new System.Drawing.Point(310, 5);
            this.selectFile.Name = "selectFile";
            this.selectFile.Size = new System.Drawing.Size(75, 23);
            this.selectFile.TabIndex = 2;
            this.selectFile.Text = "选择";
            this.selectFile.UseVisualStyleBackColor = true;
            this.selectFile.Click += new System.EventHandler(this.selectFile_Click);
            // 
            // btn_Reset
            // 
            this.btn_Reset.Location = new System.Drawing.Point(391, 5);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(75, 23);
            this.btn_Reset.TabIndex = 4;
            this.btn_Reset.Text = "重置";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // listBox2
            // 
            this.listBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.HorizontalScrollbar = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(3, 17);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(469, 403);
            this.listBox2.TabIndex = 5;
            this.listBox2.DoubleClick += new System.EventHandler(this.listBox_DoubleClick);
            // 
            // txt_KeyValue
            // 
            this.txt_KeyValue.Location = new System.Drawing.Point(12, 39);
            this.txt_KeyValue.Name = "txt_KeyValue";
            this.txt_KeyValue.Size = new System.Drawing.Size(292, 21);
            this.txt_KeyValue.TabIndex = 6;
            // 
            // btn_SearchKeyValue
            // 
            this.btn_SearchKeyValue.Location = new System.Drawing.Point(310, 39);
            this.btn_SearchKeyValue.Name = "btn_SearchKeyValue";
            this.btn_SearchKeyValue.Size = new System.Drawing.Size(75, 23);
            this.btn_SearchKeyValue.TabIndex = 7;
            this.btn_SearchKeyValue.Text = "搜索内容";
            this.btn_SearchKeyValue.UseVisualStyleBackColor = true;
            this.btn_SearchKeyValue.Click += new System.EventHandler(this.btn_SearchKeyValue_Click);
            // 
            // btn_ClearResult
            // 
            this.btn_ClearResult.Location = new System.Drawing.Point(391, 39);
            this.btn_ClearResult.Name = "btn_ClearResult";
            this.btn_ClearResult.Size = new System.Drawing.Size(75, 23);
            this.btn_ClearResult.TabIndex = 8;
            this.btn_ClearResult.Text = "清空";
            this.btn_ClearResult.UseVisualStyleBackColor = true;
            this.btn_ClearResult.Click += new System.EventHandler(this.btn_ClearResult_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 423);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查找结果";
            // 
            // FindFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 489);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_ClearResult);
            this.Controls.Add(this.btn_SearchKeyValue);
            this.Controls.Add(this.txt_KeyValue);
            this.Controls.Add(this.btn_Reset);
            this.Controls.Add(this.selectFile);
            this.Controls.Add(this.txtFilepath);
            this.Name = "FindFileForm";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox txtFilepath;
        private System.Windows.Forms.Button selectFile;
        private System.Windows.Forms.Button btn_Reset;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.TextBox txt_KeyValue;
        private System.Windows.Forms.Button btn_SearchKeyValue;
        private System.Windows.Forms.Button btn_ClearResult;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

