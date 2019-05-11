namespace MonoGame.Extended.Input.InputListeners

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
{
    public abstract class InputListenerSettings<T>
        where T : InputListener
    {
        public abstract T CreateListener();
    }
}