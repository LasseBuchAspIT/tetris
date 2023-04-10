using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    public class GameState
    {
        private Block currentBlock;

        public Block CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset();

                for(int i = 0; i < 2 ; i++)
                {
                    currentBlock.Move(1, 0);
                    if (!BlockFits())
                    {
                        currentBlock.Move(-1, 0);
                    }
                }
            }
        }

        public GameGrid GameGrid { get; }

        public BlockQueue BlockQueue { get; }
        public bool GameOver { get; private set; }

        public GameState()
        {
            GameGrid = new GameGrid(22, 10);
            BlockQueue = new BlockQueue();
            CurrentBlock = BlockQueue.GetAndUpdate();
        }

        public bool BlockFits()
        {
            foreach(Position p in currentBlock.TilePositions())
            {
                if (!GameGrid.IsEmpty(p.Row, p.Column))
                {
                    return false;
                }
            }
            return true;
        }
        
        public void RotateBlockCW()
        {
            CurrentBlock.RotateCW();
            if (!BlockFits())
            {
                currentBlock.RotateCCW();
            }
        }

        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCCW();
            if (!BlockFits())
            {
                currentBlock.RotateCW();
            }
        }

        public void MoveBlockLeft()
        {
            currentBlock.Move(0, -1);

            if (!BlockFits())
            {
                currentBlock.Move(0, 1);
            }
        }

        public void MoveBlockRight()
        {
            currentBlock.Move(0, 1);

            if (!BlockFits())
            {
                currentBlock.Move(0, -1);
            }
        }

        private bool IsGameOver()
        {
            return !(GameGrid.IsRowEmpty(0) && GameGrid.IsRowEmpty(1));
        }

        private void PlaceBlock()
        {
            foreach (Position p in currentBlock.TilePositions())
            {
                GameGrid[p.Row, p.Column] = CurrentBlock.Id;
                Console.WriteLine("Placed");
            }

            GameGrid.ClearFullRows();

            if (IsGameOver())
            {
                GameOver = true;
            }

            else
            {
                CurrentBlock = BlockQueue.GetAndUpdate();
            }
        }

        public void MoveBlockDown()
        {
            currentBlock.Move(1, 0);

            if (!BlockFits())
            {
                currentBlock.Move(-1, 0);
                PlaceBlock();
                
            }
        }

        public void DropBlock()
        {
            while (true)
            {
                currentBlock.Move(1, 0);

                if (!BlockFits())
                {
                    currentBlock.Move(-1, 0);
                    PlaceBlock();
                    return;
                }
                
            }
        }
    }
}
