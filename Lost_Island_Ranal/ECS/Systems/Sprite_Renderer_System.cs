﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using static Lost_Island_Ranal.ECS.Component;


//-----------------------------------------------------------------------------
// Created by: Ayran Olckers AKA The Geekiest One
// -2019-
// -Game Development Project-
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.
//-----------------------------------------------------------------------------

namespace Lost_Island_Ranal.ECS
{
    class Sprite_Renderer_System : System
    {

        private Tiled_Map tile_map_reference = null;

        public void Give_Tile_Map(Tiled_Map _tilemap)
        {
            this.tile_map_reference = _tilemap;
        }

        public Sprite_Renderer_System() : base(Types.Body, Types.Sprite)
        {
        }

        //public RenderSystem(GraphicsDevice graphicsDevice)
        //: base(Aspect.All(typeof(Sprite), typeof(Transform2)))
        //{
        //    _graphicsDevice = graphicsDevice;
        //    _spriteBatch = new SpriteBatch(graphicsDevice);
        //}

        protected float Get_Layer(Body body)
        {
            if (tile_map_reference == null) return 0.3f;
            return 0.3f + (body.Y / tile_map_reference.Map_Height_In_Pixels) * 0.1f;
        }

        public override void Draw(SpriteBatch batch, Entity entity)
        {

        //    public override void Draw(GameTime gameTime)
        //{
        //    _spriteBatch.Begin();

        //    foreach (var entity in ActiveEntities)
        //    {
        //        // draw your entities
        //    }

        //    _spriteBatch.End();
        //}

        var sprite = (Sprite)entity.Get(Types.Sprite);
            var body = (Body)entity.Get(Types.Body);

            //animation.Layer = 0.3f + (body.Y / Game1.Map_Height_Pixels) * 0.1f;
            if (body == null || sprite == null) return;

            sprite.Layer = Get_Layer(body);

            batch.Draw(
                sprite.Texture, 
                body.Position - new Vector2(sprite.Quad.Width / 2 - body.Width / 2, sprite.Quad.Height - body.Height), 
                sprite.Quad, 
                sprite.Color, 
                0f, 
                Vector2.Zero, 
                Vector2.One, 
                SpriteEffects.None, 
                sprite.Layer + sprite.Layer_Offset);
        }
    }
}