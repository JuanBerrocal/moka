
using Moka.Import;

namespace Moka.Services
{
        public interface IStoreImportService
    {
        public Task<StoreImportResult> ImportAsync(IFormFile file);
    }
}
