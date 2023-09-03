using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Microsoft.Extensions.DependencyInjection;

namespace LightMvvm.Avalonia;

public class ViewLocator : IDataTemplate
{
    private readonly Dictionary<Type,Func<Control>> _descriptors;

    public ViewLocator(IServiceProvider serviceProvider)
    {
        _descriptors = serviceProvider.GetServices<ViewLocatorDescriptor>()
            .ToDictionary(x => x.ViewModel, x => x.Factory);
    }
    
    public virtual Control? Build(object? param)
    {
        if (param == null)
            return null;
        return _descriptors[param.GetType()]();
    }

    public virtual bool Match(object? data) => data != null && _descriptors.ContainsKey(data.GetType());
}