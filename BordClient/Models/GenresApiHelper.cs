using System.Threading.Tasks;
using RestSharp;

namespace BordClient.Models
{
  class GenresApiHelper
  {
    public static async Task<string> GetAll()
    {
      RestClient client = new RestClient("http://localhost:5000/api");
      RestRequest request = new RestRequest($"genres", Method.GET);
      var response = await client.ExecuteTaskAsync(request);
      return response.Content;
    }

    public static async Task<string> Get(int id)
    {
      RestClient client = new RestClient("http://localhost:5000/api");
      RestRequest request = new RestRequest($"genres/{id}?api-version=2.0", Method.GET);
      var response = await client.ExecuteTaskAsync(request);
      return response.Content;
    }
    public static async Task Post(string newGenre)
    {
      RestClient client = new RestClient("http://localhost:5000/api");
      RestRequest request = new RestRequest($"genres", Method.POST);
      request.AddHeader("Content-Type", "application/json");
      request.AddJsonBody(newGenre);
      var response = await client.ExecuteTaskAsync(request);    
    }
    public static async Task Put(int id, string newGenre)
    {
      RestClient client = new RestClient("http://localhost:5000/api");
      RestRequest request = new RestRequest($"genres/{id}", Method.PUT);
      request.AddHeader("Content-Type", "application/json");
      request.AddJsonBody(newGenre);
      var response = await client.ExecuteTaskAsync(request);    
    }

    public static async Task Delete (int id)
    {
      RestClient client = new RestClient("http://localhost:5000/api");
      RestRequest request = new RestRequest($"genres/{id}", Method.DELETE);
      request.AddHeader("Content-Type", "application/json");
      var response  = await client.ExecuteTaskAsync(request);
    }
  }
}