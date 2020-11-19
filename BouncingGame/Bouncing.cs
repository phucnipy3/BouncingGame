using Engine;
using Microsoft.Xna.Framework;
using System;

namespace BouncingGame
{
    public class Bouncing : ExtendedGame
    {
        [STAThread]
        static void Main()
        {
            Bouncing game = new Bouncing();
            game.Run();
        }

        public Bouncing()
        {
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            // set a custom world and window size
            worldSize = new Point(700, 1200);
            windowSize = new Point(525, 900);

            // to let these settings take effect, we need to set the FullScreen property again
            FullScreen = false;


            //// add the game states
            //GameStateManager.AddGameState(StateName_Title, new TitleMenuState());
            //GameStateManager.AddGameState(StateName_LevelSelect, new LevelMenuState());
            //GameStateManager.AddGameState(StateName_Help, new HelpState());
            //GameStateManager.AddGameState(StateName_Playing, new PlayingState());

            //// start at the title screen
            //GameStateManager.SwitchTo(StateName_Title);

            //// play background music
            //AssetManager.PlaySong("Sounds/snd_music", true);
        }
    }
}
