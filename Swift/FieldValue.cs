using System;

namespace Swift
{
    public abstract class FieldValue
    {
        public FieldValue(int code, bool required)
        {
            Code = code;
            Required = required;
        }
        public int Code { get; set; }
        public bool Required { get; }

        public abstract string Build();
    }
}