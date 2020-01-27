using System.Data.Entity.Core.Objects;
using System.Net.Http;
using System.Threading.Tasks;

namespace MediaToolsetWebCoreMVC.Services.MetaData
{
    public interface IMetaDataApiSvc
    {
        string RequestUrl { get; }
        dynamic Result { get; }

        HttpClient GetRequestClient();
        Task<T> GetShowResultAsync<T>(int ImdbId) where T : class;
        Task<T> GetShowResultAsync<T>(params ObjectParameter[] _parameters) where T : class;
        Task<T> GetShowResultAsync<T>(string showName) where T : class;
    }
}