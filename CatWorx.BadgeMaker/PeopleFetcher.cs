using Newtonsoft.Json.Linq;

namespace CatWorx.BadgeMaker
{
    class PeopleFetcher
    {
        async public static Task<List<Employee>> GetFromApi()
        {
            List<Employee> employees = new List<Employee>();

            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync("https://randomuser.me/api/?results=10&nat=us&inc=name,id,picture");

                JObject json = JObject.Parse(response);


                foreach (JToken token in json.SelectToken("results")!)
                {
                    // Parse JSON data
                    Employee emp = new Employee
                    (
                      token.SelectToken("name.first")!.ToString(),
                      token.SelectToken("name.last")!.ToString(),
                      Int32.Parse(token.SelectToken("id.value")!.ToString().Replace("-", "")),
                      token.SelectToken("picture.large")!.ToString()
                    );
                    employees.Add(emp);
                }

            }
            return employees;
        }
    }
}