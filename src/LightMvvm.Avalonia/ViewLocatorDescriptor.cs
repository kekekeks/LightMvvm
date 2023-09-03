using Avalonia.Controls;

namespace LightMvvm.Avalonia;

class ViewLocatorDescriptor
{
    public Type ViewModel { get; set; }
    public Func<Control> Factory { get; set; }
}