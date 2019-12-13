using Entities.Configuration;
using Entities.Data;
using Entities.Data.TvMaze;
using Entities.Ext;
using Entities.Library;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SortManagerWpfUI
{
    /// <summary>
    /// Interaction logic for TvMazeShowSearch.xaml
    /// </summary>
    public partial class TvMazeShowSearch : Window
    {
        private ProgramConfiguration AppSettings;
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public TvMazeShowSearch()
        {
            InitializeComponent();
        }

        public TvMazeShowSearch(IOptions<ProgramConfiguration> _settings)
        {
            InitializeComponent();
            AppSettings = _settings.Value;

            string x = AppSettings.SortConfiguration.LocalSortDirectory;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://api.tvmaze.com/search/shows");
            //client.DefaultRequestHeaders.Add("appkey", "myapp_key");
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("?q=" + TxtUserQuery.Text).Result;
            if (response.IsSuccessStatusCode)
            {
                string str = response.Content.ReadAsStringAsync().Result;
                List<TvMazeShowResult> showResults = JsonConvert.DeserializeObject<List<TvMazeShowResult>>(str);

                //List<TvMazeResult> showResults = response.Content.ReadAsAsync<List<TvMazeResult>>().Result;
                List<TvMazeShowResultViewModel> shows = new List<TvMazeShowResultViewModel>();

                foreach (TvMazeShowResult _searchResult in showResults)
                {
                    var vm = _searchResult.show.GetViewModel();
                    int showPremiereYear = 0;
                    //vm.IsExistingShow = 

                    if (DoesShowExist(vm.Name, out showPremiereYear))
                    {
                        if (showPremiereYear == 0)
                        {                            
                            vm.IsExistingShow = true;
                        }
                        else
                        {

                        }
                        
                    } 

                shows.Add(vm);

                }

                SearchResultsGrid.ItemsSource = shows;

            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        private bool DoesShowExist(string ShowName, out int showPremiereYear)
        {
            List<string> allShows = new List<string>();
            showPremiereYear = 0;

            foreach (string _drive in AppSettings.TelevisionLibraryConfiguration.TelevisionLibrary.LibraryFolders)
            {
                var dirs = Directory.GetDirectories(_drive);                

                foreach (string _show in dirs)
                {
                    string showDirectoryName = _show.Split("//").Last();

                    if (showDirectoryName.ToLower().Contains(ShowName.ToLower()))
                    {
                        string regex = @"(?<ShowName>.*)\s(?<ShowPremiere>[(](?<PremiereYear>\d\d\d\d)[)])";
                        var match = Regex.Match(showDirectoryName, regex);

                        if (match.Success)
                        {
                            int.TryParse(match.Groups["PremiereYear"].Value, out showPremiereYear);
                        }

                        allShows.Add(_show);
                    }                    
                }
            }            

            if (allShows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
