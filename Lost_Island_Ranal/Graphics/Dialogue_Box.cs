using Lost_Island_Ranal.ECS.Systems;
using Lost_Island_Ranal.Graphics;
using Lost_Island_Ranal.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using CColor = System.Drawing.Color;

//-----------------------------------------------------------------------------
// Created by: Ayran Olckers AKA The Geekiest One
// -2019-
// -Game Development Project-
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.
//-----------------------------------------------------------------------------

/// <summary>
/// Draws the Dialogue Box
/// 
/// inspired by: http://community.monogame.net/t/how-to-implement-a-simple-dialog-box/8200
/// 
/// 
/// Static things:
//- A background.
//- Any fancy stuff you would like to add (like the quest giver's avatar in the top right corner).

//Dynamic things:
//- X number of texts that are displayed in each step of the dialogue.
//- Buttons for navigating in the dialogue (obviously back and continue visibility/enabled status should be based on the step number, like Back cannot be seen at the first step and Continue cannot be seen in the final step, etc...).
//- Probably a container for specific controls that are used for reacting on quests.Sometimes an Accept/Decline button pair, sometimes multiple button where more than 2 selections are available, etc...These should be a user control and all of them should be inherited from a generic base control.
/// </summary>
namespace Lost_Island_Ranal
{
    class Dialog_Box
    {
        public Dialogue CurrentDialog {get; private set;} = null; //current dialoge from NPC
        public int CurrentDialogTextPointer {get; private set;} = 1;
        public bool IsOpen { get => CurrentDialog != null; }
        public int Selector { get; private set; } = 0;

        private float timer = 0;
        private Lua lua; //Call Lua Npcs/dialogue?
        private Lua_Function_System engine; //call functions from class

        public int GetNextDialogPointer() {
            if (IsOpen == false) return 0;
            return
                CurrentDialog.Dialog_Texts[CurrentDialogTextPointer].NextDialogText;
        }

        //call PrimitiveBatch to draw the dialogue box
        PrimitivesBatch primitives;
        public Dialog_Box(PrimitivesBatch _primitives, Lua _lua, Lua_Function_System _engine) {
            primitives  = _primitives;
            lua         = _lua;
            engine      = _engine;
        }

        public bool TryOpen(Dialogue dialog) {
            if (!IsOpen && this.timer <= 0) {
                CurrentDialogTextPointer = 1;
                CurrentDialog = dialog;
                return true;
            }
            return false;
        }

        //NPC 
        public void Update(GameTime time) {
            timer -= (float)time.ElapsedGameTime.TotalSeconds;
            if (!IsOpen) { Selector = 0; return; }

            //// Check and see if the npc was destroyed
            //if (CurrentDialog.Speaker == null || CurrentDialog.Speaker.Remove == true)
            //{
            //    CurrentDialog = null;
            //    LostIslandRanal.Request_Resume();
            //    return;
            //}

            LostIslandRanal.Request_Pause(); //Pause game

            //press enter for seleccted option
            if (Input.It.Is_Key_Pressed(Keys.Enter) || Input.It.Is_Key_Pressed(Keys.Z)) {
                if (CurrentDialog.Dialog_Texts[CurrentDialogTextPointer].options.Count() == 0) {
                    CurrentDialogTextPointer = GetNextDialogPointer();
                    if (CurrentDialogTextPointer == 0)
                    {
                        timer = Constants.DIALOG_COOLDOWN;
                        CurrentDialog = null;
                        LostIslandRanal.Request_Resume();
                        return;
                    }
                } 
            }

            //navigation controll of mrnu
            var dialog_text = CurrentDialog.Dialog_Texts[CurrentDialogTextPointer];
            if (dialog_text.options.Count > 0)
            {
                if (Input.It.Is_Key_Pressed(Keys.Left) || Input.It.Is_Key_Pressed(Keys.Up)) 
                {
                    Selector--;
                } 
                else if (Input.It.Is_Key_Pressed(Keys.Right) || Input.It.Is_Key_Pressed(Keys.Down))
                {
                    Selector++;
                }

                if (Input.It.Is_Key_Pressed(Keys.Enter) || Input.It.Is_Key_Pressed(Keys.Z))
                {
                    var the_option = dialog_text.options[Selector];
                    CurrentDialogTextPointer = the_option.NextDialogText;
                    if (the_option.action != null)
                    {
                        var result = the_option.action.Call(CurrentDialog.Speaker, CurrentDialog.Target, engine);
                        if (result != null)
                        {
                            CurrentDialogTextPointer = (int)(result[0] as double?);
                        }
                    }
                }

                Selector = Selector < 0 ? dialog_text.options.Count - 1 : (Selector > dialog_text.options.Count - 1 ? 0 : Selector);
            }
        }

        public void Draw(SpriteBatch batch) {
            if (!IsOpen) return;
            if (CurrentDialogTextPointer == 0)
            {
                CurrentDialog = null;
                LostIslandRanal.Request_Resume();
                return;
            }

            var height = LostIslandRanal.ScreenHeight / 3;
            var color = new Color(0.2f, 0.2f, 0.2f, 0.9f);

            var font = Assets.It.Get<SpriteFont>("gfont"); //add font to dialogue

            var dialog_text = CurrentDialog.Dialog_Texts[CurrentDialogTextPointer];
            var text = dialog_text.Value;

            primitives.DrawFilledRect(
                new Vector2(0, LostIslandRanal.ScreenHeight - height),
                new Vector2(LostIslandRanal.ScreenWidth, height),
                color,
                0,
                0.98f
            );

            batch.DrawString(
                    font,
                    text,
                    new Vector2(32, LostIslandRanal.ScreenHeight - height + 32),
                    Color.White,
                    0.0f,
                    Vector2.Zero,
                    0.5f,
                    SpriteEffects.None,
                    1.0f
                    );

            if (dialog_text.options.Count > 0)
            {
                var x = 0.0f;
                var index = 0;
                foreach(var option in dialog_text.options)
                {
                    var margin = 16;
                    var tsize = font.MeasureString(option.Value) * 0.5f;
                    var size_x = tsize.X + margin;
                    batch.DrawString(
                        font,
                        option.Value,
                        new Vector2(32 + x, LostIslandRanal.ScreenHeight - 32 - margin),
                        Color.White,
                        0.0f,
                        Vector2.Zero,
                        0.5f,
                        SpriteEffects.None,
                        1.0f
                    );

                    if (index == Selector)
                    {
                        var rect = Assets.It.Get<Texture2D>("gui-rect");
                        batch.Draw(
                        rect,
                        new Rectangle((int)(32 + x), LostIslandRanal.ScreenHeight - 32 - margin, (int)tsize.X, (int)tsize.Y),
                        new Rectangle(0, 0, 512, 512),
                        Color.Orange,
                        0f,
                        Vector2.Zero,
                        SpriteEffects.None,
                        0.99f
                        );
                    }

                    x += size_x;
                    index++;
                }
            }
        }
    }
}
