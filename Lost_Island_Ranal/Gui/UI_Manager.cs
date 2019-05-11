using Lost_Island_Ranal.ECS;
using Lost_Island_Ranal.ECS.Components;
using Lost_Island_Ranal.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lost_Island_Ranal.ECS.Component;


/*
 * Created by: Ayran Olckers AKA The Geekiest One
 * -2019-
 * -Game Development Project-
 * This file is subject to the terms and conditions defined in
 * file 'LICENSE.txt', which is part of this source code package.
 */


namespace Lost_Island_Ranal.Gui
{
    class UI_Manager
    {
        List<Inventory> invatories;
        public int Square_Size { get; set; } = 24;

        Point Cursor = Point.Zero;

        public bool Showing { get; set; } = false;
        private bool ShowMenu { get; set; } = false;

        public Vector2 Offset { get; private set; } = Vector2.Zero;

        public Dictionary<string, Action> MenuActions;
        public Entity Player { get; set; } = null;
        private World entity_world;
        private PrimitivesBatch primitives;

        public static readonly float MARGIN = 32;
        public static readonly float CELL_SIZE = 64;
        public static readonly float CELL_MARGIN = 8;
        public static readonly Color CELL_COLOR = new Color(0, 0, 0, 0.75f);

        public UI_Manager(World _entity_world, PrimitivesBatch _primitives)
        {
            invatories = new List<Inventory>();
            entity_world = _entity_world;
            primitives = _primitives;

            MenuActions = new Dictionary<string, Action>
            {
                //drop coins possibly can expand to items??
                {"Drop", ()=>{
                    invatories.First().Drop_Item(Cursor.X, Cursor.Y);
                }},
                {"Move", ()=>{ Console.WriteLine("Moving!"); }},
                {"Info", ()=>{ Console.WriteLine("Info!"); }},
            };
        }
        public void Update(GameTime time)
        {
            var players = entity_world.Get_All_With_Component(Component.Types.Player);
            if (players.Count > 0) Player = players.First();
            if (Player != null && Player.Remove) Player = null;
            if (Player == null) return;

            if (Input.It.Is_Key_Pressed(Keys.Q)) { Showing = !Showing; } 

            if (Input.It.Is_Key_Pressed(Keys.Left)) { Cursor.X--; } 
            if (Input.It.Is_Key_Pressed(Keys.Right)) { Cursor.X++; } 
            if (Input.It.Is_Key_Pressed(Keys.Up)) { Cursor.Y--; } 
            if (Input.It.Is_Key_Pressed(Keys.Down)) { Cursor.Y++; }

            var inventory = (Inventory)Player.Get(Types.Inventory);
            if (inventory != null)
            {
                if (Cursor.X < 0) { Cursor.X = inventory.W - 1; }
                if (Cursor.Y < 0) { Cursor.X = inventory.H - 1; }
                if (Cursor.X > inventory.W - 1) { Cursor.X = 0; }
                if (Cursor.Y > inventory.H - 1) { Cursor.Y = 0; }
            }
        }
        
        public void DrawPlayerHealth(SpriteBatch batch)
        {
            var gui     = Assets.It.Get<Texture2D>("gui");
            // Draw the players health
            var health  = (Health)Player.Get(Types.Health);

            var margin = CELL_MARGIN;
            var health_pos = new Vector2(MARGIN) + Offset;
            var scale = 4;

            for (int i = 0; i < health.Amount; i++)
            {
                if (i > 0) health_pos += (new Vector2(scale * 16 + margin, 0));
                batch.Draw(
                    gui,
                    health_pos,
                    new Rectangle(0, 24, 16, 16),
                    Color.White,
                    0f,
                    Vector2.Zero,
                    scale,
                    SpriteEffects.None,
                    1f
                    );
            }
        }
      

        public void UIDraw(SpriteBatch batch)
        {
            if (Player != null && Player.Remove) Player = null;
            if (Player == null) return;

            //draw player health to gui
            DrawPlayerHealth(batch);

            if (!Showing) return;

           
        }
    }
}
