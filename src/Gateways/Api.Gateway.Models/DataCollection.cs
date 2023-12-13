﻿namespace Api.Gateway.Models;

public class DataCollection<T>
{
    public bool HasItems { get; set; }
    public IEnumerable<T> Items { get; set; }=new List<T>();
    public int Total { get; set; }
    public int Page { get; set; }
    public int Pages { get; set; }
}
