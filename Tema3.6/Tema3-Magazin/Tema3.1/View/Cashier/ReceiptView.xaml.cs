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
using System.Windows.Shapes;
using Tema3._1.ViewModel;

namespace Tema3._1.View.Cashier
{
    /// <summary>
    /// Interaction logic for ReceiptView.xaml
    /// </summary>
    public partial class ReceiptView : Window
    {
        public ReceiptView()
        {
            InitializeComponent();
        }
        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext is Receipt_ProductVM viewModel)
            {
                viewModel.CloseReceipt();
            }
        }
    }
}
