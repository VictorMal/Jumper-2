using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Jumper.Helpers
{
    public static class LoadHelper
    {
        public static Dictionary<TextureEnum,Texture2D> Textures;
        public static Dictionary<FontEnum, SpriteFont> Fonts;

        public static void Load(ContentManager content)
        {
            Textures = new Dictionary<TextureEnum, Texture2D>();
            Fonts = new Dictionary<FontEnum, SpriteFont>();

            Textures.Add(TextureEnum.Block1, content.Load<Texture2D>("Textures/block1"));
            Textures.Add(TextureEnum.BackGround1, content.Load<Texture2D>("Textures/background"));
            Textures.Add(TextureEnum.BackGround2, content.Load<Texture2D>("Textures/background2"));
            Textures.Add(TextureEnum.Sonic1_Idle, content.Load<Texture2D>("Textures/sonic1_idle"));
            Textures.Add(TextureEnum.Sonic1_Run, content.Load<Texture2D>("Textures/sonic1_run"));
            Textures.Add(TextureEnum.Sonic1_Jump, content.Load<Texture2D>("Textures/sonic1_jump"));
            Textures.Add(TextureEnum.Sonic1_Win,content.Load<Texture2D>("Textures/sonic_win"));
            Textures.Add(TextureEnum.Megaman_Idle, content.Load<Texture2D>("Textures/megaman_idle"));
            Textures.Add(TextureEnum.Megaman_Run, content.Load<Texture2D>("Textures/megaman_run"));
            Textures.Add(TextureEnum.Megaman_Jump, content.Load<Texture2D>("Textures/megaman_jump"));
            Textures.Add(TextureEnum.Megaman_Win, content.Load<Texture2D>("Textures/megaman_win"));
            Textures.Add(TextureEnum.SonicVsMegaman, content.Load<Texture2D>("Textures/sonicVsMegaman"));
            Textures.Add(TextureEnum.Dragon_Fly, content.Load<Texture2D>("Textures/dragon_fly"));
            Textures.Add(TextureEnum.Dragon_Idle, content.Load<Texture2D>("Textures/dragon_idle"));
            Textures.Add(TextureEnum.Knight_Idle, content.Load<Texture2D>("Textures/knight_idle"));
            Textures.Add(TextureEnum.Knight_Run, content.Load<Texture2D>("Textures/knight_run"));
            Textures.Add(TextureEnum.House, content.Load<Texture2D>("Textures/house"));
            Textures.Add(TextureEnum.FireBall, content.Load<Texture2D>("Textures/fireball_2"));
            Textures.Add(TextureEnum.SpeedBox, content.Load<Texture2D>("Textures/speedBox"));
            Textures.Add(TextureEnum.ArmorBox, content.Load<Texture2D>("Textures/armorBox"));
            Textures.Add(TextureEnum.GunBox, content.Load<Texture2D>("Textures/gunBox"));

            Fonts.Add(FontEnum.Arial22,content.Load<SpriteFont>("Fonts/Arial22"));
        }       
    }


    public enum TextureEnum
    {
        BackGround1,
        BackGround2,
        Block1,
        House,
        FireBall,
        SpeedBox,
        ArmorBox,
        GunBox,
        Sonic1_Idle,
        Sonic1_Run,
        Sonic1_Jump,
        Sonic1_Win,
        Megaman_Idle,
        Megaman_Run,
        Megaman_Jump,
        Megaman_Win,
        SonicVsMegaman,
        Dragon_Idle,
        Dragon_Fly,
        Knight_Idle,
        Knight_Run
    }

    public enum FontEnum
    { 
        Arial22
    }
}
