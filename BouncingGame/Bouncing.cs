using BouncingGame.Constants;
using BouncingGame.GameStates;
using BouncingGame.Helpers;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
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
            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1 / 60d);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            // set a custom world and window size
            worldSize = new Point(700, 1200);
            windowSize = new Point(350, 600);

            // to let these settings take effect, we need to set the FullScreen property again
            FullScreen = false;


            // add the game states
            GameStateManager.AddGameState(StateName.Home, new HomeState());
            GameStateManager.AddGameState(StateName.Play, new PlayState());
            GameStateManager.AddGameState(StateName.ChangeBall, new ChangeBallState());


            // start at the home screen
            GameStateManager.SwitchTo(StateName.Home);

            // hanlde volumn state

            bool isMuted = GameSettingHelper.GetVolumnState();
            MediaPlayer.IsMuted = isMuted;
            SoundEffect.MasterVolume = isMuted ? 0f : 1f;

            // play background music
            AssetManager.PlaySong("Sounds/snd_music", true);

        }
    }
}
