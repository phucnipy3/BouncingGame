using Engine;
using Microsoft.Xna.Framework;

namespace BouncingGame.GameObjects
{
    public class Brick : GameObjectList
    {
        int durability;
        SpriteGameObject container;
        TextGameObject displayText;

        public Brick(int durability, Vector2 position)
        {
            this.durability = durability;
            this.localPosition = position;
            container = new SpriteGameObject("Sprites/UI/spr_box", 1);
            displayText = new TextGameObject("Fonts/MainFont",1,Color.White, TextGameObject.HorizontalAlignment.Center, TextGameObject.VerticalAlignment.Center);
            displayText.LocalPosition = new Vector2(container.Width, container.Height) / 2;
            this.AddChild(container);
            this.AddChild(displayText);
        }

        public override void Update(GameTime gameTime)
        {
            this.displayText.Text = this.durability.ToString();
            base.Update(gameTime);
        }
    }
}
