﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lost_Island_Ranal.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Lost_Island_Ranal.ECS.Component;


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
    /// An animation system to manage all the entities
    /// </summary>

namespace Lost_Island_Ranal.ECS.Systems
{
    class Advanced_Animation_Rendering_System: Sprite_Renderer_System
    {

        public Advanced_Animation_Rendering_System()
        :base()
        {
            types.Clear();
            types.Add(Types.Advanced_Animation);
            types.Add(Types.Body);
        }

        public override void Update(GameTime time, Entity entity){
            var animation = (Advanced_Animation_Component)entity.Get(Types.Advanced_Animation);
            var body = (Body)entity.Get(Types.Body);

            var current_frame = animation.Get_Current_Frame();
            
            // tick the timer

            if (animation.Playing_Animation)
                animation.Timer += (float)time.ElapsedGameTime.TotalSeconds;
            else{
                animation.Current_Frame = 0;
            }

            if (animation.Timer >= current_frame.Frame_Time){
                animation.Timer = 0;
                animation.Current_Frame++; 
            }

            // Console.WriteLine($"{animation.Current_Frame} {animation.Get_Current_Animation().Frames.Count}");
            if (animation.Current_Frame == animation.Get_Current_Animation().Frames.Count - 1){
                animation.Animation_Finished = true;
            }else{
                animation.Animation_Finished = false;
            }

            // Make sure the animation doesnt over flow
            animation.Check_Bounds();
        }

        public override void Draw(SpriteBatch batch, Entity entity){

            var animation = (Advanced_Animation_Component)entity.Get(Types.Advanced_Animation);
            var body = (Body)entity.Get(Types.Body);

            // calculate the entities depth rendering layer
            animation.Layer = Get_Layer(body);
            var current_animation = animation.Get_Current_Animation();

            var frame = animation.Get_Current_Frame();
            var quad = frame.Rectangle;
            var side = animation.Scale.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            var offset = (side == SpriteEffects.None) ? 
                         current_animation.Right_Face_Offset :
                         current_animation.Left_Face_Offset;

            // do the actual drawing
            batch.Draw(
                animation.Texture,
                body.Position - new Vector2(quad.Width / 2 - body.Width / 2, quad.Height - body.Height)
                + current_animation.Offset
                + offset
                + animation.Offset
                + frame.Offset,

                quad,
                animation.Color,       
                0,
                Vector2.Zero,
                Vector2.One,
                side,
                animation.Layer + animation.Layer_Offset
            );
        }
    }
}
