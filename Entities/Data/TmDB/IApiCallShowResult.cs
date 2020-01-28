namespace Entities.Data.TmDB
{
    public interface IApiCallResult
    {
        string name { get; set; }

        public dynamic GetResult<T>()
            where T : class;
    }
}