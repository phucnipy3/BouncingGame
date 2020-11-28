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
        List<Vector2> normals = new List<Vector2> { NormalVector.StandLeft, NormalVector.StandRight, NormalVector.LieBottom, NormalVector.LieTop };
        List<Vector2> specialNormals = new List<Vector2> { NormalVector.InclinedUpRight, NormalVector.InclinedUpLeft, NormalVector.InclinedDownRight, NormalVector.InclinedDownLeft };

        List<Vector2> corners = new List<Vector2> { new Vector2(0,0), new Vector2(0, 0) }

        int row;
        Vector2 targetPosition;
        public List<Vector2> Normals
        {
            get
            {
                return normals;
            }
        }
        public List<Vector2> SpecialNormals
        {
            get
            {
                return specialNormals;
            }
        }


        // TODO: postion => column index
        public Brick(int durability, Vector2 position)
        {
            this.durability = durability;
            this.localPosition = position;
            container = new SpriteGameObject("Sprites/UI/spr_box", 1);
            container.Color = Color.Red;
            text = new TextGameObject("Fonts/MainFont", 1, Color.White, TextGameObject.HorizontalAlignment.Center, TextGameObject.VerticalAlignment.Center);
            text.LocalPosition = new Vector2(container.Width, container.Height) / 2;
            container.Parent = this;
            text.Parent = this;

            row = 1;
            targetPosition = LocalPosition;
        }

        public override void Update(GameTime gameTime)
        {
            if (durability <= 0)
                Visible = false;
            this.text.Text = this.durability.ToString();

            velocity = (targetPosition - LocalPosition) * 4;
            base.Update(gameTime);
            
            if(Vector2.Distance(targetPosition, localPosition) < 1)
            {
                LocalPosition = targetPosition;
            }

            if (LocalPosition.Y >= 946 )
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

        public SpriteGameObject TouchSprite
        {
            get
            {
                return container;
            }
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
            if (TouchSprite.HasPixelPreciseCollision(ball))
            {
                Vector2 centerBall = ball.GlobalPosition - ball.Origin + (new Vector2(ball.Width, ball.Height) / 2);
                Vector2 centerBrick = TouchSprite.GlobalPosition - TouchSprite.Origin + (new Vector2(TouchSprite.Width, TouchSprite.Height) / 2);
                var touchVector = centerBall - centerBrick;
                touchVector.Normalize();

                bool special = false;
                // if hit the corner
                foreach (var vector in specialNormals)
                {
                    if (Vector2.Distance(touchVector, vector) <= 0.001f)
                    {
                        normal = vector;
                        special = true;
                        break;
                    }
                }

                if (!special)
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
    }
}
