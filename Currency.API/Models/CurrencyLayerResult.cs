using System.Collections.Generic;

public class CurrencyLayerResult
{
    public bool Success { get; set; }
    public string Terms { get; set; }
    public string Privacy { get; set; }
    public long Timestamp { get; set; }
    public string Source { get; set; }
    public Dictionary<string, double> Quotes { get; set; }

    public CurrencyLayerError Error { get; set; }
}

public class CurrencyLayerError {
    public int Code { get; set; }
    public string Info { get; set; }
}