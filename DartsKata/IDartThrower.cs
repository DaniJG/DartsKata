﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartsKata
{
    public interface IDartThrower
    {
        IDartThrowResult ThrowDart(int targetScore);
    }
}
