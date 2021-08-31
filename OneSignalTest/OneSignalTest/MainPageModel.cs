using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace OneSignalTest
{
    public class MainPageModel
    {
        public ICommand InitCommand { get; private set; }
        public ICommand RegisterPush { get; private set; }
        public ICommand FetchCommand { get; private set; }

        public MainPageModel()
        {
            InitCommand = new Command(() =>
            {
                PushNotificationService.Instance.SetupOneSignal();
            });

            RegisterPush = new Command(() =>
            {
                PushNotificationService.Instance.RegisterPush();
            });

            FetchCommand = new Command(() =>
            {
                PushNotificationService.Instance.FetchIDs();
            });


        }
    }
}
