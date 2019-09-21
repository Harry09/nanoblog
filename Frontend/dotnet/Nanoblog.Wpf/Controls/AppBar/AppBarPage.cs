using System.Windows;
using System.Windows.Controls;

namespace Nanoblog.Wpf.Controls.AppBar
{
    public class AppBarPage : UserControl
    {
        public AppBar AppBarLeft
        {
            get { return (AppBar)GetValue(AppBarLeftProperty); }
            set { SetValue(AppBarLeftProperty, value); }
        }

        public static readonly DependencyProperty AppBarLeftProperty =
            DependencyProperty.Register("AppBarLeft", typeof(AppBar), typeof(AppBarPage), new PropertyMetadata(null));

        public AppBar AppBarRight
        {
            get { return (AppBar)GetValue(AppBarRightProperty); }
            set { SetValue(AppBarRightProperty, value); }
        }

        public static readonly DependencyProperty AppBarRightProperty =
            DependencyProperty.Register("AppBarRight", typeof(AppBar), typeof(AppBarPage), new PropertyMetadata(null));

        public Visibility AppBarVisibility
        {
            get { return (Visibility)GetValue(AppBarVisibilityProperty); }
            set { SetValue(AppBarVisibilityProperty, value); }
        }

        public static readonly DependencyProperty AppBarVisibilityProperty =
            DependencyProperty.Register("AppBarVisibility", typeof(Visibility), typeof(AppBarPage), new PropertyMetadata(Visibility.Collapsed));

        static AppBarPage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AppBarPage), new FrameworkPropertyMetadata(typeof(AppBarPage)));
        }
    }
}
