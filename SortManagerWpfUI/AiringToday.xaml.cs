﻿using Entities.Configuration;
using Entities.Data;
using Entities.Data.EF_Core;
using Entities.Data.TvMaze;
using Entities.Ext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SortManagerWpfUI.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
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
        dynamic SelectedEntry;

        private readonly ProgramConfiguration AppSettings;
        private readonly DatabaseContext DatabaseContext;
        private readonly IOptions<ProgramConfiguration> Settings;
        private readonly IServiceProvider ServiceProvider;

        public AiringToday(IOptions<ProgramConfiguration> _settings, DatabaseContext _context)
        {
            InitializeComponent();
            Settings = _settings;
            AppSettings = _settings.Value;
            DatabaseContext = _context;

            ServiceProvider = (App.Current as App).ServiceProvider;

            PopulateUI();
        }

        private void PopulateUI()
        {
            DateTime _today = DateTime.Today;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(@"http://api.tvmaze.com/");
            client.DefaultRequestHeaders.Add("ApiToken", AppSettings.MediaAPIKeyConfiguration.ApiKeyInfo.Where(x => x.Name == "TVMaze").FirstOrDefault().ApiToken);
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync($@"schedule?country=US&date={ _today.Date.Year }-{ _today.Month }-{ _today.Day }").Result;
            if (response.IsSuccessStatusCode)
            {
                string str = response.Content.ReadAsStringAsync().Result;
                List<TheMovieDbEpisodeAiringResult> airingResults = JsonConvert.DeserializeObject<List<TheMovieDbEpisodeAiringResult>>(str);

                //List<TvMazeResult> showResults = response.Content.ReadAsAsync<List<TvMazeResult>>().Result;
                List<TvMazeShowResultViewModel> shows = new List<TvMazeShowResultViewModel>();

                foreach (TheMovieDbEpisodeAiringResult _episodeAiringToday in airingResults)
                {
                    var vm =_episodeAiringToday.show.GetViewModel();

                    vm.IsExistingShow = DoesShowExist(vm.Name, out int showPremiereYear, out string MatchedDirectoryName);

                    if (vm.IsExistingShow)
                    {
                        if (showPremiereYear < 0)
                        {
                            var x = _episodeAiringToday.show.premiered;
                            var y = x.StartsWith(showPremiereYear.ToString());

                            vm.IsExistingShow = _episodeAiringToday.show.premiered.StartsWith(showPremiereYear.ToString());
                        }
                    }

                    shows.Add(vm);
                }

                AiringTodayHeader.Text = $@"New Television Episodes Airing Today: { _today.DayOfWeek } { _today.Date.Month }/{ _today.Day }/{ _today.Year }";
                shows = shows.Where(x => x.AiringDay == _today.DayOfWeek.ToString()).ToList().OrderByDescending(x => x.IsExistingShow).ThenBy(x => x.Name).ToList();                
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
                string _showName = (sender as Button).Name.ToString();
                string imdbId = (sender as Button).Tag.ToString();

                using (DatabaseContext)
                {
                    //do EF SQL add here to priority shows with Stored Procedure
                }

                var AddNewPriorityWindow = new AddNewPriorityShow(Settings);
                AddNewPriorityWindow.DataContext = SelectedEntry;
                AddNewPriorityWindow.Show();
            }
            
        }

        private bool DoesShowExist(string ShowName, out int showPremiereYear, out string matchedShowDirectoryName)
        {
            List<string> allShows = new List<string>();
            showPremiereYear = -1;
            matchedShowDirectoryName = string.Empty;

            foreach (string _drive in AppSettings.TelevisionLibraryConfiguration.TelevisionLibrary.LibraryFolders)
            {
                var dirs = Directory.GetDirectories(_drive);

                foreach (string _show in dirs)
                {
                    string showDirectoryName = _show.Split("//").Last();

                    if (showDirectoryName.ToLower().Contains(ShowName.ToLower()))
                    {
                        matchedShowDirectoryName = showDirectoryName;

                        string regex = @"(?<ShowName>.*)\s(?<ShowPremiere>[(](?<PremiereYear>\d\d\d\d)[)])";
                        var match = Regex.Match(showDirectoryName, regex);

                        if (match.Success)
                        {
                            int.TryParse(match.Groups["PremiereYear"].Value, out showPremiereYear);
                        }
                        else
                        {
                            //Premiere year is not in library folder name but show matches
                            //Allows for fuzzy doesshowexist logic
                            showPremiereYear = 0;
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

        private void Button_Click_GetMoreInfo(object sender, RoutedEventArgs e)
        {
            var _newWindow = ServiceProvider.GetRequiredService<ViewShowDetails>();
                _newWindow.DataContext = SelectedEntry as TvMazeShowResultViewModel;
                _newWindow.Show();
        }

        private void AiringTodayDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedEntry = (e.Source as DataGrid).SelectedItem;
        }
    }
}
