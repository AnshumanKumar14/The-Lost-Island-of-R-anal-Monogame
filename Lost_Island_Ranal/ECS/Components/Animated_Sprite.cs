using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Lost_Island_Ranal.Graphics;


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
/// 
/// </summary>
/// 


//Code from Dariusz, chopped it up and movied to to conform with ECS standards
////////internal class Animation
////////{
////////    private List<AnimationFrame> frames = new List<AnimationFrame>();
////////    private TimeSpan timeIntoAnimation;

////////    private TimeSpan Duration
////////    {
////////        get
////////        {
////////            double totalSeconds = 0;
////////            foreach (var frame in frames)
////////            {
////////                totalSeconds += frame.Duration.TotalSeconds;
////////            }

////////            return TimeSpan.FromSeconds(totalSeconds);
////////        }
////////    }

////////    public void AddFrame(Rectangle rectangle, TimeSpan duration)
////////    {
////////        AnimationFrame newFrame = new AnimationFrame()
////////        {
////////            sourceRectangle = rectangle,
////////            Duration = duration
////////        };

////////        frames.Add(newFrame);
////////    }

////////    public void Update(GameTime gameTime)
////////    {
////////        double secondsIntoAnimation =
////////            timeIntoAnimation.TotalSeconds + gameTime.ElapsedGameTime.TotalSeconds;

////////        double remainder = secondsIntoAnimation % Duration.TotalSeconds;

////////        timeIntoAnimation = TimeSpan.FromSeconds(remainder);
////////    }

////////    public Rectangle CurrentRectangle
////////    {
////////        get
////////        {
////////            AnimationFrame currentFrame = null;

////////            // See if we can find the frame
////////            TimeSpan accumulatedTime = new TimeSpan();
////////            foreach (var frame in frames)
////////            {
////////                if (accumulatedTime + frame.Duration >= timeIntoAnimation)
////////                {
////////                    currentFrame = frame;
////////                    break;
////////                }
////////                else
////////                {
////////                    accumulatedTime += frame.Duration;
////////                }
////////            }

////////            // If no frame was found, then try the last frame,
////////            // just in case timeIntoAnimation somehow exceeds Duration
////////            if (currentFrame == null)
////////            {
////////                currentFrame = frames.LastOrDefault();
////////            }

////////            // If no frame was found, return its rectangle, otherwise
////////            // return an empty rectangle (one with no width or height)
////////            if (currentFrame != null)
////////            {
////////                return currentFrame.sourceRectangle;
////////            }
////////            else
////////            {
////////                return Rectangle.Empty;
////////            }
////////        }
////////    }
////////}
////////}



namespace Lost_Island_Ranal.ECS
{
    class Animated_Sprite : Sprite
    {
        public Dictionary<string, Animation> Animations { get; private set; }

        public string Animation_ID { get; set; } = "";

        public string Current_Animation_ID { get => Animation_ID;
            set {
                if (!Force_Animation)
                    Animation_ID = value;
            } }

        public int Current_Frame { get; set; } = 0;
        public bool Animation_End { get; set; } = false;

        //public Texture2D Texture { get; private set; }
        public float Timer { get; set; } = 0;
        
        public float Flash_Timer { get; set; } = 0;
        //public Color Current_Color { get; set; }

        public bool Playing { get; set; } = true;

        public bool Force_Animation { get; set; } = false;
        public void Force_Play_Animation(string id)
        {
            Current_Animation_ID = id;
            Force_Animation = true;
            Current_Frame = 0;
        }

        public Animated_Sprite(Texture2D _texture, string start_animation) : base(_texture, new Rectangle())
        {
            Type = Types.Animation;
            Animations = new Dictionary<string, Animation>();
            Texture = _texture;

            Current_Animation_ID = start_animation;
        }

        public Animated_Sprite(Texture2D _texture, List<string> anim_ids) : base(_texture, new Rectangle())
        {
            Type = Types.Animation;

            Animations = new Dictionary<string, Animation>();
            Texture = _texture;

            if (anim_ids.Count < 1)  throw new Exception("ERROR:: no anim ids!");

            Current_Animation_ID = anim_ids[0];
            foreach (var id in anim_ids)
            {
                var anim = Assets.It.Get<Animation>(id);
                Animations.Add(anim.ID, anim);
            }
        }
    }
}
