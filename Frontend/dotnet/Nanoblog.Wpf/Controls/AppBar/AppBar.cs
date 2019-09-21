using System.Windows.Controls;

namespace Nanoblog.Wpf.Controls.AppBar
{
    public class AppBar : StackPanel
    {
        public AppBar()
        {
            SetValue(OrientationProperty, Orientation.Horizontal);
        }
    }
}
