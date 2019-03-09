using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace Swift
{
    public class Block
    {
        public string Name { get; }
        public string Content { get; }
        
        public Dictionary<string, Block> SubBlocks { get; } = new Dictionary<string, Block>();
        
        public Block(string name, string content)
        {
            Name = name;
            Content = content;
        }

        public Block(string name, List<Block> subBlocks)
        {
            Name = name;

            foreach (var block in subBlocks)
            {
                SubBlocks[block.Name] = block;
            }
        }
    }
}