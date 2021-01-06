﻿using BouncingGame.Models;
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
            OriginSpritePath = "Sprites/UI/spr_ball_normal_4mm"
        };

        public static List<BallModel> GetListBall()
        {
            List<BallModel> balls = JsonConvert.DeserializeObject<List<BallModel>>(Properties.GameSetting.Default.ListBall);
            return balls;
        }

        public static BallModel GetSelectedBall()
        {
            var balls = GetListBall();
            var selectedBall = balls.FirstOrDefault(x => x.Id == Properties.GameSetting.Default.SelectedId);
            if (selectedBall == null)
                return DefaultBall;
            return selectedBall;
        }

        public static void ChangeSelectedBall(int selectedId)
        {
            Properties.GameSetting.Default["SettingsKey"] = selectedId;
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

        // Call this method once in pre-productto generate list ball
        public static void GenerateListBall()
        {
            List<BallModel> balls = new List<BallModel>
            {

            };

            AssignBalls(balls);
        }
    }
}
