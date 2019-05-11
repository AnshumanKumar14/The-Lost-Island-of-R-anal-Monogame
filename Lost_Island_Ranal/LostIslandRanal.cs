/*
 * Created by: Ayran Olckers AKA The Geekiest One
 * -2019-
 * -Game Development Project-
 * This file is subject to the terms and conditions defined in
 * file 'LICENSE.txt', which is part of this source code package.
 */




using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Lost_Island_Ranal.ECS;
using NLua;
using System.IO;
using Newtonsoft.Json;
using Lost_Island_Ranal.Screens;
using Penumbra;
using Lost_Island_Ranal.Utils;
using Lost_Island_Ranal.ECS.Systems;
using MonoGame.Extended.Input.InputListeners;
using Lost_Island_Ranal.Graphics;
using Lost_Island_Ranal.Gui;
using Microsoft.Xna.Framework.Media;

/// <summary>
/// this iis the main class similar to the Game1. 
/// </summary>
namespace Lost_Island_Ranal
{
    public class LostIslandRanal : Game
    {
        public enum State
        {
            PLAYING,
            PAUSED,
        }

        public static State Game_State { get; set;  } = State.PLAYING;
        public static void Toggle_Pause()
        {
            if ( LostIslandRanal.Game_State == LostIslandRanal.State.PLAYING )
                LostIslandRanal.Game_State = LostIslandRanal.State.PAUSED;
            else LostIslandRanal.Game_State = LostIslandRanal.State.PLAYING;
        }

        public static void Request_Pause()  => Game_State = State.PAUSED;
        public static void Request_Resume() => Game_State = State.PLAYING;

        // bool function to skip intro logos for debug
        private bool skip_intro_animation = false;

        public static bool DEBUG = false;

        public static bool SHOULD_QUIT = false;
        public static readonly float SCALE = 3f;
        public static readonly float GRAPHICS_SCALING = 4f;


        //Call all the classes from the ECS system
        private static GraphicsDeviceManager graphics;
        SpriteBatch batch;
        GameCamera          camera;
        World               world;
        Monogui             gui;
        Screen_Manager      screen_manager;
        Particle_World      particle_world;
        PenumbraComponent   penumbra;
        Debug_Console       console;
        Lua                 lua;
        RenderTarget2D      scene;
        Physics_Engine      physics_engine;
        UI_Manager    invatory_manager;
        PrimitivesBatch     primitives;
        Dialog_Box          dialog_box;
        Lua_Function_System lua_function_system;

        public static int ScreenWidth { get => graphics.PreferredBackBufferWidth; }
        public static int ScreenHeight { get => graphics.PreferredBackBufferHeight; }
        public static (int, int) ScreenSize { get => (ScreenWidth, ScreenHeight); }

        public static int MonitorWidth { get => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width; }
        public static int MonitorHeight { get => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height; }
        public static (int, int) MonitorSize { get => (MonitorWidth, MonitorHeight); }

        public LostIslandRanal()
        {
            var device_width    = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            var device_height   = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            graphics = new GraphicsDeviceManager(this) {
                PreferredBackBufferWidth    = (int)(device_width * 0.8f),
                PreferredBackBufferHeight   = (int)(device_height * 0.8f),
                SynchronizeWithVerticalRetrace = true
            };

            var width   = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            var height  = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            Window.AllowUserResizing    = true;
            Window.AllowAltF4           = true;
            Window.ClientSizeChanged += Window_CLientSizeChanged;

            Window.Position = new Point(width / 2 - ScreenWidth / 2, 0);

            graphics.ApplyChanges();

            this.IsMouseVisible = true;

            Content.RootDirectory = "Content";
            Assets.It.Content = Content;

            lua = new Lua();

            // initialize penumbra lighting
            penumbra    = new PenumbraComponent(this);
            console     = new Debug_Console(this, lua);

            Components.Add(console);
            Components.Add(penumbra);
        }

