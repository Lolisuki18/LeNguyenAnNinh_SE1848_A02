using BusinessObject;
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

namespace LeNguyenAnNinhWpfApp
{
    /// <summary>
    /// Interaction logic for ProductDialog.xaml
    /// </summary>
    public partial class ProductDialog : Window
    {
        public Product Product { get; private set; }

        public ProductDialog(Product? product = null)
        {
            InitializeComponent();
            Product = product ?? new Product();

            if (product != null)
            {
                txtProductName.Text = product.ProductName;
                txtSupplierId.Text = product.SupplierId?.ToString();
                txtCategoryId.Text = product.CategoryId?.ToString();
                txtQuantityPerUnit.Text = product.QuantityPerUnit;
                txtUnitPrice.Text = product.UnitPrice?.ToString();
                txtStock.Text = product.UnitsInStock?.ToString();
                txtOnOrder.Text = product.UnitsOnOrder?.ToString();
                txtReorderLevel.Text = product.ReorderLevel?.ToString();
                chkDiscontinued.IsChecked = product.Discontinued;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product.ProductName = txtProductName.Text;
                Product.SupplierId = int.TryParse(txtSupplierId.Text, out var sId) ? sId : null;
                Product.CategoryId = int.TryParse(txtCategoryId.Text, out var cId) ? cId : null;
                Product.QuantityPerUnit = string.IsNullOrWhiteSpace(txtQuantityPerUnit.Text) ? null : txtQuantityPerUnit.Text;
                Product.UnitPrice = decimal.TryParse(txtUnitPrice.Text, out var price) ? price : null;
                Product.UnitsInStock = int.TryParse(txtStock.Text, out var stock) ? stock : null;
                Product.UnitsOnOrder = int.TryParse(txtOnOrder.Text, out var onOrder) ? onOrder : null;
                Product.ReorderLevel = int.TryParse(txtReorderLevel.Text, out var reorder) ? reorder : null;
                Product.Discontinued = chkDiscontinued.IsChecked == true;

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi nhập dữ liệu: " + ex.Message);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
