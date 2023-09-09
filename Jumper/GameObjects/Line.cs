using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Jumper.Helpers;
using Jumper.Enums;

namespace Jumper.GameObjects
{
    public class Line
    {
        Texture2D _blockTexture;
        List<Block> _blocks;
        int _yPosition;
        int _blockWidth;
        int _blockHeight;
        int _counter = 0;
        double _timeForRandom = 1000;
        bool _hasEmpty;
        bool _isDirectionRight = false;
        LinesDirectionEnum _lineMoveEnum = LinesDirectionEnum.None;
        ISpeedStrategy _speed;

        public List<Block> Blocks { get { return _blocks; } }
        public LinesDirectionEnum DirectionEnum { get { return _lineMoveEnum; } }
        public int Speed { get { return _speed.Speed; } }

        public Line(int yPosition, Texture2D blockTexture, int blockWidth, int blockHeight)
        {
            _blocks = new List<Block>();
            _yPosition = yPosition;
            _blockTexture = blockTexture;
            _blockWidth = blockWidth;
            _blockHeight = blockHeight;
            
            int step = 0;            
            for (int i = 0; i < 20; i++)
            {
                _blocks.Add(new Block(_blockTexture, new Vector2(step,_yPosition),_blockWidth,_blockHeight));
                step += _blockWidth;
            }    
        }

        private void Moving()
        {
            if (_isDirectionRight)
            {
                if (_counter < 3)
                {
                    for (int i = _blocks.Count - 1; i >= 0; i--)
                    {
                        _blocks[i].MoveBlock(_speed.Speed);

                        if (!_blocks[i].OnScreen)
                        {
                            _blocks[i].Alive = false;
                            _counter++;
                        }
                    }
                }
                else if (_counter < 20)
                {
                    if (_blocks.Count == 17)
                    {
                        _blocks.Add(new Block(_blockTexture,new Vector2(-_blockWidth,_yPosition),_blockWidth,_blockHeight));
                    }

                    for (int i = _blocks.Count - 1; i >= 0; i--)
                    {
                        _blocks[i].MoveBlock(_speed.Speed);

                        if (!_blocks[i].OnScreen)
                        {                            
                            _blocks[i].Alive = false;
                            _counter++;
                        }
                    }
                }
                else if (_counter < 23)
                {                    
                    if ( _counter == 20 && _blocks.Count == 17 )
                        _blocks.Add(new Block(_blockTexture, new Vector2(-_blockWidth, _yPosition), _blockWidth, _blockHeight));
                    else if (_counter == 21 && _blocks.Count == 18)
                        _blocks.Add(new Block(_blockTexture, new Vector2(-_blockWidth, _yPosition), _blockWidth, _blockHeight));
                    else if ( _counter == 22 && _blocks.Count == 19)
                        _blocks.Add(new Block(_blockTexture, new Vector2(-_blockWidth, _yPosition), _blockWidth, _blockHeight));
                    
                    for (int i = _blocks.Count - 1; i >= 0; i--)
                    {
                        _blocks[i].MoveBlock(_speed.Speed);

                        if (_blocks[i].Rectangle.Left == 0)
                        {
                            _counter++;
                        }
                    }
                }                
            }
            else //!_isDirectionRight
            {
                if (_counter < 3)
                {
                    for (int i = 0; i < _blocks.Count; i++)
                    {
                        _blocks[i].MoveBlock(-_speed.Speed);

                        if (!_blocks[i].OnScreen)
                        {
                            _blocks[i].Alive = false;
                            _counter++;
                        }
                    }
                }
                else if (_counter < 20)
                {
                    if (_blocks.Count == 17)
                    {
                        _blocks.Add(new Block(_blockTexture, new Vector2(JumperGame.Width, _yPosition), _blockWidth, _blockHeight));
                    }

                    for (int i = 0; i < _blocks.Count; i++)
                    {
                        _blocks[i].MoveBlock(-_speed.Speed);

                        if (!_blocks[i].OnScreen)
                        {
                            _blocks[i].Alive = false;
                            _counter++;
                        }
                    }
                }
                else if (_counter < 23)
                {
                    if (_counter == 20 && _blocks.Count == 17)
                        _blocks.Add(new Block(_blockTexture, new Vector2(JumperGame.Width, _yPosition), _blockWidth, _blockHeight));
                    else if (_counter == 21 && _blocks.Count == 18)
                        _blocks.Add(new Block(_blockTexture, new Vector2(JumperGame.Width, _yPosition), _blockWidth, _blockHeight));
                    else if (_counter == 22 && _blocks.Count == 19)
                        _blocks.Add(new Block(_blockTexture, new Vector2(JumperGame.Width, _yPosition), _blockWidth, _blockHeight));

                    for (int i = 0; i < _blocks.Count; i++)
                    {
                        _blocks[i].MoveBlock(-_speed.Speed);

                        if (_blocks[i].Rectangle.Right == JumperGame.Width)
                        {
                            _counter++;
                        }
                    }
                }                            
            }

        }

        public void Update(GameTime gameTime)
        {
            if (!_hasEmpty)
            {
                if (_timeForRandom > 0)
                    _timeForRandom -= gameTime.ElapsedGameTime.Milliseconds;
                else
                {
                    _timeForRandom = 1000;
                    _hasEmpty = BoolRandom.GetRandomBool();
                    _isDirectionRight = BoolRandom.GetRandomBool();
                    _speed = RandomSpeed.GetSpeed();

                    if (!_hasEmpty)
                        _lineMoveEnum = LinesDirectionEnum.None;
                }
            }
            else //if _hasEmpty
            {
                if (_isDirectionRight)
                    _lineMoveEnum = LinesDirectionEnum.Right;
                else
                    _lineMoveEnum = LinesDirectionEnum.Left;

                Moving();
                if (_counter == 23)
                {
                    _hasEmpty = false;
                    _timeForRandom = 1000;
                    _counter = 0;
                }
            }  

            for (int i = 0; i < _blocks.Count; i++)
            {
                if (_blocks[i].Alive == false)
                    _blocks.RemoveAt(i);                
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Block block in _blocks)
            {
                block.Draw(spriteBatch);
            }
        }
    }
}
