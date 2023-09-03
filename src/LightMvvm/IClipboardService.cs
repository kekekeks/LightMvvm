namespace LightMvvm;

public interface IClipboardService
{
    Task<string?> GetTextAsync();
    Task SetTextAsync(string text);
}