﻿using System.Net.Http.Json;
using MessageFlow.Frontend.Models.DTOs;
using MessageFlow.Frontend.Models.ViewModels;

namespace MessageFlow.Frontend.Services
{
    public class ChannelService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ChannelService> _logger;

        public ChannelService(HttpClient httpClient, ILogger<ChannelService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<FacebookSettingsDTO?> GetFacebookSettingsAsync(string companyId)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<FacebookSettingsDTO>($"api/channels/facebook/{companyId}");

                if (result == null || string.IsNullOrEmpty(result.Id))
                {
                    _logger.LogWarning("Invalid Facebook settings retrieved for company {CompanyId}. Generating new settings.", companyId);
                    return CreateNewSettings(companyId);
                }

                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex, "Facebook settings not found for company {CompanyId}", companyId);
                return null;
            }
        }

        public async Task<bool> SaveFacebookSettingsAsync(string companyId, FacebookSettingsDTO settings)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/channels/facebook/{companyId}", settings);
            return response.IsSuccessStatusCode;
        }

        public async Task<WhatsAppSettingsDTO?> GetWhatsAppSettingsAsync(string companyId)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<WhatsAppSettingsDTO>($"api/channels/whatsapp/{companyId}");

                if (result == null || string.IsNullOrEmpty(result.Id))
                {
                    _logger.LogWarning("Invalid Facebook settings retrieved for company {CompanyId}. Generating new settings.", companyId);
                    return CreateWhatsAppNewSettings(companyId);
                }

                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex, "Whatsapp settings not found for company {CompanyId}", companyId);
                return CreateWhatsAppNewSettings(companyId);
            }
        }

        public async Task<ApiNotificationResultVM> SaveWhatsCoreAppSettingsAsync(WhatsAppCoreSettingsDTO coreSettings)
        {
            var response = await _httpClient.PostAsJsonAsync("api/channels/whatsapp/settings", coreSettings);
            var message = await response.Content.ReadAsStringAsync();
            return new ApiNotificationResultVM
            {
                IsSuccess = response.IsSuccessStatusCode,
                Message = message
            };
        }

        public async Task<ApiNotificationResultVM> SavePhoneNumbersAsync(List<PhoneNumberInfoDTO> numbers)
        {
            var response = await _httpClient.PostAsJsonAsync("api/channels/whatsapp/numbers", numbers);


            var message = await response.Content.ReadAsStringAsync();

            return new ApiNotificationResultVM
            {
                IsSuccess = response.IsSuccessStatusCode,
                Message = message
            };
        }

        private FacebookSettingsDTO CreateNewSettings(string companyId)
        {
            return new FacebookSettingsDTO
            {
                Id = Guid.NewGuid().ToString(),
                CompanyId = companyId,
                PageId = string.Empty,
                AccessToken = string.Empty
            };
        }

        private WhatsAppSettingsDTO CreateWhatsAppNewSettings(string companyId)
        {
            return new WhatsAppSettingsDTO
            {
                Id = Guid.NewGuid().ToString(),
                CompanyId = companyId,
                BusinessAccountId = string.Empty,
                AccessToken = string.Empty
            };
        }
    }
}