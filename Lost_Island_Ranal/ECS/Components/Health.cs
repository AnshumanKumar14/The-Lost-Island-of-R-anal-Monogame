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
        public bool Should_Die { get => Amount <= 0; } //counter to detect if health 0 then die

        public float Shield_Timer{ get; set; } = 0; //time for shield replinish?
        public float Max_Shield_Timer { get; set; } = 1f;

        // total player /enemy health 
        public Health(int total) : base(Types.Health)
        {
            Amount = total;
        }

        //Plans to develop shield / energy shield / magic shield on top of health
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
