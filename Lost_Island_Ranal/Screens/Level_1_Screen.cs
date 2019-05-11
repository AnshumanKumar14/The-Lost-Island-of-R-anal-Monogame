
/*
 * Created by: Ayran Olckers AKA The Geekiest One
 * -2019-
 * -Game Development Project-
 * This file is subject to the terms and conditions defined in
 * file 'LICENSE.txt', which is part of this source code package.
 */

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Lost_Island_Ranal.ECS;
using Penumbra;
using Lost_Island_Ranal.Graphics;
using Lost_Island_Ranal.Graphics.Particle_Systems;
using NLua;
using System;
using Microsoft.Xna.Framework.Input;
using Lost_Island_Ranal.Gui;
using Microsoft.Xna.Framework.Media;

namespace Lost_Island_Ranal.Screens
{
    class Level_1_Screen : Game_Screen
    {
        //Sky_Renderer sky;
        
        // NOTE(Ayran): Maybe we need to extract this out somewhere else?
        Pause_Menu pause_menu;

        public Level_1_Screen(Screen_Manager screen_manager, World _world, GameCamera _camera, PenumbraComponent _lighting, Particle_World _particle_world, Physics_Engine _physics_engine, Lua lua, GraphicsDevice device) : base(_world, _camera, _lighting, _particle_world, _physics_engine, lua, device,"Level 1")
        {
            //sky = new Sky_Renderer(Assets.It.Get<Texture2D>("sky_1"), false);
            world = _world;
            pause_menu = new Pause_Menu(screen_manager, camera);
        }

        public override void Load(params string []args)
        {
            world.Destroy_All();
            lighting.AmbientColor = new Color(0.6f, 0.6f, 0.6f, 1.0f);
            if (args.Length > 0)
            {
                Load_Map(args[0]);
            }
            else
            {
                Load_Map("Demo");
                //Load_Map("Dungeon_Room_2");
                //Load_Map("Dungeon_Floor_1");
            }




            var ai_system = (AI_System)world.Get_System<AI_System>();
            ai_system.Give_Map(Map);

            var song = Song.FromUri("Bloom", new Uri("Content/Audio/Bloom.mp3", UriKind.Relative));
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;

            camera.Zoom = LostIslandRanal.SCALE;
            LostIslandRanal.Request_Resume(); // Make sure the game is unpaused
        }

        public override void Destroy()
        {
            MediaPlayer.Stop();
            base.Destroy();
        }

        public override void Update(GameTime time)
        {
            //sky.Update(time);
            base.Update(time);

            pause_menu.Update(time);
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            //if (this.Map.Has_Sky)
                //sky.Draw(batch);

            batch.Draw(
                Assets.It.Get<Texture2D>("Background"),
                new Rectangle(0, 0, LostIslandRanal.ScreenWidth, LostIslandRanal.ScreenHeight),
                Color.White
                );
        }

        public override void UIDraw(SpriteBatch batch)
        {
            base.UIDraw(batch);

            pause_menu.Draw(batch);
        }
    }
}
