using System;
using System.Collections.Generic;
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
        String NoteID;
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
        public NotesEdit(string noteID)
        {
            InitializeComponent();
            NoteID = noteID;
            EditingMode = true;
            PageTitleText.Text = "Editing...";
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

        private void MainConfigButton_Released(object sender, EventArgs e)
        {
            if (isButtonLongPressed)
            {
                //Long Press!

            }
            else
            {
                //Short Press!
                
            }

            //Reset The Button Conditions
            isButtonPressed = false;
            isButtonLongPressed = false;
            MainConfigButton.Text = "💾";
        }
    }
}