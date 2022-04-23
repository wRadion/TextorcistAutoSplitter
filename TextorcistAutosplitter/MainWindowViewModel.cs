using PropertyChanged;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TextorcistAutosplitter
{
    [AddINotifyPropertyChangedInterface]
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<string> Bosses { get; set; }

        public string _keys;
        public string Keys
        {
            get => _keys;
            set
            {
                _keys = value.ToUpper();
            }
        }

        public MainWindowViewModel()
        {
            Bosses = new ObservableCollection<string>()
            {
                "Caesar", "Magda", "Matthew", "Enoch Varg", "Mother Superior", "Master", "Furius", "Cardinal", "Laurentius", "Lilith"
            };

            _keys = "ZQSD";
        }

        public void MoveUp(int index)
        {
            if (index > 0)
                Bosses.Move(index, index - 1);
        }

        public void MoveDown(int index)
        {
            if (index < Bosses.Count - 1)
                Bosses.Move(index, index + 1);
        }
    }
}
