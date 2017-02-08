namespace Quotes
{
    partial class MainForm
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
            this.dgvSymbols = new System.Windows.Forms.DataGridView();
            this.txtNewSymbol = new System.Windows.Forms.TextBox();
            this.btnRegisterSymbol = new System.Windows.Forms.Button();
            this.gbNewSymbol = new System.Windows.Forms.GroupBox();
            this.btnStopSymbol = new System.Windows.Forms.Button();
            this.btnStopAllSymbols = new System.Windows.Forms.Button();
            this.gbStopSymbols = new System.Windows.Forms.GroupBox();
            this.lblQuoteRefreshRate = new System.Windows.Forms.Label();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblProvider = new System.Windows.Forms.Label();
            this.Symbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSymbols)).BeginInit();
            this.gbNewSymbol.SuspendLayout();
            this.gbStopSymbols.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSymbols
            // 
            this.dgvSymbols.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSymbols.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Symbol,
            this.Quote,
            this.Time});
            this.dgvSymbols.Location = new System.Drawing.Point(12, 72);
            this.dgvSymbols.Name = "dgvSymbols";
            this.dgvSymbols.Size = new System.Drawing.Size(401, 475);
            this.dgvSymbols.TabIndex = 0;
            // 
            // txtNewSymbol
            // 
            this.txtNewSymbol.Location = new System.Drawing.Point(6, 19);
            this.txtNewSymbol.Name = "txtNewSymbol";
            this.txtNewSymbol.Size = new System.Drawing.Size(119, 20);
            this.txtNewSymbol.TabIndex = 1;
            // 
            // btnRegisterSymbol
            // 
            this.btnRegisterSymbol.Location = new System.Drawing.Point(131, 17);
            this.btnRegisterSymbol.Name = "btnRegisterSymbol";
            this.btnRegisterSymbol.Size = new System.Drawing.Size(82, 23);
            this.btnRegisterSymbol.TabIndex = 3;
            this.btnRegisterSymbol.Text = "Register";
            this.btnRegisterSymbol.UseVisualStyleBackColor = true;
            this.btnRegisterSymbol.Click += new System.EventHandler(this.btnRegisterSymbol_Click);
            // 
            // gbNewSymbol
            // 
            this.gbNewSymbol.Controls.Add(this.txtNewSymbol);
            this.gbNewSymbol.Controls.Add(this.btnRegisterSymbol);
            this.gbNewSymbol.Location = new System.Drawing.Point(12, 12);
            this.gbNewSymbol.Name = "gbNewSymbol";
            this.gbNewSymbol.Size = new System.Drawing.Size(219, 54);
            this.gbNewSymbol.TabIndex = 4;
            this.gbNewSymbol.TabStop = false;
            this.gbNewSymbol.Text = "New symbol";
            // 
            // btnStopSymbol
            // 
            this.btnStopSymbol.Location = new System.Drawing.Point(15, 17);
            this.btnStopSymbol.Name = "btnStopSymbol";
            this.btnStopSymbol.Size = new System.Drawing.Size(89, 23);
            this.btnStopSymbol.TabIndex = 5;
            this.btnStopSymbol.Text = "Stop";
            this.btnStopSymbol.UseVisualStyleBackColor = true;
            this.btnStopSymbol.Click += new System.EventHandler(this.btnStopSymbol_Click);
            // 
            // btnStopAllSymbols
            // 
            this.btnStopAllSymbols.Location = new System.Drawing.Point(123, 17);
            this.btnStopAllSymbols.Name = "btnStopAllSymbols";
            this.btnStopAllSymbols.Size = new System.Drawing.Size(93, 23);
            this.btnStopAllSymbols.TabIndex = 6;
            this.btnStopAllSymbols.Text = "Stop All";
            this.btnStopAllSymbols.UseVisualStyleBackColor = true;
            this.btnStopAllSymbols.Click += new System.EventHandler(this.btnStopAllSymbols_Click);
            // 
            // gbStopSymbols
            // 
            this.gbStopSymbols.Controls.Add(this.btnStopSymbol);
            this.gbStopSymbols.Controls.Add(this.btnStopAllSymbols);
            this.gbStopSymbols.Location = new System.Drawing.Point(246, 12);
            this.gbStopSymbols.Name = "gbStopSymbols";
            this.gbStopSymbols.Size = new System.Drawing.Size(231, 54);
            this.gbStopSymbols.TabIndex = 7;
            this.gbStopSymbols.TabStop = false;
            this.gbStopSymbols.Text = "Stop symbol(s)";
            // 
            // lblQuoteRefreshRate
            // 
            this.lblQuoteRefreshRate.AutoSize = true;
            this.lblQuoteRefreshRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuoteRefreshRate.Location = new System.Drawing.Point(381, 558);
            this.lblQuoteRefreshRate.Name = "lblQuoteRefreshRate";
            this.lblQuoteRefreshRate.Size = new System.Drawing.Size(177, 24);
            this.lblQuoteRefreshRate.TabIndex = 8;
            this.lblQuoteRefreshRate.Text = "Refresh Rate: N/A";
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(419, 72);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(254, 475);
            this.rtbLog.TabIndex = 9;
            this.rtbLog.Text = "";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(12, 566);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(68, 13);
            this.lblVersion.TabIndex = 12;
            this.lblVersion.Text = "Version: N/A";
            // 
            // lblProvider
            // 
            this.lblProvider.AutoSize = true;
            this.lblProvider.Location = new System.Drawing.Point(177, 566);
            this.lblProvider.Name = "lblProvider";
            this.lblProvider.Size = new System.Drawing.Size(72, 13);
            this.lblProvider.TabIndex = 13;
            this.lblProvider.Text = "Provider: N/A";
            // 
            // Symbol
            // 
            this.Symbol.HeaderText = "Symbol";
            this.Symbol.Name = "Symbol";
            // 
            // Quote
            // 
            this.Quote.HeaderText = "Quote";
            this.Quote.Name = "Quote";
            // 
            // Time
            // 
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            this.Time.Width = 140;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 591);
            this.Controls.Add(this.lblProvider);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.lblQuoteRefreshRate);
            this.Controls.Add(this.gbStopSymbols);
            this.Controls.Add(this.gbNewSymbol);
            this.Controls.Add(this.dgvSymbols);
            this.Name = "MainForm";
            this.Text = "Quotes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSymbols)).EndInit();
            this.gbNewSymbol.ResumeLayout(false);
            this.gbNewSymbol.PerformLayout();
            this.gbStopSymbols.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSymbols;
        private System.Windows.Forms.TextBox txtNewSymbol;
        private System.Windows.Forms.Button btnRegisterSymbol;
        private System.Windows.Forms.GroupBox gbNewSymbol;
        private System.Windows.Forms.Button btnStopSymbol;
        private System.Windows.Forms.Button btnStopAllSymbols;
        private System.Windows.Forms.GroupBox gbStopSymbols;
        private System.Windows.Forms.Label lblQuoteRefreshRate;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblProvider;
        private System.Windows.Forms.DataGridViewTextBoxColumn Symbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quote;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
    }
}

