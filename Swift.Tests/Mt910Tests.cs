using System;
using System.Linq;
using Sprache;
using Xunit;

namespace Swift.Tests
{
    public class Mt910Tests
    {
        [Fact]
        public void ParseNonMtBlockWithSimpleField()
        {
            var result = Mt910Grammar.Block.Parse("{1:F21XXXXYYMMMXXX0155001111}");
            Assert.Equal("1", result.Name);
            Assert.Equal("F21XXXXYYMMMXXX0155001111", result.Content);
        }

        [Fact]
        public void ParseNonMtBlockWithSingleSubBlock()
        {
            {
                var result = Mt910Grammar.Block.Parse("{5:{CHK:EF80F3FB2ED6}}");
                Assert.Equal("5", result.Name);
                Assert.Equal("CHK", result.SubBlocks["CHK"].Name);
                Assert.Equal("EF80F3FB2ED6", result.SubBlocks["CHK"].Content);
            }
            {
                var result = Mt910Grammar.Block.Parse("{3:{108:1801337624.00}}");
                Assert.Equal("3", result.Name);
                Assert.Equal("108", result.SubBlocks["108"].Name);
                Assert.Equal("1801337624.00", result.SubBlocks["108"].Content);
            }
        }
        
        [Fact]
        public void ParseNonMtBlockWithMultipleSubBlocks()
        {
            var result = Mt910Grammar.Block.Parse("{4:{177:1704250744}{451:0}}");
            Assert.Equal("4", result.Name);
            Assert.Equal("177", result.SubBlocks["177"].Name);
            Assert.Equal("1704250744", result.SubBlocks["177"].Content);
            Assert.Equal("451", result.SubBlocks["451"].Name);
            Assert.Equal("0", result.SubBlocks["451"].Content);
        }

        [Fact]
        public void ParseSingleLineMtBlockFieldTest()
        {
            {
                var result = Mt910Grammar.MtBlockField.Parse("20:SSCHG1614\r\n:");
                Assert.Equal("20", result.Type);
                Assert.Null(result.Option);
                Assert.Equal("SSCHG1614", result.Content);
            }

            {
                var result = Mt910Grammar.MtBlockField.Parse("32A:170425UGX484,\r\n-");
                Assert.Equal("32", result.Type);
                Assert.Equal("A", result.Option);
                Assert.Equal("170425UGX484,", result.Content);
            }
        }
        
        [Fact]
        public void ParseMultilineMtBlockFieldTest()
        {
            {
                var result = Mt910Grammar.MtBlockField.Parse("20:SSCHG1614\r\nSSCHG1615\r\nSSCHG1616\r\n:");
                Assert.Equal("20", result.Type);
                Assert.Null(result.Option);
                Assert.Equal("SSCHG1614\r\nSSCHG1615\r\nSSCHG1616", result.Content);
            }

            {
                var result = Mt910Grammar.MtBlockField.Parse("32A:170425UGX484,\r\n-");
                Assert.Equal("32", result.Type);
                Assert.Equal("A", result.Option);
                Assert.Equal("170425UGX484,", result.Content);
            }
        }

        [Fact]
        public void ParseMtBlock()
        {
            var result = Mt910Grammar.MtBlock.Parse(
                @"{4:
:20:SSCHG1614
:21:REF1614
:25:3000000003
:32A:170425UGX484,
:52A:XXXXYYZZ
-}");
            Assert.Equal("4",MtBlock.Name);
            Assert.Equal("SSCHG1614", result.Fields.Single(f => f.Type == "20").Content);
            Assert.Equal("REF1614", result.Fields.Single(f => f.Type == "21").Content);
            Assert.Equal("3000000003", result.Fields.Single(f => f.Type == "25").Content);
            Assert.Equal("170425UGX484,", result.Fields.Single(f => f.Type == "32" && f.Option == "A").Content);
            Assert.Equal("XXXXYYZZ", result.Fields.Single(f => f.Type == "52" && f.Option == "A").Content);
        }
        
        [Fact]
        public void ParseMt910()
        {
            var result = Mt910Grammar.Mt910.Parse(
                @"{1:F21XXXXYYMMMXXX0155001111}{4:{177:1704250744}{451:0}}{1:F21XXXXYYMMMXXX0155001111}{2:O9101044170425XXXXYYZZAXXX03330334541704251044N}{3:{108:1801337624.00}}{4:
:20:SSCHG1614
:21:REF1614
:25:3000000003
:32A:170425UGX484,
:52A:XXXXYYZZ
-}{5:{CHK:EF80F3FB2ED6}}{S:{COP:P}}");
            
            Assert.Equal(6,result.Blocks.Count);
            Assert.Equal("SSCHG1614", result.MtBlock.Fields.Single(f => f.Type == "20").Content);
            Assert.Equal("REF1614", result.MtBlock.Fields.Single(f => f.Type == "21").Content);
            Assert.Equal("3000000003", result.MtBlock.Fields.Single(f => f.Type == "25").Content);
            Assert.Equal("170425UGX484,", result.MtBlock.Fields.Single(f => f.Type == "32" && f.Option == "A").Content);
            Assert.Equal("XXXXYYZZ", result.MtBlock.Fields.Single(f => f.Type == "52" && f.Option == "A").Content);
        }
    }
}