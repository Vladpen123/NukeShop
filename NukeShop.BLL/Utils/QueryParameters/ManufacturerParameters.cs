using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.BLL.Utils.QueryParameters 
{ 
    public class ManufacturerParameters : QueryStringParameters
    {
        public ManufacturerParameters()
        {
            OrderBy= "Name";
        }
    }
}
