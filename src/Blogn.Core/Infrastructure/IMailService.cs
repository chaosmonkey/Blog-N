using System.Net;
using System.Threading.Tasks;

namespace Blogn.Infrastructure
{
    public interface IMailService
    {
        Task<HttpStatusCode> SendAsync(string toAddress, string toName, string fromAddress, string fromName,
                                        string subject, string content, string htmlContent=null);
    }
}
