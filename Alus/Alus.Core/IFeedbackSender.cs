using System.Threading.Tasks;

namespace Alus.Core.Models
{
    public interface IFeedbackSender
    {
        Task SendAsync(Feedback feedback);
    }
}
