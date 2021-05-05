namespace Package.Elasticsearch
{
    public class QueryAdvanced
    {
        public string Field { get; set; }
        public Function Function { get; set; }
        public DataType ValueType { get; set; }
        public string Value { get; set; }

        public QueryAdvanced(
            string field,
            Function function,
            DataType valueType,
            string value)
        {
            Field = field;
            Function = function;
            ValueType = valueType;
            Value = value;
        }
    }

    public enum Function
    {
        Equal,
        NotEqual,
        Between,
        In,
        NotIn
    }
}