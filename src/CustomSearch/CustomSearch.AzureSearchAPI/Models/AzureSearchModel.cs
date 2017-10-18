using System;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Spatial;
using Newtonsoft.Json;

[SerializePropertyNamesAsCamelCase]
public partial class AzureSearchModel
{
    [System.ComponentModel.DataAnnotations.Key]
    [IsFilterable]
    public string listingId { get; set; }

    [IsSearchable]
    public string Title { get; set; }

    [IsSearchable]
    public string Description { get; set; }

    [IsSearchable, IsFilterable, IsSortable, IsFacetable]
    public string Category { get; set; }
    
    public string Link { get; set; }
}