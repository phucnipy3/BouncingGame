using BouncingGame.Constants;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BouncingGame.GameObjects
{
    public class Brick : GameObjectList
    {
        int durability;
        SpriteGameObject container;
        TextGameObject displayText;
        ListBall listBall;
        List<Vector2> normals = new List<Vector2> { NormalVector.StandLeft, NormalVector.StandRight, NormalVector.LieBottom, NormalVector.LieTop };
        List<Vector2> specialNormals = new List<Vector2> { NormalVector.InclinedUpRight, NormalVector.InclinedUpLeft, NormalVector.InclinedDownRight, NormalVector.InclinedDownLeft };
        public Brick(int durability, Vector2 position, ListBall listBall)
        {
            this.durability = durability;
            this.localPosition = position;
            this.listBall = listBall;
            container = new SpriteGameObject("Sprites/UI/spr_box", 1);
            displayText = new TextGameObject("Fonts/MainFont", 1, Color.White, TextGameObject.HorizontalAlignment.Center, TextGameObject.VerticalAlignment.Center);
            displayText.LocalPosition = new Vector2(container.Width, container.Height) / 2;
            this.AddChild(container);
            this.AddChild(displayText);
        }

        public override void Update(GameTime gameTime)
        {
            if(durability <= 0)
            {
                this.Visible = false;
            }
            this.displayText.Text = this.durability.ToString();
            base.Update(gameTime);

            if (Visible)
                CheckColiision();
        }

        private void CheckColiision()
        {
            foreach (var ball in listBall.Balls)
            {
                if (container.HasPixelPreciseCollision(ball))
                {
                    var touchVector =
                        (ball.GlobalPosition - ball.Origin + (new Vector2(ball.Width, ball.Height) / 2)) 
                        - ( container.GlobalPosition - container.Origin + (new Vector2(container.Width, container.Height) / 2));
                    touchVector.Normalize();

                    Vector2 normal = Vector2.Zero;
                    bool speacial = false;
                    foreach (var vector in specialNormals)
                    {
                        if (Vector2.Distance(touchVector, vector) <= 0.001f)
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

                    durability--;
                }
            }
        }
    }
}
