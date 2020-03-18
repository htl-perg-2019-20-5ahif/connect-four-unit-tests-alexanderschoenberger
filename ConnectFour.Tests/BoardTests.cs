using ConnectFour.Logic;
using System;
using Xunit;

namespace ConnectFour.Tests
{
    public class BoardTests
    {
        [Fact]
        public void AddWithInvalidColumnIndex()
        {
            var b = new Board();

            Assert.Throws<ArgumentOutOfRangeException>(() => b.AddStone(7));
        }

        [Fact]
        public void PlayerChangesWhenAddingStone()
        {
            var b = new Board();

            var oldPlayer = b.Player;
            b.AddStone(0);

            // Verify that player has changed
            Assert.NotEqual(oldPlayer, b.Player);
        }

        [Fact]
        public void AddingTooManyStonesToARow()
        {
            var b = new Board();

            for (var i = 0; i < 6; i++)
            {
                b.AddStone(0);
            }

            var oldPlayer = b.Player;
            Assert.Throws<InvalidOperationException>(() => b.AddStone(0));
            Assert.Equal(oldPlayer, b.Player);
        }

        [Fact]
        public void CheckIfBoardIsFull()
        {
            var b = new Board();
            for (byte column = 0; column < 7; column++)
            {
                for (byte row = 0; row < 6; row++)
                {
                    b.AddStone(column);
                }
            }

            Assert.True(b.IsBoardFull());
        }

        [Fact]
        public void CheckIfBoradIsEmpty()
        {
            var b = new Board();

            Assert.False(b.IsBoardFull());
        }
        [Fact]
        public void CheckIfPlayerHorizontalWon()
        {
            var b = new Board();
            // Player 1
            b.AddStone(0);
            // Player 2
            b.AddStone(0);
            // Player 1
            b.AddStone(1);
            // Player 2
            b.AddStone(0);
            // Player 1
            b.AddStone(2);
            // Player 2
            b.AddStone(2);
            // Player 1
            b.AddStone(3);
            Assert.Equal(2, b.GetHorizontalWinner());
        }

        [Fact]
        public void CheckIfPlayVerticalWon()
        {
            var b = new Board();
            // Player 1
            b.AddStone(0);
            // Player 2
            b.AddStone(2);
            // Player 1
            b.AddStone(0);
            // Player 2
            b.AddStone(3);
            // Player 1
            b.AddStone(0);
            // Player 2
            b.AddStone(3);
            // Player 1
            b.AddStone(0);
            Assert.Equal(2, b.GetVerticalWinner());
        }

        [Fact]
        public void CheckIfPlayerLeftToRightDiagonalWon()
        {
            var b = new Board();
            // Player 1
            b.AddStone(0);
            // Player 2
            b.AddStone(1);
            // Player 1
            b.AddStone(1);
            // Player 2
            b.AddStone(2);
            // Player 1
            b.AddStone(3);
            // Player 2
            b.AddStone(2);
            // Player 1
            b.AddStone(2);
            // Player 2
            b.AddStone(3);
            // Player 1
            b.AddStone(3);
            // Player 2
            b.AddStone(4);
            // Player 1
            b.AddStone(3);
            Assert.Equal(2, b.GetDiagonalWinner());
        }

        [Fact]
        public void CheckIfPlayerRightToLeftDiagonalWon()
        {
            var b = new Board();
            // Player 1
            b.AddStone(5);
            // Player 2
            b.AddStone(4);
            // Player 1
            b.AddStone(4);
            // Player 2
            b.AddStone(3);
            // Player 1
            b.AddStone(2);
            // Player 2
            b.AddStone(3);
            // Player 1
            b.AddStone(3);
            // Player 2
            b.AddStone(2);
            // Player 1
            b.AddStone(2);
            // Player 2
            b.AddStone(4);
            // Player 1
            b.AddStone(2);
            Assert.Equal(2, b.GetDiagonalWinner());
        }
        [Fact]
        public void CheckIfPlayerIsFirstPlayer()
        {
            var b = new Board();
            //First Player
            Assert.Equal(0, b.getCurrentPlayer());
            b.AddStone(4);
            // Second Player
            Assert.Equal(1, b.getCurrentPlayer());
        }
    }
}
