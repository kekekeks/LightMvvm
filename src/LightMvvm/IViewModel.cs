namespace LightMvvm;

public interface IViewModel
{
    void AddView(IView view);
    void RemoveView(IView view);
    IReadOnlyList<IView> Views { get; }
}

public interface IView
{
    object? GetPlatformService(Type service);
}

internal class ViewModelHelper : IViewModel
{
    public void AddView(IView view)
    {
        
    }

    public void RemoveView(IView view)
    {
        
    }

    public IReadOnlyList<IView> Views { get; }
}