using KPU_Comment_cleaner.Control.Base;
using KPU_Comment_cleaner.Control.Component;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KPU_Comment_cleaner.Control.Container
{
    /// <summary>
    /// Interaction logic for WriteMessageContainer.xaml
    /// </summary>
    public partial class WriteMessageContainer : ItemsSourceControl
    {
        public static readonly DependencyProperty RemoveCommandProperty = DependencyProperty.Register("RemoveCommand", typeof(ICommand), typeof(WriteMessageContainer));

        public ICommand RemoveCommand
        {
            get { return (ICommand)GetValue(RemoveCommandProperty); }
            set { SetValue(RemoveCommandProperty, value); }
        }

        public WriteMessageContainer()
        {
            InitializeComponent();
        }

        public override Panel GetContainer()
        {
            return this.Container;
        }

        public override FrameworkElement OnCreate(object context)
        {
            var control = new MessageComponent() { DataContext = context };
            control.Remove += this.Component_Remove;
            return control;
        }

        private void Component_Remove(object sender, EventArgs e)
        {
            if(this.RemoveCommand.CanExecute(sender))
                this.RemoveCommand.Execute(sender);
        }

        public override void OnFinedDestroyedItem(FrameworkElement control, object context)
        {
            var component = control as MessageComponent;
            component.Dispose();
            component.Remove -= this.Component_Remove;
            base.OnFinedDestroyedItem(control, context);
        }
    }
}
