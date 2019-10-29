using System;

namespace TicTacToe
{
    public class Game
    {
        private readonly bool?[,]   _field;
        private readonly int        _width;

        private bool                _currentTurn;
        private bool                _gameHasEnded;
        private int                 _moveCount;

        public Game(int width, bool starts)
        {
            if (width < 1 || width > 100)
                throw new ArgumentOutOfRangeException(nameof(width), "The width has to be between 1 and 100.");

            _width = width;
            _field = new bool?[width, width];
            _currentTurn = starts;
        }

        public MoveResult SetMark(int x, int y)
        {
            if (_gameHasEnded)
                throw new InvalidOperationException("The game has already ended.");

            // validate x and y
            CheckInput(x, y);

            // actually set cell
            _field[x, y] = _currentTurn;

            // increase move-counter
            _moveCount++;

            // check if the player has won
            bool gameWon = CheckGameWon(x, y, _currentTurn);
            bool draw = false;
            
            if (!gameWon)
            {
                draw = CheckGameDraw();
            }

            bool gameEnded = gameWon || draw;

            MoveResult result = new MoveResult(draw, gameEnded, _moveCount, _currentTurn);

            // prepare for next turn
            _currentTurn = !_currentTurn;
            _gameHasEnded = gameEnded;

            // return the value which was set
            return result;
        }

        private void CheckInput(int x, int y)
        {
            if (x < 0 || x > _width - 1)
                throw new ArgumentOutOfRangeException(nameof(x), $"The x coordinate has to be on the field. Valid values go from 0 to {_width - 1}.");

            if (y < 0 || y > _width - 1)
                throw new ArgumentOutOfRangeException(nameof(y), $"The y coordinate has to be on the field. Valid values go from 0 to {_width - 1}.");

            if (_field[x, y].HasValue)
                throw new InvalidOperationException($"The cell at {x}, {y} already has a value.");
        }

        private bool CheckGameWon(int x, int y, bool played)
        {
            // check col
            for (int i = 0; i < _width; i++)
            {
                if (_field[x, i] != played) break;

                if (i == _width - 1)
                {
                    return true;
                }
            }

            //check row
            for (int i = 0; i < _width; i++)
            {
                if (_field[i, y] != played) break;

                if (i == _width - 1)
                {
                    return true;
                }
            }

            //check diag
            if (x == y)
            {
                for (int i = 0; i < _width; i++)
                {
                    if (_field[i, i] != played) break;

                    if (i == _width - 1)
                    {
                        return true;
                    }
                }
            }

            //check anti diag
            if (x + y == _width - 1)
            {
                for (int i = 0; i < _width; i++)
                {
                    if (_field[i, _width - 1 - i] != played) break;

                    if (i == _width - 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckGameDraw() => _moveCount == (_width * _width);
    }
}
