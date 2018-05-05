using System.Threading.Tasks;
using TwitterBackup.Data.DTO;

namespace TwitterBackup.Data.Services.ServiceInterfaces
{
    public interface ITweetService
    {
        TweetDTO GetTweetById(long id);

        Task<bool> Add(TweetDTO dto);

        Task<bool> Delete(long id);
    }
}
