using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Collections;

namespace Sterling
{
    public partial class Form1 : Form, IStrategyManager
    {
        private int quantity;
        private double dpr;
        private double price;
        private double r;
        private int s;
        private int N;

        private string symbol;
        private string exchange;
        private static int runningStratsCounter = -1;
        private static ArrayList runningStrats = new ArrayList();
        //private static ConcurrentBag<Task> runningTasks = new ConcurrentBag<Task>();
        private static ArrayList runningTasks = new ArrayList();
        //private static ArrayList cancelTokenSources = new ArrayList();
        private ArrayList checkRun = new ArrayList();
        private static string account;

        public Form1()
        {
            InitializeComponent();
        }

        public bool StrategyRunning(int strategyIndex)
        {
            return (bool)checkRun[strategyIndex];
        }

        public void SetStrategyRunningStatus(int strategyIndex, bool running)
        {
            checkRun[strategyIndex] = running;
        }

        private void OnLogMessage(String text)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(OnLogMessage), new object[] { text });
                return;
            }
            LogTextBox.AppendText(text);
        }

        private void BreakButton_Click(object sender, EventArgs e)
        {

            if (runningDataGridView.SelectedRows.Count > 0 && runningDataGridView.SelectedRows.Count <= 1)
            {
                int selectedIndex = runningDataGridView.CurrentRow.Index;

                if (runningDataGridView.Rows[selectedIndex].Cells[0].Value != null)
                {
                    checkRun[selectedIndex] = false;
                    MessageBox.Show("Order placing for the selected strategy stopped.");
                }
            }
            else
            {
                MessageBox.Show("Select one  row.");
            }

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (runningDataGridView.SelectedRows.Count > 0 && runningDataGridView.SelectedRows.Count <= 1)
            {
                int selectedIndex = runningDataGridView.CurrentRow.Index;
                if (runningDataGridView.Rows[selectedIndex].Cells[0].Value != null)
                {
                    checkRun[selectedIndex] = false;
                    TradeExecutor trade = (TradeExecutor)runningStrats[selectedIndex];
                    trade.CancelAllOrders();
                }
            }
            else
            {
                MessageBox.Show("Select one row.");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "My Application", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                // Cancel the Closing event from closing the form.
                e.Cancel = true;

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OnTradeStopped(string symbol) //<CHG> Added handler for tradeStopped event
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                int symbolRow = -1;
                foreach (DataGridViewRow row in runningDataGridView.Rows)
                {
                    object symbolValue = row.Cells[0].Value;
                    if (symbolValue != null && symbolValue.ToString().Equals(symbol))
                    {
                        symbolRow = row.Index;
                        StopStrategy(symbolRow);
                        break;
                    }
                }
            });
        }

        private void StopAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < runningDataGridView.Rows.Count - 1; i++)
            {
                if (runningDataGridView.Rows[i].Visible)
                {
                    int selectedIndex = i;
                    try
                    {
                        checkRun[selectedIndex] = false;
                    }
                    catch
                    {
                        Console.WriteLine("here");
                    }

                    TradeExecutor trade = (TradeExecutor)runningStrats[selectedIndex];
                    trade.StopTrade();
                    string sym = runningDataGridView.Rows[selectedIndex].Cells[0].Value.ToString();
                    string pnl = "";
                    try
                    {
                        pnl = runningDataGridView.Rows[selectedIndex].Cells[5].Value.ToString();
                    }
                    catch
                    {

                    }

                    DataGridViewRow row = (DataGridViewRow)stoppedDataGridView.Rows[0].Clone();
                    row.Cells[0].Value = sym;
                    row.Cells[1].Value = pnl;
                    stoppedDataGridView.Rows.Add(row);
                    runningDataGridView.Rows[selectedIndex].Visible = false;

                }
            }

            MessageBox.Show("All strategies stopped, all positions closed.");
        }

        //<CHG> Separated GUI logic from business logic a bit
        private void StopButton_Click(object sender, EventArgs e)
        {
            if (runningDataGridView.SelectedRows.Count > 0 && runningDataGridView.SelectedRows.Count <= 1)
            {
                int selectedIndex = runningDataGridView.CurrentRow.Index;
                StopStrategy(selectedIndex);
            }
            else
            {
                MessageBox.Show("Select one row.");
            }
        }

        //<CHG> New method
        private void StopStrategy(int strategyIndex)
        {
            if (runningDataGridView.Rows[strategyIndex].Cells[0].Value != null)
            {
                checkRun[strategyIndex] = false;
                TradeExecutor trade = (TradeExecutor)runningStrats[strategyIndex];
                trade.StopTrade();
                string sym = runningDataGridView.Rows[strategyIndex].Cells[0].Value.ToString();
                string pnl = "";
                try
                {
                    pnl = runningDataGridView.Rows[strategyIndex].Cells[5].Value.ToString();
                }
                catch
                {

                }

                DataGridViewRow row = (DataGridViewRow)stoppedDataGridView.Rows[0].Clone();
                row.Cells[0].Value = sym;
                row.Cells[1].Value = pnl;
                stoppedDataGridView.Rows.Add(row);
                runningDataGridView.Rows[strategyIndex].Visible = false;
                MessageBox.Show("Strategy stopped and position closed for symbol: " + trade.Symbol); //<CHG> Print stopped symbol name
            }
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if ((StrategyComboBox.SelectedIndex > -1) && (DPRTextBox.Text != "") && (PriceTextBox.Text != "") && (acctTextBox.Text != "") &&
                (QuantTextBox.Text != "") && (SymbolTextBox.Text != "") && (ExchangeTextBox.Text != "") && (NTextBox.Text != "") &&
                (RTextBox.Text != "") && (STextBox.Text != "") && (BuyRadioButton.Checked || SellRadioButton.Checked))
            {
                if ((int.TryParse(QuantTextBox.Text, out quantity)) && (int.TryParse(STextBox.Text, out s)) &&
                    (Double.TryParse(DPRTextBox.Text, out dpr)) && (Double.TryParse(PriceTextBox.Text, out price)) &&
                    (Double.TryParse(RTextBox.Text, out r)) && (int.TryParse(NTextBox.Text, out N)))
                {
                    dpr = dpr / N;
                    symbol = SymbolTextBox.Text;
                    exchange = ExchangeTextBox.Text;
                    account = acctTextBox.Text;
                    string strategy = StrategyComboBox.Text;
                    runningStratsCounter++;
                    TradeExecutor mTrade;
                    string side;
                    if (BuyRadioButton.Checked) side = "B";
                    else side = "S";

                    try
                    {
                        mTrade = new TradeExecutor(account, symbol, exchange, strategy, price, quantity, dpr, r, s, this);
                        mTrade.TradeStopped += OnTradeStopped;
                        mTrade.LogMessage += OnLogMessage;
                        runningStrats.Insert(runningStratsCounter, mTrade);
                        DataGridViewRow row = (DataGridViewRow)runningDataGridView.Rows[0].Clone();
                        row.Visible = true;
                        row.Cells[0].Value = symbol;
                        //string[] row = new string[] { symbol, "", "", "", "" };
                        //DataGridViewRow mRow = new DataGridViewRow(row);
                        runningDataGridView.Rows.Insert(runningStratsCounter, row);

                        //CancellationTokenSource Canceller = new CancellationTokenSource();
                        bool run = true;
                        checkRun.Insert(runningStratsCounter, run);
                        Task worker;
                        worker = new Task(() => mTrade.RunStrategy(ref row, side, runningStratsCounter)/*, Canceller.Token*/);

                        //cancelTokenSources.Add(Canceller);
                        runningTasks.Insert(runningStratsCounter, worker);

                        worker.Start();
                    }
                    catch
                    {
                        MessageBox.Show("Error occurred. Please make sure your are connected to the Sterling terminal.");
                        System.Windows.Forms.Application.ExitThread();
                    }

                }
                else MessageBox.Show("Quantity and S should be integers; Price, DPR and R should be numerical.");
            }
            else MessageBox.Show("All the fields are compulsory.");

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
