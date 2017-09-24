using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using static System.Math;

namespace PresentationTools
{
    public partial class Frame
    {
        private bool _wasMouseDown;
        private Point _topLeftCorner;
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
            catch
            {
                // ignored
            }
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
                var left = Min(_topLeftCorner.X, position.X);
                var top = Min(_topLeftCorner.Y, position.Y);

                var width = Max(Abs(_topLeftCorner.X - position.X), 20);
                var height = Max(Abs(_topLeftCorner.Y - position.Y), 20);

                Canvas.SetLeft(SelectionFrame, left);
                Canvas.SetTop(SelectionFrame, top);

                SelectionFrame.Width = width;
                SelectionFrame.Height = height;
            }

        }

        private void Root_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _wasMouseDown = true;

            _topLeftCorner = e.GetPosition(this);

        }

        private void Root_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _wasReleased = true;

            var position = e.GetPosition(this);

            Mouse.OverrideCursor = null;


            GridPanel.ColumnDefinitions[0].Width = new GridLength(Canvas.GetLeft(SelectionFrame));
            GridPanel.RowDefinitions[0].Height = new GridLength(Canvas.GetTop(SelectionFrame));

            GridPanel.ColumnDefinitions[1].Width = new GridLength(SelectionFrame.Width);
            GridPanel.RowDefinitions[1].Height = new GridLength(SelectionFrame.Height);

            GridPanel.Visibility = Visibility.Visible;
            CanvasArea.Visibility = Visibility.Collapsed;
        }
    }
}