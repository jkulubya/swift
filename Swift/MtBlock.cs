using System.Collections.Generic;
using System.Linq;

namespace Swift
{
    public class MtBlock
    {
        public const string Name = "4";
        public readonly List<MtField> Fields;

        public MtBlock(IEnumerable<MtField> fields)
        {
            Fields = fields.ToList();
        }
    }
}