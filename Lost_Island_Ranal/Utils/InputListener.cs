using Microsoft.Xna.Framework;

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

namespace MonoGame.Extended.Input.InputListeners
{
    public abstract class InputListener
    {
        protected InputListener()
        {
        }

        //public ViewportAdapter ViewportAdapter { get; set; }

        public abstract void Update(GameTime gameTime);
    }
}