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
            arrow = new SpriteGameObject("Sprites/UI/spr_arrow", 1);
            arrow.SetOriginToLeftCenter();
            arrow.Rotation = -MathHelper.Pi / 2;
            arrow.Parent = this;
            this.Visible = false;
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

                Vector2 force = endPosition - startPosition;
                arrow.Rotation = (float)Math.Atan2(force.Y, force.X) + MathHelper.Pi;
                if (force.Length() > 10 && (arrow.Rotation > MathHelper.Pi + MathHelper.Pi / 12) && (arrow.Rotation < MathHelper.TwoPi - MathHelper.Pi / 12))
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
        }
    }
}
