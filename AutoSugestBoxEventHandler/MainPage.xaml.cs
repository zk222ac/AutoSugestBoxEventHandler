using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;

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
    }

    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

}
