using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jumper.GameObjects;
using Jumper.Helpers;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Jumper.Enums;

namespace Jumper
{
    public class GameManager
    {
        float g = 0.2f;

        int _width;
        int _height;
        int _level;
        int _maxLevel;


        Texture2D _background;
        Hero _sonic;
        Hero _megaman;
        House _house;      //making in CreateLevel()
        List<Line> _lines; //making in CreateLevel()
        List<AnimatedSprite> _enemies; //making in CreateLevel() +
        List<FireBall> _fireBalls;
        List<Bonus> _bonuses;

        public GameManager(int widthForItem, int heightForItem)
        {
            _width = widthForItem;
            _height = heightForItem;
            _level = 1;
            _maxLevel = 2;

            CreateLevel();
        }

        private void CreateLevel()
        {
            _lines = new List<Line>();
            _bonuses = new List<Bonus>();
            _fireBalls = new List<FireBall>();
            _enemies = new List<AnimatedSprite>();
            _sonic = new Hero(LoadHelper.Textures[TextureEnum.Sonic1_Idle], LoadHelper.Textures[TextureEnum.Sonic1_Run], LoadHelper.Textures[TextureEnum.Sonic1_Jump], new Vector2(400, JumperGame.Height - _height - 20), _width, _height, 45, 41, 30, 32) { Speed = 2 };
            _megaman = new Hero(LoadHelper.Textures[TextureEnum.Megaman_Idle], LoadHelper.Textures[TextureEnum.Megaman_Run], LoadHelper.Textures[TextureEnum.Megaman_Jump], new Vector2(200, JumperGame.Height - _height - 20), _width, _height, 51, 41, 31, 27) { Speed = 2 };


            string[] s = File.ReadAllLines("Content/Levels/level"+ _level.ToString() + ".txt");
            int xStep = 0;
            int yStep = 0;

            foreach (string str in s)
            {
                foreach (char c in str)
                {
                    switch (c)
                    {
                        case 'D':
                            DragonEnemy dragon = new DragonEnemy(LoadHelper.Textures[TextureEnum.Dragon_Idle], LoadHelper.Textures[TextureEnum.Dragon_Fly], new Vector2(xStep, yStep), _width, _height, 45, 40) { Speed = 2 };
                            dragon.Move(BoolRandom.GetRandomBool());
                            _enemies.Add(dragon);
                            break;
                        case 'K':
                            KnightEnemy knight = new KnightEnemy(LoadHelper.Textures[TextureEnum.Knight_Idle], LoadHelper.Textures[TextureEnum.Knight_Run], new Vector2(xStep, yStep), _width + 10, _height + 10, 81, 57, 3);
                            knight.Move(BoolRandom.GetRandomBool());
                            _enemies.Add(knight);
                            break;
                        case 'S':
                            _bonuses.Add(new Bonus(BonusEnum.Speed, LoadHelper.Textures[TextureEnum.SpeedBox], new Vector2(xStep, yStep), _width, _height));
                            break;
                        case 'A':
                            _bonuses.Add(new Bonus(BonusEnum.Armor, LoadHelper.Textures[TextureEnum.ArmorBox], new Vector2(xStep, yStep), _width, _height));
                            break;
                        case 'G':
                            _bonuses.Add(new Bonus(BonusEnum.Gun, LoadHelper.Textures[TextureEnum.GunBox], new Vector2(xStep, yStep), _width, _height));
                            break;
                        case 'H':
                            _house = new House(LoadHelper.Textures[TextureEnum.House], new Vector2(xStep, yStep), _width * 2, _height * 2);
                            break;
                        case 'L':
                            _lines.Add(new Line(yStep, LoadHelper.Textures[TextureEnum.Block1], _width, _height / 2));
                            break;
                        case '1':
                            _background = LoadHelper.Textures[TextureEnum.BackGround1];
                            break;
                        case '2':
                            _background = LoadHelper.Textures[TextureEnum.BackGround2];
                            break;
                    }
                    xStep += _width;
                }
                xStep = 0;
                yStep += _height;
            }
        }

        private bool CollideWithHero(Rectangle rectangle)
        {
            if (rectangle.Intersects(_sonic.Rectangle))
                return true;
            if (rectangle.Intersects(_megaman.Rectangle))
                return true;
            return false;
        }

        //блоки двигают спрайт, который поподает на них
        private void SpriteMovingByBlocks(AnimatedSprite sprite)
        {
            foreach (Line line in _lines)
            {
                foreach (Block block in line.Blocks)
                {
                    if (block.Rectangle.Intersects(sprite.Rectangle))
                    {
                        var rect = sprite.Rectangle;
                        if (line.DirectionEnum == LinesDirectionEnum.Right)
                            rect.Offset(line.Speed, 0);
                        else
                            rect.Offset(-line.Speed, 0);
                        sprite.Rectangle = rect;
                    }
                }
            }
        }

