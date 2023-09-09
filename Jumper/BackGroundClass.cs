using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Jumper.Helpers;
using Microsoft.Xna.Framework;

namespace Jumper
{
    public static class BackGroundClass
    {
        static Texture2D _sonicTexture = LoadHelper.Textures[TextureEnum.Sonic1_Win];
        static Texture2D _megamanTexture = LoadHelper.Textures[TextureEnum.Megaman_Win];
        static Texture2D _sonicVsMegaman = LoadHelper.Textures[TextureEnum.SonicVsMegaman];
        static Rectangle _rectangle = new Rectangle(0, 0, JumperGame.Width, JumperGame.Height);

        public static void SonicWinDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sonicTexture, _rectangle, Color.White);
            spriteBatch.DrawString(LoadHelper.Fonts[FontEnum.Arial22], "Sonic wins!!!", new Vector2(400, 400), Color.White);
        }

        public static void MegamanWinDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_megamanTexture, _rectangle, Color.White);
            spriteBatch.DrawString(LoadHelper.Fonts[FontEnum.Arial22], "Megaman wins!!!", new Vector2(400, 300), Color.White);
        }
        public static void DrawSonicVsMegama(SpriteBatch spriteBatch)
        {
            var rect = new Rectangle(0,100 , JumperGame.Width, JumperGame.Height - 200);
            spriteBatch.Draw(_sonicVsMegaman, rect, Color.White);
        }

    }
}
