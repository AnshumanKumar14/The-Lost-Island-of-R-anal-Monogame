using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Lost_Island_Ranal.Utils;
using Lost_Island_Ranal.Interfaces;

//-----------------------------------------------------------------------------
// Created by: Ayran Olckers AKA The Geekiest One
// -2019-
// -Game Development Project-
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.
//-----------------------------------------------------------------------------
/// <summary>
///  Code altered from http://community.monogame.net/t/simple-2d-camera/9135 
///  
/// 
/// Attempt to have the game scale to players screen with the hopes to have graphics setting, addly not fully working
/// 
/// Utilising mostly got Camera class to call variables from
/// </summary>
namespace Lost_Island_Ranal.Graphics
{
    class Camera : IMovable, IRotatable
    {
        private readonly ViewportAdapter _viewportAdapter;
        private float _maximumZoom = float.MaxValue;
        private float _minimumZoom;
        private float _zoom;


        /* 
         * 
         * public class Camera
{
    public float Zoom { get; set; }
    public Vector2 Position { get; set; }
    public Rectangle Bounds { get; protected set; }
    public Rectangle VisibleArea { get; protected set; }
    public Matrix Transform { get; protected set; }

    private float currentMouseWheelValue, previousMouseWheelValue, zoom, previousZoom;

    public Camera(Viewport viewport)
    {
        Bounds = viewport.Bounds;
        Zoom = 1f;
        Position = Vector2.Zero;
    }


    private void UpdateVisibleArea()
    {
        var inverseViewMatrix = Matrix.Invert(Transform);

        var tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
        var tr = Vector2.Transform(new Vector2(Bounds.X, 0), inverseViewMatrix);
        var bl = Vector2.Transform(new Vector2(0, Bounds.Y), inverseViewMatrix);
        var br = Vector2.Transform(new Vector2(Bounds.Width, Bounds.Height), inverseViewMatrix);

        var min = new Vector2(
            MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
            MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
        var max = new Vector2(
            MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
            MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));
        VisibleArea = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
    }

    private void UpdateMatrix()
    {
        Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                Matrix.CreateScale(Zoom) *
                Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
        UpdateVisibleArea();
    }

    public void MoveCamera(Vector2 movePosition)
    {
        Vector2 newPosition = Position + movePosition;
        Position = newPosition;
    }

    public void AdjustZoom(float zoomAmount)
    {
        Zoom += zoomAmount;
        if (Zoom < .35f)
        {
            Zoom = .35f;
        }
        if (Zoom > 2f)
        {
            Zoom = 2f;
        }
    }

    public void UpdateCamera(Viewport bounds)
    {
        Bounds = bounds.Bounds;
        UpdateMatrix();

        Vector2 cameraMovement = Vector2.Zero;
        int moveSpeed;

        if (Zoom > .8f)
        {
            moveSpeed = 15;
        }
        else if (Zoom < .8f && Zoom >= .6f)
        {
            moveSpeed = 20;
        }
        else if (Zoom < .6f && Zoom > .35f)
        {
            moveSpeed = 25;
        }
        else if (Zoom <= .35f)
        {
            moveSpeed = 30;
        }
        else
        {
            moveSpeed = 10;
        }


        if (Keyboard.GetState().IsKeyDown(KeyBinds.CameraMoveUp))
        {
            cameraMovement.Y = -moveSpeed;
        }

        if (Keyboard.GetState().IsKeyDown(KeyBinds.CameraMoveDown))
        {
            cameraMovement.Y = moveSpeed;
        }

        if (Keyboard.GetState().IsKeyDown(KeyBinds.CameraMoveLeft))
        {
            cameraMovement.X = -moveSpeed;
        }

        if (Keyboard.GetState().IsKeyDown(KeyBinds.CameraMoveRight))
        {
            cameraMovement.X = moveSpeed;
        }

        previousMouseWheelValue = currentMouseWheelValue;
        currentMouseWheelValue = Mouse.GetState().ScrollWheelValue;

        if (currentMouseWheelValue > previousMouseWheelValue)
        {
            AdjustZoom(.05f);
            Console.WriteLine(moveSpeed);
        }

        if (currentMouseWheelValue < previousMouseWheelValue)
        {
            AdjustZoom(-.05f);
            Console.WriteLine(moveSpeed);
        }

        previousZoom = zoom;
        zoom = Zoom;
        if (previousZoom != zoom)
        {
            Console.WriteLine(zoom);

        }

        MoveCamera(cameraMovement);
    }
}
         * 
         */


        public Camera(GraphicsDevice graphicsDevice)
            : this(new Default_Viewport_Adapter(graphicsDevice))
        {
        }

        public ViewportAdapter GetViewportAdapter() => _viewportAdapter;

        public Camera(ViewportAdapter viewportAdapter)
        {
            _viewportAdapter = viewportAdapter;

            Rotation = 0;
            Zoom = 1;
            Origin = new Vector2(viewportAdapter.VirtualWidth/2f, viewportAdapter.VirtualHeight/2f);
            Position = Vector2.Zero;
        }

        public Vector2 Origin { get; set; }

