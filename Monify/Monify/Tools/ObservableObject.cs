using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Monify.Tools
{
    abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void SetProperty<T>(ref T field, T value, [CallerMemberName] string prop = "")
        {
            field = value;
            OnPropertyChanged(prop);
        }
    }
}
