using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


//-----------------------------------------------------------------------------
// Created by: Ayran Olckers AKA The Geekiest One
// -2019-
// -Game Development Project-
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.
//-----------------------------------------------------------------------------


//-----------------------------------------------------------------------------
// PrimitiveBatch.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

/// <summary>
/// PrimitiveBatch is a class that handles efficient rendering automatically for its
/// users, in a similar way to SpriteBatch. PrimitiveBatch can render lines, points,
/// and triangles to the screen. In this sample, it is used to draw a .
/// </summary>
/// 

//Source: https://github.com/CartBlanche/MonoGame-Samples/blob/master/Primitives/PrimitiveBatch.cs
//Ultered that code to tdraw a dialougue box to the screen, this may have further use for more shapes to be drawn to screen
///////////////////
namespace Lost_Island_Ranal.Graphics
{
    class PrimitivesBatch
    {
        private SpriteBatch batch;
        private GraphicsDevice device;

        private Texture2D rect_pixel;

        public PrimitivesBatch(SpriteBatch _batch, GraphicsDevice _device)
        {
            batch = _batch;
            device = _device;

            rect_pixel = new Texture2D(device, 1, 1);
            Color[] data = new Color[1] { Color.White };
            rect_pixel.SetData(data);
        }

        //Fill the rectangle 
        public void DrawFilledRect(Vector2 position, Vector2 size, Color color, float rotation = 0.0f, float layer = 1.0f)
        {
            batch.Draw(
                rect_pixel, 
                position,
                null,
                color,
                rotation,
                Vector2.Zero,
                size,
                SpriteEffects.None,
                layer
                );
        }

        //Draw Dialouge box shape: 
        public void DrawLineRect(Vector2 position, Vector2 size, Color color, int line_width = 2, float layer = 1.0f)
        {
            // NOTE: Rotation takes some trig, so I'll deal with that later .... 

            //Left Line
            DrawFilledRect(
                position - new Vector2(line_width) / 2, 
                new Vector2(line_width, size.Y), 
                color, 
                0.0f, 
                layer);

            //Top line
            DrawFilledRect(
                position - new Vector2(line_width) / 2, 
                new Vector2(size.X, line_width), 
                color, 
                0.0f, 
                layer);

            //Left Line
            DrawFilledRect(
                position + new Vector2(size.X, 0) - new Vector2(line_width) / 2, 
                new Vector2(line_width, size.Y + line_width), 
                color, 
                0.0f, 
                layer);

            //Top line
            DrawFilledRect(
                position + new Vector2(0, size.Y) - new Vector2(line_width) / 2, 
                new Vector2(size.X + line_width, line_width), 
                color, 
                0.0f, 
                layer);
        }
    }
}
