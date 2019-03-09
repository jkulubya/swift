using System.Collections;
using System.Collections.Generic;

namespace Swift
{
    public class Mt910
    {
        public Dictionary<string, Block> Blocks { get; } = new Dictionary<string, Block>();
        public MtBlock MtBlock { get; }
        
        public Mt910(IEnumerable<Block> blocks)
        {
            foreach (var block in blocks)
            {
                Blocks[block.Name] = block;
            }
        }
        
        public Mt910(IEnumerable<Block> blocks, MtBlock mtBlock)
        {
            foreach (var block in blocks)
            {
                Blocks[block.Name] = block;
            }

            MtBlock = mtBlock;
        }
    }
}