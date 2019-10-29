using System;

namespace TicTacToe
{
    public struct MoveResult
    {
        private readonly bool _isDraw;

        public bool GameEnded { get; }
        public int MoveCount { get; }
        public bool MarkedPlayer { get; set; }

        public bool Winner
        {
            get
            {
                if (!GameEnded)
                    throw new InvalidOperationException("The game hasn't ended.");

                if (_isDraw)
                    throw new InvalidOperationException("It's a draw.");

                return MarkedPlayer;
            }
        }

        public bool IsDraw
        {
            get
            {
                if (!GameEnded)
                    throw new InvalidOperationException("The game hasn't ended.");

                return _isDraw;
            }
        }

        public MoveResult(bool isDraw, bool gameEnded, int moveCount, bool markedPlayer)
        {
            _isDraw = isDraw;
            GameEnded = gameEnded;
            MoveCount = moveCount;
            MarkedPlayer = markedPlayer;
        }
    }
}
