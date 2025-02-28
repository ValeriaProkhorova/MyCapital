using FontAwesome.Sharp;
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

namespace MyCapital.User_Controls
{
    /// <summary>
    /// Логика взаимодействия для InfoCard.xaml
    /// </summary>
    public partial class InfoCard : UserControl
    {
        public InfoCard()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", 
            typeof(string), typeof(InfoCard));
        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register("Number", 
            typeof(int), typeof(InfoCard));

        public FontAwesome.Sharp.IconChar Icon
        {
            get { return (FontAwesome.Sharp.IconChar)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon",
           typeof(FontAwesome.Sharp.IconChar), typeof(InfoCard));

        public Color Background1
        {
            get { return (Color)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background",
            typeof(Color), typeof(InfoCard));

        public Color Background2
        {
            get { return (Color)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public static readonly DependencyProperty Background2Property = DependencyProperty.Register("Background2",
            typeof(Color), typeof(InfoCard));

        public Color EllipseBackground1
        {
            get { return (Color)GetValue(EllipseBackgroundProperty); }
            set { SetValue(EllipseBackgroundProperty, value); }
        }

        public static readonly DependencyProperty EllipseBackgroundProperty = DependencyProperty.Register("EllipseBackground1",
            typeof(Color), typeof(InfoCard));


        public Color EllipseBackground2
        {
            get { return (Color)GetValue(EllipseBackground2Property); }
            set { SetValue(EllipseBackground2Property, value); }
        }

        public static readonly DependencyProperty EllipseBackground2Property = DependencyProperty.Register("EllipseBackground2",
            typeof(Color), typeof(InfoCard));
    }
}
