using PresentationTools.Utils;
using System;
using System.Windows.Interop;
using System.Windows.Threading;

namespace PresentationTools
{
    public partial class Pointer
    {
        public Pointer()
        {
            InitializeComponent();

            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000)
            };

            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            var hwnd = new WindowInteropHelper(this).Handle;
            WinApi.SetWindowExTransparent(hwnd);
            //WinApi.HideFromAltTab(hwnd);
        }
    }
}