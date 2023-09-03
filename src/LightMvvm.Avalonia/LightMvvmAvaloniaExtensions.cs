using System.Diagnostics.CodeAnalysis;
using Avalonia;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace LightMvvm.Avalonia;

public static class LightMvvmAvaloniaExtensions
{
    public static IServiceCollection AddLightMvvmAvalonia(this IServiceCollection services)
    {
        services.AddSingleton<ViewLocator>();
        services.AddSingleton<IWindowManager>(AvaloniaDefaultWindowManager.Instance);
        return services;
    }

    public static IServiceCollection AddView<TViewModel, TView>(this IServiceCollection services)
        where TView : Control, new() =>
        services.AddSingleton(new ViewLocatorDescriptor
        {
            ViewModel = typeof(TViewModel),
            Factory = () => new TView()
        });

    public static AppBuilder WithLightMvvm(this AppBuilder builder, Action<AvaloniaLightMvvmBuilder> cb) =>
        builder.AfterSetup(builder =>
        {
            var b = new AvaloniaLightMvvmBuilder(builder);
            cb(b);
            b.Setup();
        });
}