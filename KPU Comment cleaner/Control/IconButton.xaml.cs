using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace KPU_Comment_cleaner.Control
{
    /// <summary>
    /// Interaction logic for IconButton.xaml
    /// </summary>
    public partial class IconButton : UserControl
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(IconButton));

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(IconButton), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty IconWidthProperty = DependencyProperty.Register("IconWidth", typeof(int), typeof(IconButton), new FrameworkPropertyMetadata(10));
        public static readonly DependencyProperty IconHeightProperty = DependencyProperty.Register("IconHeight", typeof(int), typeof(IconButton), new FrameworkPropertyMetadata(10));
        public static readonly DependencyProperty HoverHackgroundProperty = DependencyProperty.Register("HoverBackground", typeof(SolidColorBrush), typeof(IconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public string Icon
        {
            get { return GetValue(IconProperty).ToString(); }
            set { SetValue(IconProperty, value); }
        }

        public int Image
        {
            get { return (int)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        public SolidColorBrush HoverBackground
        {
            get { return (SolidColorBrush)GetValue(HoverHackgroundProperty); }
            set { SetValue(HoverHackgroundProperty, value); }
        }

        public int Text
        {
            get { return (int)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public event RoutedEventHandler Click
        {
            add { this.button.AddHandler(ButtonBase.ClickEvent, value); }
            remove { this.button.RemoveHandler(ButtonBase.ClickEvent, value); }
        }

        public IconButton()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
