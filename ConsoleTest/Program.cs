using Entities;
using Entities.Data;
using Entities.Data.EzTv;
using System;
using System.Collections.Generic;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var entity = new EztvDownloadRequest("00c6d4deeaf3fb52d74872d203f5f68e9350acd7");

            var helper = new EzTvApiHelper().MakeAPICall();

            //var eztvRequest = helper.MakeAPICall(ApiRequestAgents.EzTV, ApiRequestTypes.TvMaze_SINGLE, "6048596");

            //var tvMazeRequest = helper.MakeAPICall(ApiRequestAgents.TvMaze, ApiRequestTypes.TvMaze_SINGLE, "6048596");

            //var theMovieDbRequest = helper.MakeAPICall(ApiRequestAgents.TheMovieDb, ApiRequestTypes.TvMaze_SINGLE, "6048596", "44");

            //var openMovieDbRequest = helper.MakeAPICall(ApiRequestAgents.Omdb, ApiRequestTypes.TvMaze_SINGLE, "6048596");

            Console.WriteLine("Completed!!");
            Console.ReadLine();
        }
    }
}
