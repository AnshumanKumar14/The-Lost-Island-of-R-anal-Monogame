using Lost_Island_Ranal.ECS;
using Lost_Island_Ranal.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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


/// <summary>
/// Game aCamera - Camera followsw player around
/// 
/// giving an isometric feel
/// </summary>
/// 




////    Here's one way to do it if you don't mind taking advantage of XNA's API, which should work for every case. I can think of a few alternatives but they would involve doing more calculations manually, by taking advantage of the fact that your camera is orthogonal (i.e. the BoundingFrustum is just a box too).

////Step 1) Store BoundingSphere(or BoundingBox) for each model

////You can create the BoundingSphere using the following helper method:

////BoundingSphere boundingSphere = BoundingSphere.CreateFromPoints(model.Vertices);
////Ideally this should be done in the content processor and stored in the model, but you can also do it at load time. Don't forget to also Transform the BoundingSphere whenever you move the model.

////Step 2) Create a BoundingFrustum for your camera

////The constructor takes the ViewProrjectionMatrix:

////BoundingFrustum frustum = new BoundingFrustum(viewMatrix* projectionMatrix);
////Step 3) Check for Intersection

////Draw only the models whose BoundingSpheres intersect(or are contained) by the BoundingFrustum:

////models.Where(m => frustum.Contains(m.BoundingSphere) != ContainmentType.Disjoint).Draw();
///SOurce::: https://gamedev.stackexchange.com/questions/21779/how-to-draw-only-visible-models-of-the-game






namespace Lost_Island_Ranal
{

    //set camera size relaticve to to player
    class GameCamera
    {
        private Camera camera;

        public float Zoom { get => camera.Zoom; set => camera.Zoom = value; }
        public float Rotation { get; set; }

        public Vector2 Position { get => camera.Position; set { camera.Position = value; } }
        public Vector2 Origin { get => camera.Origin; set { camera.Origin = value; } }

        public float X { get { return Position.X; } }
        public float Y { get { return Position.Y; } }

        bool can_move = true;

        private float shake_timer = 0;
        private float shake_intensity = 10;

        public GameCamera(GraphicsDevice device, bool _scrollable = false)
        { 
            Rotation = 0;
            
            camera = new Camera(device);
        }

        public void Update(GameTime time)
        {

            var rnd = new Random();
            if (shake_timer > 0 && (int)time.TotalGameTime.TotalMilliseconds % 2 == 0)
            {
                camera.Move(new Vector2(
                    (float)((-shake_intensity / 2) + rnd.NextDouble() * shake_intensity),
                    (float)((-shake_intensity / 2) + rnd.NextDouble() * shake_intensity)
                    ));
                shake_timer -= (float) time.ElapsedGameTime.TotalSeconds;
            }
        }

        //Window resize using ViewPortAdapter
        public void WindowResized(GraphicsDevice device)
        {
            var ad = camera.GetViewportAdapter();
            camera.Origin = new Vector2(ad.VirtualWidth/2f, ad.VirtualHeight/2f);
        }

        public void Track(Body body, float smoothing)
        {
            if (!can_move) {
                can_move = true;
                return;
            }

            var dx = (X - (body.Center.X) + (LostIslandRanal.ScreenWidth) / 2);
            var dy = (Y - (body.Center.Y) + (LostIslandRanal.ScreenHeight) / 2);

            camera.Move(new Vector2(-dx * smoothing, -dy * smoothing));
        }

        public void Shake(float intensity, float time)
        {
            shake_timer = time;
            shake_intensity = intensity;
        }

        public BoundingFrustum Get_Camera_Frustum()
        {
            return camera.GetBoundingFrustum();
        }

        public Vector2 Left
        {
            get => camera.Position + new Vector2(LostIslandRanal.ScreenWidth / 2, LostIslandRanal.ScreenHeight / 2) - new Vector2(LostIslandRanal.ScreenWidth / 2 / Zoom, LostIslandRanal.ScreenHeight / 2 / Zoom);
        }

        public void Move(Vector2 by)
        {
            can_move = false;
            camera.Move(by);
        }

        public Camera Get_Controller() => camera;

        public Vector2 Get_Camera_Position_In_Worldspace()
        {
            return Left;
        }

        public Vector2 World_To_Screen(Vector2 point)
        {
            return camera.WorldToScreen(point);
        }

        public Vector2 Screen_To_World(Vector2 point)
        {
            return camera.ScreenToWorld(point);
        }

        public Matrix View_Matrix
        {
            get
            {
                return
                    Matrix.CreateTranslation(new Vector3(-new Vector2(Position.X, Position.Y), 0.0f)) *
                    Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                    Matrix.CreateRotationZ(Rotation) *
                    Matrix.CreateScale(Zoom, Zoom, 1) *
                    Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
            }
        }
    }
}
