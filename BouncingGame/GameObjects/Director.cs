using BouncingGame.GameStates;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace BouncingGame.GameObjects
{
    public class Director : GameObjectList
    {
        SpriteGameObject arrow;
        Vector2 startPosition = Vector2.Zero;
        Vector2 endPosition = Vector2.Zero;
        bool startAim = false;
        public Director()
        {
            arrow = new SpriteGameObject("Sprites/UI/spr_arrow", 1);
            arrow.SetOriginToLeftCenter();
            arrow.Rotation = -MathHelper.Pi / 2;
            this.AddChild(arrow);
            this.Visible = false;
        }

        public override void Update(GameTime gameTime)
        {
            LocalPosition = ((PlayState)ExtendedGame.GameStateManager.GetGameState(Bouncing.StateName_Play)).DropPosition + new Vector2(0, -10);
            base.Update(gameTime);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            Vector2 mousePosition = inputHelper.MousePositionWorld;
            Rectangle rectangle = new Rectangle(0, 150, 700, 900);

            if (inputHelper.MouseLeftButtonPressed())
            {
                if (rectangle.Contains(mousePosition))
                {
                    startPosition = mousePosition;
                    startAim = true;
                }
                else 
                {
                    startAim = false;
                }
            }

            if (startAim && inputHelper.MouseLeftButtonDown())
            {
                endPosition = mousePosition;

                Vector2 force = endPosition - startPosition;
                arrow.Rotation = (float)Math.Atan2(force.Y, force.X) + MathHelper.Pi;
                Debug.WriteLine(arrow.Rotation);
                if (force.Length() > 10 && (arrow.Rotation > MathHelper.Pi + MathHelper.Pi/12) && (arrow.Rotation< MathHelper.TwoPi - MathHelper.Pi/12))
                {
                    Visible = true;
                }
                else
                {
                    Visible = false;
                }
            }
            else
            {
                Visible = false;
            }

            

            base.HandleInput(inputHelper);
        }
    }
}
