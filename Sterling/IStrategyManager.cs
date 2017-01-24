using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sterling
{
    public interface IStrategyManager
    {
        bool StrategyRunning(int strategyIndex);

        void SetStrategyRunningStatus(int strategyIndex, bool running);
    }
}
