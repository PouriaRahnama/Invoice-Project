namespace Invoice.Framework.Common
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) { }
    }
}