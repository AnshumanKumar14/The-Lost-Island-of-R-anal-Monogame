using Lost_Island_Ranal.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Created by: Ayran Olckers AKA The Geekiest One
 * -2019-
 * -Game Development Project-
 * This file is subject to the terms and conditions defined in
 * file 'LICENSE.txt', which is part of this source code package.
 */

/// <summary>
/// source: https://codereview.stackexchange.com/questions/101645/inventories-containers-and-filters
/// For your Container class, the indexer probably should be read only. The Add and Insert methods have CanAddItem checks. But this line:
/// public Item this[int index] { get { return _Items[index]; } set { _Items[index] = value; } }
/// could allow someone to replace the value with an item that fails the CanAddItem check. I wouldn't suggest adding extra checks in the setter. It really should be a readonly property.
/// public Item this[int index] { get { return _Items[index]; } }
/// 
/// below not in use, but can have future use to create an inventory system??????????????????
/// </summary>
namespace Lost_Island_Ranal.Gui
{
    class Invatory_Container
    {
        public List<Inventory> Invatories { get; set; }
        
        public Invatory_Container()
        {
            Invatories = new List<Inventory>();
        }

        public void Update(GameTime time)
        {
            
        }
        
    }
}
