using BouncingGame.GameStates;
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
            LocalPosition = ListBall.Instance.DropPosition + new Vector2(0, -10);
            base.Update(gameTime);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (ListBall.Instance.Shooting)
                return;

            Vector2 mousePosition = inputHelper.MousePositionWorld;
            Rectangle rectangle = new Rectangle(0, 150, 700, 900);

            if (inputHelper.MouseLeftButtonPressed() && rectangle.Contains(mousePosition))
            {
                aimStarted = true;
                startPosition = mousePosition;
            }

            if(aimStarted && inputHelper.MouseLeftButtonDown())
            {
                endPosition = mousePosition;

                Vector2 force = startPosition -endPosition;
                alignment.LocalPosition = Vector2.Normalize(force) * (arrow.Width + 20);
                arrow.Rotation = (float)Math.Atan2(force.Y, force.X);
                alignment.Rotation = arrow.Rotation;
                alignment.Scale = Map(force.Length(), 10f, 1140f, 1f, 0.2f);
                if (force.Length() > 10 && (arrow.Rotation < -MathHelper.Pi / 12) && (arrow.Rotation > -MathHelper.Pi + MathHelper.Pi / 12))
                {
                    canShoot = true;
                }
                else
                {
                    canShoot = false;
                }
            }

            if (inputHelper.MouseLeftButtonReleased())
            {
                if (aimStarted && canShoot)
                {
                    ListBall.Instance.Shoot(arrow.Rotation);
                }

                aimStarted = false;
                canShoot = false;
            }

            Visible = canShoot;

            base.HandleInput(inputHelper);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Visible)
                return;
            arrow.Draw(gameTime, spriteBatch);
            alignment.Draw(gameTime, spriteBatch);
        }

        private float Map(float value, float minSource, float maxSource, float minDestination, float maxDestination)
        {
            return minDestination + (maxDestination - minDestination) * ((maxSource - value) / (maxSource - minSource));
        }
    }
}
