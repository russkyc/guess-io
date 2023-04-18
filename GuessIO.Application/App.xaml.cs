using System;
using org.russkyc.guessio.ViewModels;
using org.russkyc.guessio.Views;

namespace WordGuessApplication;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    /// <summary>
    /// Application Entry for GuessIO.Application
    /// </summary>
    public App()
    {
        InitializeComponent();
        
        var view = new GuessView
        {
            DataContext = Activator.CreateInstance<GuessViewModel>()
        };
        view.Show();
    }
}