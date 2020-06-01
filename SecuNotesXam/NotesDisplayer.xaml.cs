using System;
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
    public partial class NotesDisplayer : ContentPage
    {
        bool isMainButtonPressed = false;
        bool isMainButtonLongPress = false;

        public NotesDisplayer()
        {
            InitializeComponent();
        }
        private void DemoOnlyFillWithRandom(int range)
        {
            List<NotedItem> ListOfNotes = new List<NotedItem>();
            ListOfNotes.Add(new NotedItem() { TitleText = "HelloWorld", ContentText = "This is a TestString1" });
            
            for(int i = 0; i < range; i++)
            {
                ListOfNotes.Add(new NotedItem()
                {
                    TitleText = "TestNote - " + i.ToString(),
                    ContentText = "Lorem ipsum dolor sit amet, " +
                    "consectetur adipiscing elit. Sed cursus malesuada efficitur. " +
                    "Proin in sapien arcu. Ut elementum, ipsum nec tempus imperdiet, " +
                    "ante mauris pharetra mauris, nec volutpat purus nunc ut dui",
                    LastModifiedDate = DateTime.Now,
                    ID = i.ToString()
                }); ;
            }

            MemoDisplay.ItemsSource = ListOfNotes; 
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            DemoOnlyFillWithRandom(20);
        }

        private async void GetNotes()
        {
            List<NotedItem> ListofItems = new List<NotedItem>();
            string SAoN = await SecureStorage.GetAsync("NotesAmount_Key");
            int AmountOfNotes = Convert.ToInt32(SAoN);
            for(int i = 0; i  < AmountOfNotes; i++)
            {

            }
        }

        

        private void MainButton_Pressed(object sender, EventArgs e)
        {
            isMainButtonPressed = true;
            Device.StartTimer(TimeSpan.FromMilliseconds(400), () => {
                if (isMainButtonPressed)
                {
                    isMainButtonLongPress = true;
                    MainButton.Text = "Settings!";
                }
                //To make sure the Timer only run once
                return false;
            });
        }

        private async void MainButton_Released(object sender, EventArgs e)
        {
            if(isMainButtonLongPress)
            {
                //Long Press!

            }
            else
            {
                //Short Press!
                await Navigation.PushModalAsync(new NotesEdit(), true);
            }

            //Reset The Button Conditions
            isMainButtonPressed = false;
            isMainButtonLongPress = false;
            MainButton.Text = "+";
        }
    }
}