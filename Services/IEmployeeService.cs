﻿using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IEmployeeService
    {
        public Employee? GetEmpByUsenameAndPassword(string useName, string password);
        public List<Employee> GetAllEmployees();
        public bool UpdateEmployee(Employee employee);
    }
}
