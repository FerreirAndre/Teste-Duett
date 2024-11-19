namespace UserControl.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name) : base($"User {name} not found.")
        {
        }
    }
}
