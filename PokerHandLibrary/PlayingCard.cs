﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHandLibrary
{
    /// <summary>
    /// single playing card
    /// </summary>
    public class PlayingCard
    {
        public RankType Rank { get; set; }
        public SuitType Suit { get; set; }
    }
}
