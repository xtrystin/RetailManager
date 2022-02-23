using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace TRMDesktopUI.Library.Helpers
{
    // TODO: Move this from config to the API
    public class ConfigHelper : IConfigHelper
    {
        private readonly IConfiguration _config;

        public ConfigHelper(IConfiguration config)
        {
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

            output = output / 100;

            return output;
        }
    }
}
