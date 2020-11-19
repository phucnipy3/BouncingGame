using BouncingGame.GameStates;
using Engine;
using Microsoft.Xna.Framework;
using System;

namespace BouncingGame
{
    public class Bouncing : ExtendedGame
    {
        public static string StateName_Home = "home";
        public static string StateName_Play = "play";


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


            // add the game states
            GameStateManager.AddGameState(StateName_Home, new HomeState());
            GameStateManager.AddGameState(StateName_Play, new PlayState());


            // start at the home screen
            GameStateManager.SwitchTo(StateName_Home);

            //// play background music
            //AssetManager.PlaySong("Sounds/snd_music", true);
        }
    }
}
