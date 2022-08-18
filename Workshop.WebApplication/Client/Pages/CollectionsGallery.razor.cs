using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workshop.Shared.Models;

namespace Client.Pages
{
    public partial class CollectionsGallery
    {
        private List<ItemCollectionModel> Collections = new();
        private List<ItemModel> Items = new List<ItemModel>();
        [Inject] private HttpClient HttpClient { get; set; }
        [Inject] private IConfiguration Configuration { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var collectionResult = await HttpClient.GetAsync(Configuration["itemApiUrl"] + "/api/Collection");
            
            if (collectionResult.IsSuccessStatusCode)
            {
                Collections = await collectionResult.Content.ReadFromJsonAsync<List<ItemCollectionModel>>();
                foreach(var collection in Collections)
                {
                    var itemResult = await HttpClient.GetAsync(Configuration["itemApiUrl"] + "/api/Item/" + collection.CollectionId);
                    if (itemResult.IsSuccessStatusCode)
                        Items.AddRange(await itemResult.Content.ReadFromJsonAsync<List<ItemModel>>());
                }
            }
        }
    }
}
