using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Jumper.Enums;
using Jumper.Helpers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Jumper
{
    class MainMenu
    {
        private int selected = 0;
        private bool arrowPressed = true;

        public void Update()
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (!arrowPressed)
            {
                if (keyboard.IsKeyDown((Keys.Up)))
                {
                    if (selected == 0)
                        selected = 1;
                    else
                        selected--;
                }
                if (keyboard.IsKeyDown((Keys.Down)))
                {
                    if (selected == 1)
                        selected = 0;
                    else
                        selected++;
                }
            }
            if (keyboard.IsKeyDown((Keys.Enter)))
            {
                switch (selected)
                {
                    case 0: //game
                        JumperGame.GameState = GameStateEnum.Game;
                        break;
                    case 1: //exit
                        JumperGame.GameState = GameStateEnum.Exit;
                        break;
                }
            }

            if (keyboard.IsKeyUp((Keys.Up)) && keyboard.IsKeyUp((Keys.Down)))
                arrowPressed = false;
            else arrowPressed = true;


        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin(SpriteBlendMode.AlphaBlend);

            BackGroundClass.DrawSonicVsMegama(spritebatch);
            spritebatch.DrawString(LoadHelper.Fonts[FontEnum.Arial22], "Play Game", new Vector2(300, 300), selected == 0 ? Color.Green : Color.Gray);
            spritebatch.DrawString(LoadHelper.Fonts[FontEnum.Arial22], "Exit", new Vector2(300, 350), selected == 1 ? Color.Green : Color.Gray);

            spritebatch.End();
        }

    }
}
