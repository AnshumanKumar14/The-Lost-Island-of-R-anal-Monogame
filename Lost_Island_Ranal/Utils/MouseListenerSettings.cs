using Lost_Island_Ranal.Graphics;

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
    public class MouseListenerSettings : InputListenerSettings<MouseListener>
    {
        public MouseListenerSettings()
        {
            // initial values are windows defaults
            DoubleClickMilliseconds = 500;
            DragThreshold = 2;
        }

        public int DragThreshold { get; set; }
        public int DoubleClickMilliseconds { get; set; }
        public ViewportAdapter ViewportAdapter { get; set; }

        public override MouseListener CreateListener()
        {
            return new MouseListener(this);
        }
    }
}