using PropertyChanged;
using System;
using System.ComponentModel;

namespace KPU_Comment_cleaner.ViewModel.Base
{
    [ImplementPropertyChanged]
    public class BaseViewModel : INotifyPropertyChanged
    {
        public interface IValidAssertable
        {
            bool Valid();
        }

        public class ExceptableText<T> : IValidAssertable
        {
            public delegate void AssertEventHandler(T content);

            public event AssertEventHandler Assert;
            public event EventHandler Complete;

            private T _content;

            public T Content
            {
                get { return this._content; }
                set
                {
                    this._content = value;
                    try
                    {
                        this.Assert?.Invoke(this._content);
                        this.IsValid = true;
                    }
                    catch (Exception e)
                    {
                        this.IsValid = false;
                        this.ExceptionText = e.Message;
                    }
                    finally
                    {
                        this.Complete?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
            public bool IsValid { get; set; } = true;
            public string ExceptionText { get; set; } = string.Empty;

            public bool Valid()
            {
                return this.IsValid;
            }
        }

        /// <summary>
        /// The event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Call this to fire a <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
