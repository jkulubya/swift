namespace Swift
{
    public class StringFieldValue : FieldValue
    {
        private readonly string _value;
        
        public StringFieldValue(int code, string value, bool required) : base(code, required)
        {
            _value = value;
        }

        public override string Build() => $":{Code}:{_value}\r\n";
    }
}