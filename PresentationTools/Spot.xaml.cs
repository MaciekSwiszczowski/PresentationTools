using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Gma.System.MouseKeyHook;

namespace PresentationTools
{
    public partial class Spot : INotifyPropertyChanged
    {
        private double _x;
        private double _y;
        private Matrix _matrix;
        private double _radiusX;
        private double _radiusY;
        private readonly IKeyboardMouseEvents _mGlobalHook;

        public Spot()
        {
            DataContext = this;
            InitializeComponent();

            SizeChanged += OnSizeChanged;

            _mGlobalHook = Hook.GlobalEvents();
            _mGlobalHook.MouseMove += OnMouseMove;

            var pos = Mouse.GetPosition(this);
            Matrix = new Matrix
            {
                OffsetX = pos.X - Width / 2,
                OffsetY = pos.Y - Height / 2
            };
        }

        public void OnClose()
        {
            SizeChanged -= OnSizeChanged;
            _mGlobalHook.MouseMove -= OnMouseMove;
            _mGlobalHook.Dispose();
        }

        private void OnMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Matrix = new Matrix
            {
                OffsetX = e.X - Width / 2,
                OffsetY = e.Y - Height / 2
            };
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Calculate();
        }


        public Matrix Matrix
        {
            get => _matrix;
            set
            {
                _matrix = value;
                OnPropertyChanged();
            }
        }

        public double RadiusX
        {
            get => _radiusX;
            set
            {
                _radiusX = value;
                OnPropertyChanged();
            }
        }
        public double RadiusY
        {
            get => _radiusY;
            set
            {
                _radiusY = value;
                OnPropertyChanged();
            }
        }
        public double X
        {
            get => _x;
            set
            {
                _x = value;
                OnPropertyChanged();

                if (_x < 0.1)
                    return;

                Calculate();
            }
        }

        private void Calculate()
        {
            RadiusX = 200 / Width;
            RadiusY = 200 / Height;
        }

        public double Y
        {
            get => _y;
            set
            {
                _y = value;
                OnPropertyChanged();
                if (_y < 0.1)
                    return;

                Calculate();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
