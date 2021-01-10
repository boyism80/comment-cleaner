using KPU_Comment_cleaner.Command;
using KPU_Comment_cleaner.Model;
using SimpleJSON;
using System;
using System.Windows.Input;

namespace KPU_Comment_cleaner.ViewModel.Base
{
    public abstract class ComponentViewModel : BaseViewModel
    {
        public delegate void ResponseEventHandler(object sender, bool result, string message);

        public ConnectionViewModel Parent { get; private set; }
        public Component Model { get; private set; }

        public string Title { get { return this.Model.Title; } }
        public string Content { get { return this.Model.Content; } }
        public string URL { get { return this.Model.URL; } }
        public DateTime DateTime { get { return this.Model.DateTime; } }
        public abstract string AnotherButtonText { get; }

        public ICommand ShowCommand { get; private set; }
        public ICommand AnotherWorkCommand { get; private set; }

        protected ComponentViewModel(ConnectionViewModel owner, JSONNode json)
        {
            this.Parent = owner;
            this.Model = this.AllocateComponent(owner, json);

            this.ShowCommand = new RelayCommand(this.OnShowInBrowser);
            this.AnotherWorkCommand = new RelayCommand(this.OnAnotherWork);
        }

        protected abstract Component AllocateComponent(ConnectionViewModel owner, JSONNode json);

        protected abstract void OnAnotherWork(object obj);

        private void OnShowInBrowser(object obj)
        {
            System.Diagnostics.Process.Start(this.URL);
        }
    }
}
