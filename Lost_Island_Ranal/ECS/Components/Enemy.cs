
using System.Collections.Generic;

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
///  Drop Items and have experience
/// 
/// </summary>

namespace Lost_Island_Ranal.ECS.Components
{
    class Enemy : Component
    {
        public List<string> Drop_Items { get; set; } // Drop items 
        public float Experience { get; set; } = 0; //expereince points

        public Enemy(List<string> _drop_items) : base(Types.Enemy)
        {
            Drop_Items = _drop_items;
        }
    }
}
