using System.Collections.Generic;

namespace Runtime.UIFrameworks.MVVM
{
    public abstract class ViewModelBase : IViewModelValue
    {
        private readonly List<IViewModelValue> _viewModelValues = new();

        protected ViewModelBase()
        {
            foreach (var fieldInfo in GetType().GetFields())
            {
                if (fieldInfo.GetValue(this) is IViewModelValue value)
                {
                    _viewModelValues.Add(value);
                }
            }
        }

        public void CleanUp()
        {
            foreach (var resettable in _viewModelValues)
            {
                resettable.CleanUp();
            }
        }

        public void Disconnect()
        {
            foreach (var resettable in _viewModelValues)
            {
                resettable.Disconnect();
            }
        }
    }
}