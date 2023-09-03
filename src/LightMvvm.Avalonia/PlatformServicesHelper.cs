using Avalonia.Controls;

namespace LightMvvm.Avalonia;

struct PlatformServicesHelper
{
    private AvaloniaTopLevelPlatformServices? _services;
    public void Reset()
    {
        _services = null;
    }

    public object? GetPlatformService(Control control, Type service)
    {
        if (service == typeof(IWindowManager))
            return AvaloniaDefaultWindowManager.Instance;
        
        if (service == typeof(IClipboardService))
        {
            if (_services != null)
                return _services;
            var tl = TopLevel.GetTopLevel(control);
            if (tl == null)
                throw new InvalidOperationException("The view is not attached to the visual tree");
            return _services = new AvaloniaTopLevelPlatformServices(tl);
        }
        return null;
    }
}