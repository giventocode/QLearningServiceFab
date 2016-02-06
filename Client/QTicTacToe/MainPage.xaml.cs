using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using QTicTacToe.Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace QTicTacToe
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private QTicTacToeViewModel _model = new QTicTacToeViewModel();
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = _model;
        }

        private async void Play_Click(object sender, RoutedEventArgs e)
        {
            var tag = ((FrameworkElement)sender).Tag.ToString();

            await _model.PlayAsync(int.Parse(tag));
       }
        private async void MachinePlayer1_Click(object sender, RoutedEventArgs e)
        {
            await _model.FirstPlayAsync();
        }


        private void Reset_Click(object sender, RoutedEventArgs e)
        {
           _model.Reset(((ContentControl)sender).Content.ToString());
        }
    }
}
