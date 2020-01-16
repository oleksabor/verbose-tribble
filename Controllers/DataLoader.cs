using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace miniprofiler.Controllers
{
    public class DataLoader
    {
        static HttpClient httpClient = new HttpClient();

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var taskRes = await GetResult<Employee[]>("http://dummy.restapiexample.com/api/v1/employees");
            return taskRes.Data;
        }

        async Task<RequestResult<T>> GetResult<T>(string uri)
        {
           var taskRes = await httpClient.GetAsync(uri);
            var content = await taskRes.Content.ReadAsStringAsync();
            
            if (taskRes.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<RequestResult<T>>(content);
            else 
                throw new ApplicationException($"errorCode:{taskRes.StatusCode} {content}");
        }
    }

    public class Employee
    {
        public int Id {get; set;}
        public string   Employee_name {get;set;}
        public decimal Employee_salary {get; set;} 
        public int Employee_age {get; set;}
    }

    public class RequestResult<T>
    {
        public string Status {get; set;}
        public T Data {get; set;}
    }
}