using System;
using System.Collections.Generic;
using System.Linq;
using Lost_Island_Ranal.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


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
    class Advanced_Animation_Component : Sprite
    {
        public Dictionary<string, Animation> Animations{get; private set;}
        public string Current_Animation_ID {get;set;} = "";
        public int Current_Frame = 0;
        public bool Playing_Animation{get;set;} = false;
        public bool Animation_Finished{get;set;} = false;
        public float Timer {get; set;} = 0;
        // public bool Play_Animation {get;set;} = false;

        public void Request_Animation_Playback(string animation_id){

            if (Animations.ContainsKey(animation_id) == false){
                Console.WriteLine($"WARNING:: Component doesnt contain the animation {animation_id}"); //Error Handling
            }

            if (Playing_Animation) return;

            Animation_Finished = false;
            Playing_Animation = true;
            Current_Animation_ID = animation_id;
            Current_Frame = 0;
        }//Repeat animation
        public void Force_Animation_Playback(string animation_id){
            if (Animations.ContainsKey(animation_id) == false){
                Console.WriteLine($"WARNING:: Component doesnt contain the animation {animation_id}"); //Error Handling
            }

            if (Playing_Animation) {
                if (animation_id == Current_Animation_ID) return;   
            }

            Animation_Finished = false;
            Playing_Animation = true;
            Current_Animation_ID = animation_id;
            Current_Frame = 0;
        }

        //force stop the animation when enitity not called
        public void Stop() => Playing_Animation = false;

        public Animation Get_Current_Animation() => Animations[Current_Animation_ID];
        public Animation_Frame Get_Current_Frame() => Get_Current_Animation().Frames[Current_Frame];
        public void Check_Bounds(){
            if (Current_Frame > Get_Current_Animation().Frames.Count - 1)
                Current_Frame  = 0;
            if (Current_Frame < 0)
                Current_Frame  = Get_Current_Animation().Frames.Count - 1;
        }
        public Advanced_Animation_Component(Texture2D _texture, List<string> animation_ids) : base(_texture, new Rectangle())
        {
            // We extend sprite, so we have to say what the type is
            Type = Types.Advanced_Animation;
            Animations = new Dictionary<string, Animation>();

            if (animation_ids.Count < 1) {
                throw new Exception("ERROR::ADVANCED::ANIMATION::COMPONENT:: this component requires at least one animation!"); //Error Handling
            }            

            Current_Animation_ID = animation_ids.First();
            foreach(var id in animation_ids){
                var animation = Assets.It.Get<Animation>(id);
                Animations.Add(animation.ID, animation);
            }

        }
    }
}
