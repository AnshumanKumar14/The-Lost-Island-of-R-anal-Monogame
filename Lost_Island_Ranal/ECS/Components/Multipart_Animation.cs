
using System.Collections.Generic;


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


namespace Lost_Island_Ranal.ECS.Components
{
    class Multipart_Animation : Component
    {
        public Dictionary<string, Animated_Sprite> Animation_Components { get; set; }

        public Multipart_Animation() : base(Types.Multipart_Animation)
        {
            Animation_Components = new Dictionary<string, Animated_Sprite>();
        }

    }
}
