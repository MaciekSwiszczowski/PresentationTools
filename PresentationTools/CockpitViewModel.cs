using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace PresentationTools
{
    public class CockpitViewModel
    {
        public ObservableCollection<CommandViewModel> Commands { get; } = new ObservableCollection<CommandViewModel>();

        public CockpitViewModel()
        {
            
        }

    }

    public class CommandViewModel : INotifyPropertyChanged
    {
        public ICommand Command { get; }
        public string Description { get; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
