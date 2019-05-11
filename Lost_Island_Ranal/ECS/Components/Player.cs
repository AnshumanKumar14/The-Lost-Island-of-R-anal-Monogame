using Lost_Island_Ranal.ECS;


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
/// 
/// </summary>
/// 


namespace Lost_Island_Ranal
{
    class Player : Component
    {

        ///
        /// Created by Dariusz - I have just chopped up various parts to fit in with the ECS System:
        /// 
        /// 
        /// Entities
        ///The Entities package is a modern high performance Artemis based Entity Component System.Many of the features found in this implementation were inspired by artemis-odb.Although, many others were also studied during development.As you'll see the systems are designed to feel familar to MonoGame developers.
        /// 
        /// 
        ////////internal class Player : Camera2D
        ////////{
        ////////    public int health;
        ////////    public static Texture2D PlayerClassTextures;
        ////////    public bool Active;
        ////////    public bool isAttacking;
        ////////    public readonly bool Attacking;
        ////////    const float maxAttackTime = 0.33f;
        ////////    float attackTime;
        ////////    public int playerTextureHeight;
        ////////    public int playerTextureWidth;
        ////////    float velocity = 0.9f;
        ////////    KeyboardState previousState;
        ////////    public Vector2 playerPosition;
        ////////    public Rectangle playerRecatngle;
        ////////    public Animation walkDown;
        ////////    public Animation walkUp;
        ////////    public Animation walkLeft;
        ////////    public Animation walkRight;
        ////////    public Animation standDown;
        ////////    public Animation standUp;
        ////////    public Animation standLeft;
        ////////    public Animation standRight;
        ////////    public Animation playerDead;
        ////////    public Animation currentAnimation;



        ////////    //code handling animations and frames
        ////////    public Player(GraphicsDevice graphicsDevice, int newHealth)
        ////////    {
        ////////        if (PlayerClassTextures == null)
        ////////        {
        ////////            using (var stream = TitleContainer.OpenStream("Sprites/charactersheet.png"))
        ////////            {
        ////////                PlayerClassTextures = Texture2D.FromStream(graphicsDevice, stream);
        ////////            }
        ////////        }
        ////////        walkUp = new Animation();
        ////////        walkUp.AddFrame(new Rectangle(144, 0, 16, 16), TimeSpan.FromSeconds(.25));
        ////////        walkUp.AddFrame(new Rectangle(160, 0, 16, 16), TimeSpan.FromSeconds(.25));
        ////////        walkUp.AddFrame(new Rectangle(144, 0, 16, 16), TimeSpan.FromSeconds(.25));
        ////////        walkUp.AddFrame(new Rectangle(176, 0, 16, 16), TimeSpan.FromSeconds(.25));

        ////////        walkDown = new Animation();
        ////////        walkDown.AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));
        ////////        walkDown.AddFrame(new Rectangle(16, 0, 16, 16), TimeSpan.FromSeconds(.25));
        ////////        walkDown.AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));
        ////////        walkDown.AddFrame(new Rectangle(32, 0, 16, 16), TimeSpan.FromSeconds(.25));

        ////////        walkLeft = new Animation();
        ////////        walkLeft.AddFrame(new Rectangle(48, 0, 16, 16), TimeSpan.FromSeconds(.25));
        ////////        walkLeft.AddFrame(new Rectangle(64, 0, 16, 16), TimeSpan.FromSeconds(.25));
        ////////        walkLeft.AddFrame(new Rectangle(48, 0, 16, 16), TimeSpan.FromSeconds(.25));
        ////////        walkLeft.AddFrame(new Rectangle(80, 0, 16, 16), TimeSpan.FromSeconds(.25));

        ////////        walkRight = new Animation();
        ////////        walkRight.AddFrame(new Rectangle(96, 0, 16, 16), TimeSpan.FromSeconds(.25));
        ////////        walkRight.AddFrame(new Rectangle(112, 0, 16, 16), TimeSpan.FromSeconds(.25));
        ////////        walkRight.AddFrame(new Rectangle(96, 0, 16, 16), TimeSpan.FromSeconds(.25));
        ////////        walkRight.AddFrame(new Rectangle(128, 0, 16, 16), TimeSpan.FromSeconds(.25));

