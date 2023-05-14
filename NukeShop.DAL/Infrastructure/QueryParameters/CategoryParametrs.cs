using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.DAL.Infrastructure.QueryParameters
{
    public class CategoryParametrs : QueryStringParameters
    {
        public CategoryParametrs()
        {
            OrderBy= "Name";
        }
    }
}
