using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    public class BlockQueue
    {
        private readonly Block[] blocks = new Block[]
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock(),
        };

        private readonly Random random = new Random();

        public Block NextBLock { get; private set; }

        public BlockQueue()
        {
            NextBLock = RandomBlock();
        }

        public Block RandomBlock()
        {
            return blocks[random.Next(blocks.Length)];
        }

        public Block GetAndUpdate()
        {
            Block block = NextBLock;


            // prevents same block from spawning twice in a row


            do
            {
                NextBLock = RandomBlock();
            }
            while (block.Id == NextBLock.Id);


            return block;
        }
    }
}
