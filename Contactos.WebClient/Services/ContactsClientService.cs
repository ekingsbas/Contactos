using Contactos.Shrd.DTO;
using Contactos.Shrd.DTO.API;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Contactos.WebClient.Services
{
    public class ContactsClientService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly JsonSerializerOptions _jsonOpts;

        private string clientName = string.Empty;
        public ContactsClientService(IHttpClientFactory client, IConfiguration configuration, IOptions<JsonSerializerOptions> jsonOptions) 
        {
            _config = configuration;
            _jsonOpts = jsonOptions.Value;
            _clientFactory = client;
            clientName = _config["Client:Name"];
            
        }

        public async Task<List<ContactDTO>> GetContacts()
        {
            var result = new List<ContactDTO>();
            try
            {
                
                using (var httpClient = _clientFactory.CreateClient(clientName))
                {
                    
                    var response = await httpClient.GetAsync("contactos");

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        
                        result = JsonSerializer.Deserialize<List<ContactDTO>>(jsonString, _jsonOpts);
                    }
                }

                return result;
                
            }
            catch(Exception)
            {
                throw;
            }
            
        }

        public async Task<ContactDTO> GetContact(int id)
        {
            var result = new ContactDTO();
            try
            {

                using (var httpClient = _clientFactory.CreateClient(clientName))
                {

                    var response = await httpClient.GetAsync($"contactos/detail/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();

                        result = JsonSerializer.Deserialize<ContactDTO>(jsonString, _jsonOpts);
                    }
                }

                return result;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<int?> AddContact(ContactDTO contact)
        {
            try
            {
                using (var httpClient = _clientFactory.CreateClient(clientName))
                {
                    var json = JsonSerializer.Serialize(contact);
                    var response = await httpClient.PostAsync($"contactos", new StringContent(json, UnicodeEncoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var savedContact = JsonSerializer.Deserialize<ContactDTO>(jsonString, _jsonOpts);
                        
                        return savedContact.Id;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResultDTO?> EditContact(ContactDTO contact)
        {
            
            var result = new ApiResultDTO();
            try
            {
                using (var httpClient = _clientFactory.CreateClient(clientName))
                {
                    var json = JsonSerializer.Serialize(contact);
                    var response = await httpClient.PutAsync($"contactos", new StringContent(json, UnicodeEncoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        result = JsonSerializer.Deserialize<ApiResultDTO>(jsonString, _jsonOpts);

                        return result;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResultDTO?> DeleteContact(int id)
        {
            var result = new ApiResultDTO();
            try
            {

                using (var httpClient = _clientFactory.CreateClient(clientName))
                {
                    var response = await httpClient.DeleteAsync($"contactos/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();

                        result = JsonSerializer.Deserialize<ApiResultDTO>(jsonString, _jsonOpts);
                    }
                }

                return result;

            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
