using BouncingGame.Constants;
using BouncingGame.GameStates;
using BouncingGame.Helpers;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BouncingGame.GameObjects
{
    public class Ball : SpriteGameObject
    {
        double eslapsedTime = 0;
        Vector2 lastNormal = Vector2.Zero;

        Vector2? targetPosition;
        Vector2 dropPosition;

        public new ListBall Parent
        {
            get
            {
                return base.Parent as ListBall;
            }
        }

        public bool Shooting { get; private set; }

        public Vector2 PreviousLocation { get; set; }

        private bool droped = true;

        public Vector2 UnitVelocity
        {
            get
            {
                return Vector2.Normalize(velocity);
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }
        }

        List<Brick> touchedBricks;

        public Ball() : base("Sprites/UI/spr_ball_normal_4mm", 0.1f)
        {
            SetOriginToCenterBottom();
            touchedBricks = new List<Brick>();
            Radius = Width / 2;
        }

        public override void Update(GameTime gameTime)
        {
            PreviousLocation = LocalPosition;

            if (targetPosition.HasValue)
            {
                if (targetPosition.Value == dropPosition)
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
                else
                {
                    LocalPosition += (float)gameTime.ElapsedGameTime.TotalSeconds * Vector2.Normalize(targetPosition.Value - dropPosition) * Constant.SideVelocity;

                    if ((dropPosition.X > targetPosition.Value.X && LocalPosition.X - targetPosition.Value.X <= 0) ||
                        (dropPosition.X <= targetPosition.Value.X && LocalPosition.X - targetPosition.Value.X >= 0))
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


            if (!droped)
            {
                HandleCollision();
            }
        }

        public void ReflectRandom()
        {
            var rotation = ExtendedGame.Random.NextDouble() * MathHelper.Pi * 2;
            rotation = MathHelperExtension.Map(rotation, 0, MathHelper.Pi * 2, MathHelper.Pi / 6, MathHelper.Pi - MathHelper.Pi / 6);
            velocity = new Vector2((float)Math.Cos(rotation),-(float)Math.Sin(rotation)) * Constant.BallVelocity;
            lastNormal = Vector2.Zero;
        }

        private void HandleCollision()
        {
            Vector2 distance = LocalPosition - PreviousLocation;
            int stateCount = (int)distance.Length() + 1;
            Vector2 currentPosition = LocalPosition;
            int count;
            for (count = 0; count < stateCount; count++)
            {
                LocalPosition = PreviousLocation + count * UnitVelocity;

                var normals = new List<Vector2>();

                if (HitBottomWall())
                {
                    Drop();
                    break;
                }

                if (HitRightWall())
                {
                    normals.Add(UnitVector.Angle180);
                }

                if (HitLeftWall())
                {
                    normals.Add(UnitVector.Angle0);
                }

                if (HitTopWall())
                {
                    normals.Add(UnitVector.Angle270);
                }

                RefreshTouchedBrick();

                normals.AddRange(ListBrick.Instance.GetNormalVectorsWhenTouchBall(this));

                if (normals.Any())
                {
                    Vector2 combineNormal = normals[0];
                    foreach (var normal in normals)
                    {
                        combineNormal = UnitVector.Combine(combineNormal, normal);
                    }
                    LocalPosition = PreviousLocation + (count - 1) * UnitVelocity;
                    if (Reflect(combineNormal))
                    {
                        break;
                    }
                    else
                    {
                        RevertTouchedBricks();
                    }
                }

                ListItemAddBall.Instance.CheckCollisionWithBall(this);
                ListItemClearRow.Instance.CheckCollisionWithBall(this);
                ListItemClearColumn.Instance.CheckCollisionWithBall(this);
                ListItemAddCoin.Instance.CheckCollisionWithBall(this);
                if (ListItemSpreadBall.Instance.CheckCollisionWithBall(this))
                {
                    break;
                }
            }

            if (count == stateCount)
                LocalPosition = currentPosition;
        }

        public bool Reflect(Vector2 normal)
        {
            return Reflect(normal, ref this.velocity, ref this.lastNormal);
        }


        public bool Reflect(Vector2 normal, ref Vector2 velocity, ref Vector2 lastNormal)
        {
            if (float.IsNaN(normal.X) || float.IsNaN(normal.Y))
                return false;

            if (Vector2.Distance(normal, lastNormal) < 0.2f)
                return false;

            velocity = Vector2.Reflect(velocity, normal);
            lastNormal = normal;
            return true;
        }

        private void Drop()
        {
            float yDistance = 1050f + Origin.Y - Height - GlobalPosition.Y;

            LocalPosition += velocity * (yDistance / velocity.Y);
            if (LocalPosition.X - Origin.X + Width > 700)
            {
                LocalPosition = new Vector2(700 + Origin.X + Width, LocalPosition.Y);
            }
            if (LocalPosition.X - Origin.X < 0)
            {
                LocalPosition = new Vector2(Origin.X, LocalPosition.Y);
            }
            if (Parent.NonDrop)
            {
                velocity = Vector2.Zero;
                Parent.FirstDropBall = this;
                Shooting = false;
                if (!Parent.Shooting)
                {
                    Parent.AllDrop();
                }
            }
            else
            {
                targetPosition = Parent.FirstDropBall.LocalPosition;
                dropPosition = LocalPosition;
            }

            lastNormal = UnitVector.Angle90;
            droped = true;
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
            velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * Constant.BallVelocity;
            eslapsedTime = -peddingTime;
            Shooting = true;
            droped = false;
        }

        public void AddTouchedBrick(Brick brick)
        {
            touchedBricks.Add(brick);
        }

        private void RefreshTouchedBrick()
        {
            touchedBricks.Clear();
        }

        private void RevertTouchedBricks()
        {
            foreach (var brick in touchedBricks)
            {
                brick.RevertTouched();
            }
        }

        public bool Contains(Vector2 globalPosition)
        {
            if (BoundingBox.Contains(globalPosition))
            {
                var pixelLocation = globalPosition - (GlobalPosition - Origin);
                if (!sprite.IsPixelTransparent((int)pixelLocation.X, (int)pixelLocation.Y))
                    return true;
            }

            return false;
        }

        public Vector2 GlobalCenter
        {
            get
            {
                return GlobalPosition - Origin + new Vector2(Width, Height) / 2;
            }
        }

        public int Radius { get; private set; }
    }
}