        private bool CollidesWithBlocks(Rectangle rectangle)
        {
            foreach (Line line in _lines)
            {
                foreach (Block block in line.Blocks)
                {
                    if (rectangle.Intersects(block.Rectangle))
                        return true;
                }
            }
            return false;
        }

        private int NumberOfCrossingLineWithRectangle(Rectangle rectangle)
        {
            int number = -1;

            for (int i = 0; i < _lines.Count; i++)
            {
                foreach (Block block in _lines[i].Blocks)
                {
                    if (block.Rectangle.Intersects(rectangle))
                    {
                        number = i;
                        break;
                    }
                }
                if (number != -1)
                    break;
            }
            return number;
        }

        private void ApplyGravity(ref Hero hero, GameTime gameTime)
        {
            hero._yVelocity = hero._yVelocity - (g * gameTime.ElapsedGameTime.Milliseconds / 10);
            float dy = hero._yVelocity * gameTime.ElapsedGameTime.Milliseconds / 10;

            Rectangle nextPosition = hero.Rectangle;
            nextPosition.Offset(0, -(int)dy);

            //intersect            

            if (!CollidesWithBlocks(nextPosition) && nextPosition.Bottom < JumperGame.Height && nextPosition.Top > 0)
                hero.Rectangle = nextPosition;

            bool collideOnFallDown = (CollidesWithBlocks(nextPosition) && hero._yVelocity < 0);

            if (nextPosition.Bottom > JumperGame.Height || collideOnFallDown)
            {
                hero._isJumping = false;
                hero._yVelocity = 0;
            }

        }

        private void Shoot(AnimatedSprite sprite)
        {
            if (sprite.IsMovingRight)
            {
                _fireBalls.Add(new FireBall(LoadHelper.Textures[TextureEnum.FireBall], new Vector2(sprite.Rectangle.X + sprite.Rectangle.Width, sprite.Rectangle.Y + sprite.Rectangle.Height / 2), _width / 3, _height / 3, 4, true, sprite));
            }
            else
            {//здесь (sprite.Rectangle.X - width/3) чтобы rectangle героев не пересекался изночально с пулей!!!
                _fireBalls.Add(new FireBall(LoadHelper.Textures[TextureEnum.FireBall], new Vector2(sprite.Rectangle.X - _width / 3, sprite.Rectangle.Y + sprite.Rectangle.Height / 2), _width / 3, _height / 3, 4, false, sprite));
            }
        }

        public void NextLevel()
        {
            if (_sonic.Win || _megaman.Win)
            {
                if (_level < _maxLevel)
                    _level++;
                else
                    _level = 1;

                CreateLevel();
            }
        }

        public void RunHero(HeroEnum hero, bool isRunningRight)
        {
            if (hero == HeroEnum.Sonic)
            {
                if (!_sonic.IsShocked)
                {
                    _sonic.Move(isRunningRight);
                }
            }
            else if (hero == HeroEnum.Megaman)
            {
                if (!_megaman.IsShocked)
                {
                    _megaman.Move(isRunningRight);
                }
            }
        }

        public void StopHero(HeroEnum hero)
        {
            if (hero == HeroEnum.Sonic)
                _sonic.Stop();
            else if (hero == HeroEnum.Megaman)
                _megaman.Stop();
        }

        public void JumpHero(HeroEnum hero)
        {
            if (hero == HeroEnum.Sonic)
            {
                if (!_sonic.IsShocked)
                {
                    _sonic.Jump();
                }
            }
            else if (hero == HeroEnum.Megaman)
            {
                if (!_megaman.IsShocked)
                {
                    _megaman.Jump();
                }
            }
        }

        public void ShootHero(HeroEnum hero)
        {
            if (hero == HeroEnum.Sonic)
            {
                if (!_sonic.IsShocked)
                {
                    if (_sonic.CanShooting)
                    {
                        Shoot(_sonic);
                        _sonic.HasShooted();
                    }
                }
            }
            else if (hero == HeroEnum.Megaman)
            {
                if (!_megaman.IsShocked)
                {
                    if (_megaman.CanShooting)
                    {
                        Shoot(_megaman);
                        _megaman.HasShooted();
                    }
                }
            }
        }


        public void Update(GameTime gameTime)
        {
            UpdateLines(gameTime);
            UpdateBonuses(gameTime);
            UpdateFireBalls(gameTime);

            UpdateEnemies(gameTime);
            UpdateHeroes(gameTime);
        }


