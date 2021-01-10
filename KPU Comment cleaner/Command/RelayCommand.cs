using System;
using System.Windows.Input;

namespace KPU_Comment_cleaner.Command
{
    public class RelayCommand<T> : ICommand
        where T : new()
    {
        #region Private members
        /// <summary>
        /// 실행할 액션
        /// </summary>
        private Action<T> _action;
        #endregion

        #region Public events
        /// <summary>
        /// 이 이벤트는 <see cref="CanExecute(object)"/> 값이 변경될 때 발생합니다.
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        #endregion

        #region Constructor
        public RelayCommand(Action<T> action)
        {
            this._action = action;
        }
        #endregion

        #region Command methods

        /// <summary>
        /// 릴레이 명령은 항상 실행할 수 있습니다.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this._action((T)parameter);
        }
        #endregion
    }

    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action<object> action) : base(action)
        { }
    }
}
