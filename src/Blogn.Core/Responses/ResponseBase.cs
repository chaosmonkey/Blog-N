namespace Blogn.Responses
{
    public abstract class ResponseBase
    {
        public ResponseType Type { get; protected set; }

        public string ErrorMessage { get; protected set; }
    }

    public abstract class ResponseBase<TResult> : ResponseBase
    {
        public TResult Data { get; protected set; }
    }
}
