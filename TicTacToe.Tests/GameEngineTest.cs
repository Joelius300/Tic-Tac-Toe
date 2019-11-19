using System;
using System.Collections.Generic;
using Xunit;

namespace TicTacToe.Tests
{
    public class GameEngineTest
    {
        private const bool starter = true; // can be anything
        private const int width = 3; // can be anything
        private static readonly (int x, int y) ZeroPoint = (0, 0);

        [Fact]
        public void StarterMarkGetsSet()
        {
            // Arrange
            (int x, int y) = ZeroPoint;
            Game game = new Game(width, starter);

            // Act
            game.SetMark(x, y);

            // Assert
            Assert.Equal(starter, game.GetCell(x, y));
        }

        [Fact]
        public void MarkCellTwice_ThrowsInvalidOperationException()
        {
            // Arrange
            (int x, int y) = ZeroPoint;
            Game game = new Game(width, starter);

            // Act
            game.SetMark(x, y);

            // Assert
            Assert.Throws<InvalidOperationException>(() => game.SetMark(x, y));
        }

        [Fact]
        public void AfterWin_ThrowsInvalidOperationException()
        {
            // Arrange
            Game game = new Game(width, starter);

            // Act
            foreach ((int x, int y) in GetStarterWinningSequence(width))
            {
                game.SetMark(x, y);
            }

            // Assert
            Assert.Throws<InvalidOperationException>(() => game.SetMark(ZeroPoint.x, ZeroPoint.y));
        }

        [Fact]
        public void AfterDraw_ThrowsInvalidOperationException()
        {
            // Arrange
            const int width3 = 3;
            Game game = new Game(width3, starter);

            // Act
            foreach ((int x, int y) in GetDrawingSequenceWidth3())
            {
                game.SetMark(x, y);
            }

            // Assert
            Assert.Throws<InvalidOperationException>(() => game.SetMark(ZeroPoint.x, ZeroPoint.y));
        }

        private static IEnumerable<(int x, int y)> GetStarterWinningSequence(int width)
        {
            if (width < 2)
                throw new ArgumentOutOfRangeException(nameof(width), "Width has to be greater than 1");

            for (int i = 0; i < width; i++)
            {
                yield return (i, 0);

                if (i < width - 1)
                    yield return (i, 1);
            }
        }

        private static IEnumerable<(int x, int y)> GetDrawingSequenceWidth3()
        {
            yield return (0, 0);
            yield return (2, 0);
            yield return (0, 2);
            yield return (2, 2);
            yield return (2, 1);
            yield return (0, 1);
            yield return (1, 1);
            yield return (1, 2);
            yield return (1, 0);
        }
    }
}
