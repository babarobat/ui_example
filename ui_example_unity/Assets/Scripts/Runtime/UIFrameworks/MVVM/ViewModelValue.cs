using System;
using System.Collections.Generic;

namespace Runtime.UIFrameworks.MVVM
{
    public class ViewModelValue<TValue> : IViewModelValue
    {
        private TValue _value;
        private Action<TValue> _changed;

        public ViewModelValue() { }

        public ViewModelValue(TValue initial)
        {
            _value = initial;
        }
        
        public TValue Value
        {
            get => _value;
            set
            {
                if (EqualityComparer<TValue>.Default.Equals(value, _value))
                {
                    return;
                }

                _value = value;
                _changed?.Invoke(_value);
            }
        }

        public void Bind(Action<TValue> handle)
        {
            if (_changed is not null)
            {
                throw new Exception("Already binded");
            }
            _changed = handle;
        
            if (_value is not null)
            {
                _changed?.Invoke(_value);    
            }
        }

        public void CleanUp()
        {
            _value = default;
        }

        public void Disconnect()
        {
            _changed = default;
        }
    }
}