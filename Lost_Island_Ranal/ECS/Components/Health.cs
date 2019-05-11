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
    class Health : Component
    {
        public int Amount { get; set; } = 3;
        public bool Should_Die { get => Amount <= 0; }

        public float Shield_Timer{ get; set; } = 0;
        public float Max_Shield_Timer { get; set; } = 1f;

        public Health(int total) : base(Types.Health)
        {
            Amount = total;
        }

        public void Hurt(int dammage, bool shield = false) {
            if (shield)
            {
                if (Shield_Timer <= 0)
                {
                    Shield_Timer = Max_Shield_Timer;
                    Amount -= dammage;
                }
            }else
                Amount -= dammage;
        }

        public void Heal(int health)  => Amount += health;
    }
}
