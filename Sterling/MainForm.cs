using Quotes;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sterling
{
    public partial class MainForm : Form, ISterlingView
    {
        private bool _forcedClose = false;
        private const string SIDE_COLUMN_NAME = "Side";
        private const string STRATEGY_COLUMN_NAME = "Strategy";
        private const string SYMBOL_COLUMN_NAME = "Symbol";
        private SterlingPresenter _presenter;

        public MainForm()
        {
            InitializeComponent();
        }

        public void LogMessage(string msg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string>(LogMessage), new object[] { msg });
                return;
            }
            LogTextBox.AppendText(msg);
        }

        public void ShowMessage(string msg, bool fatalError)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                MessageBox.Show(msg);
                if (fatalError)
                {
                    _forcedClose = true;
                    Close();
                }
            });
        }

        public void StopStrategy(Strategy strategy)
        {
            if (strategy == null)
            {
                return;
            }
            BeginInvoke((MethodInvoker)delegate
            {
                _presenter.StopStrategy(strategy);
                _presenter.StopTrade(strategy);
                DataGridViewRow stoppedStrategyRow = FindRow(strategy);
                string pnl = "";
                try
                {
                    pnl = stoppedStrategyRow.Cells["RunningPL"].Value.ToString();
                }
                catch (Exception ex)
                {
                    LogMessage(ex.Message);
                }
                DataGridViewRow row = (DataGridViewRow)stoppedDataGridView.Rows[0].Clone();
                row.Cells[SYMBOL_COLUMN_NAME].Value = strategy.Symbol;
                row.Cells[1].Value = pnl;
                stoppedDataGridView.Rows.Add(row);
                _dgvStrategies.Rows.Remove(stoppedStrategyRow);
                ShowMessage("Strategy " + strategy.Name + " stopped and position closed for symbol: " + strategy.Symbol, false);
            });
        }

        public void UpdateQuotes(Dictionary<string, Quote> quotes)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                foreach(DataGridViewRow row in _dgvStrategies.Rows)
                {
                    string symbol = (string) row.Cells[SYMBOL_COLUMN_NAME].Value;
                    if(symbol != null && quotes.ContainsKey(symbol))
                    {
                        row.Cells["LTP"].Value = quotes[symbol].LastPrice;
                        Strategy strategy = GetStrategyFromRow(row);
                        row.Cells["PL"].Value = _presenter.CalculatePl(strategy, quotes[symbol].LastPrice);
                    }
                }
            });
        }

        public void UpdateTradeStats(Strategy strategy, double buyPrice, int quantity, double sellPrice)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                DataGridViewRow row = FindRow(strategy);
                row.Cells["Price"].Value = buyPrice.ToString();
                row.Cells["Quantity"].Value = quantity.ToString();
                row.Cells["SL"].Value = sellPrice.ToString();
            });
        }

        private bool GetFormInput(out double dpr, out double price, out double r,
                                  out int N, out int quantity, out int s)
        {
            dpr = price = r = 0;
            N = quantity = s = 0;
            if (string.IsNullOrWhiteSpace(DPRTextBox.Text) ||
                string.IsNullOrWhiteSpace(PriceTextBox.Text) ||
                string.IsNullOrWhiteSpace(RTextBox.Text) ||
                string.IsNullOrWhiteSpace(NTextBox.Text) ||
                string.IsNullOrWhiteSpace(QuantTextBox.Text) ||
                string.IsNullOrWhiteSpace(STextBox.Text) ||
                (StrategyComboBox.SelectedIndex <= -1) ||
                string.IsNullOrWhiteSpace(acctTextBox.Text) ||
                string.IsNullOrWhiteSpace(SymbolTextBox.Text) ||
                string.IsNullOrWhiteSpace(ExchangeTextBox.Text) ||
                (!BuyRadioButton.Checked && !SellRadioButton.Checked))
            {
                ShowMessage("All fields are compulsory", false);
                return false;
            }
            if (!int.TryParse(QuantTextBox.Text, out quantity) ||
                !int.TryParse(STextBox.Text, out s) ||
                !double.TryParse(DPRTextBox.Text, out dpr) ||
                !double.TryParse(PriceTextBox.Text, out price) ||
                !double.TryParse(RTextBox.Text, out r) ||
                !int.TryParse(NTextBox.Text, out N))
            {
                ShowMessage("Quantity and S should be integers; Price, DPR and R should be numerical.", false);
                return false;
            }
            return true;
        }

        private DataGridViewRow FindRow(Strategy strategy)
        {
            DataGridViewRow symbolRow = null;
            // Look for existing symbol in list
            foreach (DataGridViewRow row in _dgvStrategies.Rows)
            {
                if ((string)row.Cells[SYMBOL_COLUMN_NAME].Value == strategy.Symbol &&
                    (Side)Enum.Parse(typeof(Side), (string)row.Cells[SIDE_COLUMN_NAME].Value) == strategy.Side &&
                    (string)row.Cells[STRATEGY_COLUMN_NAME].Value == strategy.Name)
                {
                    symbolRow = row;
                    break;
                }
            }
            return symbolRow;
        }

        private Strategy GetStrategyFromRow(DataGridViewRow row)
        {
            string symbol = (string) row.Cells[SYMBOL_COLUMN_NAME].Value;
            string side = (string) row.Cells[SIDE_COLUMN_NAME].Value;
            string name = (string) row.Cells[STRATEGY_COLUMN_NAME].Value;
            Side eSide;
            if(string.IsNullOrWhiteSpace(symbol) ||
               string.IsNullOrWhiteSpace(side) ||
               !Enum.TryParse<Side>(side, out eSide) ||
               string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            return new Strategy(eSide, name, symbol);
        }

        private void BreakButton_Click(object sender, EventArgs e)
        {
            if (_dgvStrategies.SelectedRows.Count > 0 && _dgvStrategies.SelectedRows.Count <= 1)
            {
                Strategy strategy = GetStrategyFromRow(_dgvStrategies.CurrentRow);
                if (strategy != null)
                {
                    _presenter.StopStrategy(strategy);
                    ShowMessage("Order placing for the selected strategy stopped.", false);
                }
            }
            else
            {
                ShowMessage("Select one row.", false);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (_dgvStrategies.SelectedRows.Count > 0 && _dgvStrategies.SelectedRows.Count <= 1)
            {
                Strategy strategy = GetStrategyFromRow(_dgvStrategies.CurrentRow);
                if (strategy != null)
                {
                    _presenter.CancelAllOrders(strategy);
                }
            }
            else
            {
                ShowMessage("Select one row.", false);
            }
        }

        private void LoadSettings()
        {
            StrategyComboBox.Text = Properties.Settings.Default.Strategy;
            QuantTextBox.Text = Properties.Settings.Default.Quantity;
            DPRTextBox.Text = Properties.Settings.Default.DPR;
            PriceTextBox.Text = Properties.Settings.Default.Price;
            NTextBox.Text = Properties.Settings.Default.N;
            SymbolTextBox.Text = Properties.Settings.Default.Symbol;
            ExchangeTextBox.Text = Properties.Settings.Default.Exchange;
            RTextBox.Text = Properties.Settings.Default.REntry;
            STextBox.Text = Properties.Settings.Default.SQty;
            acctTextBox.Text = Properties.Settings.Default.Account;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_forcedClose)
            {
                return;
            }
            if (MessageBox.Show("Are you sure you want to exit?", "My Application", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                // Cancel the Closing event from closing the form.
                e.Cancel = true;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _presenter = new SterlingPresenter(this);
            LoadSettings();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.Strategy = StrategyComboBox.Text;
            Properties.Settings.Default.Quantity = QuantTextBox.Text;
            Properties.Settings.Default.DPR = DPRTextBox.Text;
            Properties.Settings.Default.Price = PriceTextBox.Text;
            Properties.Settings.Default.N = NTextBox.Text;
            Properties.Settings.Default.Symbol = SymbolTextBox.Text;
            Properties.Settings.Default.Exchange = ExchangeTextBox.Text;
            Properties.Settings.Default.REntry = RTextBox.Text;
            Properties.Settings.Default.SQty = STextBox.Text;
            Properties.Settings.Default.Account = acctTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void StopAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _dgvStrategies.Rows.Count - 1; i++)
            {
                if (_dgvStrategies.Rows[i].Visible)
                {
                    int selectedIndex = i;
                    Strategy strategy = GetStrategyFromRow(_dgvStrategies.Rows[i]);
                    _presenter.StopStrategy(strategy);
                    _presenter.StopTrade(strategy);
                    string pnl = "";
                    try
                    {
                        pnl = _dgvStrategies.Rows[selectedIndex].Cells[5].Value.ToString();
                    }
                    catch
                    {
                        //TODO Log error message
                    }
                    DataGridViewRow row = (DataGridViewRow)stoppedDataGridView.Rows[0].Clone();
                    row.Cells[SYMBOL_COLUMN_NAME].Value = strategy.Symbol;
                    row.Cells[1].Value = pnl;
                    stoppedDataGridView.Rows.Add(row);
                    _dgvStrategies.Rows.RemoveAt(selectedIndex);
                }
            }

            ShowMessage("All strategies stopped, all positions closed.", false);
        }

        //<CHG> Separated GUI logic from business logic a bit
        private void StopButton_Click(object sender, EventArgs e)
        {
            if (_dgvStrategies.SelectedRows.Count > 0 && _dgvStrategies.SelectedRows.Count <= 1)
            {
                Strategy strategy = GetStrategyFromRow(_dgvStrategies.CurrentRow);
                StopStrategy(strategy);
            }
            else
            {
                ShowMessage("Select one row.", false);
            }
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            double dpr, price, r;
            int N, quantity, s;
            if (!GetFormInput(out dpr, out price, out r, out N, out quantity, out s))
            {
                return;
            }
            dpr = dpr / N;
            Side side = (BuyRadioButton.Checked) ? Sterling.Side.Buy : Sterling.Side.Sell;
            try
            {
                Strategy newStrategy = new Strategy(side, StrategyComboBox.Text, SymbolTextBox.Text);
                _presenter.AddStrategy
                (
                    newStrategy, ExchangeTextBox.Text, acctTextBox.Text,
                    dpr, N, price,
                    r, s
                );
                DataGridViewRow row = (DataGridViewRow)_dgvStrategies.Rows[0].Clone();
                row.Visible = true;
                row.Cells[SYMBOL_COLUMN_NAME].Value = newStrategy.Symbol;
                row.Cells[SIDE_COLUMN_NAME].Value = newStrategy.Side.ToString();
                row.Cells[STRATEGY_COLUMN_NAME].Value = newStrategy.Name;
                _dgvStrategies.Rows.Add(row);
                _presenter.StartStrategy(newStrategy);
                SaveSettings();
            }
            catch
            {
                ShowMessage("Error occurred. Please make sure your are connected to the Sterling terminal.", true);
            }
        }

    }

    [StructLayout(LayoutKind.Sequential)]

    public class SYSTEMTIME
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMilliseconds;
    }

    public class LibWrap
    {
        [DllImport("Kernel32.dll")]
        public static extern void GetSystemTime([In, Out] SYSTEMTIME st);
    }
}
