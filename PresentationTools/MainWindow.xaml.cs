using System;
using System.Windows;
using System.Windows.Input;
using NHotkey;
using NHotkey.Wpf;
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

            HotkeyManager.Current.AddOrReplace("Increment", Key.F, ModifierKeys.Control | ModifierKeys.Alt, ShowFrame);

            //HotkeyManager.HotkeyAlreadyRegistered
        }

        private void ShowFrame(object sender, HotkeyEventArgs e)
        {
            ShowFrameToggleButton.IsChecked = !ShowFrameToggleButton.IsChecked;

            ShowFrame();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton == MouseButton.Left)
                    DragMove();
            }
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
            catch (Exception)
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
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
            ShowFrame();
        }

        private void ShowFrame()
        {
            if (ShowFrameToggleButton.IsChecked == true)
            {
                var currentScreen = Screen.FromPoint(MouseHelper.MousePosition);

                _frame = new Frame
                {
                    Left = currentScreen.WorkingArea.Left,
                    Top = currentScreen.WorkingArea.Top,
                    Width = currentScreen.WorkingArea.Width,
                    Height = currentScreen.WorkingArea.Height,
                    Topmost = true
                };

                _frame.Show();

                //Owner = _frame;
            }
            else
            {
                Owner = null;
                _frame?.Close();
                _frame = null;
            }
        }
    }
}