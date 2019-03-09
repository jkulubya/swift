using System.Runtime.InteropServices.ComTypes;

namespace Swift
{
    public class MtField
    {
        public string Type { get; }
        public string Option { get; }
        public string Content { get; set; }
        
        public MtField(string type, string option, string content)
        {
            Type = type;
            Option = option;
            Content = content;
        }
    }
}