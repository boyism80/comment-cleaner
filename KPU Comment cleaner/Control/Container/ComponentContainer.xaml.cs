using KPU_Comment_cleaner.Control.Base;
using KPU_Comment_cleaner.Control.Component;
using System.Windows;
using System.Windows.Controls;

namespace KPU_Comment_cleaner.Control.Container
{
    /// <summary>
    /// Interaction logic for CommentContainer.xaml
    /// </summary>
    public partial class ComponentContainer : ItemsSourceControl
    {
        public ComponentContainer()
        {
            InitializeComponent();
        }

        public override Panel GetContainer()
        {
            return this.Container;
        }

        public override FrameworkElement OnCreate(object context)
        {
            var component = new ComponentControl() { DataContext = context };
            return component;
        }

        public override void OnFinedDestroyedItem(FrameworkElement control, object context)
        {
            var component = control as ComponentControl;
            component.Dispose();

            base.OnFinedDestroyedItem(control, context);
        }
    }
}
