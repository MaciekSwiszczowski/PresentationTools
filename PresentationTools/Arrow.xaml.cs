using System.Windows.Input;

namespace PresentationTools
{
    public partial class Arrow 
    {
        public Arrow()
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
            Width += step * sign * 1.5;
        }
    }
}
