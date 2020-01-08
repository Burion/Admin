using System.Threading.Tasks;

namespace Admin.Services
{
    public interface IFileSavingService
    {
        Task<string> SaveFileAsync(byte[] bytes, string fileType, string subpath);
        Task<string> SaveFileAsync(string bytesEncoded, string fileType, string subpath);
    }
}