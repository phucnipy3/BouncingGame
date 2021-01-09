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
                new List<Vector2> { new Vector2(0 + 3, 0 + 3), new Vector2(0 + 3, 100 - 3), new Vector2(100 - 3, 100 - 3), new Vector2(100 - 3, 0 + 3) },
                new List<Vector2> { new Vector2(0 + 3, 0 + 3), new Vector2(0 + 3, 100 - 3), new Vector2(100 - 3, 100 - 3) },
                new List<Vector2> { new Vector2(0 + 3, 0 + 3), new Vector2(0 + 3, 100 - 3), new Vector2(100 - 3, 0 + 3) },
                new List<Vector2> { new Vector2(0 + 3, 0 + 3), new Vector2(100 - 3, 0 + 3), new Vector2(100 - 3, 100 - 3) },
                new List<Vector2> { new Vector2(0 + 3, 100 - 3), new Vector2(100 - 3, 0 + 3), new Vector2(100 - 3, 100 - 3) },
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
        private bool special;
        int row;


        public int Row
        {
            get
            {
                return row;
            }
        }

        int column;

        public int Column
        {
            get
            {
                return column;
            }
        }
        Vector2 targetPosition;


        public Brick(int durability, int type, int column, bool special = false)
        {
            // adapt input values
            this.durability = durability;
            if (special)
            {
                this.durability *= 2;
            }
            this.column = column;
            this.localPosition = new Vector2(100 * column, 150);
            this.special = special;

            // initialize data
            row = 0;
            targetPosition = LocalPosition;

            // initialize container
            container = new SpriteGameObject("Sprites/UI/spr_brick@5", 0);
            container.Color = Color.White;
            container.Parent = this;

            // initialize text
            text = new TextGameObject("Fonts/Brick", 0.1f, Color.White, TextGameObject.HorizontalAlignment.Center, TextGameObject.VerticalAlignment.Center);
            text.Parent = this;

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
            var color = CalculateColor();
            text.Color = color;
            text.Text = this.durability.ToString();
            container.Color = color;

            base.Update(gameTime);

            if (targetPosition.Y - localPosition.Y <= 0)
            {
                LocalPosition = targetPosition;
                velocity = Vector2.Zero;
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
            if (durability <= 0)
            {
                Visible = false;
            }
        }

        public void MoveDown()
        {
            row++;
            targetPosition = LocalPosition + new Vector2(0, 100);
            velocity = new Vector2(0, 1) * Constant.MoveDownVelocity;
        }

        public bool CheckCollisionWithBall(Ball ball, out Vector2 normal)
        {
            return this.CheckCollisionWithBall(this.corners, this.GlobalPosition, this.Visible, ball, out normal);
        }

        public bool CheckCollisionWithBall(
            List<Vector2> corners, Vector2 globalPosition,
            bool visible, Ball ball, out Vector2 normal)
        {
            normal = Vector2.Zero;

            if (!visible)
                return false;

            Vector2 closestPoint = ClosestPoint(ball.GlobalCenter, corners.Select(x => x + globalPosition).ToArray());

            if (Vector2.Distance(ball.GlobalCenter, closestPoint) <= ball.Radius)
            {
                normal = Vector2.Normalize(ball.GlobalCenter - closestPoint);
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

        public static Vector2 ClosestPoint(Vector2 point, params Vector2[] vertices)
        {
            Vector2 closestPoint = Vector2.Zero;
            float distance = float.MaxValue;



            var S = CalculateArea(vertices);
            var tempS = 0f;
            for (int i = 0; i < vertices.Length; i++)
            {
                tempS += CalculateArea(point, vertices[i], vertices[(i + 1) % vertices.Length]);
            }

            // if point is inside shape
            if (Math.Abs(tempS - S) < 0.00001f)
            {
                return point;
            }

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector2 A = vertices[i];
                Vector2 B = vertices[(i + 1) % vertices.Length];
                Vector2 AB = B - A;
                Vector2 footOnAB = CaculateSetTwoEquations(
                                            new Vector3(AB.X, AB.Y, -AB.X * point.X - AB.Y * point.Y),
                                            new Vector3(AB.Y, -AB.X, -A.X * AB.Y + AB.X * A.Y));

                Vector2 closestPointOnAB;
                closestPointOnAB.X = MathHelper.Clamp(footOnAB.X, Math.Min(A.X, B.X), Math.Max(A.X, B.X));
                closestPointOnAB.Y = MathHelper.Clamp(footOnAB.Y, Math.Min(A.Y, B.Y), Math.Max(A.Y, B.Y));
                float distanceToAB = Vector2.Distance(point, closestPointOnAB);
                if (distanceToAB < distance)
                {
                    distance = distanceToAB;
                    closestPoint = closestPointOnAB;
                }
            }

            return closestPoint;
        }

        public static Vector2 CaculateSetTwoEquations(Vector3 equation1, Vector3 equation2)
        {
            float D = equation1.X * equation2.Y - equation2.X * equation1.Y;
            float Da = -equation1.Z * equation2.Y + equation2.Z * equation1.Y;
            float Db = -equation1.X * equation2.Z + equation2.X * equation1.Z;

            return new Vector2(Da / D, Db / D);
        }


        // Calcuate area of a triangle (3 vertices) or a rectangle (4 vertices)
        public static float CalculateArea(params Vector2[] vertices)
        {
            float p = 0;
            float[] lengths = new float[4];
            for (int i = 0; i < vertices.Length; i++)
            {
                lengths[i] = Vector2.Distance(vertices[i], vertices[(i + 1) % vertices.Length]);
            }

            p = lengths.Sum() / 2;

            float temp = 1f;
            foreach (var length in lengths)
            {
                temp *= (p - length);
            }

            float s = (float)Math.Sqrt(temp);

            return s;
        }

        private Color CalculateColor()
        {
            int level = ((PlayState)ExtendedGame.GameStateManager.GetGameState(StateName.Play)).Level;
            float diff = 255f / level;
            if (special)
                return new Color(100, (int)((level*2 - durability) * diff / 2), 255);
            return new Color(255, (int)((level - durability) * diff), 100);
        }
    }

}
