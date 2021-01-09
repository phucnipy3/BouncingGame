using BouncingGame.Constants;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncingGame.GameObjects
{
    public class ItemSpreadBall : AnimatedGameObject
    {
        public int Row { get; private set; } = 0;
        private Vector2 targetPosition;
        private bool intersected = false;

        private int[] offsetWidths = new int[] { -10, -8, -6, -4, -2, 0, -2, -4, -6, -8, -10 };


        public ItemSpreadBall(int column): base(0f)
        {
            LoadAnimation("Sprites/UI/spr_item_spread_ball", "stay", false, 1);
            LoadAnimation("Sprites/Animations/spr_animation_item_spread_ball@11", "anim", false, 0.01f);
            PlayAnimation("stay", true);
            SetOriginToCenter();
            LocalPosition = new Vector2(50 + column * 100,50 + 150);
            targetPosition = LocalPosition;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public void MoveDown()
        {
            if (intersected)
            {
                Visible = false;
                return;
            }
            Row++;
            targetPosition = LocalPosition + new Vector2(0, 100);
            velocity = new Vector2(0, 1) * Constant.MoveDownVelocity;
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);

            if (targetPosition.Y - localPosition.Y <= 0)
            {
                LocalPosition = targetPosition;
                velocity = Vector2.Zero;
            }

            if (LocalPosition.Y >= 956)
            {
                Visible = false;
            }
        }

        public Circle BouncingBox
        {
            get
            {
                return new Circle(Width / 2 + offsetWidths[SheetIndex], GlobalPosition);
            }
        }

        public bool Intersecting(Ball ball) 
        {
            return intersectingBalls.Contains(ball);
        }

        private List<Ball> intersectingBalls = new List<Ball>();

        public void StartIntersect(Ball ball)
        {
            if (!Intersecting(ball))
            {
                intersectingBalls.Add(ball);
            }
            intersected = true;
        }

        public void StopIntersect(Ball ball)
        {
            if (Intersecting(ball))
            {
                intersectingBalls.Remove(ball);
            }
        }

        public void PlayEffect()
        {
            PlayAnimation("anim", true);
        }
    }
}
