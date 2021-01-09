using BouncingGame.GameStates;
using BouncingGame.Helpers;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BouncingGame.GameObjects
{
    public class Director : GameObject
    {
        SpriteGameObject arrow;
        SpriteGameObject alignment;
        Vector2 startPosition = Vector2.Zero;
        Vector2 endPosition = Vector2.Zero;
        bool aimStarted = false;
        bool canShoot = false;
        Vector2 force = Vector2.Zero;
        float rotation = 0f;


        private static Director instance = new Director();

        public static Director Instance
        {
            get
            {
                return instance;
            }
        }
        private Director()
        {
            arrow = new SpriteGameObject("Sprites/UI/spr_arrow", 0);
            arrow.SetOriginToLeftCenter();
            arrow.Rotation = -MathHelper.Pi / 2;
            arrow.Parent = this;
            this.Visible = false;
            alignment = new SpriteGameObject("Sprites/UI/spr_alignment_line", 0.2f);
            alignment.SetOriginToLeftCenter();
            alignment.Rotation = -MathHelper.Pi / 2;
            alignment.Scale = 1f;
            alignment.Parent = this;
        }

        public override void Update(GameTime gameTime)
        {
            LocalPosition = ListBall.Instance.DropPosition - ListBall.Instance.BallOffset;
            UpdateElements();
            base.Update(gameTime);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (ListBall.Instance.Shooting)
                return;
            Visible = CanShoot(inputHelper);
            base.HandleInput(inputHelper);
        }

        private bool CanShoot(InputHelper inputHelper)
        {
            bool shot;
            var result = CanShoot(
                inputHelper.MousePositionWorld,
                inputHelper.MouseLeftButtonPressed(),
                inputHelper.MouseLeftButtonDown(),
                inputHelper.MouseLeftButtonReleased(),
                Visible,
                ref aimStarted,
                ref startPosition,
                ref force,
                ref rotation,
                out shot);
            if (shot)
            {
                ListBall.Instance.Shoot(rotation);
            }

            return result;
        }

        private bool CanShoot(
            Vector2 mousePosition, bool mouseLeftPressed, bool mouseLeftDown,
            bool mouseLeftReleased, bool visible,
            ref bool aimStarted, ref Vector2 startPosition, ref Vector2 force, ref float rotation, out bool shot)
        {
            Rectangle rectangle = new Rectangle(0, 150, 700, 900);
            shot = false;

            if (mouseLeftPressed && rectangle.Contains(mousePosition))
            {
                aimStarted = true;
                startPosition = mousePosition;
            }

            if (aimStarted && mouseLeftDown)
            {
                force = startPosition - mousePosition;
                rotation = (float)Math.Atan2(force.Y, force.X);
                return force.Length() > 10 && (rotation < -MathHelper.Pi / 12) && (rotation > -MathHelper.Pi + MathHelper.Pi / 12);
            }

            if (mouseLeftReleased)
            {
                if (visible)
                {
                    shot = true;
                }

                aimStarted = false;
                return false;
            }

            return false;
        }


        private void UpdateElements()
        {
            alignment.LocalPosition = Vector2.Normalize(force) * (arrow.Width + 20);
            arrow.Rotation = rotation;
            alignment.Rotation = rotation;
            alignment.Scale = MathHelperExtension.Map(force.Length(), 10f, 1140f, 1f, 0.2f);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Visible)
                return;
            arrow.Draw(gameTime, spriteBatch);
            alignment.Draw(gameTime, spriteBatch);
        }

        
    }
}
