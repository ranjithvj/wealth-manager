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
    public class FixedDepositAPIClient
    {
        private readonly HttpClient _httpClient;

        public FixedDepositAPIClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<FixedDepositVM>> GetAllFixedDeposits()
        {
            var response = await _httpClient.GetFromJsonAsync<List<FixedDepositVM>>(Constants.FixedDepositApi.GetAll);
            return response;
        }

        public async Task<FixedDepositVM> GetFixedDepositById(int Id)
        {
            var response = await _httpClient.GetFromJsonAsync<FixedDepositVM>(string.Format(Constants.FixedDepositApi.Get,Id));
            return response;
        }

        public async Task CreateFixedDeposit(FixedDepositVM vm)
        {
            var response = await _httpClient.PostAsJsonAsync<FixedDepositVM>(Constants.FixedDepositApi.Create, vm);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateFixedDeposit(FixedDepositVM vm)
        {
            var response = await _httpClient.PutAsJsonAsync<FixedDepositVM>(string.Format(Constants.FixedDepositApi.Update, vm.Id), vm);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteFixedDeposit(int id)
        {
            var response = await _httpClient.DeleteAsync(string.Format(Constants.FixedDepositApi.Delete, id));
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<BankVM>> GetAllBanks()
        {
            var response = await _httpClient.GetFromJsonAsync<List<BankVM>>(Constants.BankApi.GetAll);
            return response;
        }

        public async Task<List<FixedDepositTypeVM>> GetAllFixedDepositTypes()
        {
            var response = await _httpClient.GetFromJsonAsync<List<FixedDepositTypeVM>>(Constants.FixedDepositTypeApi.GetAll);
            return response;
        }
    }
}
