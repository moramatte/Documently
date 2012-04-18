using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Documently.WpfClient.Shared {
    /// <summary>
    /// Interaction logic for FlippingKey.xaml
    /// </summary>
    public partial class FlippingKey : UserControl {
        public FlippingKey() {
            InitializeComponent();
            DataContext = this;
        }
       
        public string Text {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(FlippingKey), new PropertyMetadata("*"));

    }
}
