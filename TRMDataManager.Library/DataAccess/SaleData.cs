﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public class SaleData : ISaleData
    {
        private readonly IProductData _productData;
        private readonly ISqlDataAccess _sql;
        private readonly IConfiguration _config;

        public SaleData(IProductData productData, ISqlDataAccess sql, IConfiguration config)
        {
            _productData = productData;
            _sql = sql;
            _config = config;
        }

        public decimal GetTaxRate()
        {
            string rateTax = _config.GetValue<string>("TaxRate");

            bool isValidTaxRate = Decimal.TryParse(rateTax, out decimal output);

            if (isValidTaxRate == false)
            {
                throw new ConfigurationErrorsException("The tax rate is not set up properly");
            }

            output =  output / 100;

            return output;
        }

        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            //TODO: Make this SOLID/DRY/Better
            // Start filling in the sales detail models we will save to the database
            // Fill in the available information
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            var taxRate = GetTaxRate();

            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                // Get the information about this product
                var productInfo = _productData.GetProductById(detail.ProductId);

                if (productInfo == null)
                {
                    throw new Exception($"The product Id of {detail.ProductId} could not be found in the database.");
                }

                detail.PurchasePrice = (productInfo.RetailPrice * detail.Quantity);

                if (productInfo.IsTaxable)
                {
                    detail.Tax = (detail.PurchasePrice * taxRate);
                }

                details.Add(detail);
            }
            // Create the Sale model
            SaleDBModel sale = new SaleDBModel
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId = cashierId
            };

            sale.Total = sale.SubTotal + sale.Tax;

            try
            {
                _sql.StartTransaction("TRMData");

                // Save the Sale model
                _sql.SaveDataInTransaction("spSale_Insert", sale);

                // Get the ID from the Sale model
                sale.Id = _sql.LoadDataInTransaction<int, dynamic>("spSale_Lookup", new { sale.CashierId, sale.SaleDate }).FirstOrDefault();

                // Finish filling in the Sale detail models
                foreach (var item in details)
                {
                    item.SaleId = sale.Id;
                    // Save the sale detail models
                    _sql.SaveDataInTransaction("spSaleDetail_Insert", item);
                }

                _sql.CommitTransaction();
            }
            catch
            {
                _sql.RollbackTransaction();
                throw;
            }

        }

        public List<SaleReportModel> GetSaleReport()
        {
            var output = _sql.LoadData<SaleReportModel, dynamic>("spSale_SaleReport", new { }, "TRMData");

            return output;
        }
    }
}
