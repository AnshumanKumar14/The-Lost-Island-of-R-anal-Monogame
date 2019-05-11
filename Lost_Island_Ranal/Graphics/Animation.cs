﻿using Microsoft.Xna.Framework;
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




namespace Lost_Island_Ranal.Graphics
{
    class Animation
    {
        public List<Animation_Frame> Frames { get; set; }

        public enum Playback_Types
        {
            FORWARD,
            BACKWARD,
            ONCE_FORWARD,
            ONCE_BACKWARD,
            
        };

        public string ID { get; private set; }
        public Playback_Types Playback_Type { get; set; } = Playback_Types.FORWARD;

        public Vector2 Offset { get; set; } = Vector2.Zero;
        public Vector2 Left_Face_Offset { get; set; } = Vector2.Zero;
        public Vector2 Right_Face_Offset { get; set; } = Vector2.Zero;

        public float Offset_X {
            get => Offset.X;
            set => Offset = new Vector2(value, Offset.Y);
        }

        public float Offset_Y {
            get => Offset.Y;
            set => Offset = new Vector2(Offset.X, value);
        }

        public Animation(List<Animation_Frame> _frames, string _id)
        {
            Frames = _frames;
            ID = _id;
        }
    }
}