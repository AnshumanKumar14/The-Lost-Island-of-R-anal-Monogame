
using Microsoft.Xna.Framework.Graphics;
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
    class Body_Renderer_System : System
    {
        public Body_Renderer_System() : base(Types.Body)
        {
        }
        //public RenderSystem(GraphicsDevice graphicsDevice)
        //: base(Aspect.All(typeof(Sprite), typeof(Transform2)))
        //{
        //    _graphicsDevice = graphicsDevice;
        //    _spriteBatch = new SpriteBatch(graphicsDevice);
        //}

        public override void Draw(SpriteBatch batch, Entity entity)
        {
            var body = (Body)entity.Get(Component.Types.Body);
            //batch.DrawRectangle(new RectangleF(body.Position, body.Size), Color.Red, 2);
        }
    }
}
