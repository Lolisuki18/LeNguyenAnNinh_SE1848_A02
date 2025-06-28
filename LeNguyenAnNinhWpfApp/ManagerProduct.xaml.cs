using BusinessObject;

using DataAccessLayer;

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

namespace LeNguyenAnNinhWpfApp
{
    /// <summary>
    /// Interaction logic for ManagerProduct.xaml
    /// </summary>
    public partial class ManagerProduct : UserControl
    {
        public ManagerProduct()
        {
            InitializeComponent();
            LoadProductList();
        }
        private void LoadProductList()
        {
            var products = ProductDAO.Instance.GetAllProduct();
            dgProducts.ItemsSource = null;
            dgProducts.ItemsSource = products;
            //MessageBox.Show($"Đã load {products.Count} sản phẩm.");
        }
        private void dgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgProducts.SelectedItem is Product selected)
            {
                txtProductId.Text = selected.ProductId.ToString();
                txtProductName.Text = selected.ProductName;
                txtUnitPrice.Text = selected.UnitPrice?.ToString();
                txtStock.Text = selected.UnitsInStock?.ToString();
                txtOnOrder.Text = selected.UnitsOnOrder?.ToString();
                txtReorderLevel.Text = selected.ReorderLevel?.ToString();
                txtCategoryId.Text = selected.CategoryId?.ToString();
                txtSupplierId.Text = selected.SupplierId?.ToString();
                txtQuantityPerUnit.Text = selected.QuantityPerUnit;
                chkDiscontinued.IsChecked = selected.Discontinued;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ProductDialog(); // form nhập thông tin
            if (dialog.ShowDialog() == true)
            {
                bool added = ProductDAO.Instance.AddProduct(dialog.Product);
                if (added)
                {
                    MessageBox.Show("Thêm sản phẩm thành công!");
                    LoadProductList();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại. Vui lòng kiểm tra dữ liệu (SupplierId, CategoryId,...)");
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgProducts.SelectedItem is not Product selected)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để sửa.");
                return;
            }

            var dialog = new ProductDialog(new Product
            {
                ProductId = selected.ProductId,
                ProductName = selected.ProductName,
                SupplierId = selected.SupplierId,
                CategoryId = selected.CategoryId,
                QuantityPerUnit = selected.QuantityPerUnit,
                UnitPrice = selected.UnitPrice,
                UnitsInStock = selected.UnitsInStock,
                UnitsOnOrder = selected.UnitsOnOrder,
                ReorderLevel = selected.ReorderLevel,
                Discontinued = selected.Discontinued
            });

            if (dialog.ShowDialog() == true)
            {
                var updatedProduct = dialog.Product;
                updatedProduct.ProductId = selected.ProductId; // giữ nguyên ID

                bool updated = ProductDAO.Instance.UpdateProduct(updatedProduct);
                if (updated)
                {
                    MessageBox.Show("Cập nhật thành công.");
                    LoadProductList();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại. Kiểm tra dữ liệu.");
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xoá sản phẩm này?", "Xác nhận xoá", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    int id = int.Parse(txtProductId.Text);
                    var product = ProductDAO.Instance.GetProductById(id);
                    if (product != null)
                    {
                        bool delete = ProductDAO.Instance.DeleteProduct(product);
                        if (!delete)
                        {
                            MessageBox.Show("Xoá sản phẩm thất bại.");
                        }
                        LoadProductList();
                        MessageBox.Show("Xoá sản phẩm thành công.");
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xoá sản phẩm: " + ex.Message);
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
             try
            {
                string keyword = txtSearchProduct.Text.Trim();

                if (string.IsNullOrEmpty(keyword))
                {
                    LoadProductList();
                    return;
                }

                List<Product> results = new List<Product>();

                if (int.TryParse(keyword, out int id))
                {
                    var product = ProductDAO.Instance.GetProductById(id);
                    if (product != null)
                        results.Add(product);
                }
                //else if (keyword.ToLower() == "discontinued")
                //{
                //    var discontinuedProducts = ProductDAO.Instance.GetDiscontinuedProducts();
                //    if (discontinuedProducts != null)
                //        results.AddRange(discontinuedProducts);
                //}
                else
                {
                    var nameMatches = ProductDAO.Instance.SearchByName(keyword);
                    if (nameMatches != null)
                        results.AddRange(nameMatches);
                }

                dgProducts.ItemsSource = results;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm sản phẩm: " + ex.Message);
            }
        }
    }
}