        private void UpdateBonuses(GameTime gameTime)
        {
            foreach (Bonus bonus in _bonuses)
            {
                if (CollideWithHero(bonus.Rectangle))
                {
                    if (_sonic.Rectangle.Intersects(bonus.Rectangle))
                    {
                        _sonic.SetBonus(bonus.GetBonus);
                    }
                    else
                    {
                        _megaman.SetBonus(bonus.GetBonus);
                    }
                    bonus.Alive = false;
                }
            }

            for (int i = 0; i < _bonuses.Count; i++)
            {
                if (!_bonuses[i].Alive)
                    _bonuses.RemoveAt(i);
            }
        }

        private void UpdateLines(GameTime gameTime)
        {
            foreach (Line line in _lines)
            {
                line.Update(gameTime);
            }
        }


        private void UpdateEnemies(GameTime gameTime)
        {
            UpdateDragons(gameTime);
            UpdateKnights(gameTime);

            for (int i = 0; i < _enemies.Count; i++)
            {
                if (!_enemies[i].Alive)
                    _enemies.RemoveAt(i);
            }
        }

        private void UpdateDragons(GameTime gameTime)
        {
            foreach (var item in _enemies)
            {
                if (item is DragonEnemy)
                {
                    var dragon = (DragonEnemy)item;

                    if (dragon.IsMoving)
                    {
                        Rectangle nextPosition = dragon.Rectangle;

                        if (dragon.IsMovingRight)
                        {
                            nextPosition.Offset(dragon.Speed, 0);
                        }
                        else
                        {
                            nextPosition.Offset(-dragon.Speed, 0);
                        }

                        if (nextPosition.Left > 0 && nextPosition.Right < JumperGame.Width)
                        {
                            dragon.Rectangle = nextPosition;
                        }
                        else
                        {
                            bool direction = dragon.IsMovingRight;
                            dragon.Move(!direction);
                        }
                    }

                    if (dragon.CanShooting)
                    {
                        Shoot(dragon);
                        dragon.HasShooted();
                    }

                    if (CollideWithHero(dragon.Rectangle))
                    {
                        if (dragon.Rectangle.Intersects(_sonic.Rectangle))
                        {
                            _sonic.Shocked();
                        }
                        else
                        {
                            _megaman.Shocked();
                        }
                    }

                    dragon.Update(gameTime);
                }
            }
        }

        private void UpdateKnights(GameTime gameTime)
        {
            foreach (var item in _enemies)
            {
                if (item is KnightEnemy)
                {
                    var knight = (KnightEnemy)item;

                    if (knight.IsMoving)
                    {
                        Rectangle nextPosition = knight.Rectangle;

                        if (knight.IsMovingRight)
                            nextPosition.Offset(knight.Speed, 0);
                        else
                            nextPosition.Offset(-knight.Speed, 0);


                        if (!CollidesWithBlocks(nextPosition) && nextPosition.Bottom < JumperGame.Height)
                        {
                            nextPosition.Offset(0, 8);
                        }

                        if (nextPosition.Left >= 0 && nextPosition.Right <= JumperGame.Width)
                            knight.Rectangle = nextPosition;
                        else
                        {
                            bool direction = knight.IsMovingRight;
                            knight.Move(!direction);
                        }

                        if (knight.Rectangle.Bottom + 5 >= JumperGame.Height)
                        {
                            if (knight.Rectangle.Left < 3 || knight.Rectangle.Right > JumperGame.Width - 3)
                            {
                                //knight.Alive = false;
                                Rectangle rect = new Rectangle(_width, _height, _width, _height);
                                knight.Rectangle = rect;
                            }
                        }
                    }

                    if (CollideWithHero(knight.Rectangle))
                    {

                        if (knight.Rectangle.Intersects(_sonic.Rectangle))
                        {
                            if (_sonic.Bonus != BonusEnum.Armor)
                            {
                                _sonic.Rectangle = new Rectangle(JumperGame.Width / 2, JumperGame.Height - _sonic.Rectangle.Width, _sonic.Rectangle.Width, _sonic.Rectangle.Height);
                            }
                        }
                        else
                        {
                            if (_megaman.Bonus != BonusEnum.Armor)
                            {
                                _megaman.Rectangle = new Rectangle(JumperGame.Width / 2, JumperGame.Height - _megaman.Rectangle.Width, _megaman.Rectangle.Width, _megaman.Rectangle.Height);
                            }
                        }

                    }

                    knight.Update(gameTime);
                }
            }
        }

        private void UpdateHeroes(GameTime gameTime)
        {
            UpdateHero(ref _sonic, gameTime);
            UpdateHero(ref _megaman, gameTime);
        }

