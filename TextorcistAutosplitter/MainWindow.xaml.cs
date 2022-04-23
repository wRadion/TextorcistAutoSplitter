using System.Threading.Tasks;
using System.Windows;

namespace TextorcistAutosplitter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LiveSplitClient? _livesplit;
        private readonly GlobalKeyboardHook _keyboardHook;
        private readonly MainWindowViewModel _viewModel;
        private readonly TextTypingEngine _engine;

        public MainWindow()
        {
            InitializeComponent();

            _keyboardHook = new GlobalKeyboardHook();
            _keyboardHook.KeyboardPressed += KeyboardHook_KeyboardPressed;
            _viewModel = new MainWindowViewModel();
            _engine = new TextTypingEngine(_viewModel);

            DataContext = _viewModel;
            Closed += MainWindow_Closed;
            _engine.TypedAmen += () => _livesplit?.Split();
        }

        private void MainWindow_Closed(object? sender, System.EventArgs e)
        {
            _livesplit?.Dispose();
            _keyboardHook.Dispose();
        }

        private void KeyboardHook_KeyboardPressed(object? sender, GlobalKeyboardHookEventArgs e)
        {
            int code = e.KeyboardData.VirtualCode;
            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
            {
                if (code == GlobalKeyboardHook.VkLShift || code == GlobalKeyboardHook.VkRShift)
                    _engine.IsMoving = true;

                if ('A' <= code && code <= 'Z')
                    _engine.OnLetterPressed((char)code);
            }
            else if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp)
            {
                if (code == GlobalKeyboardHook.VkLShift || code == GlobalKeyboardHook.VkRShift)
                    _engine.IsMoving = false;
            }
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_livesplit == null || !_livesplit.Connected)
            {
                startBtn.Content = "Connecting...";
                Task.Run(() =>
                {
                    try
                    {
                        _livesplit = new LiveSplitClient();
                    }
                    catch
                    {
                        MessageBox.Show("Could not connect to LiveSplit. Make sure the LiveSplit server is running", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Dispatcher.Invoke(() => startBtn.Content = "Start");
                        return;
                    }

                    if (_livesplit != null && _livesplit.Connected)
                        Dispatcher.Invoke(() => startBtn.Content = "Stop");
                });
            }
            else
            {
                _livesplit?.Dispose();
                startBtn.Content = "Start";
            }
        }

        /*private void MoveUpBtn_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MoveUp(bossList.SelectedIndex);
        }

        private void MoveDownBtn_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MoveDown(bossList.SelectedIndex);
        }*/
    }
}
