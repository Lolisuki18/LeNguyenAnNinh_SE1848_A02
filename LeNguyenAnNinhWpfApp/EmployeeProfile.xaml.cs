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
    /// Interaction logic for EmployeeProfile.xaml
    /// </summary>
    public partial class EmployeeProfile : UserControl
    {
        public EmployeeProfile(Employee employee)
        {
            InitializeComponent();

            if (employee == null)
            {
                MessageBox.Show("Employee data is not available.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            // Hiển thị dữ liệu
            txtEmployeeId.Text = employee.EmployeeId.ToString();
            txtEmployeeName.Text = employee.Name;
            txtUserName.Text = employee.UserName;
            txtJobTitle.Text = employee.JobTitle ?? "";
            dpBirthDate.SelectedDate = employee.BirthDate;
            dpHireDate.SelectedDate = employee.HireDate;
            txtAddress.Text = employee.Address ?? "";
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cusrentEmployee = new Employee();
                // Cập nhật dữ liệu
                cusrentEmployee.Name = txtEmployeeName.Text.Trim();
                cusrentEmployee.UserName = txtUserName.Text.Trim();
                cusrentEmployee.JobTitle = string.IsNullOrWhiteSpace(txtJobTitle.Text) ? null : txtJobTitle.Text.Trim();
                cusrentEmployee.BirthDate = dpBirthDate.SelectedDate;
                cusrentEmployee.HireDate = dpHireDate.SelectedDate;
                cusrentEmployee.Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim();

                EmployeeDAO.Instance.UpdateEmployee(cusrentEmployee); 

                MessageBox.Show("Đã cập nhập thành công", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhập thông tin người dùng\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

}
