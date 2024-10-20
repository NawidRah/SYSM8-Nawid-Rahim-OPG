﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitTrack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void UsernameInput_MouseEnter(object sender, MouseEventArgs e)
        {

            UsernameInputTEXT.Visibility = Visibility.Collapsed;


        }

        private void UsernameInput_MouseLeave(object sender, MouseEventArgs e)
        {
            UsernameInputTEXT.Visibility = Visibility.Visible;

        }

        private void UsernameInputTEXT_MouseEnter(object sender, MouseEventArgs e)
        {
            UsernameInputTEXT.Visibility = Visibility.Collapsed;

        }

        private void UsernameInputTEXT_MouseLeave(object sender, MouseEventArgs e)
        {
            UsernameInputTEXT.Visibility = Visibility.Visible;
        }

        private void UsernameInput_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (sender is TextBox UserName)
            {
                if (string.IsNullOrEmpty(UserName.Text))
                {
                    UsernameInputTEXT.Visibility = Visibility.Visible;
                }
                else
                {
                    UsernameInputTEXT.Visibility = Visibility.Collapsed;
                }
            }

        }

        private void PasswordInput_MouseEnter(object sender, MouseEventArgs e)
        {
            PasswordInputTEXT.Visibility = Visibility.Collapsed;

        }

        private void PasswordInput_MouseLeave(object sender, MouseEventArgs e)
        {
            PasswordInputTEXT.Visibility = Visibility.Visible;
        }

        private void PasswordInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox Pass)
            {
                if (string.IsNullOrEmpty(Pass.Password))
                {
                    PasswordInputTEXT.Visibility = Visibility.Visible;
                }
                else
                {
                    PasswordInputTEXT.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void PasswordInputTEXT_MouseEnter(object sender, MouseEventArgs e)
        {
            PasswordInputTEXT.Visibility = Visibility.Collapsed;

        }

        private void PasswordInputTEXT_MouseLeave(object sender, MouseEventArgs e)
        {
            
            PasswordInputTEXT.Visibility = Visibility.Visible;
        }
    }
}