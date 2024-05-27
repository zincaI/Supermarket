using SupermarketFinalVersion.View.Cashier;
using SupermarketFinalVersion.ViewModel;
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
using SupermarketFinalVersion.View;
using SupermarketFinalVersion.View.Admin;

namespace SupermarketFinalVersion.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            (DataContext as UserVM).OnMoveToNextWindow += LogInView_OnMoveToNextWindow;

        }
        private void LogInView_OnMoveToNextWindow(object sender, bool isAdmin)
        {
            if (isAdmin)
            {
                AdminWindow adminView = new AdminWindow();
                adminView.ShowDialog();
            }
            else
            {
                CashierView cashierView = new CashierView();
                cashierView.ShowDialog();
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is UserVM viewModel)
            {
                viewModel.Password = ((PasswordBox)sender).SecurePassword.Copy();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Register register;
            register = new Register();
            register.Show();
            this.Close();
        }
    }
}

