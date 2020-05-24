using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace SecuNotesXam
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            GetStorageStat();

            GetDeviceID();
        }
        

        private void GetStorageStat()
        {
            Task<bool> SecureCheck = CheckIfSecureStorageIsSupported();

            if (SecureCheck.Result)
            {
                StorageStatDisplay.Text = "SecureStorage Is Supported";
                Vibration.Vibrate(150);
            }
            else
            {
                StorageStatDisplay.Text = "SecureStorage Is NOT Supported";
                Vibration.Vibrate();
            }

        }

        private static async Task<bool> CheckIfSecureStorageIsSupported()
        {
            try
            {
                await SecureStorage.SetAsync("GenericTestKey", "Sucsess!");
                await SecureStorage.GetAsync("GenericTestKey");
                return true;
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
                return false;
            }
        }

        private void GetDeviceID()
        {
            StringBuilder StringDevice = new StringBuilder();
            StringDevice.AppendLine("Device Model :" + DeviceInfo.Model);
            StringDevice.AppendLine("Manufacturer :" + DeviceInfo.Manufacturer);
            DeviceIDStatDisplay.Text = StringDevice.ToString();
        }

    }
}