        ////////        // Standing animations only have a single frame of animation:
        ////////        standDown = new Animation();
        ////////        standDown.AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));

        ////////        standUp = new Animation();
        ////////        standUp.AddFrame(new Rectangle(144, 0, 16, 16), TimeSpan.FromSeconds(.25));

        ////////        standLeft = new Animation();
        ////////        standLeft.AddFrame(new Rectangle(48, 0, 16, 16), TimeSpan.FromSeconds(.25));

        ////////        standRight = new Animation();
        ////////        standRight.AddFrame(new Rectangle(96, 0, 16, 16), TimeSpan.FromSeconds(.25));

        ////////        playerDead = new Animation();
        ////////        playerDead.AddFrame(new Rectangle(0, 115, 16, 16), TimeSpan.FromSeconds(.25));

        ////////        health = newHealth;


        ////////    }

        ////////    void doAttack(GameTime currentAttackTime)
        ////////    {
        ////////        if (isAttacking == true)
        ////////        {
        ////////            attackTime = maxAttackTime;
        ////////            if (attackTime > 0)
        ////////            {
        ////////                currentAnimation = playerDead;

        ////////            }
        ////////        }
        ////////    }

        ////////    //movement implementation and correct animations used depending on keyboard or player state
        ////////    public void Update(GameTime gameTime)
        ////////    {
        ////////        KeyboardState state = Keyboard.GetState();

        ////////        if (health == 0)
        ////////        {
        ////////            currentAnimation = playerDead;

        ////////        }
        ////////        if (Keyboard.GetState().IsKeyDown(Keys.D))
        ////////        {
        ////////            playerPosition.X += 2 * velocity;
        ////////            currentAnimation = walkRight;
        ////////        }
        ////////        else if (Keyboard.GetState().IsKeyDown(Keys.A))
        ////////        {
        ////////            playerPosition.X -= 2 * velocity;
        ////////            currentAnimation = walkLeft;
        ////////        }
        ////////        else if (Keyboard.GetState().IsKeyDown(Keys.W))
        ////////        {
        ////////            playerPosition.Y -= 2 * velocity;
        ////////            currentAnimation = walkUp;
        ////////        }
        ////////        else if (Keyboard.GetState().IsKeyDown(Keys.S))
        ////////        {
        ////////            playerPosition.Y += 2 * velocity;
        ////////            currentAnimation = walkDown;
        ////////        }
        ////////        else if (state.IsKeyDown(Keys.F) & !previousState.IsKeyDown(Keys.F))
        ////////        {

        ////////            isAttacking = true;
        ////////            doAttack(gameTime);
        ////////        }
        ////////        else
        ////////        {
        ////////            if (currentAnimation == walkRight)
        ////////            {
        ////////                currentAnimation = standRight;
        ////////            }
        ////////            else if (currentAnimation == walkLeft)
        ////////            {
        ////////                currentAnimation = standLeft;
        ////////            }
        ////////            else if (currentAnimation == walkUp)
        ////////            {
        ////////                currentAnimation = standUp;
        ////////            }
        ////////            else if (currentAnimation == walkDown)
        ////////            {
        ////////                currentAnimation = standDown;
        ////////            }
        ////////            else if (currentAnimation == null)
        ////////            {
        ////////                currentAnimation = standDown;
        ////////            }
        ////////        }

        ////////        currentAnimation.Update(gameTime);
        ////////        previousState = state;
        ////////    }

        ////////    public void Initialize(Vector2 Position)
        ////////    {
        ////////        previousState = Keyboard.GetState();
        ////////        playerPosition = Position;
        ////////        Active = true;
        ////////        playerTectureHeight = PlayerClassTextures.Height;
        ////////        playerTextureWidth = PlayerClassTextures.Width;
        ////////    }
        ////////    public void Draw(SpriteBatch spriteBatch)
        ////////    {
        ////////        Color tintColor = Color.White;
        ////////        var sourceRectangle = currentAnimation.CurrentRectangle;
        ////////        spriteBatch.Draw(PlayerClassTextures, playerPosition, sourceRectangle, Color.White);

