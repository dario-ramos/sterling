namespace Sterling
{
    internal class SterlingPresenter
    {
        private SterlingModel _model;

        public SterlingPresenter()
        {
            _model = new SterlingModel();
        }

        public void CancelAllOrders(string symbol)
        {
            _model.CancelAllOrders(symbol);
        }

        public void StopStrategy(string symbol)
        {
            _model.StopStrategy(symbol);
        }
    }
}
