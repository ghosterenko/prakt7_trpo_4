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
        private HttpResponseMessage response = new( );
        public EmployesApi()
        {
            _client.BaseAddress = new Uri("https://localhost:7114/");
        }

        public async Task<List<Employe>> GetEmployesAsync()
        {
            var responseList = await _client.GetFromJsonAsync<List<Employe>>("api/v1/Employees/");
            if (responseList == null)
            {
                return null!;
            }
            return responseList.ToList();
        }

        public async Task<List<Employe>> GetEmployesFilterAsync(string? pos, decimal? minSalary)
        {
            var employees = new List<string>();

            if (!string.IsNullOrWhiteSpace(pos))
                employees.Add($"pos={Uri.EscapeDataString(pos)}");

            if (minSalary.HasValue && minSalary.Value > 0)
                employees.Add($"minSalary={minSalary.Value.ToString()}");

            var list = await _client.GetFromJsonAsync<List<Employe>>("api/v1/Employees" + (employees.Count > 0 ? "?" + string.Join("&", employees) : ""));
            return list != null ? list : new List<Employe>();
        }


        public async Task<bool> PostEmploesAsync(Employe employe)
        {
            response = await _client.PostAsJsonAsync(
                "api/v1/Employees/",
                employe
            );
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<bool> PutEmployesAsync(int id, Employe employe)
        {
            response = await _client.PutAsJsonAsync(
                $"api/v1/Employees/{id}",
                employe
            );
            if (response.IsSuccessStatusCode)
                return true;
            return false;

        }
        public async Task<bool> PatchEmployesAsync(int id, EmployeeDto employeDto)
        {
            response = await _client.PatchAsJsonAsync(
                $"api/v1/Employees/{id}",
                employeDto
            );
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
        public async Task<bool> DeleteEmployesAsync(int id)
        {
            response = await _client.DeleteAsync(
                $"api/v1/Employees/{id}"    
            );

            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

    }
}
