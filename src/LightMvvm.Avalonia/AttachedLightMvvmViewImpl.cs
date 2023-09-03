using Avalonia;
using Avalonia.Controls;

namespace LightMvvm.Avalonia;

class AttachedLightMvvmViewImpl : IView
{
    private readonly Control _control;
    private IViewModel? _currentVm;
    private bool _closed;
    private PlatformServicesHelper _services;

    public static readonly AttachedProperty<AttachedLightMvvmViewImpl?> AttachedViewImplProperty =
        AvaloniaProperty.RegisterAttached<AttachedLightMvvmViewImpl, Control, AttachedLightMvvmViewImpl?>("AttachedViewImpl");

    public AttachedLightMvvmViewImpl(Control control)
    {
        _control = control;
        _currentVm = control.DataContext as IViewModel;
        if (_currentVm != null && TopLevel.GetTopLevel(control) != null)
            _currentVm.AddView(this);
        control.DataContextChanged += DataContextChanged;
        control.AttachedToVisualTree += OnDetachedFromVisualTree;
        control.AttachedToVisualTree += OnAttachedToVisualTree;
        if (control is WindowBase wb)
        {
            wb.Closed += OnClosed;
        }
    }

    private void OnClosed(object? sender, EventArgs e)
    {
        _closed = true;
        _currentVm?.RemoveView(this);
        _currentVm = null;
        _services.Reset();
    }

    private void OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        _currentVm = _control.DataContext as IViewModel;
        _currentVm?.AddView(this);
    }

    private void OnDetachedFromVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        _currentVm?.RemoveView(this);
        _currentVm = null;
        _services.Reset();
    }

    private void DataContextChanged(object? sender, EventArgs e)
    {
        if(TopLevel.GetTopLevel(_control) == null || _closed)
            return;
        _currentVm?.RemoveView(this);
        _currentVm = _control.DataContext as IViewModel;
        _currentVm?.AddView(this);
    }
    
    public object? GetPlatformService(Type service) => _services.GetPlatformService(_control, service);
}