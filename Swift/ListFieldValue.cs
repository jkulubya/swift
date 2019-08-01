using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Swift
{
    public class ListFieldValue : FieldValue
    {
        private readonly IEnumerable<string> _values;
        
        public ListFieldValue(IEnumerable<string> values, int code, bool required) : base(code, required)
        {
            _values = values;
        }

        public override string Build()
        {
            var builder = new StringBuilder();
            const string nl = "\r\n";
            builder.Append(":").Append(Code).Append(":");
            foreach (var value in _values)
            {
                builder.Append(value).Append(nl);
            }

            return builder.ToString();
        }
    }
}