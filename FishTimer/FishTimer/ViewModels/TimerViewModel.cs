using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FishTimer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FishTimer.ViewModels
{
    public partial class TimerViewModel : ObservableObject
    {
        public TimerViewModel()
        {
            Timers = new ObservableCollection<TimerModel>();
        }

        [ObservableProperty]
        ObservableCollection<TimerModel> timers;

        [RelayCommand]
        async Task GetTimerData()
        {
            // Clear out the timers so we can retrieve the new list
            Timers.Clear();

            // Else, load the timers from the databse
            var timers = await App.TimerRepository.GetAllTimers();
            
            timers.ForEach(timer => {
                Timers.Add(timer);
            });
        }
    }
}
