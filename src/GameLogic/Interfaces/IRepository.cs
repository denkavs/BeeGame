﻿using GameLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Interfaces
{
    public interface IRepository
    {
        void Save(List<Bee> bees, int gameId);
        List<Bee> Restore(int gameId);

    }
}
