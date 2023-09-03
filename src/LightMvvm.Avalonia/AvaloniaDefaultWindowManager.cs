using Avalonia;
using Avalonia.Controls;

namespace LightMvvm.Avalonia;

class AvaloniaDefaultWindowManager : IAvaloniaWindowManager
{
    public static AvaloniaDefaultWindowManager Instance { get; } = new();
    
    public void Show(object model) => CreateWindow(model).Show();

    public Task<object?> ShowDialog(object model, IViewModel parent)
    {
        var view = (ViewBase)LightMvvmExtensions.GetFirstView(parent);
        var window = TopLevel.GetTopLevel(view) as Window;
        if (window == null)
            throw new InvalidOperationException("Parent view isn't attached to a Window");
        return CreateWindow(model).ShowDialog<object?>(window);
    }

    public Window CreateWindow(object viewModel)
    {
        var view = DataTemplateHelper.ResolveViewFromApp(viewModel);
        if (view is Window window)
            return window;
        return new Window
        {
            Content = view,
            Title = viewModel.ToString()
        };
    }
}

public interface IAvaloniaWindowManager : IWindowManager
{
    Window CreateWindow(object viewModel);
}