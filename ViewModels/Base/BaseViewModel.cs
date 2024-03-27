using System.ComponentModel;
using PropertyChanged;

namespace WpfAppRohdeSchwarzTest
{
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e)=> { };
    }
}
