using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AutoSugestBoxEventHandler
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
       private ObservableCollection<Patient> _listOfPatients = new ObservableCollection<Patient>();
       private ObservableCollection<Patient> _resultPatients = new ObservableCollection<Patient>();

        public MainPage()
        {
            this.InitializeComponent();
            GetPatientList();
            AutoSuggestBox.ItemsSource = _resultPatients;
        }

        public void GetPatientList()
        {
            _listOfPatients.Add(new Patient()
            {
                Id = 23,
                Name = "zuhair"
            });
            _listOfPatients.Add(new Patient()
            {
                Id = 23,
                Name = "funny"
            });


            for (int i = 0; i < 50; i++)
            {

                _listOfPatients.Add(new Patient()
                {
                    Id = i,
                    Name = "Patient Name:" + i
                });
            }
            
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
           var list = _listOfPatients.Where(a => (a.Name ?? "").ToLower().Contains(args.QueryText.ToLower()));
            _resultPatients.Clear();
            foreach (var item in list)
            {
                _resultPatients.Add(item);
            }
        }  

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem is Patient p)
                TxtInfo.Text = $"Patients Id:{p.Id} , Name:{p.Name}";
        }

       // private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
       //{
       //     if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
       //     {
       //        sender.ItemsSource = sender.Text.Length > 1 ? this.GetSuggestion(sender.Text) : new string[] {"No suggestion"};
       //     }
       // }
        //private string[] _suggestions = { "zoro", "zuhair" , "zanni" , "zada" , "zumba", "zaddi" , "aadi" , "ada" , "adu" , "assol"};
        //private string[] GetSuggestion(string text)
        //{
        //    string[] result = null;
        //    result = _suggestions.Where(x => x.StartsWith(text)).ToArray();
        //    return result;
        //}
    }

    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

}
