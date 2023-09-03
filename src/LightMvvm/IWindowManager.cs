namespace LightMvvm;

public interface IWindowManager
{
    void Show(object model);
    Task<object?> ShowDialog(object model, IViewModel parent);
}