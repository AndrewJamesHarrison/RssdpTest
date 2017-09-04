using System;
using System.Collections.Generic;
using System.Text;
using Rssdp;
using System.ComponentModel;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;

namespace Rssdptest
{
    public class CommunicationController  : INotifyPropertyChanged
    {
        public IEnumerable<DiscoveredSsdpDevice> FoundDevices { get; private set; }

        public ObservableCollection<string> DeviceNames { get; set; }

        public Command TestCommand { get; set; }

        public string Test1 => "Test";

        public int DeviceCount { get; set; }

        private string test2;

        public string Test2
        {
            get
            {
                return test2;
            }
            set
            {
                test2 = value;
                NotifyPropertyChanged(nameof(Test2));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CommunicationController()
        {
            TestCommand = new Command(()=>BeginSearch());
            DeviceNames = new ObservableCollection<string>();
            DeviceCount = 0;
            Test2 = "Start";
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private SsdpDeviceLocator _DeviceLocator;

        // Call this method from somewhere in your code to start the search.
        public void BeginSearch()
        {
            _DeviceLocator = new SsdpDeviceLocator();

            // (Optional) Set the filter so we only see notifications for devices we care about 
            // (can be any search target value i.e device type, uuid value etc - any value that appears in the 
            // DiscoverdSsdpDevice.NotificationType property or that is used with the searchTarget parameter of the Search method).

            //_DeviceLocator.NotificationFilter = "uuid:Andrew-PC";

            // Connect our event handler so we process devices as they are found
            _DeviceLocator.DeviceAvailable += deviceLocator_DeviceAvailable;

            // Enable listening for notifications (optional)
            _DeviceLocator.StartListeningForNotifications();

            // Perform a search so we don't have to wait for devices to broadcast notifications 
            // again to get any results right away (notifications are broadcast periodically).
            //_DeviceLocator.SearchAsync();

        }

        // Process each found device in the event handler
        void deviceLocator_DeviceAvailable(object sender, DeviceAvailableEventArgs e)
        {
            DeviceNames.Add(e.DiscoveredDevice.DescriptionLocation.ToString());
            //Can retrieve the full device description easily though.
            /*var fullDevice = await e.DiscoveredDevice.GetDeviceInfo();
            try
            {
                if (fullDevice != null)
                    DeviceNames.Add(fullDevice.FriendlyName);
            }
            catch
            {
                Android.Util.Log.Debug("CommunicationController: searchFordevices method", "Null Error");
            }*/
            DeviceCount++;
        }
        /*
        public async void SearchForDevices()
        {
            using (var deviceLocator = new SsdpDeviceLocator())
            {
                FoundDevices = await deviceLocator.SearchAsync("uuid:<Andrew-PC>");
                while(DeviceCount<15) 
                {
                    SsdpDevice fullDevice = await FoundDevices.ElementAt(DeviceCount).GetDeviceInfo();
                    try
                    {
                        if (fullDevice != null)
                            DeviceNames.Add(fullDevice.FriendlyName);
                    }
                    catch
                    {
                        Android.Util.Log.Debug("CommunicationController: searchFordevices method", "Null Error");
                    }
                    DeviceCount++;
                }
                NotifyPropertyChanged(nameof(DeviceCount));
                Test2 = "Finish";
            }
        }
        */
    }
}
