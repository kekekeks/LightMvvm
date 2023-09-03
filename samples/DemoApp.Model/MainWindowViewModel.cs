using CommunityToolkit.Mvvm.ComponentModel;
using LightMvvm.CommunityToolkit;

namespace DemoApp.Model;

public class MainWindowViewModel : ViewModelBase
{
    private readonly AppConfiguration _appConfig;
    public MainViewViewModel MainView { get; }
    public string Title => _appConfig.MainWindowTitle;

    public MainWindowViewModel(MainViewViewModel mainView, AppConfiguration appConfig)
    {
        _appConfig = appConfig;
        MainView = mainView;
    }
}

public class MainViewViewModel : ViewModelBase
{
    public AppConfiguration Config { get; }

    public MainViewViewModel(AppConfiguration config)
    {
        Config = config;
    }
}

public class DialogViewModel : ViewModelBase
{
    public string Text { get; set; }
}

public class AppConfiguration
{
    public string StringFromImportedService { get; set; }
    public string MainWindowTitle { get; set; }
}