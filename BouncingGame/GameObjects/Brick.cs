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
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
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

    }
}
