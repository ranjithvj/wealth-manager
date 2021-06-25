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
    public class RecurringDepositAPIClient
    {
        private readonly HttpClient _httpClient;

        public RecurringDepositAPIClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RecurringDepositVM>> GetAllRecurringDeposits()
        {
            var response = await _httpClient.GetFromJsonAsync<List<RecurringDepositVM>>(Constants.RecurringDepositApi.GetAll);
            return response;
        }

        public async Task<RecurringDepositVM> GetRecurringDepositById(int Id)
        {
            var response = await _httpClient.GetFromJsonAsync<RecurringDepositVM>(string.Format(Constants.RecurringDepositApi.Get,Id));
            return response;
        }

        public async Task CreateRecurringDeposit(RecurringDepositVM vm)
        {
            var response = await _httpClient.PostAsJsonAsync<RecurringDepositVM>(Constants.RecurringDepositApi.Create, vm);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateRecurringDeposit(RecurringDepositVM vm)
        {
            var response = await _httpClient.PutAsJsonAsync<RecurringDepositVM>(string.Format(Constants.RecurringDepositApi.Update, vm.Id), vm);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteRecurringDeposit(int id)
        {
            var response = await _httpClient.DeleteAsync(string.Format(Constants.RecurringDepositApi.Delete, id));
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<BankVM>> GetAllBanks()
        {
            var response = await _httpClient.GetFromJsonAsync<List<BankVM>>(Constants.BankApi.GetAll);
            return response;
        }
    }
}
