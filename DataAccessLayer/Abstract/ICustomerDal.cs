﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOsLayer.DTOs;

namespace DataAccessLayer.Abstract
{
    public interface ICustomerDal
    {
        Customer GetById(int id);  // ID ile müşteri al
        Task<List<Customer>> GetAll();
        Task<Customer> GetSingleCustomerOperationAsync();
        Task<int> GetCustomerStatisticsAsync();
        Task<bool> CustomerExistsAsync();
        Task<decimal?> GetValueAsync();

        Task<List<CustomerNameDto>> GetCustomerFullNamesAsync();
        Task<List<string?>> GetDistinctFirstNamesAsync();

        Task InsertAsync(Customer customer);

        void Update(Customer customer);

        void Delete(Customer customer);
    }
}
