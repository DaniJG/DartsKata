﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartsKata
{
    public interface IScorecard
    {
        int Score { get; }

        void Add(params IDartThrowResult[] dartsThrown);
    }
}
