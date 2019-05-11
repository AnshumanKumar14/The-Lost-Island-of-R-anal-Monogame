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
    /// </summary>

namespace Lost_Island_Ranal.ECS.Components
{
    class Timed_Destroy : Component
    {
        public float Time_Left { get; set; } = 0f;

        public Timed_Destroy(float time) : base(Types.Timed_Destroy)
        {
            Time_Left = time;
        }
    }
}
