using Newtonsoft.Json;

namespace Infrastructure.Repository.Common;

public abstract class BasePagination<T> {
    
    [JsonProperty("count_paginated")]
    public long ItemsPaginated { set; get; }
    
    [JsonProperty("data")]
    public List<T>? Data { set; get; }
    
}