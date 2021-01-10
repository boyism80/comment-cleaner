using KPU_Comment_cleaner.Control.Base;
using KPU_Comment_cleaner.Control.Component;
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
