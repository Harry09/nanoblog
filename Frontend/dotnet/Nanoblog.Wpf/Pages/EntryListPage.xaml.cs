using Nanoblog.Core.Navigation;
using Nanoblog.Core.ViewModels.Pages;
using Nanoblog.Wpf.Controls.AppBar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nanoblog.Wpf.Pages
{
    /// <summary>
    /// Interaction logic for EntryListPage.xaml
    /// </summary>
    public partial class EntryListPage : AppBarPage, INavigable<EntryListPageViewModel>
    {
        public EntryListPage()
        {
            InitializeComponent();
        }
    }
}
