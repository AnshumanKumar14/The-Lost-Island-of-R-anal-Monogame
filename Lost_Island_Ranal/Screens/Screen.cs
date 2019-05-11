using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
namespace Lost_Island_Ranal.Screens
{
    abstract class Screen
    {
        public string ID { get; protected set; }
        public Screen(string _id)
        {
            ID = _id;
        }

        public virtual void Load(params string[] args) { }
        public virtual void Update(GameTime time) { }
        public virtual void PreDraw(SpriteBatch batch) { }
        public virtual void Draw(SpriteBatch batch) { }
        public virtual void UIDraw(SpriteBatch batch) { }
        public virtual void FilteredDraw(SpriteBatch batch) { }
        public virtual void Destroy() { }
    }
}
