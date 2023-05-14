using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NukeShop.BLL.Utils
{
    public static class Extensions
    {
        public static async Task<TBody> ReadFromJsonAsync<TBody>(this HttpResponseMessage response)
        {
            if (response.Content == null) return default;

            string content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TBody>(content);
        }
    }
}
