using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Media;
using CSDeskBand;
using Color = System.Windows.Media.Color;

namespace TaskbarButton
{
    [ComVisible(true)]
    [Guid("c474b514-372c-44b3-aec2-b0aea87b99ea")]
    [CSDeskBandRegistration(Name = "AutoTaskbar", ShowDeskBand = true)]
    public partial class TaskbarButtonControl : INotifyPropertyChanged
    {
        private const string OnColor = "#27ae60";
        private const string OffColor = "#e84118";

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

        private SolidColorBrush _taskbarcolor;
        public SolidColorBrush TaskbarColor
        {
            get => _taskbarcolor;
            set
            {
                if (value == _taskbarcolor) return;
                _taskbarcolor = value;
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

            var point = new Point(1, 1);
            switch (TaskbarInfo.Edge)
            {
                case Edge.Bottom:
                    point.Y = Screen.PrimaryScreen.Bounds.Height - 1;
                    break;
                case Edge.Right:
                    point.X = Screen.PrimaryScreen.Bounds.Width - 1;
                    break;
            }

            var tmp = TaskbarManager.GetPixelColor(point.X, point.Y);
            TaskbarColor = new SolidColorBrush(Color.FromArgb(tmp.A, tmp.R, tmp.G, tmp.B));
            if (TaskbarManager.GetTaskbarState() == TaskbarManager.AppBarStates.AlwaysOnTop)
            {
                ButtonContent = "OFF";
                ButtonColor = OffColor;
            }
            else
            {
                ButtonContent = "ON";
                ButtonColor = OnColor;
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (TaskbarManager.GetTaskbarState() == TaskbarManager.AppBarStates.AlwaysOnTop)
            {
                TaskbarManager.SetTaskbarState(TaskbarManager.AppBarStates.AutoHide);
                ButtonContent = "ON";
                ButtonColor = OnColor;
            }
            else
            {
                TaskbarManager.SetTaskbarState(TaskbarManager.AppBarStates.AlwaysOnTop);
                ButtonContent = "OFF";
                ButtonColor = OffColor;
            }
        }
    }
}
