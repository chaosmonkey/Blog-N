namespace Blogn.Responses
{
    public enum ResponseType
    {
        /// <summary>
        /// Indications the command completed successfully
        /// </summary>
        Success,
        /// <summary>
        /// Indicates there was an error processing the request
        /// </summary>
        Error,
        /// <summary>
        /// Indicates the command was accepted and is processing asynchronously.
        /// </summary>
        Processing,
        /// <summary>
        /// Indicated that the command failed because the resource was not found.
        /// </summary>
        NotFound
    }
}
