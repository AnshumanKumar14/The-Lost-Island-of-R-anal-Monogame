using Lost_Island_Ranal.Utils;
using Microsoft.Xna.Framework;
using Penumbra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


/*
 * Created by: Ayran Olckers AKA The Geekiest One
 * -2019-
 * -Game Development Project-
 * This file is subject to the terms and conditions defined in
 * file 'LICENSE.txt', which is part of this source code package.
 */

    /// <summary>
    /// Class to load up the intro Logos! 
    /// </summary>
    /// TO DO: Add credits 

namespace Lost_Island_Ranal.Screens
{
    class Intro_Logos_Screen : Screen
    {
        Tasker tasker;

        Texture2D SkyVaultLogo;
        Texture2D MonoGameLogo;
        Texture2D Background;

        Vector2 Logo_Position;
        int Logo_Size = 320;

        float SkyLogoAlpha = 1f;
        float MonoLogoAlpha = 1f;

        float SkyLogoY = LostIslandRanal.ScreenHeight;
        float MonoLogoY = LostIslandRanal.ScreenHeight;

        Color sky_color = Color.White;
        Color mono_color = Color.White;

        Screen_Manager screen_manager;

        public Intro_Logos_Screen(Screen_Manager _manager, GameCamera _camera, PenumbraComponent _penumbra, ContentManager _content) : base("Logo")
        {
            _camera.Zoom = 1;
            _penumbra.AmbientColor = Color.White;

            screen_manager = _manager;

            Logo_Position = new Vector2(
                LostIslandRanal.ScreenWidth / 2,
                LostIslandRanal.ScreenHeight / 2
            );

            SkyLogoY += Logo_Size;
            MonoLogoY += Logo_Size;

            Background = Assets.It.Get<Texture2D>("Background");
        }

        public override void Load(params string []args)
        {
            SkyVaultLogo = Assets.It.Load_Texture("logo", "Logo");
            MonoGameLogo = Assets.It.Load_Texture("SquareLogo_1024px", "Mono");

            float timing = 0.02f;

            tasker = new Tasker(
                (time) =>
                {
                    SkyLogoY = Math2.Lerp(SkyLogoY, LostIslandRanal.ScreenHeight / 2 - Logo_Size / 2, 0.08f);
                    if (SkyLogoY < LostIslandRanal.ScreenHeight / 2 - (Logo_Size / 2) + 1)
                        tasker.Next();
                },
                (time) =>
                {
                    sky_color = Math2.Lerp(sky_color, Color.Transparent, 0.02f);
                    if (Vector4.Distance(sky_color.ToVector4(), Color.Transparent.ToVector4()) < 0.1f)
                    {
                        tasker.Next();
                    }
                },
                (time) =>
                {
                    MonoLogoY = Math2.Lerp(MonoLogoY, LostIslandRanal.ScreenHeight / 2 - Logo_Size / 2, 0.08f);
                    sky_color = Math2.Lerp(sky_color, Color.Transparent, 0.02f);
                    if (MonoLogoY < LostIslandRanal.ScreenHeight / 2 - (Logo_Size / 2) + 1)
                        tasker.Next();
                },
                (time) =>
                {
                    sky_color = Math2.Lerp(sky_color, Color.Transparent, 0.02f);
                    mono_color = Math2.Lerp(mono_color, Color.Transparent, 0.02f);
                    if (Vector4.Distance(mono_color.ToVector4(), Color.Transparent.ToVector4()) < 0.1f)
                    {
                        tasker.Next();
                    }
                },
                (time) =>
                {
                    sky_color = Math2.Lerp(sky_color, Color.Transparent, 0.02f);
                    mono_color = Math2.Lerp(mono_color, Color.Transparent, 0.02f);

                    screen_manager.Goto_Screen("Menu", true);
                    tasker.Next();
                }, (time) => { }
                );   
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            if (Input.It.Is_Key_Pressed(Microsoft.Xna.Framework.Input.Keys.Enter))
                screen_manager.Goto_Screen("Menu");

            if (Input.It.Is_Gamepad_Button_Pressed(Microsoft.Xna.Framework.Input.Buttons.A))
            {
                tasker.Next();
            }

            tasker.Update(time);
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(
                Background,
                new Rectangle(0, 0, LostIslandRanal.ScreenWidth, LostIslandRanal.ScreenHeight),
                Color.White
                );

            batch.Draw(
                MonoGameLogo,
                new Rectangle(
                    (int)Logo_Position.X - Logo_Size / 2,
                    (int)MonoLogoY,
                    Logo_Size,
                    Logo_Size),
                mono_color
                );

            batch.Draw(
                SkyVaultLogo,
                new Rectangle(
                    (int)Logo_Position.X - Logo_Size / 2,
                    (int)SkyLogoY,
                    Logo_Size,
                    Logo_Size),
                sky_color
                );
        }

        public override void Destroy()
        {
            base.Destroy();

            Assets.It.Remove("Logo", typeof(Texture2D));
            Assets.It.Remove("Mono", typeof(Texture2D));
            SkyVaultLogo.Dispose();
            MonoGameLogo.Dispose();
        }
    }
}
