using Client.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workshop.Shared.Models;

namespace Client.Pages
{
    public partial class ItemsGallery
    {
        private List<ItemModel> Items = new();
        [Inject] private HttpClient HttpClient { get; set; }
        [Inject] private IConfiguration Configuration { get; set; }
        [Inject] private ITokenService TokenService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var tokenResponse = await TokenService.GetToken("Items");
            HttpClient.SetBearerToken(tokenResponse.AccessToken);
            var result = await HttpClient.GetAsync(Configuration["itemApiUrl"] + "/api/Item");

            if (result.IsSuccessStatusCode)
            {
                Items = await result.Content.ReadFromJsonAsync<List<ItemModel>>();
            }
        }
    }
}
