﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tema1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
        }

        private void AdminBtn(object sender, RoutedEventArgs e)
        {
            Main.Content = new Admin();
        }

        private void SearchBtn(object sender, RoutedEventArgs e)
        {
            Main.Content = new Search();
        }

        private void DivertismentBtn(object sender, RoutedEventArgs e)
        {
            Main.Content = new Divertisment();
        }
    }
}
