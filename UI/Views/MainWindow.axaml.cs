using Avalonia.Controls;
using Avalonia.Interactivity;
using UI.ViewModels;

namespace UI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        ((MainWindowViewModel)DataContext!).HandleButtonClick(MainCanvas);
    }
}