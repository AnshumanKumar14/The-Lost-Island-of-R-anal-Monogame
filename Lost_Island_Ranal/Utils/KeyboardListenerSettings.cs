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
    public class KeyboardListenerSettings : InputListenerSettings<KeyboardListener>
    {
        public KeyboardListenerSettings()
        {
            InitialDelayMilliseconds = 800;
            RepeatDelayMilliseconds = 50;
        }

        public int InitialDelayMilliseconds { get; set; }
        public int RepeatDelayMilliseconds { get; set; }

        public override KeyboardListener CreateListener()
        {
            return new KeyboardListener(this);
        }
    }
}