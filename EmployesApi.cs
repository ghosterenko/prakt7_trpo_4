using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Konkov_prakt7
{
    public class EmployesApi
    {
        private HttpClient _client = new();
        public EmployesApi()
        {
            _client.BaseAddress = new Uri("https://localhost:7114/");
        }

        public async Task<List<Employe>> GetEmployesAsync()
        {
            List<Employe>? response = await _client.GetFromJsonAsync<List<Employe>>("api/v1/Employees/");
            if (response == null)
            {
                return null!;
            }
            return response.ToList();
        }

    }
}
