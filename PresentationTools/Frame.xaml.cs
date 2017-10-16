using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System;
using static System.Math;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using PresentationTools.Utils;

namespace PresentationTools
{
    public partial class Frame
    {
        private Point _staringPosition;
        private bool _wasReleased;

        public Frame()
        {
            InitializeComponent();
            AllowsTransparency = true;

            Mouse.OverrideCursor = Cursors.Cross;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton == MouseButton.Left)
                    DragMove();
            }
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
            catch
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
            {
                // ignored
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            var hwnd = new WindowInteropHelper(this).Handle;
            WinApi.HideFromAltTab(hwnd);
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            const int step = 10;
            var sign = e.Delta > 0 ? 1 : -1;

            Height += step * sign;
            Width += step * sign;
        }


        private void Root_MouseMove(object sender, MouseEventArgs e)
        {
            if (_wasReleased)
                return;

            var position = e.GetPosition(this);

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                var left = Min(_staringPosition.X, position.X);
                var top = Min(_staringPosition.Y, position.Y);

                var width = Max(Abs(_staringPosition.X - position.X), 20);
                var height = Max(Abs(_staringPosition.Y - position.Y), 20);

                Canvas.SetLeft(SelectionFrame, left);
                Canvas.SetTop(SelectionFrame, top);

                SelectionFrame.Width = width;
                SelectionFrame.Height = height;
            }

        }

        private void Root_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _staringPosition = e.GetPosition(this);

        }

        private void Root_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_wasReleased)
                return;

            if (Abs(SelectionFrame.Width) < 2.0 || 
                Abs(SelectionFrame.Height) < 2.0 ||
                double.IsNaN(SelectionFrame.Width) ||
                double.IsNaN(SelectionFrame.Height))
                Close();

            _wasReleased = true;

            var position = e.GetPosition(this);

            Mouse.OverrideCursor = null;

            var top = Canvas.GetTop(SelectionFrame);
            var left = Canvas.GetLeft(SelectionFrame);

            if (double.IsNaN(left) || (double.IsNaN(top)))
                return;


            GridPanel.ColumnDefinitions[0].Width = new GridLength(left);
            GridPanel.RowDefinitions[0].Height = new GridLength(top);

            GridPanel.ColumnDefinitions[1].Width = new GridLength(SelectionFrame.Width);
            GridPanel.RowDefinitions[1].Height = new GridLength(SelectionFrame.Height);

            GridPanel.Visibility = Visibility.Visible;
            CanvasArea.Visibility = Visibility.Collapsed;


            if (OwnedWindows.Count > 0 &&  OwnedWindows[0] != null)
            {
                Dispatcher.BeginInvoke((Action)(() => OwnedWindows[0].Topmost = false));
                Dispatcher.BeginInvoke((Action)(() => OwnedWindows[0].Topmost = true));
            }
        }

        private void LeftGridSplitter_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            var newWidth = GridPanel.ColumnDefinitions[1].Width.Value - e.HorizontalChange;
            if (newWidth <= 15)
            {

                GridPanel.ColumnDefinitions[0].Width = new GridLength(GridPanel.ColumnDefinitions[0].Width.Value - e.HorizontalChange);
                e.Handled = false;
                return;
            }

            GridPanel.ColumnDefinitions[1].Width = new GridLength(newWidth);
        }

        private void TopGridSplitter_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            var newWidth = GridPanel.RowDefinitions[1].Height.Value - e.VerticalChange;
            if (newWidth <= 15)
            {

                GridPanel.RowDefinitions[0].Height = new GridLength(GridPanel.RowDefinitions[0].Height.Value - e.VerticalChange);
                e.Handled = false;
                return;
            }

            GridPanel.RowDefinitions[1].Height = new GridLength(newWidth);
        }

    }
}