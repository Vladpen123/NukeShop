using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.DAL.Infrastructure.QueryParameters
{
    public class ManufacturerParameters : QueryStringParameters
    {
        public ManufacturerParameters()
        {
            OrderBy= "Name";
        }
    }
}
