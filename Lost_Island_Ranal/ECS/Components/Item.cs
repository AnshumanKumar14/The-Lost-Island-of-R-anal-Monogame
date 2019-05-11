using System;


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

namespace Lost_Island_Ranal.ECS
{
    class Item : Component
    {
        public string Description { get; set; } = "Just an item";
        public bool Can_Collect { get; set; } = false;
        public float Collect_Timer { get; set; } = 0;
        public Item() : base(Types.Item)
        {
        }
    }
}
