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
        List<NotedItem> ListofItems;

        public NotesDisplayer()
        {
            InitializeComponent();
        }
        
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await GetNotes();
        }

        private async Task<int> GetNotes()
        {
            ListofItems = new List<NotedItem>();

            //retives it then if null its converts it to 0
            string SAoN = await SecureStorage.GetAsync("NotesAmount_Key");
            int AmountOfNotes = Convert.ToInt32(SAoN);
            
            if (AmountOfNotes.Equals(0)){ 
                return 0;
            }
            string KeyNote = "SecureNoteKey";
            for(int i = 1; i <= AmountOfNotes; i++)
            {
                //Loop to each item and 
                string NoteJSON = await SecureStorage.GetAsync(string.Join("_", KeyNote, i));
                
                //if there is nothing there just skip it
                if (!Equals(NoteJSON,null)){
                    //if there is something there move it
                    ListofItems.Add(NotedItem.Static_PraseJSON(NoteJSON));
                }
                
            }

            NotesDisplay.ItemsSource = ListofItems;

            return 1;
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
                await Navigation.PushModalAsync(new ConfigPage(), true);
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

        private async void NotesDisplay_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            int SelectedID = ListofItems.ElementAt(e.ItemIndex).ID;
            await Navigation.PushModalAsync(new NotesEdit(SelectedID), true);
        }
    }
}