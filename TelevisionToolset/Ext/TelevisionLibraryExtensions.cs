using Entities.Television;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TelevisionToolset.Ext
{
    public static class TelevisionLibraryExtensions
    {
        public static bool DoesShowExist(string ShowName)
        {
            int count = 0;

            foreach (string _showDirectory in Directory.GetDirectories(@"E:\TV Shows").Where(x => x.ToLower().Contains(ShowName.ToLower())))
            {
                count++;
            }

            foreach (string _showDirectory in Directory.GetDirectories(@"F:\TV Shows").Where(x => x.ToLower().Contains(ShowName.ToLower())))
            {
                count++;
            }

            foreach (string _showDirectory in Directory.GetDirectories(@"G:\TV Shows").Where(x => x.ToLower().Contains(ShowName.ToLower())))
            {
                count++;
            }

            foreach (string _showDirectory in Directory.GetDirectories(@"H:\TV Shows").Where(x => x.ToLower().Contains(ShowName.ToLower())))
            {
                count++;
            }

            foreach (string _showDirectory in Directory.GetDirectories(@"I:\TV Shows").Where(x => x.ToLower().Contains(ShowName.ToLower())))
            {
                count++;
            }

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DoesShowExist(string ShowName, out string ShowRootDirectory)
        {
            List<string> allShows = new List<string>();

            foreach (string _showDirectory in Directory.GetDirectories(@"E:\TV Shows").Where(x => x.ToLower().Contains(ShowName.ToLower())))
            {
                allShows.Add(_showDirectory);
            }

            foreach (string _showDirectory in Directory.GetDirectories(@"F:\TV Shows").Where(x => x.ToLower().Contains(ShowName.ToLower())))
            {
                allShows.Add(_showDirectory);
            }

            foreach (string _showDirectory in Directory.GetDirectories(@"G:\TV Shows").Where(x => x.ToLower().Contains(ShowName.ToLower())))
            {
                allShows.Add(_showDirectory);
            }

            foreach (string _showDirectory in Directory.GetDirectories(@"H:\TV Shows").Where(x => x.ToLower().Contains(ShowName.ToLower())))
            {
                allShows.Add(_showDirectory);
            }

            foreach (string _showDirectory in Directory.GetDirectories(@"I:\TV Shows").Where(x => x.ToLower().Contains(ShowName.ToLower())))
            {
                allShows.Add(_showDirectory);
            }

            if (allShows.Count > 0)
            {
                ShowRootDirectory = allShows.First();
                return true;
            }
            else
            {
                ShowRootDirectory = null;
                return false;
            }            
        }

    }
}
