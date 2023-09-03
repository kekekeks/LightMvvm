using Avalonia.Controls;

namespace LightMvvm.Avalonia;

class AvaloniaTopLevelPlatformServices : IClipboardService
{
    private readonly TopLevel _tl;

    public AvaloniaTopLevelPlatformServices(TopLevel tl)
    {
        _tl = tl;
    }

    public Task<string?> GetTextAsync() => _tl.Clipboard.GetTextAsync();

    public Task SetTextAsync(string text) => _tl.Clipboard.SetTextAsync(text);
}