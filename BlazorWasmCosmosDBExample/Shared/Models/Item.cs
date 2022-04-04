namespace BlazorWasmCosmosDBExample.Shared.Models
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    public class Item
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Name is too long.")]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "isComplete")]
        public bool Completed { get; set; }
    }
}
