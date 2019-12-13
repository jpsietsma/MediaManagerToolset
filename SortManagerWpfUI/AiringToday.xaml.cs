using Entities.Configuration;
using Entities.Data;
using Entities.Data.TvMaze;
using Entities.Ext;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for TelevisionLibrary.xaml
    /// </summary>
    public partial class AiringToday : Window
    {
        private readonly ProgramConfiguration AppSettings;
        private readonly IOptions<ProgramConfiguration> Settings;

        private IServiceProvider ServiceProvider;

        string _query = "cops";

        public AiringToday(IOptions<ProgramConfiguration> _settings)
        {
            InitializeComponent();
            Settings = _settings;
            AppSettings = _settings.Value;

            PopulateUI();
        }

        private void PopulateUI()
        {
            DateTime _today = DateTime.Today;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(@"http://api.tvmaze.com/");
            //client.DefaultRequestHeaders.Add("appkey", "myapp_key");
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync($@"schedule?country=US&date={ _today.Date.Year }-{ _today.Month }-{ _today.Day }").Result;
            if (response.IsSuccessStatusCode)
            {
                string str = response.Content.ReadAsStringAsync().Result;
                List<TvMazeEpisodeAiringResult> airingResults = JsonConvert.DeserializeObject<List<TvMazeEpisodeAiringResult>>(str);

                //List<TvMazeResult> showResults = response.Content.ReadAsAsync<List<TvMazeResult>>().Result;
                List<TvMazeShowResultViewModel> shows = new List<TvMazeShowResultViewModel>();

                foreach (TvMazeEpisodeAiringResult _episodeAiringToday in airingResults)
                {
                    shows.Add(_episodeAiringToday.show.GetViewModel());
                }

                AiringTodayHeader.Text = $@"New Television Episodes Airing Today: { _today.DayOfWeek } { _today.Date.Month }/{ _today.Day }/{ _today.Year }";
                shows = shows.Where(x => x.AiringDay == _today.DayOfWeek.ToString()).ToList();
                AiringTodayDataGrid.ItemsSource = shows;

            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Tag != null)
            {
                string _showName = (sender as Button).Tag.ToString();

                MessageBox.Show(_showName);

                new AddNewPriorityShow(Settings).Show();
            }
            
        }
    }
}
