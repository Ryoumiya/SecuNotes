using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace SecuNotesXam
{
    class NotedItem
    {
        public DateTime LastModifiedDate { get; set; }
        public string TitleText { get; set; }
        public string ContentText { get; set; }
        public string PesudoID { get; set; }


        public override string ToString()
        {
            return TitleText + "/:=:/" + ContentText;
        }

        public void PraseFromString(string input)
        {
            string[] items = input.Split(new string[] { "/:=:/" }, StringSplitOptions.None);
            TitleText = items[0];
            ContentText = items[1];
        }

        public string GetJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void PraseJSON(string input)
        {
            NotedItem temp = JsonConvert.DeserializeObject<NotedItem>(input);
            LastModifiedDate = temp.LastModifiedDate;
            TitleText = temp.TitleText;
            ContentText = temp.ContentText;

        }

    }
}
