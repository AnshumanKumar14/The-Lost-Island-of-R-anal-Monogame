using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
/// https://www.c-sharpcorner.com/UploadFile/iersoy/how-to-use-graphicsadapter-in-xna/
/// 
/// define Graphics adapter
/// </summary>

namespace Lost_Island_Ranal.Graphics
{
    class Default_Viewport_Adapter : ViewportAdapter
    {
        private readonly GraphicsDevice _graphicsDevice;

        public Default_Viewport_Adapter(GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public override int VirtualWidth => _graphicsDevice.Viewport.Width;
        public override int VirtualHeight => _graphicsDevice.Viewport.Height;
        public override int ViewportWidth => _graphicsDevice.Viewport.Width;
        public override int ViewportHeight => _graphicsDevice.Viewport.Height;

        public override Matrix GetScaleMatrix()
        {
            return Matrix.Identity;
        }
    }
}
