using Avalonia;
using Avalonia.Controls;

namespace LightMvvm.Avalonia;

public class WindowViewBase : Window, IView
{
    private PlatformServicesHelper _services;
    object? IView.GetPlatformService(Type service) => LightMvvmGetPlatformService(service);

    protected virtual object? LightMvvmGetPlatformService(Type service) =>
        _services.GetPlatformService(this, service);

    protected override void OnClosed(EventArgs e)
    {
        (DataContext as IViewModel)?.RemoveView(this);
        _services.Reset();
        base.OnClosed(e);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        if (change.Property == DataContextProperty)
        {
            if (change.OldValue is IViewModel oldVm)
                oldVm.RemoveView(this);
            if (change.NewValue is IViewModel newVm)
                newVm.AddView(this);
        }
        base.OnPropertyChanged(change);
    }
}