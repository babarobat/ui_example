using UnityEngine;

namespace UIFramework.SimpleViews
{
    public abstract class ABaseView<TContext> : ABaseView
    {
        protected TContext Context { get; private set; }

        public void Connect(TContext context)
        {
            Context = context;
            
            OnConnected();
        }

        protected abstract void OnConnected();

        protected virtual void OnDestroy()
        {
            Context = default;
        }
    }

    public abstract class ABaseView : MonoBehaviour
    {
    }
}