        public float Zoom
        {
            get { return _zoom; }
            set
            {
                if ((value < MinimumZoom) || (value > MaximumZoom))
                    throw new ArgumentException("Zoom must be between MinimumZoom and MaximumZoom");

                _zoom = value;
            }
        }

        public float MinimumZoom
        {
            get { return _minimumZoom; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("MinimumZoom must be greater than zero");

                if (Zoom < value)
                    Zoom = MinimumZoom;

                _minimumZoom = value;
            }
        }

        public float MaximumZoom
        {
            get { return _maximumZoom; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("MaximumZoom must be greater than zero");

                if (Zoom > value)
                    Zoom = value;

                _maximumZoom = value;
            }
        }

        public RectangleF BoundingRectangle
        {
            get
            {
                var frustum = GetBoundingFrustum();
                var corners = frustum.GetCorners();
                var topLeft = corners[0];
                var bottomRight = corners[2];
                var width = bottomRight.X - topLeft.X;
                var height = bottomRight.Y - topLeft.Y;
                return new RectangleF(topLeft.X, topLeft.Y, width, height);
            }
        }

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }

        public void Move(Vector2 direction)
        {
            Position += Vector2.Transform(direction, Matrix.CreateRotationZ(-Rotation));
        }

        public void Rotate(float deltaRadians)
        {
            Rotation += deltaRadians;
        }

        public void ZoomIn(float deltaZoom)
        {
            ClampZoom(Zoom + deltaZoom);
        }

        public void ZoomOut(float deltaZoom)
        {
            ClampZoom(Zoom - deltaZoom);
        }

        private void ClampZoom(float value)
        {
            if (value < MinimumZoom)
                Zoom = MinimumZoom;
            else
                Zoom = value > MaximumZoom ? MaximumZoom : value;
        }

        public void LookAt(Vector2 position)
        {
            Position = position - new Vector2(_viewportAdapter.VirtualWidth/2f, _viewportAdapter.VirtualHeight/2f);
        }

        public Vector2 WorldToScreen(float x, float y)
        {
            return WorldToScreen(new Vector2(x, y));
        }

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            var viewport = _viewportAdapter.Viewport;
            return Vector2.Transform(worldPosition + new Vector2(viewport.X, viewport.Y), GetViewMatrix());
        }

        public Vector2 ScreenToWorld(float x, float y)
        {
            return ScreenToWorld(new Vector2(x, y));
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            var viewport = _viewportAdapter.Viewport;
            return Vector2.Transform(screenPosition - new Vector2(viewport.X, viewport.Y),
                Matrix.Invert(GetViewMatrix()));
        }

        public Matrix GetViewMatrix(Vector2 parallaxFactor)
        {
            return GetVirtualViewMatrix(parallaxFactor)*_viewportAdapter.GetScaleMatrix();
        }


        //public Matrix get_transformation(GraphicsDevice graphicsDevice)
        //{
        //    var ViewportWidth = graphicsDevice.Viewport.Width;
        //    var ViewportHeight = graphicsDevice.Viewport.Height;
        //    Transform =
        //      Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y * Zoom, 0)) *
        //                                 Matrix.CreateRotationZ(Rotation) *
        //                                 Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
        //                                 Matrix.CreateTranslation(new Vector3(ViewportWidth * 0.5f, ViewportHeight * 0.5f, 0));
        //    return Transform;
        //}

            //Derived from code abovem with Dariuz
        private Matrix GetVirtualViewMatrix(Vector2 parallaxFactor)
        {
            return
                Matrix.CreateTranslation(new Vector3(-Position*parallaxFactor, 0.0f))*
                Matrix.CreateTranslation(new Vector3(-Origin, 0.0f))*
                Matrix.CreateRotationZ(Rotation)*
                Matrix.CreateScale(Zoom, Zoom, 1)*
                Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }

        private Matrix GetVirtualViewMatrix()
        {
            return GetVirtualViewMatrix(Vector2.One);
        }

        public Matrix GetViewMatrix()
        {
            return GetViewMatrix(Vector2.One);
        }

        public Matrix GetInverseViewMatrix()
        {
            return Matrix.Invert(GetViewMatrix());
        }

        private Matrix GetProjectionMatrix(Matrix viewMatrix)
        {
            var projection = Matrix.CreateOrthographicOffCenter(0, _viewportAdapter.VirtualWidth,
                _viewportAdapter.VirtualHeight, 0, -1, 0);
            Matrix.Multiply(ref viewMatrix, ref projection, out projection);
            return projection;
        }

        public BoundingFrustum GetBoundingFrustum()
        {
            var viewMatrix = GetVirtualViewMatrix();
            var projectionMatrix = GetProjectionMatrix(viewMatrix);
            return new BoundingFrustum(projectionMatrix);
        }

        public ContainmentType Contains(Point point)
        {
            return Contains(point.ToVector2());
        }

        public ContainmentType Contains(Vector2 vector2)
        {
            return GetBoundingFrustum().Contains(new Vector3(vector2.X, vector2.Y, 0));
        }

        public ContainmentType Contains(Rectangle rectangle)
        {
            var max = new Vector3(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height, 0.5f);
            var min = new Vector3(rectangle.X, rectangle.Y, 0.5f);
            var boundingBox = new BoundingBox(min, max);
            return GetBoundingFrustum().Contains(boundingBox);
        }
    }
}