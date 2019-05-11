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
    class Named_Action_List
    {
        Dictionary<string,Action> actions;

        public Named_Action_List(Dictionary<string, Action> actions) {
            this.actions = actions;
        }

        public string[] Names {
            get => actions.Keys.ToArray();
        }

        public void Add(string id, Action action)
        {
            this.actions.Add(id, action);
        }

        public void Call(int index)
        {
            if (index >= 0 && index < actions.Count)
                actions.ElementAt(index).Value?.Invoke();
        }

        public Action this[int index]
        {
            get => actions.ElementAt(index).Value;
        }
    }
}
