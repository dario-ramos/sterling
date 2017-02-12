namespace Sterling
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
            this._dgvStrategies = new System.Windows.Forms.DataGridView();
            this.Symbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Strategy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Side = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LTP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stoppedDataGridView = new System.Windows.Forms.DataGridView();
            this.StoppedSymbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StoppedPL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StoppedPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StoppedQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StoppedLTP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.LogTextBox = new System.Windows.Forms.TextBox();
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
            this.lblVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._dgvStrategies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stoppedDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(28, 437);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 47);
            this.button1.TabIndex = 12;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // BuyRadioButton
            // 
            this.BuyRadioButton.AutoSize = true;
            this.BuyRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuyRadioButton.Location = new System.Drawing.Point(22, 393);
            this.BuyRadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.BuyRadioButton.Name = "BuyRadioButton";
            this.BuyRadioButton.Size = new System.Drawing.Size(50, 21);
            this.BuyRadioButton.TabIndex = 10;
            this.BuyRadioButton.TabStop = true;
            this.BuyRadioButton.Text = "Buy";
            this.BuyRadioButton.UseVisualStyleBackColor = true;
            // 
            // SellRadioButton
            // 
            this.SellRadioButton.AutoSize = true;
            this.SellRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SellRadioButton.Location = new System.Drawing.Point(107, 393);
            this.SellRadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.SellRadioButton.Name = "SellRadioButton";
            this.SellRadioButton.Size = new System.Drawing.Size(49, 21);
            this.SellRadioButton.TabIndex = 11;
            this.SellRadioButton.TabStop = true;
            this.SellRadioButton.Text = "Sell";
            this.SellRadioButton.UseVisualStyleBackColor = true;
            // 
            // PriceTextBox
            // 
            this.PriceTextBox.Location = new System.Drawing.Point(98, 169);
            this.PriceTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.PriceTextBox.Name = "PriceTextBox";
            this.PriceTextBox.Size = new System.Drawing.Size(76, 20);
            this.PriceTextBox.TabIndex = 4;
            // 
            // QuantTextBox
            // 
            this.QuantTextBox.Location = new System.Drawing.Point(98, 90);
            this.QuantTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.QuantTextBox.Name = "QuantTextBox";
            this.QuantTextBox.Size = new System.Drawing.Size(76, 20);
            this.QuantTextBox.TabIndex = 2;
            // 
            // SymbolTextBox
            // 
            this.SymbolTextBox.Location = new System.Drawing.Point(98, 251);
            this.SymbolTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.SymbolTextBox.Name = "SymbolTextBox";
            this.SymbolTextBox.Size = new System.Drawing.Size(76, 20);
            this.SymbolTextBox.TabIndex = 6;
            // 
            // ExchangeTextBox
            // 
            this.ExchangeTextBox.Location = new System.Drawing.Point(98, 292);
            this.ExchangeTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.ExchangeTextBox.Name = "ExchangeTextBox";
            this.ExchangeTextBox.Size = new System.Drawing.Size(76, 20);
            this.ExchangeTextBox.TabIndex = 7;
            // 
            // RTextBox
            // 
            this.RTextBox.Location = new System.Drawing.Point(98, 326);
            this.RTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.RTextBox.Name = "RTextBox";
            this.RTextBox.Size = new System.Drawing.Size(76, 20);
            this.RTextBox.TabIndex = 8;
            // 
            // STextBox
            // 
            this.STextBox.Location = new System.Drawing.Point(98, 360);
            this.STextBox.Margin = new System.Windows.Forms.Padding(2);
            this.STextBox.Name = "STextBox";
            this.STextBox.Size = new System.Drawing.Size(76, 20);
            this.STextBox.TabIndex = 9;
            // 
            // StopButton
            // 
            this.StopButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopButton.Location = new System.Drawing.Point(596, 25);
            this.StopButton.Margin = new System.Windows.Forms.Padding(2);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(104, 31);
            this.StopButton.TabIndex = 14;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // StopAllButton
            // 
            this.StopAllButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopAllButton.Location = new System.Drawing.Point(727, 25);
            this.StopAllButton.Margin = new System.Windows.Forms.Padding(2);
            this.StopAllButton.Name = "StopAllButton";
            this.StopAllButton.Size = new System.Drawing.Size(104, 31);
            this.StopAllButton.TabIndex = 15;
            this.StopAllButton.Text = "Stop All";
            this.StopAllButton.UseVisualStyleBackColor = true;
            this.StopAllButton.Click += new System.EventHandler(this.StopAllButton_Click);
            // 
            // _dgvStrategies
            // 
            this._dgvStrategies.AllowUserToDeleteRows = false;
            this._dgvStrategies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvStrategies.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Symbol,
            this.Strategy,
            this.Side,
            this.Price,
            this.Quantity,
            this.LTP,
            this.SL,
            this.PL});
            this._dgvStrategies.Location = new System.Drawing.Point(203, 75);
            this._dgvStrategies.Margin = new System.Windows.Forms.Padding(2);
            this._dgvStrategies.MultiSelect = false;
            this._dgvStrategies.Name = "_dgvStrategies";
            this._dgvStrategies.ReadOnly = true;
            this._dgvStrategies.RowTemplate.Height = 24;
            this._dgvStrategies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgvStrategies.Size = new System.Drawing.Size(628, 249);
            this._dgvStrategies.TabIndex = 0;
            // 
            // Symbol
            // 
            this.Symbol.HeaderText = "Symbol";
            this.Symbol.Name = "Symbol";
            this.Symbol.ReadOnly = true;
            this.Symbol.Width = 60;
            // 
            // Strategy
            // 
            this.Strategy.HeaderText = "Strategy";
            this.Strategy.Name = "Strategy";
            this.Strategy.ReadOnly = true;
            this.Strategy.Width = 60;
            // 
            // Side
            // 
            this.Side.HeaderText = "Side";
            this.Side.Name = "Side";
            this.Side.ReadOnly = true;
            this.Side.Width = 60;
            // 
            // Price
            // 
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            this.Price.Width = 80;
            // 
            // Quantity
            // 
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 80;
            // 
            // LTP
            // 
            this.LTP.HeaderText = "LTP";
            this.LTP.Name = "LTP";
            this.LTP.ReadOnly = true;
            this.LTP.Width = 80;
            // 
            // SL
            // 
            this.SL.HeaderText = "SL";
            this.SL.Name = "SL";
            this.SL.ReadOnly = true;
            this.SL.Width = 80;
            // 
            // PL
            // 
            this.PL.HeaderText = "PL";
            this.PL.Name = "PL";
            this.PL.ReadOnly = true;
            this.PL.Width = 80;
            // 
            // stoppedDataGridView
            // 
            this.stoppedDataGridView.AllowUserToDeleteRows = false;
            this.stoppedDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.stoppedDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StoppedSymbol,
            this.StoppedPL});
            this.stoppedDataGridView.Location = new System.Drawing.Point(847, 75);
            this.stoppedDataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.stoppedDataGridView.MultiSelect = false;
            this.stoppedDataGridView.Name = "stoppedDataGridView";
            this.stoppedDataGridView.ReadOnly = true;
            this.stoppedDataGridView.RowHeadersVisible = false;
            this.stoppedDataGridView.RowTemplate.Height = 24;
            this.stoppedDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.stoppedDataGridView.Size = new System.Drawing.Size(146, 249);
            this.stoppedDataGridView.TabIndex = 0;
            // 
            // StoppedSymbol
            // 
            this.StoppedSymbol.HeaderText = "Symbol";
            this.StoppedSymbol.Name = "StoppedSymbol";
            this.StoppedSymbol.ReadOnly = true;
            this.StoppedSymbol.Width = 60;
            // 
            // StoppedPL
            // 
            this.StoppedPL.HeaderText = "PL";
            this.StoppedPL.Name = "StoppedPL";
            this.StoppedPL.ReadOnly = true;
            this.StoppedPL.Width = 80;
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
            this.label1.Location = new System.Drawing.Point(38, 130);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 18);
            this.label1.TabIndex = 16;
            this.label1.Text = "DPR";
            // 
            // LogTextBox
            // 
            this.LogTextBox.Location = new System.Drawing.Point(203, 344);
            this.LogTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogTextBox.Size = new System.Drawing.Size(790, 158);
            this.LogTextBox.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(38, 169);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 18);
            this.label2.TabIndex = 18;
            this.label2.Text = "Price";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 249);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 18);
            this.label3.TabIndex = 19;
            this.label3.Text = "Symbol";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 290);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 18);
            this.label4.TabIndex = 20;
            this.label4.Text = "Exchange";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(30, 324);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 18);
            this.label5.TabIndex = 21;
            this.label5.Text = "R/Entry";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(38, 358);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 18);
            this.label6.TabIndex = 22;
            this.label6.Text = "S/Qty";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(243, 33);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "RUNNING";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(909, 29);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "STOPPED";
            // 
            // DPRTextBox
            // 
            this.DPRTextBox.Location = new System.Drawing.Point(98, 130);
            this.DPRTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.DPRTextBox.Name = "DPRTextBox";
            this.DPRTextBox.Size = new System.Drawing.Size(76, 20);
            this.DPRTextBox.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(25, 90);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 18);
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
            this.StrategyComboBox.Location = new System.Drawing.Point(90, 37);
            this.StrategyComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.StrategyComboBox.Name = "StrategyComboBox";
            this.StrategyComboBox.Size = new System.Drawing.Size(92, 21);
            this.StrategyComboBox.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(22, 37);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 18);
            this.label11.TabIndex = 30;
            this.label11.Text = "Strategy";
            // 
            // BreakButton
            // 
            this.BreakButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BreakButton.Location = new System.Drawing.Point(472, 25);
            this.BreakButton.Margin = new System.Windows.Forms.Padding(2);
            this.BreakButton.Name = "BreakButton";
            this.BreakButton.Size = new System.Drawing.Size(104, 31);
            this.BreakButton.TabIndex = 13;
            this.BreakButton.Text = "Break";
            this.BreakButton.UseVisualStyleBackColor = true;
            this.BreakButton.Click += new System.EventHandler(this.BreakButton_Click);
            // 
            // NTextBox
            // 
            this.NTextBox.Location = new System.Drawing.Point(98, 212);
            this.NTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.NTextBox.Name = "NTextBox";
            this.NTextBox.Size = new System.Drawing.Size(76, 20);
            this.NTextBox.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(56, 210);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 18);
            this.label10.TabIndex = 33;
            this.label10.Text = "N";
            // 
            // acctTextBox
            // 
            this.acctTextBox.Location = new System.Drawing.Point(997, 437);
            this.acctTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.acctTextBox.Name = "acctTextBox";
            this.acctTextBox.Size = new System.Drawing.Size(88, 20);
            this.acctTextBox.TabIndex = 34;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(997, 408);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 18);
            this.label12.TabIndex = 35;
            this.label12.Text = "Account";
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(346, 25);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(104, 31);
            this.cancelButton.TabIndex = 36;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(999, 481);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(65, 13);
            this.lblVersion.TabIndex = 37;
            this.lblVersion.Text = "Version N/A";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 506);
            this.Controls.Add(this.lblVersion);
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
            this.Controls.Add(this.LogTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stoppedDataGridView);
            this.Controls.Add(this._dgvStrategies);
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
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Trader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._dgvStrategies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stoppedDataGridView)).EndInit();
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
        private System.Windows.Forms.DataGridViewTextBoxColumn StoppedPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn StoppedQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn StoppedLTP;
        private System.Windows.Forms.Button BreakButton;
        private System.Windows.Forms.TextBox NTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox acctTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button cancelButton;
        public System.Windows.Forms.DataGridView _dgvStrategies;
        public System.Windows.Forms.DataGridView stoppedDataGridView;
        public System.Windows.Forms.TextBox LogTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn StoppedSymbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn StoppedPL;
        private System.Windows.Forms.DataGridViewTextBoxColumn Symbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Strategy;
        private System.Windows.Forms.DataGridViewTextBoxColumn Side;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn LTP;
        private System.Windows.Forms.DataGridViewTextBoxColumn SL;
        private System.Windows.Forms.DataGridViewTextBoxColumn PL;
        private System.Windows.Forms.Label lblVersion;
    }
}

