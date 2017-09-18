using System;
using System.Windows;
using System.Windows.Input;

namespace PresentationTools
{
    public partial class MainWindow
    {
        private Frame _frame = null;
        private Arrow _arrow = null;

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
                _frame = new Frame();
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