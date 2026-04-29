namespace ApiDemo.Exceptions
{
    public class UnAuthorizeRequest : AppException
    {
        public UnAuthorizeRequest(string message) : base(message, 401)
        {

        }
    }
}
