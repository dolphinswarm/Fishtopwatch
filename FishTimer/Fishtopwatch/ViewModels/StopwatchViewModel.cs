﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fishtopwatch.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Fishtopwatch.ViewModels
{
    public partial class StopwatchViewModel : ObservableObject
    {
        public StopwatchViewModel()
        {
            Stopwatches = new ObservableCollection<StopwatchModel>();
        }

        [ObservableProperty]
        ObservableCollection<StopwatchModel> stopwatches;

        [RelayCommand]
        async Task GetStopwatchData()
        {
            // Clear out the stopwatches so we can retrieve the new list
            Stopwatches.Clear();

            // Else, load the stopwatches from the databse
            var stopwatches = await App.StopwatchRepository.GetAllStopwatchs();

            stopwatches.ForEach(stopwatch =>
            {
                Stopwatches.Add(stopwatch);
            });
        }    
    }
}
