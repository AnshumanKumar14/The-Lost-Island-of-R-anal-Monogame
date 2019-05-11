using Lost_Island_Ranal.Utils;
using Microsoft.Xna.Framework;
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
    /// Assigning values to Animation frames
    /// </summary>


namespace Lost_Island_Ranal.Graphics
{


    
    //public class AnimationFrame
    //{
    //    public float Time = 0;
    //    public Matrix[] Transforms;

    //    public AnimationFrame(float time, Matrix[] transforms)
    //    {
    //        Time = time;
    //        Transforms = transforms;
    //    }
    //}



class Animation_Frame : RectangleF
    {
        public float Frame_Time { get; set; }
        public Vector2 Offset { get; set; }

        public Animation_Frame(int x, int y, int w, int h, float time = 0.1f) : base(x, y, w, h)
        {
            Frame_Time = time;
        }

        public Animation_Frame(Vector2 pos, Vector2 size, float time = 0.1f) : base(pos.X, pos.Y, size.X, size.Y)
        {
            Frame_Time = time;
        }
    }
}
