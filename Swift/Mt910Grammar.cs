using System.Collections;
using System.Linq;
using System.Net.Sockets;
using Sprache;

namespace Swift
{
    public class Mt910Grammar
    {
        public static readonly Parser<string> MtBlockFieldContentEndDelimiter =
            (from lineEnd in Parse.LineEnd
                from _ in Parse.WhiteSpace.Until(Parse.Char(':'))
                select $"{lineEnd}:")
            .Or(
                from lineEnd in Parse.LineEnd
                from _ in Parse.WhiteSpace.Until(Parse.Char('-'))
                select $"{lineEnd}-"
            );

        public static readonly Parser<string> MtBlockFieldContent =
            Parse.AnyChar.Until(MtBlockFieldContentEndDelimiter).Text();

        public static readonly Parser<MtField> MtBlockField =
            from fieldType in Parse.Digit.Repeat(2).Text()
            from fieldTypeOption in Parse.Optional(Parse.Upper)
            from __ in Parse.Char(':')
            from fieldContent in MtBlockFieldContent
            select new MtField(type: fieldType,
                option: fieldTypeOption.IsDefined ? fieldTypeOption.Get().ToString() : null,
                content: fieldContent);
        
        private static readonly Parser<Block> SimpleBlockParser =
            from _ in Parse.Not(Parse.String("{4:"))
            from __ in Parse.Char('{')
            from name in Parse.LetterOrDigit.Many().Text()
            from ___ in Parse.Char(':')
            from content in Parse.AnyChar.Until(Parse.Char('}')).Text()
            select new Block(name, content);

        public static readonly Parser<Block> Block =
            (
                from __ in Parse.Char('{')
                from name in Parse.LetterOrDigit.Many().Text()
                from ___ in Parse.Char(':')
                from content in Parse.AtLeastOnce(SimpleBlockParser)
                from ____ in Parse.Char('}')
                select new Block(name, content.ToList())
            ).Or(from block in SimpleBlockParser select block);

        public static readonly Parser<MtBlock> MtBlock =
            from _ in Parse.String("{4:")
            from __ in Parse.WhiteSpace.Many()
            from ___ in Parse.Char(':') 
            from fields in MtBlockField.Until(Parse.String("}"))
            select new MtBlock(fields);

        public static readonly Parser<Mt910> Mt910 =
            from preBlocks in Parse.Many(Block)
            from mtBlock in MtBlock
            from postBlocks in Parse.Many(Block).End()
            select new Mt910(preBlocks.Concat(postBlocks), mtBlock);
    }
}