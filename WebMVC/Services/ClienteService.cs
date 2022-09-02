
using System.Net;
using System.Threading.Tasks;
using System;
using WebMVC.Models;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace WebMVC.Services
{
    public class ClienteService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializer _jsonSerializer;
        public const string BasePath = "http://localhost:51456/api/cliente";

        public ClienteService()
        {
            _client = new HttpClient();
            _jsonSerializer = new JsonSerializer();
        }

        public async Task CreateClient(Cliente model)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            _jsonSerializer.Serialize(new JsonTextWriter(sw), model);

            StringContent queryString = new StringContent(sb.ToString(), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(BasePath + "/Criar", queryString);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Algum erro aconteceu ao chamar a api");
        }

        public async Task DeleteClientById(long id)
        {
            var response = await _client.DeleteAsync($"{BasePath}/Deletar/{id}");
            if (!response.IsSuccessStatusCode)
                throw new Exception("Algum erro aconteceu ao chamar a api");
        }

        public async Task<IEnumerable<Cliente>> FindAllClients()
        {
            var response = await _client.GetAsync(BasePath + "/ObterTodos");
            System.IO.Stream contentStream = await response.Content.ReadAsStreamAsync();


            var streamReader = new StreamReader(contentStream);
            var jsonReader = new JsonTextReader(streamReader);

            JsonSerializer serializer = new JsonSerializer();

            try
            {
                return serializer.Deserialize<List<Cliente>>(jsonReader);
            }
            catch (JsonReaderException)
            {
                Console.WriteLine("Invalid JSON.");
            }

            return null;
        }

        public async Task<Cliente> FindClientById(long id)
        {
            var response = await _client.GetAsync($"{BasePath}/Procurar/" + id);
            System.IO.Stream contentStream = await response.Content.ReadAsStreamAsync();


            var streamReader = new StreamReader(contentStream);
            var jsonReader = new JsonTextReader(streamReader);

            JsonSerializer serializer = new JsonSerializer();

            try
            {
                return serializer.Deserialize<Cliente>(jsonReader);
            }
            catch (JsonReaderException)
            {
                Console.WriteLine("Invalid JSON.");
            }

            return null;
        }

        public async Task UpdateClient(Cliente model)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            _jsonSerializer.Serialize(new JsonTextWriter(sw), model);

            StringContent queryString = new StringContent(sb.ToString(), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(BasePath + "/Editar", queryString);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong when calling API");
        }
    }
}
