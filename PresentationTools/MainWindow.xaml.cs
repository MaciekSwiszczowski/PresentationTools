using System;
using System.Windows;
using System.Windows.Input;
using NHotkey;
using NHotkey.Wpf;
using WpfScreenHelper;
using Gma.System.MouseKeyHook;

namespace PresentationTools
{
    public partial class MainWindow
    {
        private Frame _frame;
        private Arrow _arrow;
        private Spot _spot;
        public MainWindow()

        {
            InitializeComponent();

            HotkeyManager.Current.AddOrReplace("ShowFrame", Key.F, ModifierKeys.Control | ModifierKeys.Alt, ShowFrame);
            HotkeyManager.Current.AddOrReplace("ShowArrow", Key.A, ModifierKeys.Control | ModifierKeys.Alt, ShowArrow);
            HotkeyManager.Current.AddOrReplace("ShowSpot", Key.S, ModifierKeys.Control | ModifierKeys.Alt, ShowSpot);

            var mGlobalHook = Hook.GlobalEvents();
            mGlobalHook.MouseDownExt += GlobalHookMouseDownExt;
        }

        private void ShowSpot(object sender, HotkeyEventArgs e)
        {
            ShowSpotToggleButton.IsChecked = !ShowSpotToggleButton.IsChecked;
            ShowSpot();
        }

        private void ShowSpot()
        {
            if (ShowSpotToggleButton.IsChecked == true)
            {
                _spot = new Spot();
                _spot.Show();
            }
            else
            {
                _spot.OnClose();
                _spot.Close();
            }

        }

        private void OnShowSpotClick(object sender, RoutedEventArgs e)
        {
            ShowSpot();
        }


        private void ShowArrow(object sender, HotkeyEventArgs e)
        {
            ShowArrowToggleButton.IsChecked = !ShowArrowToggleButton.IsChecked;
            ShowArrow();
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            var position = MouseHelper.MousePosition;

            var w = new Pointer
            {
                Width = 30,
                Height = 30
            };

            w.Left = position.X - w.Width / 2;
            w.Top = position.Y - w.Height / 2;
            w.Topmost = true;

            w.Show();
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
                // ignored
            }
        }

        private void OnShowArrowClick(object sender, RoutedEventArgs e)
        {
            ShowArrow();
        }

        private void OnShowFrameClick(object sender, RoutedEventArgs e)
        {
            ShowFrame();
        }

        private void ShowArrow()
        {
            if (ShowArrowToggleButton.IsChecked == true)
            {
                var position = MouseHelper.MousePosition;

                _arrow = new Arrow();

                _arrow.Left = position.X - _arrow.Width / 2;
                _arrow.Top = position.Y - _arrow.Height / 2;
                _arrow.Show();
            }
            else
            {
                _arrow?.Close();
                _arrow = null;
            }
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