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

namespace WorksCheck.Resources.UserControls
{
    /// <summary>
    /// CustomTextBox.xaml の相互作用ロジック
    /// </summary>
    public partial class CustomTextBox : UserControl
    {
        public CustomTextBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string),typeof(CustomTextBox), new PropertyMetadata(string.Empty,null));

        public static readonly DependencyProperty AcceptsReturnProperty = DependencyProperty.Register(
    "AcceptsReturn", typeof(bool), typeof(CustomTextBox), new PropertyMetadata(false, null));

        public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register(
    "TextWrapping", typeof(TextWrapping), typeof(CustomTextBox), new PropertyMetadata(TextWrapping.NoWrap, null));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public bool AcceptsReturn
        {
            get { return(bool)GetValue(AcceptsReturnProperty); }
            set { SetValue(AcceptsReturnProperty, value); }
        }

        public TextWrapping TextWrapping
        {
            get { return (TextWrapping)GetValue(TextWrappingProperty); }
            set { SetValue(TextWrappingProperty, value);}
        }

    }
}
