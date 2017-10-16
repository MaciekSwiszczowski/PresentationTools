using System.Windows;
using Gma.System.MouseKeyHook;

namespace PresentationTools
{
    public partial class App
    {
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            Hook.GlobalEvents().Dispose();
        }
    }
}