        void Window_CLientSizeChanged(object sender, EventArgs args)
        {
            camera.WindowResized(GraphicsDevice);
            // Handle window resizing
            //var device_width    = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //var device_height   = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            //graphics.PreferredBackBufferWidth = device_width;
            //graphics.PreferredBackBufferHeight = device_height;

            //graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            Assets.It.Generate_Quads("quads", 512, 8, 8);

            Assets.It.Load_Animations_From_Lua("Content/Lua/Animation.lua");

            //var frames = Assets.It.Get_Quads("player-attack");
            camera = new GameCamera(GraphicsDevice, true);
            screen_manager = new Screen_Manager();

            camera.Zoom = SCALE;

            world           = new World(penumbra);
            particle_world  = new Particle_World();

            //UserInterface.Initialize(Content, BuiltinThemes.hd);

            world.Add_System<Sprite_Renderer_System>(new Sprite_Renderer_System());
            world.Add_System<Player_Controller_System>(new Player_Controller_System(camera, particle_world, screen_manager));
            world.Add_System<Animation_Renderer_System>(new Animation_Renderer_System());
            physics_engine = (Physics_Engine)world.Add_System<Physics_Engine>(new Physics_Engine(world));


            world.Add_System<AI_System>(new AI_System());
            world.Add_System<Light_Emitter_System>(new Light_Emitter_System());
            world.Add_System<World_Interaction_System>(new World_Interaction_System());
            lua_function_system = (Lua_Function_System)world.Add_System<Lua_Function_System>(new Lua_Function_System(lua, camera));
            world.Add_System<Timed_Destroy_System>(new Timed_Destroy_System());
            world.Add_System<Particle_Emitter_System>(new Particle_Emitter_System());
            world.Add_System<Enemy_System>(new Enemy_System());
            world.Add_System<Multipart_Animation_System>(new Multipart_Animation_System());
            world.Add_System<Item_System>(new Item_System());
            world.Add_System<Health_System>(new Health_System());
            world.Add_System<Advanced_Animation_Rendering_System>(new Advanced_Animation_Rendering_System());

            lua["Engine"] = lua_function_system;

            gui = new Monogui();


            penumbra.Initialize();

            // initialize keylisteners
            var keyboard_listener = new KeyboardListener(new KeyboardListenerSettings());
            Components.Add(new InputListenerComponent(this, keyboard_listener));

            keyboard_listener.KeyTyped += console.Key_Typed;

            scene = new RenderTarget2D(graphics.GraphicsDevice, ScreenWidth, ScreenHeight, false, SurfaceFormat.Color, DepthFormat.None, 2, RenderTargetUsage.DiscardContents);

            base.Initialize();
        }

        public void Quit()
        {
            Exit();
        }

