namespace Invoice.Framework.Common
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
    }
}