using BouncingGame.Constants;
using BouncingGame.GameStates;
using Engine;
using Microsoft.Xna.Framework;
using System;

namespace BouncingGame.GameObjects
{
    public class Ball : SpriteGameObject
    {
        float forceLength = 900;
        double eslapsedTime = 0;
        Vector2 lastNormal = NormalVector.LieBottom;

        Vector2? targetPosition;

        public new ListBall Parent 
        {
            get
            {
                return base.Parent as ListBall;
            }
        }

        public bool Shooting { get; private set; }
        public Ball() : base("Sprites/UI/spr_ball_normal_4mm", 1)
        {
            SetOriginToCenterBottom();
        }

        public override void Update(GameTime gameTime)
        {

            if (targetPosition.HasValue)
            {
                LocalPosition += (float)gameTime.ElapsedGameTime.TotalSeconds * (targetPosition.Value - LocalPosition) * 8;
                if(Vector2.Distance(LocalPosition, targetPosition.Value) < 2)
                {
                    Shooting = false;
                    LocalPosition = targetPosition.Value;
                    targetPosition = null;
                    velocity = Vector2.Zero;
                    if (!Parent.Shooting)
                    {
                        Parent.AllDrop();
                    }
                }
            }
            else
            {
                eslapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

                if (eslapsedTime > 0)
                {
                    LocalPosition += velocity * (float)eslapsedTime;
                    eslapsedTime = 0;
                }
            }

            CheckCollision();
        }

        private void CheckCollision()
        {
            if (HitRightWall())
            {
                velocity = Vector2.Reflect(velocity, NormalVector.StandRight);
                lastNormal = NormalVector.StandRight;
            }

            if (HitLeftWall())
            {
                velocity = Vector2.Reflect(velocity, NormalVector.StandLeft);
                lastNormal = NormalVector.StandLeft;
            }

            if (HitTopWall())
            {
                velocity = Vector2.Reflect(velocity, NormalVector.LieTop);
                lastNormal = NormalVector.LieTop;
            }

            if (HitBottomWall())
            {
                Drop();
                lastNormal = NormalVector.LieBottom;
            }
        }

        public void Reflect(Vector2 normal)
        {
            if (normal.Equals(lastNormal))
                return;
            velocity = Vector2.Reflect(velocity, normal);
            lastNormal = normal;
        }

        private void Drop()
        {
            float yDistance = 1050f - GlobalPosition.Y;
            LocalPosition = new Vector2(LocalPosition.X, LocalPosition.Y + yDistance);
            if (Parent.NonDrop)
            {
                velocity = Vector2.Zero;
                Parent.FirstDropBall = this;
                Shooting = false;
            }
            else
            {
                targetPosition = Parent.FirstDropBall.LocalPosition;
            }
        }

        private bool HitBottomWall()
        {
            return GlobalPosition.Y - Origin.Y + Height > 1050;
        }

        private bool HitTopWall()
        {
            return GlobalPosition.Y - Origin.Y < 150;
        }

        private bool HitLeftWall()
        {
            return GlobalPosition.X - Origin.X < 0;
        }

        private bool HitRightWall()
        {
            return GlobalPosition.X - Origin.X + Width > 700;
        }

        public void Shoot(float rotation, double peddingTime)
        {
            velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * forceLength;
            eslapsedTime = -peddingTime;
            Shooting = true;
        }

    }
}
