using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sagdieva_Matie
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SvernutBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
            "Свернуть окно?",
            "Подтверждение",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Window window = Window.GetWindow(this);
                if (window != null)
                {
                    window.WindowState = WindowState.Minimized;
                }
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
            "Закрыть приложение?",
            "Подтверждение закрытия",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);


            if (result == MessageBoxResult.Yes)
            {
                Window.GetWindow(this)?.Close();
                Application.Current.Shutdown();
            }
        }
    }
}