        ////////    }
        ////////}
    ///}


    public bool Can_Move { get; set; } = true;

        public enum Action_State
        {
            IDLE,
            RUNNING,
            ATTACKING,
            DASHING,
            
            NUM_ACTION_STATES
        }

        public int Combo_Counter = 0;
        public bool Performed_Combo = true;
        public bool Dying = false;
        public bool Going_To_Menu = false;

        //code handling animations and frames
        //    public Player(GraphicsDevice graphicsDevice, int newHealth)
        //    {
        //        if (PlayerClassTextures == null)
        //        {
        //            using (var stream = TitleContainer.OpenStream("Sprites/charactersheet.png"))
        //            {
        //                PlayerClassTextures = Texture2D.FromStream(graphicsDevice, stream);
        //            }
        //        }
        //        walkUp = new Animation();
        //        walkUp.AddFrame(new Rectangle(144, 0, 16, 16), TimeSpan.FromSeconds(.25));
        //        walkUp.AddFrame(new Rectangle(160, 0, 16, 16), TimeSpan.FromSeconds(.25));
        //        walkUp.AddFrame(new Rectangle(144, 0, 16, 16), TimeSpan.FromSeconds(.25));
        //        walkUp.AddFrame(new Rectangle(176, 0, 16, 16), TimeSpan.FromSeconds(.25));

        //        walkDown = new Animation();
        //        walkDown.AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));
        //        walkDown.AddFrame(new Rectangle(16, 0, 16, 16), TimeSpan.FromSeconds(.25));
        //        walkDown.AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));
        //        walkDown.AddFrame(new Rectangle(32, 0, 16, 16), TimeSpan.FromSeconds(.25));

        //        walkLeft = new Animation();
        //        walkLeft.AddFrame(new Rectangle(48, 0, 16, 16), TimeSpan.FromSeconds(.25));
        //        walkLeft.AddFrame(new Rectangle(64, 0, 16, 16), TimeSpan.FromSeconds(.25));
        //        walkLeft.AddFrame(new Rectangle(48, 0, 16, 16), TimeSpan.FromSeconds(.25));
        //        walkLeft.AddFrame(new Rectangle(80, 0, 16, 16), TimeSpan.FromSeconds(.25));

        //        walkRight = new Animation();
        //        walkRight.AddFrame(new Rectangle(96, 0, 16, 16), TimeSpan.FromSeconds(.25));
        //        walkRight.AddFrame(new Rectangle(112, 0, 16, 16), TimeSpan.FromSeconds(.25));
        //        walkRight.AddFrame(new Rectangle(96, 0, 16, 16), TimeSpan.FromSeconds(.25));
        //        walkRight.AddFrame(new Rectangle(128, 0, 16, 16), TimeSpan.FromSeconds(.25));

        //        // Standing animations only have a single frame of animation:
        //        standDown = new Animation();
        //        standDown.AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));

        //        standUp = new Animation();
        //        standUp.AddFrame(new Rectangle(144, 0, 16, 16), TimeSpan.FromSeconds(.25));

        //        standLeft = new Animation();
        //        standLeft.AddFrame(new Rectangle(48, 0, 16, 16), TimeSpan.FromSeconds(.25));

        //        standRight = new Animation();
        //        standRight.AddFrame(new Rectangle(96, 0, 16, 16), TimeSpan.FromSeconds(.25));

        //        playerDead = new Animation();
        //        playerDead.AddFrame(new Rectangle(0, 115, 16, 16), TimeSpan.FromSeconds(.25));

        //        health = newHealth;


        //    }

        public float Dash_Timer = 0;
        public float Max_Dash_Time { get => 1f; }
        public float Attack_Walk_Speed_Multiplyer { get => 0.25f; }



        public Action_State State { get; set; } = Action_State.IDLE;

        public Player() : base(Types.Player)
        {
        }
    }
}
