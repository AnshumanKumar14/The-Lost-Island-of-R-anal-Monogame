using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace TheLostIslandRanal
{
    class Player
    {
        public Texture2D PlayerTexture;
        public Vector2 PlayerPosition;
        public bool Active;
        public int Health;
        public Rectangle SourceRect;
        Vector2 graphicsInfo;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        float playerMoveSpeed;
        public int Width
        {
            get { return PlayerTexture.Width; }
        }
        public int Height
        {
            get { return PlayerTexture.Height; }
        }

        public void Initialize(Texture2D texture, Vector2 position, Vector2 grInfo)
        {
            PlayerTexture = texture;

            SourceRect = new Rectangle(0, 0, 115, 69);

            PlayerPosition = position;

            Active = true;

            Health = 3;

            graphicsInfo = grInfo;
            playerMoveSpeed = 4.0f;
        }
        public void Update(GameTime gameTime)
        {
            previousKeyboardState = currentKeyboardState;

            currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                PlayerPosition.X -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                PlayerPosition.X += playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                PlayerPosition.Y -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                PlayerPosition.Y += playerMoveSpeed;
            }

            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlayerTexture,
                PlayerPosition,
                SourceRect,
                Color.White,
                0f,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                0f);
        }
    }
}
