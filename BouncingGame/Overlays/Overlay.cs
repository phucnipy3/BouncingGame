using Engine;

namespace BouncingGame.Overlays
{
    public class Overlay : GameObjectList
    {
        public Overlay()
        {
            Hide();
        }
        public virtual void Show()
        {
            Visible = true;
            foreach (var child in children)
                child.Visible = true;
        }

        public void Hide()
        {
            Visible = false;
            foreach (var child in children)
                child.Visible = false;
        }

        public override void Reset()
        {
            base.Reset();
            Hide();
        }
    }
}
