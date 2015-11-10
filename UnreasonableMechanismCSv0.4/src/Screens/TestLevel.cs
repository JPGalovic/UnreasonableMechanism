﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnreasonableMechanismEngineCS;
using SwinGameSDK;

using Colour = SwinGameSDK.Color;
using Vector = UnreasonableMechanismEngineCS.Vector;

namespace UnreasonableMechanismCS
{
    public class TestLevel : Screen
    {
        private Random _rand;

        private ItemType[] _itemTypes;

        private int[] _triggers;

        public TestLevel()
        {
            _rand = new Random();

            _itemTypes = new ItemType[]
            {
                ItemType.BigPower,
                ItemType.Bomb,
                ItemType.FullPower,
                ItemType.Life,
                ItemType.Point,
                ItemType.Power,
                ItemType.Star
            };

            _triggers = new int[]
            {
                _rand.Next()%340 + 600,
                _rand.Next()%340 + 40,
                _rand.Next()%340 + 6000,
                _rand.Next()%340 + 60,
                _rand.Next()%140 + 40,
                _rand.Next()%140 + 20,
                _rand.Next()%140 + 40
            };
        }

        public override void Draw()
        {
            SwinGame.ClearScreen(Color.DarkSlateGray);

            GameObjects.DrawItems();
            GameObjects.DrawPlayer();

            SwinGame.DrawBitmap(GameResources.GameImage("GameArea"), 0, 0);
        }

        public override void Initalise()
        {
            Tick = 1;
        }

        public override void ProvessEvents()
        {
            if(SwinGame.KeyTyped(Settings.PAUSE))
            {
                ScreenControler.SetScreen("PauseMenu");
            }

            for (int i = 0; i < 7; ++i)
            {
                if (Tick % (_triggers[i]) == 0)
                {
                    GameObjects.AddItem(new ItemEntity(new Point(_rand.Next() % (460 - GameResources.GameImage("Item" + _itemTypes[i].ToString()).Width) + 40, 50), _itemTypes[i]));
                }
            }

            GameObjects.ProcessItemEvents();
            GameObjects.Player.ProcessEvents();

            Tick++;
        }
    }
}