using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace QTicTacToe.Model
{


        public abstract class Bindable : INotifyPropertyChanged, INotifyDataErrorInfo
        {
            #region INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;

            protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
            {
                if (object.Equals(storage, value)) return false;

                if (CheckErrorState<T>(value, propertyName))
                    OnErrorStateChanged(propertyName);

                storage = value;
                this.OnPropertyChanged(propertyName);
                return true;
            }

            protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                var eventHandler = this.PropertyChanged;
                if (eventHandler != null)
                {
                    eventHandler(this, new PropertyChangedEventArgs(propertyName));
                }
            }
            #endregion

            #region INotifyDataErrorInfo
            private Dictionary<string, string> _errors = new Dictionary<string, string>();

            public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

            public bool HasErrors
            {
                get { return _errors.Count > 0; }
            }

            protected void OnErrorStateChanged([CallerMemberName] string propertyName = null)
            {
                var eventHandler = this.ErrorsChanged;
                if (eventHandler != null)
                {
                    eventHandler(this, new DataErrorsChangedEventArgs(propertyName));
                }
            }

            private bool CheckErrorState<T>(T value, [CallerMemberName] String propertyName = null)
            {
                if (propertyName == null)
                    return false;

                var msg = ValidateProperty(value, propertyName);

                if (!string.IsNullOrEmpty(msg))
                {
                    if (_errors.ContainsKey(propertyName))
                    {
                        _errors[propertyName] = msg;
                    }
                    else
                    {
                        _errors.Add(propertyName, msg);
                    };

                    return true;
                }


                if (_errors.ContainsKey(propertyName))
                {
                    _errors.Remove(propertyName);

                    return true;
                }


                return false;

            }

            public System.Collections.IEnumerable GetErrors(string propertyName)
            {
                return _errors;
            }

            public string GetErrorsFlattened(string separator)
            {
                return string.Join(separator, _errors.Values.ToArray<string>());
            }

            #endregion

            #region Virtual Methods
            protected virtual string ValidateProperty(object value, [CallerMemberName] string propertyName = null)
            { return string.Empty; }
            #endregion

        }   

}
