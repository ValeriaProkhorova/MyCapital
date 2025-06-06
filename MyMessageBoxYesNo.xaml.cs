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
using System.Windows.Shapes;

namespace MyCapital
{
    /// <summary>
    /// Логика взаимодействия для MyMessageBoxYesNo.xaml
    /// </summary>
    public partial class MyMessageBoxYesNo : Window
    {
        public MyMessageBoxYesNo()
        {
            InitializeComponent();
        }

        public string Message
        {
            get { return MessageText.Text; }
            set { MessageText.Text = value; }
        }

        public string Title
        {
            get { return TitleText.Text; }
            set { TitleText.Text = value; }
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
