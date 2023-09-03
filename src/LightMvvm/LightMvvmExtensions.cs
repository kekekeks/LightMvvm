namespace LightMvvm;

public static class LightMvvmExtensions
{
    internal static IView GetFirstView(this IViewModel vm) =>
        vm.Views.FirstOrDefault() ??
        throw new InvalidOperationException(
            "The view model isn't attached to any view");
    
    public static IWindowManager GetWindowManager(this IViewModel vm) => vm.GetRequiredService<IWindowManager>();
    public static IClipboardService GetClipboard(this IViewModel vm) => vm.GetRequiredService<IClipboardService>();

    private static T GetRequiredService<T>(this IViewModel vm) => vm.GetFirstView().GetRequiredService<T>();
    private static T GetRequiredService<T>(this IView view) => (T)view.GetPlatformService(typeof(T)) ??
                                                               throw new InvalidOperationException(
                                                                   $"The view doesn't provide {typeof(T)} service");
    
    
}