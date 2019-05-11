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
///  
/// 
/// 

namespace MonoGame.Extended.Input.InputListeners
{
    [Flags]
    public enum KeyboardModifiers
    {
        Control = 1,
        Shift = 2,
        Alt = 4,
        None = 0
    }
}