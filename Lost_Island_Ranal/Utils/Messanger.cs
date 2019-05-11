using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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


namespace Lost_Island_Ranal.Utils
{
    class Messanger
    {
        private static Messanger it;
        private List<string> messages;

        private Messanger() {
            messages = new List<string>();
        }

        public void Push(string m)
        {
            messages.Add(m);
        }

        public string Top()
        {
            if (messages.Count > 0)
                return messages.Last();
            else return "";
        }

        public void Clear()
        {
            messages.Clear();
        }

        public static Messanger It
        {
            get
            {
                if (it == null) it = new Messanger();
                return it;
            }
        }
    }
}
