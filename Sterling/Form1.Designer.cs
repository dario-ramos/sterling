namespace Sterling
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
            this.button1 = new System.Windows.Forms.Button();
            this.BuyRadioButton = new System.Windows.Forms.RadioButton();
            this.SellRadioButton = new System.Windows.Forms.RadioButton();
            this.PriceTextBox = new System.Windows.Forms.TextBox();
            this.QuantTextBox = new System.Windows.Forms.TextBox();
            this.SymbolTextBox = new System.Windows.Forms.TextBox();
            this.ExchangeTextBox = new System.Windows.Forms.TextBox();
            this.RTextBox = new System.Windows.Forms.TextBox();
            this.STextBox = new System.Windows.Forms.TextBox();
            this.StopButton = new System.Windows.Forms.Button();
            this.StopAllButton = new System.Windows.Forms.Button();
            runningDataGridView = new System.Windows.Forms.DataGridView();
            this.RunningSymbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RunningPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RunningQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RunningLTP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RunningSL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RunningPL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            stoppedDataGridView = new System.Windows.Forms.DataGridView();
            this.StoppedSymbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StoppedPL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StoppedPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StoppedQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StoppedLTP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            LogTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.DPRTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.StrategyComboBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.BreakButton = new System.Windows.Forms.Button();
            this.NTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.acctTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(runningDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(stoppedDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(37, 538);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 58);
            this.button1.TabIndex = 12;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // BuyRadioButton
            // 
            this.BuyRadioButton.AutoSize = true;
            this.BuyRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuyRadioButton.Location = new System.Drawing.Point(30, 484);
            this.BuyRadioButton.Name = "BuyRadioButton";
            this.BuyRadioButton.Size = new System.Drawing.Size(59, 24);
            this.BuyRadioButton.TabIndex = 10;
            this.BuyRadioButton.TabStop = true;
            this.BuyRadioButton.Text = "Buy";
            this.BuyRadioButton.UseVisualStyleBackColor = true;
            // 
            // SellRadioButton
            // 
            this.SellRadioButton.AutoSize = true;
            this.SellRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SellRadioButton.Location = new System.Drawing.Point(143, 484);
            this.SellRadioButton.Name = "SellRadioButton";
            this.SellRadioButton.Size = new System.Drawing.Size(58, 24);
            this.SellRadioButton.TabIndex = 11;
            this.SellRadioButton.TabStop = true;
            this.SellRadioButton.Text = "Sell";
            this.SellRadioButton.UseVisualStyleBackColor = true;
            // 
            // PriceTextBox
            // 
            this.PriceTextBox.Location = new System.Drawing.Point(130, 208);
            this.PriceTextBox.Name = "PriceTextBox";
            this.PriceTextBox.Size = new System.Drawing.Size(100, 22);
            this.PriceTextBox.TabIndex = 4;
            // 
            // QuantTextBox
            // 
            this.QuantTextBox.Location = new System.Drawing.Point(130, 111);
            this.QuantTextBox.Name = "QuantTextBox";
            this.QuantTextBox.Size = new System.Drawing.Size(100, 22);
            this.QuantTextBox.TabIndex = 2;
            // 
            // SymbolTextBox
            // 
            this.SymbolTextBox.Location = new System.Drawing.Point(130, 309);
            this.SymbolTextBox.Name = "SymbolTextBox";
            this.SymbolTextBox.Size = new System.Drawing.Size(100, 22);
            this.SymbolTextBox.TabIndex = 6;
            // 
            // ExchangeTextBox
            // 
            this.ExchangeTextBox.Location = new System.Drawing.Point(130, 359);
            this.ExchangeTextBox.Name = "ExchangeTextBox";
            this.ExchangeTextBox.Size = new System.Drawing.Size(100, 22);
            this.ExchangeTextBox.TabIndex = 7;
            // 
            // RTextBox
            // 
            this.RTextBox.Location = new System.Drawing.Point(130, 401);
            this.RTextBox.Name = "RTextBox";
            this.RTextBox.Size = new System.Drawing.Size(100, 22);
            this.RTextBox.TabIndex = 8;
            // 
            // STextBox
            // 
            this.STextBox.Location = new System.Drawing.Point(130, 443);
            this.STextBox.Name = "STextBox";
            this.STextBox.Size = new System.Drawing.Size(100, 22);
            this.STextBox.TabIndex = 9;
            // 
            // StopButton
            // 
            this.StopButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopButton.Location = new System.Drawing.Point(795, 31);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(138, 38);
            this.StopButton.TabIndex = 14;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // StopAllButton
            // 
            this.StopAllButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopAllButton.Location = new System.Drawing.Point(969, 31);
            this.StopAllButton.Name = "StopAllButton";
            this.StopAllButton.Size = new System.Drawing.Size(138, 38);
            this.StopAllButton.TabIndex = 15;
            this.StopAllButton.Text = "Stop All";
            this.StopAllButton.UseVisualStyleBackColor = true;
            this.StopAllButton.Click += new System.EventHandler(this.StopAllButton_Click);
            // 
            // runningDataGridView
            // 
            runningDataGridView.AllowUserToDeleteRows = false;
            runningDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            runningDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RunningSymbol,
            this.RunningPrice,
            this.RunningQuantity,
            this.RunningLTP,
            this.RunningSL,
            this.RunningPL});
            runningDataGridView.Location = new System.Drawing.Point(271, 92);
            runningDataGridView.MultiSelect = false;
            runningDataGridView.Name = "runningDataGridView";
            runningDataGridView.ReadOnly = true;
            runningDataGridView.RowTemplate.Height = 24;
            runningDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            runningDataGridView.Size = new System.Drawing.Size(836, 307);
            runningDataGridView.TabIndex = 0;
            // 
            // RunningSymbol
            // 
            this.RunningSymbol.HeaderText = "Symbol";
            this.RunningSymbol.Name = "RunningSymbol";
            this.RunningSymbol.ReadOnly = true;
            // 
            // RunningPrice
            // 
            this.RunningPrice.HeaderText = "Price";
            this.RunningPrice.Name = "RunningPrice";
            this.RunningPrice.ReadOnly = true;
            // 
            // RunningQuantity
            // 
            this.RunningQuantity.HeaderText = "Quantity";
            this.RunningQuantity.Name = "RunningQuantity";
            this.RunningQuantity.ReadOnly = true;
            // 
            // RunningLTP
            // 
            this.RunningLTP.HeaderText = "LTP";
            this.RunningLTP.Name = "RunningLTP";
            this.RunningLTP.ReadOnly = true;
            // 
            // RunningSL
            // 
            this.RunningSL.HeaderText = "SL";
            this.RunningSL.Name = "RunningSL";
            this.RunningSL.ReadOnly = true;
            // 
            // RunningPL
            // 
            this.RunningPL.HeaderText = "PL";
            this.RunningPL.Name = "RunningPL";
            this.RunningPL.ReadOnly = true;
            // 
            // stoppedDataGridView
            // 
            stoppedDataGridView.AllowUserToDeleteRows = false;
            stoppedDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            stoppedDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StoppedSymbol,
            this.StoppedPL});
            stoppedDataGridView.Location = new System.Drawing.Point(1177, 92);
            stoppedDataGridView.MultiSelect = false;
            stoppedDataGridView.Name = "stoppedDataGridView";
            stoppedDataGridView.ReadOnly = true;
            stoppedDataGridView.RowHeadersVisible = false;
            stoppedDataGridView.RowTemplate.Height = 24;
            stoppedDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            stoppedDataGridView.Size = new System.Drawing.Size(203, 307);
            stoppedDataGridView.TabIndex = 0;
            // 
            // StoppedSymbol
            // 
            this.StoppedSymbol.HeaderText = "Symbol";
            this.StoppedSymbol.Name = "StoppedSymbol";
            this.StoppedSymbol.ReadOnly = true;
            // 
            // StoppedPL
            // 
            this.StoppedPL.HeaderText = "PL";
            this.StoppedPL.Name = "StoppedPL";
            this.StoppedPL.ReadOnly = true;
            // 
            // StoppedPrice
            // 
            this.StoppedPrice.HeaderText = "Price";
            this.StoppedPrice.Name = "StoppedPrice";
            this.StoppedPrice.ReadOnly = true;
            // 
            // StoppedQty
            // 
            this.StoppedQty.HeaderText = "Quantity";
            this.StoppedQty.Name = "StoppedQty";
            this.StoppedQty.ReadOnly = true;
            // 
            // StoppedLTP
            // 
            this.StoppedLTP.HeaderText = "LTP";
            this.StoppedLTP.Name = "StoppedLTP";
            this.StoppedLTP.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(51, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 24);
            this.label1.TabIndex = 16;
            this.label1.Text = "DPR";
            // 
            // LogTextBox
            // 
            LogTextBox.Location = new System.Drawing.Point(271, 424);
            LogTextBox.Multiline = true;
            LogTextBox.Name = "LogTextBox";
            LogTextBox.ReadOnly = true;
            LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            LogTextBox.Size = new System.Drawing.Size(1109, 193);
            LogTextBox.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(51, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 24);
            this.label2.TabIndex = 18;
            this.label2.Text = "Price";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(38, 307);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 24);
            this.label3.TabIndex = 19;
            this.label3.Text = "Symbol";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 357);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 24);
            this.label4.TabIndex = 20;
            this.label4.Text = "Exchange";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(40, 399);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 24);
            this.label5.TabIndex = 21;
            this.label5.Text = "R/Entry";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(51, 441);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 24);
            this.label6.TabIndex = 22;
            this.label6.Text = "S/Qty";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(324, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 25);
            this.label7.TabIndex = 0;
            this.label7.Text = "RUNNING";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1272, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 25);
            this.label8.TabIndex = 0;
            this.label8.Text = "STOPPED";
            // 
            // DPRTextBox
            // 
            this.DPRTextBox.Location = new System.Drawing.Point(130, 160);
            this.DPRTextBox.Name = "DPRTextBox";
            this.DPRTextBox.Size = new System.Drawing.Size(100, 22);
            this.DPRTextBox.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(33, 111);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 24);
            this.label9.TabIndex = 26;
            this.label9.Text = "Quantity";
            // 
            // StrategyComboBox
            // 
            this.StrategyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StrategyComboBox.FormattingEnabled = true;
            this.StrategyComboBox.Items.AddRange(new object[] {
            "IQ_RSL",
            "IQ_Increment_Qty",
            "DSP"});
            this.StrategyComboBox.Location = new System.Drawing.Point(120, 45);
            this.StrategyComboBox.Name = "StrategyComboBox";
            this.StrategyComboBox.Size = new System.Drawing.Size(121, 24);
            this.StrategyComboBox.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(29, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 24);
            this.label11.TabIndex = 30;
            this.label11.Text = "Strategy";
            // 
            // BreakButton
            // 
            this.BreakButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BreakButton.Location = new System.Drawing.Point(629, 31);
            this.BreakButton.Name = "BreakButton";
            this.BreakButton.Size = new System.Drawing.Size(138, 38);
            this.BreakButton.TabIndex = 13;
            this.BreakButton.Text = "Break";
            this.BreakButton.UseVisualStyleBackColor = true;
            this.BreakButton.Click += new System.EventHandler(this.BreakButton_Click);
            // 
            // NTextBox
            // 
            this.NTextBox.Location = new System.Drawing.Point(130, 261);
            this.NTextBox.Name = "NTextBox";
            this.NTextBox.Size = new System.Drawing.Size(100, 22);
            this.NTextBox.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(75, 259);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 24);
            this.label10.TabIndex = 33;
            this.label10.Text = "N";
            // 
            // acctTextBox
            // 
            this.acctTextBox.Location = new System.Drawing.Point(1463, 595);
            this.acctTextBox.Name = "acctTextBox";
            this.acctTextBox.Size = new System.Drawing.Size(100, 22);
            this.acctTextBox.TabIndex = 34;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(1459, 557);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 24);
            this.label12.TabIndex = 35;
            this.label12.Text = "Account";
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(462, 31);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(138, 38);
            this.cancelButton.TabIndex = 36;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1670, 844);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.acctTextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.NTextBox);
            this.Controls.Add(this.BreakButton);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.StrategyComboBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.DPRTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(LogTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(stoppedDataGridView);
            this.Controls.Add(runningDataGridView);
            this.Controls.Add(this.StopAllButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.STextBox);
            this.Controls.Add(this.RTextBox);
            this.Controls.Add(this.ExchangeTextBox);
            this.Controls.Add(this.SymbolTextBox);
            this.Controls.Add(this.QuantTextBox);
            this.Controls.Add(this.PriceTextBox);
            this.Controls.Add(this.SellRadioButton);
            this.Controls.Add(this.BuyRadioButton);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(runningDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(stoppedDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton BuyRadioButton;
        private System.Windows.Forms.RadioButton SellRadioButton;
        private System.Windows.Forms.TextBox PriceTextBox;
        private System.Windows.Forms.TextBox QuantTextBox;
        private System.Windows.Forms.TextBox SymbolTextBox;
        private System.Windows.Forms.TextBox ExchangeTextBox;
        private System.Windows.Forms.TextBox RTextBox;
        private System.Windows.Forms.TextBox STextBox;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button StopAllButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox DPRTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox StrategyComboBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewTextBoxColumn RunningSymbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn RunningPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn RunningQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn RunningLTP;
        private System.Windows.Forms.DataGridViewTextBoxColumn RunningSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn RunningPL;
        private System.Windows.Forms.DataGridViewTextBoxColumn StoppedSymbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn StoppedPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn StoppedQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn StoppedLTP;
        private System.Windows.Forms.DataGridViewTextBoxColumn StoppedPL;
        private System.Windows.Forms.Button BreakButton;
        private System.Windows.Forms.TextBox NTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox acctTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button cancelButton;
        public static System.Windows.Forms.DataGridView runningDataGridView;
        public static System.Windows.Forms.DataGridView stoppedDataGridView;
        public static System.Windows.Forms.TextBox LogTextBox;
    }
}

