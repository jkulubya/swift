# DISCLAIMER 
This code is (probably) slow, and its scope is limited.
It was pulled out of a project where it did the job for me 100%. There is no validation (that was done elsewhere). The api could do with a bit of help. etc etc

# How to
```
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
```

See tests project for more guidance
