using System.Threading.Tasks;

namespace Musical_WebStore_BlazorApp.Server.Services
{
    public interface IFileSavingService
    {
        Task<string> SaveFileAsync(byte[] bytes, string fileType, string subpath);
        Task<string> SaveFileAsync(string bytesEncoded, string fileType, string subpath);
    }
}