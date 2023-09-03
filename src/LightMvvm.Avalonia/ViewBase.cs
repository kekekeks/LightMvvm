using Avalonia;
using Avalonia.Controls;

namespace LightMvvm.Avalonia;

public class ViewBase : UserControl, IView
{
    private PlatformServicesHelper _services;
    object? IView.GetPlatformService(Type service) => LightMvvmGetPlatformService(service);

    protected virtual object? LightMvvmGetPlatformService(Type service) =>
        _services.GetPlatformService(this, service);

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        if (DataContext is IViewModel vm)
            vm.AddView(this);
        
        base.OnAttachedToVisualTree(e);
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        _services.Reset();
        if(DataContext is IViewModel vm)
            vm.RemoveView(this);
        base.OnDetachedFromVisualTree(e);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        if (TopLevel.GetTopLevel(this) == null)
            return;
        
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