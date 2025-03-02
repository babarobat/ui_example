namespace Runtime.UIFrameworks.MVVM
{
    public abstract class AScreen<TView, TViewModel, TContext> : IScreen
        where TView : AViewBase<TViewModel>
        where TViewModel : ViewModelBase
    {
        protected TContext Context { get; private set; }
        protected TView View { get; private set; }
        protected TViewModel ViewModel { get; private set; }

        public void Initialize()
        {
            OnInitialized();
        }
        
        protected virtual void OnInitialized() { }

        public void Connect(TView view, TViewModel viewModel, TContext context)
        {
            Context = context;
            View = view;
            ViewModel = viewModel;
            
            OnConnected();
        }

        protected abstract void OnConnected();

        public void Tick(float deltaTime)
        {
            OnTick(deltaTime);
        }

        protected virtual void OnTick(float deltaTime) { }

        public void TurnOn()
        {
            OnTurnedOn();
            View.TurnOn();
            View.gameObject.SetActive(true);
        }

        protected virtual void OnTurnedOn(){}

        public void TurnOff()
        {
            View.TurnOff();
            View.gameObject.SetActive(false);
            OnTurnedOff();
        }

        protected virtual void OnTurnedOff(){}

        public virtual void CleanUp()
        {
            View.CleanUp();
            ViewModel.CleanUp();

            ViewModel = default;
            View = default;
            Context = default;
        }
    }
}