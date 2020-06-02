using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace SecuNotesXam
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
#if DEBUG 
            //For Debug and Testing ONLY!
            MainPage = new SecuNotesXam.MainPage();
#else
            //this is the real main page
            MainPage = new NotesDisplayer();
#endif
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
