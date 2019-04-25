using System.Windows;
using Safeguard.Common.Ui;

namespace Safeguard.Test.Ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SafeguardForm.DataContext = new AddinViewModel(new SafeguardTestImpl(), new ExcelTestImpl());
        }
    }
}
