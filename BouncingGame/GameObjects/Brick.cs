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
            this.displayText.Text = this.durability.ToString();
            base.Update(gameTime);

            if (Visible)
                CheckColiision();
        }

        private void CheckColiision()
        {
            foreach (var ball in listBall.Balls)
            {
                Vector2 touchPoint;
                if (container.HasPixelPreciseCollision(ball, out touchPoint))
                {
                    var touchVector = new Vector2(container.Width, container.Height) / 2 - touchPoint;
                    touchVector.Normalize();
                    float minDistance = normals.Min(x => Vector2.Distance(x, touchVector));
                    ball.Reflect(normals.FirstOrDefault(x => Vector2.Distance(x, touchVector) == minDistance));
                }
            }
        }
    }
}
