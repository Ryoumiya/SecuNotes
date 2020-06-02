using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SecuNotesXam
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigPage : ContentPage
    {
        public ConfigPage()
        {
            InitializeComponent();
        }

        private async void NukeButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("WARNING!", "This Will Remove Everything!", "OK", "No");
            if (answer)
            {
                //Bye bye...
                SecureStorage.RemoveAll();
                NukeConfirm.On = false;
            }
        }

        private void PwsdSwitch_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //Password Enable Stuff
            //Debug.WriteLine("PswdSwitch Changed!");
            //Debug.WriteLine(PwsdSwitch.On);
        }

        private void NukeConfirm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (NukeConfirm.On)
            {
                NukeButton.IsEnabled = true;
            }
            else
            {
                NukeButton.IsEnabled = false;
            }
        }
    }
}