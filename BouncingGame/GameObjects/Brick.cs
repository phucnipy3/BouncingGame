using BouncingGame.Constants;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace BouncingGame.GameObjects
{
    public class Brick : GameObjectList
    {
        int durability;
        SpriteGameObject container;
        TextGameObject displayText;
        List<Vector2> normals = new List<Vector2> { NormalVector.StandLeft, NormalVector.StandRight, NormalVector.LieBottom, NormalVector.LieTop };
        List<Vector2> specialNormals = new List<Vector2> { NormalVector.InclinedUpRight, NormalVector.InclinedUpLeft, NormalVector.InclinedDownRight, NormalVector.InclinedDownLeft };
        public Brick(int durability, Vector2 position)
        {
            this.durability = durability;
            this.localPosition = position;
            container = new SpriteGameObject("Sprites/UI/spr_box", 1);
            container.Color = Color.Red;
            displayText = new TextGameObject("Fonts/MainFont", 1, Color.White, TextGameObject.HorizontalAlignment.Center, TextGameObject.VerticalAlignment.Center);
            displayText.LocalPosition = new Vector2(container.Width, container.Height) / 2;
            this.AddChild(container);
            this.AddChild(displayText);
        }

        public override void Update(GameTime gameTime)
        {
            if (durability <= 0)
            {
                this.Visible = false;
            }
            this.displayText.Text = this.durability.ToString();
            base.Update(gameTime);

            if (Visible)
                CheckColiision();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
        private void CheckColiision()
        {
            foreach (var ball in ListBall.Instance.Balls)
            {
                Vector2 distance = ball.LocalPosition - ball.PreviousLocation;
                int stateCount = (int)(distance.X / ball.UnitVelocity.X) + 1;
                Vector2 currentPosition = ball.LocalPosition;
                int count;
                for (count = 0; count < stateCount; count++)
                {
                    ball.LocalPosition = ball.PreviousLocation + count * ball.UnitVelocity;
                    if (container.HasPixelPreciseCollision(ball))
                    {
                        var touchVector =
                        (ball.GlobalPosition - ball.Origin + (new Vector2(ball.Width, ball.Height) / 2))
                        - (container.GlobalPosition - container.Origin + (new Vector2(container.Width, container.Height) / 2));
                        touchVector.Normalize();

                        Vector2 normal = Vector2.Zero;
                        bool speacial = false;
                        foreach (var vector in specialNormals)
                        {
                            if (Vector2.Distance(touchVector, vector) <= 0.02f)
                            {
                                normal = vector;
                                speacial = true;
                                break;
                            }
                        }

                        if (!speacial)
                        {
                            float minDistance = normals.Min(x => Vector2.Distance(x, touchVector));
                            normal = normals.FirstOrDefault(x => Vector2.Distance(x, touchVector) == minDistance);
                        }

                        ball.Reflect(normal);

 
                        ball.LocalPosition = ball.PreviousLocation + (count - 1) * ball.UnitVelocity;

                        durability--;
                        break;
                    }
                }

                if (count == stateCount)
                    ball.LocalPosition = currentPosition;
                //if (container.HasPixelPreciseCollision(ball))
                //{
                //    var touchVector =
                //        (ball.GlobalPosition - ball.Origin + (new Vector2(ball.Width, ball.Height) / 2)) 
                //        - ( container.GlobalPosition - container.Origin + (new Vector2(container.Width, container.Height) / 2));
                //    touchVector.Normalize();

                //    Vector2 normal = Vector2.Zero;
                //    bool speacial = false;
                //    foreach (var vector in specialNormals)
                //    {
                //        if (Vector2.Distance(touchVector, vector) <= 0.02f)
                //        {
                //            normal = vector;
                //            speacial = true;
                //            break;
                //        }
                //    }

                //    if (!speacial)
                //    {
                //        float minDistance = normals.Min(x => Vector2.Distance(x, touchVector));
                //        normal = normals.FirstOrDefault(x => Vector2.Distance(x, touchVector) == minDistance);
                //    }

                //    ball.Reflect(normal, CollisionDetection.CalculateIntersection(container.BoundingBox, ball.BoundingBox));

                //    durability--;
                //}
            }
        }
    }
}
