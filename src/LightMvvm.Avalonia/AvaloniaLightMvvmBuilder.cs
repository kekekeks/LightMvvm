using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.Extensions.DependencyInjection;

namespace LightMvvm.Avalonia;

public class AvaloniaLightMvvmBuilder
{
    private readonly AppBuilder _builder;
    private readonly ServiceCollection _services = new();
    private Type? _mainWindowViewModel;
    private Type? _mainViewViewModel;

    public string[]? CommandLineArguments { get; } 
    
    internal AvaloniaLightMvvmBuilder(AppBuilder builder)
    {
        _builder = builder;
        LightMvvmAvaloniaExtensions.AddLightMvvmAvalonia(_services);
        CommandLineArguments = builder.Instance!.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
            ? desktop.Args
            : null;
    }

    public AvaloniaLightMvvmBuilder ConfigureServices(Action<ServiceCollection> configure)
    {
        configure(_services);
        return this;
    }

    public AvaloniaLightMvvmBuilder WithMainWindow<TModel>()
    {
        _mainWindowViewModel = typeof(TModel);
        return this;
    }
    
    public AvaloniaLightMvvmBuilder WithMainView<TModel>()
    {
        _mainViewViewModel = typeof(TModel);
        return this;
    }

    internal void Setup()
    {
        var serviceProvider = _services.BuildServiceProvider();
        var viewLocator = serviceProvider.GetRequiredService<ViewLocator>();
        _builder.Instance!.DataTemplates.Insert(0, viewLocator);
        var lifetime = _builder.Instance.ApplicationLifetime;
        if (lifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainViewType = _mainWindowViewModel ?? _mainViewViewModel;
            if (mainViewType != null)
            {
                var wm = serviceProvider.GetService<IAvaloniaWindowManager>() ?? AvaloniaDefaultWindowManager.Instance;
                desktop.MainWindow =
                    wm.CreateWindow(serviceProvider.GetRequiredService(mainViewType));
            }
        }
        else if (lifetime is ISingleViewApplicationLifetime singleView)
        {
            if (_mainViewViewModel != null)
                singleView.MainView =
                    DataTemplateHelper.ResolveViewFromApp(serviceProvider.GetRequiredService(_mainViewViewModel));
        }
    }
}