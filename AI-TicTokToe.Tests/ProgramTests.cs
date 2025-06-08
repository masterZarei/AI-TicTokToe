using AI_TicTokToe.App;
using FluentAssertions;

namespace AI_TicTokToe.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void IsWinner_ShouldReturnTrue_WhenRowIsFilled()
        {
            var board = new string[3, 3]
            {
                { "x", "x", "x" },
                { " ", "o", " " },
                { "o", " ", " " }
            };
            var result = InvokeIsWinner(board, "x");
            result.Should().BeTrue();
        }

        [Fact]
        public void IsWinner_ShouldReturnTrue_WhenColumnIsFilled()
        {
            var board = new string[3, 3]
            {
                { "o", "x", " " },
                { "o", "x", " " },
                { " ", "x", " " }
            };
            var result = InvokeIsWinner(board, "x");
            result.Should().BeTrue();
        }

        [Fact]
        public void IsWinner_ShouldReturnTrue_WhenMainDiagonalIsFilled()
        {
            var board = new string[3, 3]
            {
                { "o", "x", "x" },
                { " ", "o", " " },
                { "x", " ", "o" }
            };
            var result = InvokeIsWinner(board, "o");
            result.Should().BeTrue();
        }

        [Fact]
        public void IsWinner_ShouldReturnTrue_WhenAntiDiagonalIsFilled()
        {
            var board = new string[3, 3]
            {
                { "x", "o", "o" },
                { " ", "o", " " },
                { "o", " ", "x" }
            };
            var result = InvokeIsWinner(board, "o");
            result.Should().BeTrue();
        }

        [Fact]
        public void IsWinner_ShouldReturnFalse_WhenNoWinner()
        {
            var board = new string[3, 3]
            {
                { "x", "o", "x" },
                { "o", "x", "o" },
                { "o", "x", "o" }
            };
            var result = InvokeIsWinner(board, "x");
            result.Should().BeFalse();
        }

        [Fact]
        public void IsDraw_ShouldReturnTrue_WhenBoardIsFullAndNoWinner()
        {
            var board = new string[3, 3]
            {
                { "x", "o", "x" },
                { "o", "x", "o" },
                { "o", "x", "o" }
            };
            var result = InvokeIsDraw(board);
            result.Should().BeTrue();
        }

        [Fact]
        public void IsDraw_ShouldReturnFalse_WhenBoardHasEmptyCells()
        {
            var board = new string[3, 3]
            {
                { "x", "o", " " },
                { "o", "x", "o" },
                { "o", "x", "o" }
            };
            var result = InvokeIsDraw(board);
            result.Should().BeFalse();
        }

        [Fact]
        public void ConvertNumberToPosition_ShouldReturnCorrectPosition()
        {
            var (row, col) = InvokeConvertNumberToPosition(5, 3);
            row.Should().Be(1);
            col.Should().Be(1);
        }

        [Fact]
        public void GetBestMove_ShouldReturnWinningMove()
        {
            var board = new string[3, 3]
            {
                { "x", "x", " " },
                { "o", "o", " " },
                { " ", " ", " " }
            };
            var move = InvokeGetBestMove(board, "x", "o", 6);
            move.Should().NotBeNull();
            move.Value.Should().Be((0, 2));
        }

        [Fact]
        public void AlphaBeta_ShouldReturnDrawScore_WhenDraw()
        {
            var board = new string[3, 3]
            {
                { "x", "o", "x" },
                { "o", "x", "o" },
                { "o", "x", "o" }
            };
            var (score, move) = InvokeAlphaBeta(board, 0, int.MinValue, int.MaxValue, true, "x", "o", 6);
            score.Should().Be(0);
            move.Should().BeNull();
        }

        // Helper methods to invoke private static methods using reflection
        private static bool InvokeIsWinner(string[,] board, string symbol)
        {
            var method = typeof(Program).GetMethod("IsWinner", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            return (bool)method.Invoke(null, [board, symbol]);
        }

        private static bool InvokeIsDraw(string[,] board)
        {
            var method = typeof(Program).GetMethod("IsDraw", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            return (bool)method.Invoke(null, [board]);
        }

        private static (int, int) InvokeConvertNumberToPosition(int number, int size)
        {
            var method = typeof(Program).GetMethod("ConvertNumberToPosition", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            return ((int, int))method.Invoke(null, [number, size]);
        }

        private static (int, int)? InvokeGetBestMove(string[,] board, string player, string opponent, int depthLimit)
        {
            var method = typeof(Program).GetMethod("GetBestMove", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            return ((ValueTuple<int, int>?)method.Invoke(null, [board, player, opponent, depthLimit]));
        }

        private static (int score, (int, int)? move) InvokeAlphaBeta(string[,] board, int depth, int alpha, int beta, bool isMax, string player, string opponent, int depthLimit)
        {
            var method = typeof(Program).GetMethod("AlphaBeta", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            return ((int, (int, int)?))method.Invoke(null, [board, depth, alpha, beta, isMax, player, opponent, depthLimit]);
        }
    }
}
