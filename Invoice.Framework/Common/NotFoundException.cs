namespace Invoice.Framework.Common
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}