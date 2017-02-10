﻿using Quotes;
using System;
using System.Collections.Generic;

namespace Sterling
{
    internal class SterlingPresenter
    {
        private ISterlingView _view;
        private SterlingModel _model;

        public SterlingPresenter(ISterlingView view)
        {
            _model = new SterlingModel();
            _model.ErrorMessage += OnErrorMessage;
            _model.LogMessage += OnLogMessage;
            _model.QuotesUpdate += OnQuotesUpdate;
            _model.TradeStopped += OnTradeStopped;
            _model.UpdateTradeStats += OnUpdateTradeStats;
            _view = view;
        }

        public bool AddStrategy(Strategy strategy, string exchange, string account,
                                double dpr, int N, double price,
                                double r, int s)
        {
            return _model.AddStrategy
            (
                strategy, exchange, account,
                dpr, N, price,
                r, s
            );
        }

        public double CalculatePl(Strategy strategy, double lastPrice)
        {
            return _model.CalculatePl(strategy, lastPrice);
        }

        public void CancelAllOrders(Strategy strategy)
        {
            _model.CancelAllOrders(strategy);
        }

        public void StartStrategy(Strategy strategy)
        {
            _model.StartStrategy(strategy);
        }

        public void StopStrategy(Strategy strategy)
        {
            _model.StopStrategy(strategy);
        }

        public void StopTrade(Strategy strategy)
        {
            _model.StopTrade(strategy);
        }

        private void OnLogMessage(string msg)
        {
            _view.LogMessage(msg);
        }

        private void OnErrorMessage(string msg, bool fatal)
        {
            _view.ShowMessage(msg, fatal);
        }

        private void OnQuotesUpdate(Dictionary<string, Quote> quotes)
        {
            _view.UpdateQuotes(quotes);
        }

        private void OnTradeStopped(Strategy strategy)
        {
            _view.StopStrategy(strategy);
        }

        private void OnUpdateTradeStats(Strategy strategy, double buyPrice, int quantity,
                                        double sellPrice)
        {
            _view.UpdateTradeStats(strategy, buyPrice, quantity, sellPrice);
        }
    }
}