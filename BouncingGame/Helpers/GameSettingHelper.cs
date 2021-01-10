using BouncingGame.Constants;
using BouncingGame.Models;
using Engine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncingGame.Helpers
{
    public class GameSettingHelper
    {
        public static BallModel DefaultBall = new BallModel
        {
            OriginSpritePath = "UI/spr_ball_normal_4mm"
        };

        public static List<BallModel> GetListBall()
        {
            List<BallModel> balls = JsonConvert.DeserializeObject<List<BallModel>>(Properties.GameSetting.Default.ListBall);
            return balls;
        }

        public static BallModel GetSelectedBall()
        {
            //GenerateListBall();
            var balls = GetListBall();
            var selectedBall = balls.FirstOrDefault(x => x.Id == Properties.GameSetting.Default.SelectedId);
            if (selectedBall == null)
                return DefaultBall;
            return selectedBall;
        }

        public static void ChangeSelectedBall(int selectedId)
        {
            Properties.GameSetting.Default["SelectedId"] = selectedId;
            Properties.GameSetting.Default.Save();
        }

        public static void AssignBalls(List<BallModel> balls)
        {
            Properties.GameSetting.Default["ListBall"] = JsonConvert.SerializeObject(balls);
            Properties.GameSetting.Default.Save();
        }

        public static void UnlockBall(int id)
        {
            var balls = GetListBall();
            var ball = balls.FirstOrDefault(x=> x.Id == id && x.Locked == false);
            if (ball == null)
                return;

            ball.Locked = false;

            AssignBalls(balls);
        }

        public static int GetHighScore()
        {
            return Properties.GameSetting.Default.HighScore;
        }

        public static void SetHighScore(int score)
        {
            Properties.GameSetting.Default["HighScore"] = score;
            Properties.GameSetting.Default.Save();
        }

        public static int GetMoney()
        {
            return Properties.GameSetting.Default.TotalCoin;
        }

        public static void SetMoney(int count)
        {
            Properties.GameSetting.Default["TotalCoin"] = count;
            Properties.GameSetting.Default.Save();
        }

        public static void AddOneCoin()
        {
            Properties.GameSetting.Default["TotalCoin"] = Properties.GameSetting.Default.TotalCoin + 1;
            Properties.GameSetting.Default.Save();
        }

        public static BallModel GetRandomBall()
        {
            var originalBalls = GetListBall();

            int rate = ExtendedGame.Random.Next() % 100;
            string rarity = Rarity.Normal;
            if (rate >= 98)
                rarity = Rarity.Legendary;
            else if (rate >= 90)
                rarity = Rarity.Unique;
            else if (rate >= 75)
                rarity = Rarity.Epic;
            else if (rate >= 50)
                rarity = Rarity.Rare;

            var balls = originalBalls.Where(x => x.Rarity == rarity).ToList();

            var randomBall = balls[ExtendedGame.Random.Next(balls.Count)];
            randomBall.Locked = false;

            AssignBalls(originalBalls);
            return randomBall;
        }

        public static bool GetVolumnState()
        {
            return Properties.GameSetting.Default.IsMuted;
        }

        public static void SetVolumnState(bool isMuted)
        {
            Properties.GameSetting.Default["IsMuted"] = isMuted;
            Properties.GameSetting.Default.Save();
        }

        // Call this method to re-generate list ball
        public static void GenerateListBall()
        {
            List<BallModel> balls = new List<BallModel>
            {
                new BallModel
                {
                    Id = 1,
                    Name = "SIMPLE BALL",
                    Rarity = Rarity.Normal,
                    Size = "4.00MM",
                    OriginSpritePath = "Balls/spr_ball_simple_normal_4mm",
                    ShadowSpritePath = "Balls/spr_ball_simple_normal_4mm_store",
                    LargeSpritePath = "Balls/spr_ball_simple_normal_4mm_store",
                    Locked = false,
                },
                new BallModel
                {
                    Id = 2,
                    Name = "COIN",
                    Rarity = Rarity.Legendary,
                    Size = "4.00MM",
                    OriginSpritePath = "Balls/spr_ball_coin_legendary_4mm",
                    ShadowSpritePath = "Balls/spr_ball_coin_legendary_4mm_shadow",
                    LargeSpritePath = "Balls/spr_ball_coin_legendary_4mm_store",
                    Locked = true,
                },
                new BallModel
                {
                    Id = 3,
                    Name = "15KM",
                    Rarity = Rarity.Unique,
                    Size = "3.71MM",
                    OriginSpritePath = "Balls/spr_ball_15km_legendary_3.71mm",
                    ShadowSpritePath = "Balls/spr_ball_15km_legendary_3.71mm_shadow",
                    LargeSpritePath = "Balls/spr_ball_15km_legendary_3.71mm_store",
                    Locked = true,
                    Speed = 1500
                },

                new BallModel
                {
                    Id = 4,
                    Name = "STAR",
                    Rarity = Rarity.Rare,
                    Size = "4.00MM",
                    OriginSpritePath = "Balls/spr_ball_star_unique_4mm",
                    ShadowSpritePath = "Balls/spr_ball_star_unique_4mm_shadow",
                    LargeSpritePath = "Balls/spr_ball_star_unique_4mm_store",
                    Locked = true,
                },
                new BallModel
                {
                    Id = 5,
                    Name = "SUN",
                    Rarity = Rarity.Epic,
                    Size = "11.00MM",
                    OriginSpritePath = "Balls/spr_ball_sun_unique_11mm",
                    ShadowSpritePath = "Balls/spr_ball_sun_unique_11mm_shadow",
                    LargeSpritePath = "Balls/spr_ball_sun_unique_11mm_store",
                    Locked = true,
                },

            };

            AssignBalls(balls);
        }
    }
}
