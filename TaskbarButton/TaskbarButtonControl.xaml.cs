using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CSDeskBand;

namespace TaskbarButton
{
    [ComVisible(true)]
    [Guid("c474b514-372c-44b3-aec2-b0aea87b99ea")]
    [CSDeskBandRegistration(Name = "AutoTaskbar", ShowDeskBand = true)]
    public partial class TaskbarButtonControl : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _buttonContent;
        public string ButtonContent
        {
            get => _buttonContent;
            set
            {
                if (value == _buttonContent) return;
                _buttonContent = value;
                OnPropertyChanged();
            }
        }

        private string _buttonColor;
        public string ButtonColor
        {
            get => _buttonColor;
            set
            {
                if (value == _buttonColor) return;
                _buttonColor = value;
                OnPropertyChanged();
            }
        }

        public TaskbarButtonControl()
        {
            InitializeComponent();

            Options.MinHorizontalSize.Width = 35;
            Options.MinHorizontalSize.Height = 25;
            Options.MinVerticalSize.Width = 35;
            Options.MinVerticalSize.Height = 25;

            if (TaskbarManager.GetTaskbarState() == TaskbarManager.AppBarStates.AlwaysOnTop)
            {
                ButtonContent = "OFF";
                ButtonColor = "#e84118";
            }
            else
            {
                ButtonContent = "ON";
                ButtonColor = "#27ae60";
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (TaskbarManager.GetTaskbarState() == TaskbarManager.AppBarStates.AlwaysOnTop)
            {
                TaskbarManager.SetTaskbarState(TaskbarManager.AppBarStates.AutoHide);
                ButtonContent = "ON";
                ButtonColor = "#27ae60";
                //KeyboardManager.WindowsKeyPress();
            }
            else
            {
                TaskbarManager.SetTaskbarState(TaskbarManager.AppBarStates.AlwaysOnTop);
                ButtonContent = "OFF";
                ButtonColor = "#e84118";
            }
        }
    }
}
