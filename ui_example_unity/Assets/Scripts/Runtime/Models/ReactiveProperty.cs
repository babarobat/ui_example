using System;
using System.Collections.Generic;

namespace Runtime.Models
{
    public class ReactiveProperty<TValue>
    {
        private TValue _value;

        public event Action OnChanged;

        public ReactiveProperty() { }

        public ReactiveProperty(TValue initial)
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
                OnChanged?.Invoke();
            }
        }
    }
}