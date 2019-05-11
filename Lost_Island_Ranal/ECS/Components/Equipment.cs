using Lost_Island_Ranal.Items;
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

namespace Lost_Island_Ranal.ECS.Components
{
    class Equipment : Component
    {
        public Equipable Chest_Piece   { get; set; }
        public Equipable Legs_Piece    { get; set; }
        public Equipable Helmet_Piece  { get; set; }

        public Equipable Left_Hand  { get; set; }
        public Equipable Right_Hand { get; set; }

        public Equipment() : base(Types.Equipment)
        {
        }

        public static implicit operator Equipment(Equipable v)
        {
            throw new NotImplementedException();
        }
    }
}
