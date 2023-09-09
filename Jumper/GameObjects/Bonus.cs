using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Jumper.Enums;

namespace Jumper.GameObjects
{
    public class Bonus : GameObject
    {
        BonusEnum _bonusEnum;

        public BonusEnum GetBonus { get { return _bonusEnum; } }

        public Bonus(BonusEnum bonusEnum,Texture2D texture, Vector2 position, int width, int height)
            : base(texture, position, width, height)
        {
            _bonusEnum = bonusEnum; 
        }
    }
}
