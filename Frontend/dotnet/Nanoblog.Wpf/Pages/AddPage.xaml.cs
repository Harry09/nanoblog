using Nanoblog.AppCore.Navigation;
using Nanoblog.AppCore.ViewModels.Pages;
using Nanoblog.Wpf.Controls.AppBar;
using System.Windows.Input;

namespace Nanoblog.Wpf.Pages
{
    /// <summary>
    /// Interaction logic for AddPage.xaml
    /// </summary>
    public partial class AddPage : AppBarPage, INavigable<AddPageViewModel>
    {
        public AddPage()
        {
            InitializeComponent();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && (e.KeyboardDevice.Modifiers & ModifierKeys.Control) != 0)
            {
                var vm = DataContext as AddPageViewModel;

                vm.Text = TextBox.Text;
                vm.AddCommand.Execute(null);
            }
        }
    }
}
