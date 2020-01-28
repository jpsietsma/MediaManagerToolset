﻿using Entities.Data.TmDB;
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
        Task<T1> GetShowResultAsync<T, T1>(int ImdbId) 
            where T : class
            where T1: IApiCallResult;
        Task<T1> GetShowResultAsync<T, T1>(params ObjectParameter[] _parameters)
            where T : class
            where T1: IApiCallResult;
        Task<T1> GetShowResultAsync<T, T1>(string showName)
            where T : class
            where T1: IApiCallResult;
    }
}