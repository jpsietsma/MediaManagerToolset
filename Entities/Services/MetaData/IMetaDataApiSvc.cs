using Entities.Data.TmDB;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Net.Http;
using System.Threading.Tasks;

namespace Entities.Services.MetaData
{
    public interface IMetaDataApiSvc
    {
        string RequestUrl { get; }

        Task<T1> GetShowResultAsync<T, T1>(int ImdbId) 
            where T : class
            where T1: IApiCallResult;
        Task<T1> GetShowResultAsync<T, T1>(params ObjectParameter[] _parameters)
            where T : class
            where T1: IApiCallResult;
        Task<T1> GetShowResultAsync<T, T1>(string showName)
            where T : class
            where T1: IApiCallResult;

        Task<List<T1>> GetManyShowResultsAsync<T, T1>(string showName)
           where T : IApiCallMultipleResultset
           where T1 : IApiCallResult;

        Task<TheMovieDbExternalIds> GetExternalIds(int theMovieDbId);
        
        dynamic GetResult();
    }
}