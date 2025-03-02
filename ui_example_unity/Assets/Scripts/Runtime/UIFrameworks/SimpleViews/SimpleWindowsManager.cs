using UIFramework.Common;
using UnityEngine;

namespace UIFramework.SimpleViews
{
    public class SimpleWindowsManager
    {
        private readonly RectTransform _container;
        private readonly IInstanceProvider _instanceProvider;

        private ABaseView _opened;
        
        public SimpleWindowsManager(RectTransform container, IInstanceProvider instanceProvider)
        {
            _container = container;
            _instanceProvider = instanceProvider;
        }

        public void Show<TWindow, TWindowContext>(TWindowContext context)
            where TWindow : ABaseView<TWindowContext>
        {
            CloseCurrent();
            
            var instance = _instanceProvider.Get<TWindow>(_container);
            instance.Connect(context);

            _opened = instance;
        }

        public void CloseCurrent()
        {
            if (_opened == null)
            {
                return;
            }
            
            _instanceProvider.Release(_opened.gameObject);

            _opened = default;
        }
    }
}