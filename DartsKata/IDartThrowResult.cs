using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartsKata
{
    public interface IDartThrowResult
    {
        int TotalPoints { get; }

        bool IsDouble { get; }

        bool IsBullseye { get; }
    }
}
