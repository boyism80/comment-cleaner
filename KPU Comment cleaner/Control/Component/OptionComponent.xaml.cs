using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KPU_Comment_cleaner.Control.Component
{
    /// <summary>
    /// Interaction logic for OptionComponent.xaml
    /// </summary>
    public partial class OptionComponent : UserControl
    {
        public static readonly DependencyProperty TextdProperty = DependencyProperty.Register("Text", typeof(string), typeof(OptionComponent));
        public static readonly DependencyProperty ButtonTextdProperty = DependencyProperty.Register("ButtonText", typeof(string), typeof(OptionComponent));
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(OptionComponent));

        public string Text
        {
            get { return (string)GetValue(TextdProperty); }
            set { SetValue(TextdProperty, value); }
        }

        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextdProperty); }
            set { SetValue(ButtonTextdProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public OptionComponent()
        {
            InitializeComponent();
        }
    }
}
