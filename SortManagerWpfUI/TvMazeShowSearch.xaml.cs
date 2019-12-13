using Entities.Configuration;
using Entities.Data;
using Entities.Data.TvMaze;
using Entities.Ext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
                List<TvMazeResult> showResults = JsonConvert.DeserializeObject<List<TvMazeResult>>(str);

                //List<TvMazeResult> showResults = response.Content.ReadAsAsync<List<TvMazeResult>>().Result;
                List<TvMazeShowResultViewModel> shows = new List<TvMazeShowResultViewModel>();

                foreach (TvMazeResult _searchResult in showResults)
                {
                    shows.Add(_searchResult.show.GetViewModel());
                }

                SearchResultsGrid.ItemsSource = shows;

            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

    }
}
