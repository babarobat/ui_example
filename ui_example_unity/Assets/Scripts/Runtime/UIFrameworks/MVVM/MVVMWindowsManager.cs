using UIFramework.Common;
using UnityEngine;

namespace Runtime.UIFrameworks.MVVM
{
    public class MVVMWindowsManager : ITick
    {
        private readonly RectTransform _content;
        private readonly IInstanceProvider _instanceProvider;

        private (IScreen Screen, ViewModelBase ViewModel, AViewBase View)? _opened;

        public MVVMWindowsManager(RectTransform content, IInstanceProvider instanceProvider)
        {
            _content = content;
            _instanceProvider = instanceProvider;
        }

        public void Show<TScreen, TView, TViewModel, TScreenContext>(TScreenContext context)
            where TScreen : AScreen<TView, TViewModel, TScreenContext>, new()
            where TViewModel : ViewModelBase, new()
            where TView : AViewBase<TViewModel>
        {
            CloseCurrent();

            var view = LoadView<TView, TViewModel>();
            var viewModel = new TViewModel();
            var screen = new TScreen();

            screen.Initialize();
            screen.Connect(view, viewModel, context);

            if (!view.IsInitialized)
            {
                view.Initialize();    
            }
            view.Connect(viewModel);
            
            screen.TurnOn();
            _opened = new (screen, viewModel, view);
        }

        public void Tick(float deltaTime)
        {
            if (!_opened.HasValue)
            {
                return;
            }
            
            _opened.Value.Screen.Tick(deltaTime);
        }

        private TView LoadView<TView, TViewModel>()
            where TViewModel : ViewModelBase
            where TView : AViewBase<TViewModel>
        {
            var instance = _instanceProvider.Get<TView>(_content);
            
            return instance;
        }

        public void CloseCurrent()
        {
            if (!_opened.HasValue)
            {
                return;
            }
            
            _opened.Value.Screen.TurnOff();
            _opened.Value.Screen.CleanUp();
            _instanceProvider.Release(_opened.Value.View.gameObject);

            _opened = default;
        }
    }
}