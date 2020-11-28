using BouncingGame.Constants;
using BouncingGame.GameStates;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BouncingGame.GameObjects
{
    public class Brick : GameObject
    {
        int durability;
        SpriteGameObject container;
        TextGameObject text;

        List<List<Vector2>> StandardNormals =
            new List<List<Vector2>>
            {
                new List<Vector2> { UnitVector.Angle0, UnitVector.Angle90, UnitVector.Angle180, UnitVector.Angle270 },
                new List<Vector2> { UnitVector.Angle45, UnitVector.Angle180, UnitVector.Angle270 },
                new List<Vector2> { UnitVector.Angle90, UnitVector.Angle180, UnitVector.Angle315 },
                new List<Vector2> { UnitVector.Angle0, UnitVector.Angle90, UnitVector.Angle225 },
                new List<Vector2> { UnitVector.Angle0, UnitVector.Angle135, UnitVector.Angle270 },
            };

        List<List<Vector2>> StandardCombinedNormals =
            new List<List<Vector2>>
            {
                new List<Vector2> {
                    UnitVector.Angle135,
                    UnitVector.Angle225,
                    UnitVector.Angle45,
                    UnitVector.Angle315
                },
                new List<Vector2> {
                    UnitVector.Combine(UnitVector.Angle180, UnitVector.Angle45),
                    UnitVector.Angle225,
                    UnitVector.Combine(UnitVector.Angle270, UnitVector.Angle45),

                },
                new List<Vector2> {
                    UnitVector.Angle135,
                    UnitVector.Combine(UnitVector.Angle180, UnitVector.Angle315),
                    UnitVector.Combine(UnitVector.Angle90, UnitVector.Angle315),
                },
                new List<Vector2> {
                    UnitVector.Combine(UnitVector.Angle90, UnitVector.Angle225),
                    UnitVector.Angle45,
                    UnitVector.Combine(UnitVector.Angle0, UnitVector.Angle225),
                },
                new List<Vector2> {
                    UnitVector.Combine(UnitVector.Angle270, UnitVector.Angle135),
                    UnitVector.Combine(UnitVector.Angle0, UnitVector.Angle135),
                    UnitVector.Angle315,
                },

            };

        List<List<Vector2>> StandardCorners =
            new List<List<Vector2>>
            {
                new List<Vector2> { new Vector2(0, 0), new Vector2(0, 100), new Vector2(100, 0), new Vector2(100, 100) },
                new List<Vector2> { new Vector2(0, 0), new Vector2(0, 100), new Vector2(100, 100) },
                new List<Vector2> { new Vector2(0, 0), new Vector2(0, 100), new Vector2(100, 0) },
                new List<Vector2> { new Vector2(0, 0), new Vector2(100, 0), new Vector2(100, 100) },
                new List<Vector2> { new Vector2(0, 100), new Vector2(100, 0), new Vector2(100, 100) },
            };

        List<Vector2> centers =
            new List<Vector2>
            {
                new Vector2(50,50),
                new Vector2(29.28931881f, 100 - 29.28931881f),
                new Vector2(29.28931881f, 29.28931881f),
                new Vector2(100 - 29.28931881f, 29.28931881f),
                new Vector2(100 - 29.28931881f, 100 - 29.28931881f),
            };

        List<Vector2> normals;
        List<Vector2> combinedNormals;
        List<Vector2> corners;
        Vector2 centerPoint;

        Dictionary<Vector2, Vector2> cornerNormals = new Dictionary<Vector2, Vector2>();

        int row;
        Vector2 targetPosition;


        // TODO: postion => column index
        public Brick(int durability, Vector2 position)
        {
            // adapt input values
            this.durability = durability;
            this.localPosition = position;

            // initialize data
            row = 1;
            targetPosition = LocalPosition;

            // initialize container
            container = new SpriteGameObject("Sprites/UI/spr_brick@5", 0);
            container.Color = Color.White;
            container.Parent = this;

            // initialize text
            text = new TextGameObject("Fonts/MainFont", 0.1f, Color.White, TextGameObject.HorizontalAlignment.Center, TextGameObject.VerticalAlignment.Center);
            text.Parent = this;

            // set up type different
            int type = ExtendedGame.Random.Next(8);
            if(type % 2 == 0)
            {
                type = 0;
            }
            else
            {
                type = type / 2 + 1;
            }
            container.SheetIndex = type;
            normals = StandardNormals[type];
            combinedNormals = StandardCombinedNormals[type];
            corners = StandardCorners[type];
            centerPoint = centers[type];
            text.LocalPosition = centerPoint;

            for (int i = 0; i < corners.Count; i++)
            {
                cornerNormals.Add(corners[i], combinedNormals[i]);
            }

        }

        public override void Update(GameTime gameTime)
        {
            if (durability <= 0)
                Visible = false;
            this.text.Text = this.durability.ToString();

            velocity = (targetPosition - LocalPosition) * 4;
            base.Update(gameTime);

            if (Vector2.Distance(targetPosition, localPosition) < 1)
            {
                LocalPosition = targetPosition;
            }

            if (LocalPosition.Y >= 946)
            {
                ((PlayState)ExtendedGame.GameStateManager.GetGameState(StateName.Play)).GameOver();
            }
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Visible)
                return;
            container.Draw(gameTime, spriteBatch);
            text.Draw(gameTime, spriteBatch);
        }

        public void Touched()
        {
            durability--;
        }

        public void MoveDown()
        {
            row++;
            targetPosition = LocalPosition + new Vector2(0, 100);
        }

        public bool CheckCollisionWithBall(Ball ball, out Vector2 normal)
        {
            normal = Vector2.Zero;

            if (!Visible)
                return false;
            if (container.HasPixelPreciseCollision(ball))
            {
                Vector2 centerBall = ball.GlobalCenter;
                Vector2 centerBrick = GlobalCenter;
                var touchVector = centerBall - centerBrick;
                touchVector.Normalize();

                bool touchCorner = false;
                foreach (var corner in corners)
                {
                    if (ball.Contains(GlobalPosition + corner))
                    {
                        normal = cornerNormals[corner];
                        touchCorner = true;
                        break;
                    }
                }

                if (!touchCorner)
                {
                    float minDistance = normals.Min(x => Vector2.Distance(x, touchVector));
                    normal = normals.FirstOrDefault(x => Vector2.Distance(x, touchVector) == minDistance);
                }

                return true;
            }

            return false;
        }

        public void RevertTouched()
        {
            durability++;
        }

        public Vector2 GlobalCenter
        {
            get
            {
                return container.GlobalPosition - container.Origin + centerPoint;
            }
        }
    }
}
