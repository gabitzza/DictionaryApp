using System;
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
using System.IO;

namespace Tema1
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        private string[] lines;
        public Admin()
        {
            InitializeComponent();
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
            {
                textEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                textEmail.Visibility = Visibility.Visible;
            }
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        { 
            txtPassword.Focus();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtPassword.Password)&& txtPassword.Password.Length>0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }
        private bool isAuthenticated = false;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lines = File.ReadAllLines("logininfo.txt");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Fișierul de login lipsește.");
                return;
            }
            if (lines.Length >= 2 && txtEmail.Text == lines[0] && txtPassword.Password == lines[1])
            {
                MessageBox.Show("Conectare reușită");
                isAuthenticated = true;
                NavigationService.Navigate(new Uri("Settings.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Email sau parola incorecte");
            }
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton==MouseButton.Left)
            {
                Window.GetWindow(this).DragMove();
            }
        }
    }
}