        private void UpdateHero(ref Hero hero, GameTime gameTime)
        {
            if (hero.Rectangle.Intersects(_house.Rectangle))
            {
                hero.Win = true;
            }

            if (CollidesWithBlocks(hero.Rectangle))
            {
                SpriteMovingByBlocks(hero);
                //Rectangle rect = hero.Rectangle; //создаю rect,потому что _sonic.Rectangle.Offset не работает

                //var number = NumberOfCrossingLineWithRectangle(hero.Rectangle);
                //if (number != -1)
                //{
                //    var speed = _lines[number].Speed;
                //    LinesDirectionEnum lineDirection = _lines[number].DirectionEnum;

                //    switch (lineDirection)
                //    {
                //        case LinesDirectionEnum.Left:
                //            rect.Offset(-speed, 0);   //создаю rect,
                //            hero.Rectangle = rect;  //  потому что _sonic.Rectangle.Offset не работает
                //            break;
                //        case LinesDirectionEnum.Right:
                //            rect.Offset(speed, 0);
                //            hero.Rectangle = rect;
                //            break;
                //    }
                //}
            }

            if (hero.IsMoving)
            {
                Rectangle nextPosition = hero.Rectangle;

                if (hero.IsMovingRight)
                {
                    nextPosition.Offset(hero.Speed, 0);

                    //intersects

                    if (!CollidesWithBlocks(nextPosition) && nextPosition.Left < JumperGame.Width && nextPosition.Right > 0)
                        hero.Rectangle = nextPosition;
                    else if (nextPosition.Left >= JumperGame.Width)
                        hero.Rectangle = new Rectangle(0 - _width + 1, nextPosition.Y, _width, _height);
                }
                else //isRunning Left
                {
                    nextPosition.Offset(-hero.Speed, 0);

                    //intersects

                    if (!CollidesWithBlocks(nextPosition) && nextPosition.Left < JumperGame.Width && nextPosition.Right > 0)
                        hero.Rectangle = nextPosition;
                    else if (nextPosition.Right <= 0)
                        hero.Rectangle = new Rectangle(JumperGame.Width - 1, nextPosition.Y, _width, _height);
                }
            }

            ApplyGravity(ref hero, gameTime);
            hero.Update(gameTime);
        }

        private void UpdateFireBalls(GameTime gameTime)
        {
            foreach (FireBall fireBall in _fireBalls)
            {
                Rectangle rect = fireBall.Rectangle;

                if (fireBall.isDirectionRight)
                    rect.Offset(fireBall.Speed, 0);
                else
                    rect.Offset(-fireBall.Speed, 0);

                fireBall.Rectangle = rect;

                if (!fireBall.OnScreen)
                {
                    fireBall.Alive = false;
                    continue;
                }

                if (CollideWithHero(fireBall.Rectangle))
                {
                    fireBall.Alive = false;
                    if (fireBall.Rectangle.Intersects(_sonic.Rectangle))
                    {
                        _sonic.Shocked();
                    }
                    else
                    {
                        _megaman.Shocked();
                    }
                    continue;
                }

                if (CollidesWithBlocks(fireBall.Rectangle))
                {
                    fireBall.Alive = false;
                    continue;
                }

                foreach (var enemy in _enemies)
                {
                    if (enemy.Rectangle.Intersects(fireBall.Rectangle) && (fireBall.Owner is Hero))
                    {
                        enemy.Alive = false;
                        fireBall.Alive = false;
                    }
                }
            }

            for (int i = 0; i < _fireBalls.Count; i++)
            {
                if (!_fireBalls[i].Alive)
                    _fireBalls.RemoveAt(i);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_sonic.Win || _megaman.Win)
            {
                if (_sonic.Win)
                {
                    BackGroundClass.SonicWinDraw(spriteBatch);
                }
                else if (_megaman.Win)
                {
                    BackGroundClass.MegamanWinDraw(spriteBatch);
                }
            }
            else
            {
                //backround
                spriteBatch.Draw(_background, new Rectangle(0, 0, JumperGame.Width, JumperGame.Height), Color.White);

                foreach (AnimatedSprite enemy in _enemies)
                {
                    enemy.Draw(spriteBatch);
                }

                foreach (Line line in _lines)
                {
                    line.Draw(spriteBatch);
                }

                foreach (Bonus bonus in _bonuses)
                {
                    bonus.Draw(spriteBatch);
                }

                foreach (FireBall fireBall in _fireBalls)
                {
                    fireBall.Draw(spriteBatch);
                }

                _house.Draw(spriteBatch);
                _sonic.Draw(spriteBatch);
                _megaman.Draw(spriteBatch);
            }
        }


    }
}