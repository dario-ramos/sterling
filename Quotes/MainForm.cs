using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Quotes
{
    public partial class MainForm : Form, IQuotesView
    {
        private Dictionary<string, int> _rowsBySymbol = null;
        private QuotesPresenter _quotesPresenter = null;
        private Stopwatch _sw = null;
        private int _quotesUpdates;

        public MainForm()
        {
            AppDomain.CurrentDomain.UnhandledException += MainForm_OnError;
            _sw = new Stopwatch();
            _sw.Start();
            _quotesUpdates = 0;
            InitializeComponent();
        }

        public string SelectedSymbol
        {
            get
            {
                return (string) Invoke( new Func<string>( () =>
                {
                    if (dgvSymbols.SelectedRows.Count == 1)
                    {
                        int selectedIndex = dgvSymbols.CurrentRow.Index;
                        return (string) dgvSymbols.Rows[selectedIndex].Cells[0].Value;
                    }
                    else
                    {
                        return null;
                    }
                }));
            }
        }

        public void DisplayErrorMessage(string msg)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                rtbLog.AppendText("ERROR: " + msg + Environment.NewLine, Color.Red);
            });
        }

        public void DisplayMessage(string msg)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                rtbLog.AppendText(msg + Environment.NewLine, Color.Black);
            });
        }

        public void RegisterSymbol(string symbol)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                if (_rowsBySymbol.ContainsKey(symbol))
                {
                    DisplayErrorMessage("Symbol " + symbol + " already registered");
                }
                else
                {
                    dgvSymbols.Rows.Add(symbol, "");
                    _rowsBySymbol.Add(symbol, dgvSymbols.Rows.Count - 2); //Count header
                }
            });
        }

        public void UnregisterAllSymbols()
        {
            BeginInvoke((MethodInvoker)delegate
            {
                dgvSymbols.Rows.Clear();
                _rowsBySymbol.Clear();
            });
        }

        public void UnregisterSymbol(string symbol)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                if (!_rowsBySymbol.ContainsKey(symbol))
                {
                    DisplayErrorMessage("Symbol " + symbol + " not registered");
                }
                else
                {
                    dgvSymbols.Rows.RemoveAt(_rowsBySymbol[symbol]);
                    _rowsBySymbol.Remove(symbol);
                }
            });
        }

        public void UpdateQuotes(Dictionary<string, Quote> quotes)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                foreach (KeyValuePair<string, Quote> quote in quotes)
                {
                    dgvSymbols.Rows[_rowsBySymbol[quote.Key]].Cells[1].Value = quote.Value.LastPrice;
                    dgvSymbols.Rows[_rowsBySymbol[quote.Key]].Cells[2].Value = quote.Value.Timestamp;
                }
                _quotesUpdates++;
                if(_sw.ElapsedMilliseconds > 1000)
                {
                    _sw.Stop();
                    lblQuoteRefreshRate.Text = "Refresh Rate: " + _quotesUpdates;
                    _quotesUpdates = 0;
                    _sw.Restart();
                }
            });
        }

        private void btnRegisterSymbol_Click(object sender, EventArgs e)
        {
            _quotesPresenter.RegisterNewSymbol(txtNewSymbol.Text);
        }

        private void btnStopSymbol_Click(object sender, EventArgs e)
        {
            _quotesPresenter.UnregisterSymbol();
        }

        private void btnStopAllSymbols_Click(object sender, EventArgs e)
        {
            _quotesPresenter.UnregisterAllSymbols();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _quotesPresenter.StopGettingQuotes();
            _quotesPresenter.Dispose();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblVersion.Text = lblVersion.Text.Replace("N/A", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            _quotesPresenter = new QuotesPresenter(this);
            lblProvider.Text = lblProvider.Text.Replace("N/A", _quotesPresenter.ProviderName);
            _rowsBySymbol = new Dictionary<string, int>();
            _quotesPresenter.StartGettingQuotes();
        }

        private void MainForm_OnError(object sender, UnhandledExceptionEventArgs e)
        {

            BeginInvoke((MethodInvoker)delegate
            {
                DisplayErrorMessage(e.ExceptionObject.ToString());
            });
        }

    }
}