        protected override void LoadContent()
        {
            batch = new SpriteBatch(GraphicsDevice);
            primitives = new PrimitivesBatch(batch, GraphicsDevice);
            dialog_box = new Dialog_Box(primitives, lua, lua_function_system);

            var npc_system = (Npc_System)world.Add_System<Npc_System>(new Npc_System(this, graphics, invatory_manager, dialog_box));

            Assets.It.Add("entities",       Content.Load<Texture2D>("entities"));   //add the enemies and entities // LUA 
            Assets.It.Add("items",          Content.Load<Texture2D>("items"));      //add the items from LUA
            Assets.It.Add("Tiles_1",        Content.Load<Texture2D>("Tiles_1"));    //add the Tiles_1
            Assets.It.Add("gui",            Content.Load<Texture2D>("gui"));        //add the the image to gui (health)
            //Assets.It.Add("sky_1",          Content.Load<Texture2D>("sky"));        //add the sky image   
            Assets.It.Add("Boss_Texture",   Content.Load<Texture2D>("boss_1"));     //add the boss texture
            Assets.It.Add("Background",     Content.Load<Texture2D>("Logo_background")); //add the logo background
            Assets.It.Add("font",           Content.Load<SpriteFont>("font"));      //add the font to the main menu
            Assets.It.Add("gfont",          Content.Load<SpriteFont>("GFont"));     //add the font to the game
            Assets.It.Add("dialog_font",    Content.Load<SpriteFont>("DialogFont"));//add the font to dialogu        

            Assets.It.Generate_Rectangle(GraphicsDevice, "gui-rect", 512, 512);

            // TODO(Ayran): Move this to the content pipeline
            Assets.It.Add_Table("Content/Lua/items.lua");
            Assets.It.Add_Table("Content/Lua/Player.lua", true);
            Assets.It.Add_Table("Content/Lua/Enemies.lua");
            Assets.It.Add_Table("Content/Lua/Entities.lua");
            Assets.It.Add_Table("Content/Lua/Npcs.lua");
            Assets.It.Add_Table("Content/Lua/Behaviors/Enemy_Ai.lua");
            Assets.It.Add_Table("Content/Lua/Dialog.lua");

            screen_manager.Register(new Level_1_Screen(screen_manager, world, camera, penumbra, particle_world, physics_engine, lua, GraphicsDevice));
            screen_manager.Register(new Menu_Screen(screen_manager, penumbra, camera));
            screen_manager.Register(new Level_Select_Screen(screen_manager, penumbra, camera));
            screen_manager.Register(new Intro_Logos_Screen(screen_manager, camera, penumbra, Content));

            if (skip_intro_animation == false)
                screen_manager.Goto_Screen("Logo");
            else
                screen_manager.Goto_Screen("Menu");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            if (SHOULD_QUIT) Quit();
            var time = gameTime;

            Timers.It.Update(gameTime);
            Input.It.Update(gameTime);

            camera.Update(time);
            world.Update(time);
            screen_manager.Update(time);
            dialog_box.Update(gameTime);

            Assets.It.Update(gameTime);
            //UserInterface.Update(gameTime);

            if (Input.It.Is_Key_Pressed(Keys.P)) DEBUG = !DEBUG;

            if (Game_State == State.PLAYING)
                particle_world.Update(time);

            base.Update(time);

            Messanger.It.Clear();
        }

        protected override void Draw(GameTime gameTime)
        {
            screen_manager.PreDraw(batch);

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            penumbra.BeginDraw();
            penumbra.Transform = camera.View_Matrix;

            // main draw
            batch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.DepthRead, null, null, camera.View_Matrix);
                world.Draw(batch);
                screen_manager.Draw(batch);

                particle_world.Draw(batch);
            batch.End();

            batch.Begin(SpriteSortMode.FrontToBack, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.DepthRead, null, null, camera.View_Matrix);
                particle_world.Additive_Draw(batch);
            batch.End();

            // filtereds
            batch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.AnisotropicClamp, DepthStencilState.DepthRead, null, null, camera.View_Matrix);
                screen_manager.FilteredDraw(batch);
            batch.End();

            try
            {
                penumbra.Draw(gameTime);
            } catch (Exception) { }

            // gui
            if (DEBUG)
            {
                batch.Begin();
                var font = Assets.It.Get<SpriteFont>("font");
                batch.End();
            }

            batch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp);
                world.UIDraw(batch, camera);
                gui.Draw(batch);
                console.Draw(batch);
                dialog_box.Draw(batch);

                if ( DEBUG )
                {
                    float frameRate = 1f / (float) gameTime.ElapsedGameTime.TotalSeconds;
                    batch.DrawString(Assets.It.Get<SpriteFont>("font"), frameRate.ToString(), new Vector2(10, 10), Color.BurlyWood);
                }
            batch.End();

            // filtered ui draw
            batch.Begin(SpriteSortMode.FrontToBack);
                screen_manager.UIDraw(batch);
            batch.End();


            //UserInterface.Draw(batch);

            //base.Draw(gameTime);
        }
    }
}
