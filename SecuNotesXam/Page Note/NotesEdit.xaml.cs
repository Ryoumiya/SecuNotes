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
    public partial class NotesEdit : ContentPage
    {
        //Page Control
        bool EditingMode;
        NotedItem Memo;

        //Button Control
        bool isButtonPressed = false;
        bool isButtonLongPressed = false;

        //When we create a new Item
        public NotesEdit()
        {
            InitializeComponent();
            Memo = new NotedItem();
            EditingMode = false;
            PageTitleText.Text = "New Note!";
        }
        
        //When editing an existing Item
        public NotesEdit(int noteID)
        {
            InitializeComponent();
            EditingMode = true;
            PageTitleText.Text = "Editing...";
            GetNoteFromMemory(noteID);
            NoteTitle_Displayer.Text = Memo.TitleText;
            NoteContents_Displayer.Text = Memo.ContentText;
        }

        //for some reason this works ????
        private async void GetNoteFromMemory(int ID)
        {
            string NoteKey = string.Join("_", "SecureNoteKey", ID);
            string JSONNote = await SecureStorage.GetAsync(NoteKey);
            Memo = NotedItem.Static_PraseJSON(JSONNote);
        }

        private void MainConfigButton_Pressed(object sender, EventArgs e)
        {
            isButtonPressed = true;
            Device.StartTimer(TimeSpan.FromMilliseconds(400), () =>
            {
                if (isButtonPressed)
                {
                    isButtonLongPressed = true;
                    MainConfigButton.Text = "DELETE...!!!";
                }
                //To make sure the Timer only run once
                return false;
            });
        }

        private async void MainConfigButton_Released(object sender, EventArgs e)
        {
            if (isButtonLongPressed)
            {
                //Long Press!
                bool result = await DisplayAlert("Warning!", "Are you sure to delete note ?","Yes","Cancle");
                if (EditingMode && result)
                {
                    //Delete the Item for Real...
                    string NoteKey = string.Join("_", "SecureNoteKey", Memo.ID);
                    SecureStorage.Remove(NoteKey);
                }
            }
            else
            {
                //Short Press!
                if (EditingMode)
                {
                    EditExistingNote();
                }
                else
                {
                    CreateNewNote();
                }
            }

            //Reset The Button Conditions
            //isButtonPressed = false;
            //isButtonLongPressed = false;
            //MainConfigButton.Text = "💾";

            //Close the page...
            Navigation.PopModalAsync();
        }

        private async void CreateNewNote()
        {
            //Main Stuff
            Memo.LastModifiedDate = DateTime.Now;
            Memo.TitleText = NoteTitle_Displayer.Text;
            Memo.ContentText = NoteContents_Displayer.Text;

            //Now do the saving...
            string SAoN = await SecureStorage.GetAsync("NotesAmount_Key");
            int AmountOfNotes = Convert.ToInt32(SAoN);
            AmountOfNotes++;
            Memo.ID = AmountOfNotes;

            string NoteKey = string.Join("_", "SecureNoteKey", AmountOfNotes);

            SecureStorage.SetAsync(NoteKey, Memo.ToJSON());

            await SecureStorage.SetAsync("NotesAmount_Key", AmountOfNotes.ToString());
        }

        private async void EditExistingNote()
        {
            //Main Stuff
            Memo.LastModifiedDate = DateTime.Now;
            Memo.TitleText = NoteTitle_Displayer.Text;
            Memo.ContentText = NoteContents_Displayer.Text;

            //Now do the saving...
            string NoteKey = string.Join("_", "SecureNoteKey", Memo.ID);

            SecureStorage.SetAsync(NoteKey, Memo.ToJSON());
        }
    }
}