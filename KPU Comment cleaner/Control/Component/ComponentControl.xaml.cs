using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace KPU_Comment_cleaner.Control.Component
{
    /// <summary>
    /// Interaction logic for CommentComponent.xaml
    /// </summary>
    public partial class ComponentControl : UserControl, IDisposable
    {
        public ComponentControl()
        {
            InitializeComponent();
        }

        public void Dispose()
        {
            BindingOperations.ClearAllBindings(this);
        }
    }
}
