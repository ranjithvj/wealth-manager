using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WealthManager.Client.Helpers;
using WealthManager.Client.Services.Contracts;
using WealthManager.Shared.ViewModels;

namespace WealthManager.Client.Services.Implementations
{
    public class MutualFundAPIClient
    {
        private readonly HttpClient _httpClient;

        public MutualFundAPIClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MutualFundVM>> GetAllMutualFunds()
        {
            var response = await _httpClient.GetFromJsonAsync<List<MutualFundVM>>(Constants.MutualFundApi.GetAll);
            return response;
        }

        public async Task<MutualFundVM> GetMutualFundById(int Id)
        {
            var response = await _httpClient.GetFromJsonAsync<MutualFundVM>(string.Format(Constants.MutualFundApi.Get,Id));
            return response;
        }

        public async Task ProcessCAS(MultipartFormDataContent content)
        {
            var response = await _httpClient.PostAsync(Constants.MutualFundApi.Create, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
