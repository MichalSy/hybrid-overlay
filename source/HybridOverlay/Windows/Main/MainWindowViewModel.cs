using System.Windows.Media.Imaging;
using HybridOverlay.Manager.Config;
using HybridOverlay.Manager.Config.Models;
using HybridOverlay.Manager.Windows;
using HybridOverlay.UICommon;

namespace HybridOverlay.Windows.Main;
public class MainWindowViewModel : ViewModelBase
{
    private readonly IWindowsManager _windowsManager;
    private readonly IConfigManager _configManager;

    private AppConfig? _appConfig;

    #region Member - WindowTitle
    private string? _windowTitle;
    public string? WindowTitle
    {
        get => _windowTitle;
        set => SetProperty(ref _windowTitle, value);
    }
    #endregion

    #region Member - WindowWidth
    private int _windowWidth;
    public int WindowWidth
    {
        get => _windowWidth;
        set => SetProperty(ref _windowWidth, value);
    }
    #endregion

    #region Member - WindowHeight
    private int _windowHeight;
    public int WindowHeight
    {
        get => _windowHeight;
        set => SetProperty(ref _windowHeight, value);
    }
    #endregion

    #region Member - SourceUrl
    private string? _sourceUrl;
    public string? SourceUrl
    {
        get => _sourceUrl;
        set => SetProperty(ref _sourceUrl, value);
    } 
    #endregion

    public MainWindowViewModel(IWindowsManager windowsManager, IConfigManager configManager)
    {
        _windowsManager = windowsManager ?? throw new ArgumentNullException(nameof(windowsManager));
        _configManager = configManager ?? throw new ArgumentNullException(nameof(configManager));
    }

    public async void StartApplication(MainWindow mainWindow)
    {
        _appConfig = await _configManager.ReadConfigAsync();
        if (_appConfig is null)
        {
            Application.Current.Shutdown();
            return;
        }

        WindowTitle = _appConfig.Title;
        WindowWidth = _appConfig.Width;
        WindowHeight = _appConfig.Height;
        SourceUrl = _appConfig.SourceUrl;

        mainWindow.Icon = new BitmapImage(new Uri("AppIcon.png", UriKind.Relative));
        mainWindow.Deactivated += MainWindow_Deactivated;
        mainWindow.Activated += MainWindow_Activated;
        mainWindow.Closing += MainWindow_Closing;
        
        mainWindow.Show();

        _windowsManager.BringTaskbarsToFront();
    }

    private void MainWindow_Closing(object? sender, CancelEventArgs e)
    {
        if (_appConfig is not null)
        {
            e.Cancel = !_appConfig.CanClose;
        }
    }


    private void MainWindow_Activated(object? sender, EventArgs e)
    {
        if (sender is not Window window)
            return;

        var monitorInfo = _windowsManager.getScreenInformationFromMousePosition();
        if (monitorInfo is null)
            return;

        var workingRes = monitorInfo.Value.WorkArea;

        var top = workingRes.Bottom - _windowHeight - 20;

        var monitorWidth = workingRes.Right - workingRes.Left;
        var left = (workingRes.Left + (monitorWidth / 2) - (_windowWidth / 2));
        _windowsManager.SetWindowPlacemnet(window, left, workingRes.Bottom, _windowWidth, _windowHeight);


        window.Opacity = 0;
        window.BeginAnimation(Window.TopProperty, new DoubleAnimation
        {
            EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseIn },
            Duration = new Duration(TimeSpan.FromMilliseconds(200)),
            From = workingRes.Bottom,
            To = top,
        });
        window.BeginAnimation(UIElement.OpacityProperty, new DoubleAnimation
        {
            EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseIn },
            Duration = new Duration(TimeSpan.FromMilliseconds(200)),
            From = 0,
            To = 1,
        });
    }

    private async void MainWindow_Deactivated(object? sender, EventArgs e)
    {
        if (sender is not Window window)
            return;

        window.Topmost = true;

        _windowsManager.BringTaskbarsToFront();


        window.BeginAnimation(Window.TopProperty, new DoubleAnimation
        {
            EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseIn },
            Duration = new Duration(TimeSpan.FromMilliseconds(200)),
            From = window.Top,
            To = window.Top + _windowHeight + 20,
        });
        window.BeginAnimation(UIElement.OpacityProperty, new DoubleAnimation
        {
            EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseIn },
            Duration = new Duration(TimeSpan.FromMilliseconds(200)),
            From = 1,
            To = 0,
        });

        await Task.Delay(300);

        window.Topmost = false;
        window.WindowState = WindowState.Minimized;
    }
}
