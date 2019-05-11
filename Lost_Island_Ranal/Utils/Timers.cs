﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//-----------------------------------------------------------------------------
// Created by: Ayran Olckers AKA The Geekiest One
// -2019-
// -Game Development Project-
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.
//-----------------------------------------------------------------------------



/// <summary>
/// 
/// 
/// 
/// 
///  
/// 
/// 

namespace Lost_Island_Ranal.Utils
{
    class Timer
    {
        public float Counter { get; set; }
        public Func<bool> Callback { get; set; }
    }

    class Timers
    {

        private static Timers it;

        List<Timer> timers;

        private Timers()
        {
            timers = new List<Timer>();
        }

        public static Timers It { get{
                if ( it == null ) it = new Timers();
                return it;
            } }

        public void New_Timer(Func<bool> _callback, float time)
        {
            timers.Add(new Timer() {
                Counter = time,
                Callback = _callback
            });
        }

        public void Update(GameTime time)
        {
            for (int i = timers.Count - 1; i >= 0; i-- )
            {
                var timer = timers[i];
                if (timer.Counter <= 0 ) {
                    timer.Callback();
                    timers.Remove(timer);
                }
                else {
                    timer.Counter = timer.Counter - (float) time.ElapsedGameTime.TotalSeconds;
                }
            }
        }
        
    }
}
