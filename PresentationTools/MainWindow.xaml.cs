using System;
using System.Windows;
using System.Windows.Input;
using WpfScreenHelper;

namespace PresentationTools
{
    public partial class MainWindow
    {
        private Frame _frame;
        private Arrow _arrow;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton == MouseButton.Left)
                    DragMove();
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
        }

        private void OnShowArrowClick(object sender, RoutedEventArgs e)
        {
            if (ShowArrowToggleButton.IsChecked == true)
            {
                _arrow = new Arrow();
                _arrow.Show();
            }
            else
            {
                _arrow?.Close();
                _arrow = null;
            }
        }

        private void OnShowFrameClick(object sender, RoutedEventArgs e)
        {
            if (ShowFrameToggleButton.IsChecked == true)
            {
                var currentScreen = Screen.FromPoint(MouseHelper.MousePosition);

                _frame = new Frame
                {
                    Left = currentScreen.WorkingArea.Left,
                    Top = currentScreen.WorkingArea.Top,
                    Width = currentScreen.WorkingArea.Width,
                    Height = currentScreen.WorkingArea.Height
                };

                _frame.Show();
            }
            else
            {
                _frame?.Close();
                _frame = null;
            }
        }
    }
}