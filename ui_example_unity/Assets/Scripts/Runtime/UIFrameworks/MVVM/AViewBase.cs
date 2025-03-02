using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.UIFrameworks.MVVM
{
    public abstract class AViewBase<TViewModel> : AViewBase
        where TViewModel : ViewModelBase
    {
        public TViewModel ViewModel { get; private set; }

        public void Connect(TViewModel viewModel)
        {
            if (!IsInitialized)
            {
                throw new Exception($"{GetType().Name}: {gameObject.name} not initialized");
            }
            
            CleanUp();

            ViewModel = viewModel;
            OnViewModelChanged();
        }

        protected abstract void OnViewModelChanged();

        protected override void OnCleanedUp()
        {
            ViewModel?.Disconnect();

            base.OnCleanedUp();
        }
    }
    
    public class AViewBase : MonoBehaviour, ILifeCycle
    {
        public bool IsInitialized { get; private set; }
        protected bool IsTurnedOn { get; private set; }

        private readonly HashSet<ILifeCycle> _children = new();

        protected void AddChild(ILifeCycle child)
        {
            if (_children.Contains(child))
            {
                throw new Exception($"{GetType().Name}: {gameObject.name} child already added");
            }

            _children.Add(child);

            if (IsTurnedOn)
            {
                child.TurnOn();
            }
        }

        protected void RemoveChild(ILifeCycle child)
        {
            _children.Remove(child);
        }

        public void Initialize()
        {
            if (IsInitialized)
            {
                return;
            }

            IsInitialized = true;

            OnInitialized();
        }

        protected virtual void OnInitialized()
        {
        }

        public void Tick(float deltaTime)
        {
            if (!IsTurnedOn)
            {
                return;
            }

            OnTick(deltaTime);

            foreach (var child in _children)
            {
                child.Tick(deltaTime);
            }
        }

        protected virtual void OnTick(float deltaTime)
        {
        }

        public void TurnOn()
        {
            if (!IsInitialized)
            {
                throw new Exception($"{GetType().Name}: {gameObject.name} not initialized");
            }

            if (IsTurnedOn)
            {
                return;
            }

            OnTurnedOn();

            IsTurnedOn = true;

            foreach (var child in _children)
            {
                child.TurnOn();
            }
        }

        protected virtual void OnTurnedOn()
        {
        }

        public void TurnOff()
        {
            if (!IsTurnedOn)
            {
                return;
            }

            foreach (var child in _children)
            {
                child.TurnOff();
            }
            
            OnTurnedOff();

            IsTurnedOn = false;
        }

        protected virtual void OnTurnedOff()
        {
        }

        public void CleanUp()
        {
            OnCleanedUp();
        }

        protected virtual void OnCleanedUp()
        {
        }
    }
}