using Com.OneSignal;
using Com.OneSignal.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace OneSignalTest
{
    public class PushNotificationService
    {
        private static readonly Lazy<PushNotificationService> lazy = new Lazy<PushNotificationService>(() => new PushNotificationService());
        public static PushNotificationService Instance { get { return lazy.Value; } }

        public string UUID { get; private set; }

        private void HandleIdsAvailableCallback(string playerID, string pushToken)
        {
            try
            {
                //this doesn't fire for iOS (works fine for Android)
                Debug.WriteLine("Retrieved uuid=" + playerID + " ;pushToken=" + pushToken);
                UUID = playerID;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
        public void FetchIDs()
        {
            try
            {
                OneSignal.Current.IdsAvailable(HandleIdsAvailableCallback);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
        public void SetupOneSignal()
        {
            try
            {
                Debug.WriteLine("Setting up OneSignal");

                OneSignal.Current.SetLogLevel(LOG_LEVEL.VERBOSE, LOG_LEVEL.ERROR);

                OneSignal.Current.StartInit("146069ed-8e2d-4477-b152-3016f8de6f3d")
                    .Settings(new Dictionary<string, bool>() { { IOSSettings.kOSSettingsKeyAutoPrompt, false } })
                    .InFocusDisplaying(OSInFocusDisplayOption.None)
                    .HandleNotificationReceived(NotificationReceivedHandler)
                    .EndInit();
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void RegisterPush()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {
                    case Xamarin.Forms.Device.Android:
                    case Xamarin.Forms.Device.iOS:
                        OneSignal.Current.RegisterForPushNotifications();
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }

        private void NotificationReceivedHandler(OSNotification notification)
        {
            try
            {
                OSNotificationPayload payload = notification.payload;

                Debug.WriteLine("NotificationReceivedHandler: " + payload.body + " ;displayType: " + notification.displayType);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
