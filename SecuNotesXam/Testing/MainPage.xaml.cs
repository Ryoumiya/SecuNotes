using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Diagnostics;

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
            }
            else
            {
                StorageStatDisplay.Text = "SecureStorage Is NOT Supported";
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

        private async void Memobuttonclick(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NotesDisplayer(), true);
        }

        string TextTest = "---";

        DateTime PressDurationStart;

        int LongPressMilis = 400;

        private void PressTestButton_Pressed(object sender, EventArgs e)
        {
            //TextTest += "\n Pressed!";
            DateTime Current = DateTime.Now;
            PressDurationStart = Current;
            Debug.WriteLine("Presed!" + Current.ToString("mm:ss.fff"));
            //BtnPressText.Text = TextTest;

        }

        private void PressTestButton_Released(object sender, EventArgs e)
        {
            //TextTest += "\n Relesed!";
            DateTime Current = DateTime.Now;

            var PressLength = Current - PressDurationStart;

            Debug.WriteLine("Relesed!" + Current.ToString("mm:ss.fff"));

            Debug.WriteLine("Press Duration Total MS:" + PressLength.TotalMilliseconds.ToString());
            Debug.WriteLine("Press Duration MS:" + PressLength.Milliseconds.ToString());
            //Deteremn if long press or short press
            if (PressLength.TotalMilliseconds > LongPressMilis)
            {
                Debug.WriteLine("Long Press !!");
            }
            else
            {
                Debug.WriteLine("Short Press !");
            }

            //BtnPressText.Text = TextTest;
        }

        private void PressTestButton_Clicked(object sender, EventArgs e)
        {
            /*
            TextTest += "\n Clicked!";
            DateTime Current = DateTime.Now;
            Debug.WriteLine("Clicked!" + Current.ToString("mm:ss.fff"));
            BtnPressText.Text = TextTest;
            */
        }

        bool LongPress = false;
        bool IsPressed = false;
        private void LPBtoon_Pressed(object sender, EventArgs e)
        {
            LPBtoon.Text = "Pressed!";
            IsPressed = true;
            Device.StartTimer(TimeSpan.FromMilliseconds(LongPressMilis), () =>
            {
                if (IsPressed)
                {
                    LPBtoon.Text = "Long Press!";
                    LongPress = true;
                }
                return false;
            });
        }

        private void LPBtoon_Released(object sender, EventArgs e)
        {
            IsPressed = false;
            LPBtoon.Text = "Not Pressed!";
            if (LongPress)
            {
                Debug.WriteLine("LongPress!");
            }
            else
            {
                Debug.WriteLine("ShortPress!");
            }

            LongPress = false;
        }

        private async void ToastButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
            Debug.WriteLine("Answer: " + answer);
        }

        private async void ConfigButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ConfigPage(), true);
        }
    }
}
