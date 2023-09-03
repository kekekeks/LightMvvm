namespace LightMvvm.CommunityToolkit;

public class ViewModelBase : global::CommunityToolkit.Mvvm.ComponentModel.ObservableObject, IViewModel
{
    private ViewModelHelper? _helper;
    private ViewModelHelper Helper => _helper ??= new();
    void IViewModel.AddView(IView view) => Helper.AddView(view);
    void IViewModel.RemoveView(IView view) => Helper.RemoveView(view);
    IReadOnlyList<IView> IViewModel.Views => Helper.Views;
}