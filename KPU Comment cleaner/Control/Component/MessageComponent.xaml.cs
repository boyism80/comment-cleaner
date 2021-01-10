using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace KPU_Comment_cleaner.Control.Component
{
    /// <summary>
    /// Interaction logic for MessageComponent.xaml
    /// </summary>
    public partial class MessageComponent : UserControl, IDisposable
    {
        public event EventHandler Remove;

        public MessageComponent()
        {
            InitializeComponent();
        }

        public void Dispose()
        {
            BindingOperations.ClearAllBindings(this);
        }

        private void RemoveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Remove?.Invoke(this.DataContext, EventArgs.Empty);
        }
    }
}
