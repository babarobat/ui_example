using System;
using System.Collections.Generic;
using Runtime.UIFrameworks.MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game
{
    public class AvatarShopWindowViewModel : ViewModelBase
    {
        public readonly ViewModelValue<Action> CloseClickCallback = new();
        public readonly ViewModelValue<string> Header = new();
        public readonly ViewModelValue<string> Money = new();
        public readonly ViewModelValue<List<ShopAvatarElementViewModel>> Elements = new(new ());
    }
    
    public class AvatarShopWindowView : AViewBase<AvatarShopWindowViewModel>
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _headerLabel;
        [SerializeField] private TMP_Text _moneyLabel;
        [SerializeField] private RectTransform _scrollContent;
        [SerializeField] private ShopAvatarElementView _elementPrefab;
        
        private Action _closeClickCallback;
        private readonly List<ShopAvatarElementView> _createdElements = new();
        
        protected override void OnViewModelChanged()
        {
            ViewModel.CloseClickCallback.Bind(x => _closeClickCallback = x);
            ViewModel.Header.Bind(x => _headerLabel.text = x);
            ViewModel.Money.Bind(x => _moneyLabel.text = x);
            ViewModel.Elements.Bind(OnElementsChanged);
        }

        private void OnElementsChanged(List<ShopAvatarElementViewModel> elements)
        {
            ClearScroll();

            foreach (var viewModel in elements)
            {
                var view = Instantiate(_elementPrefab, _scrollContent);
                
                view.Initialize();
                view.Connect(viewModel);
                AddChild(view);
                
                _createdElements.Add(view);
            }
        }

        private void ClearScroll()
        {
            foreach (var view in _createdElements)
            {
                RemoveChild(view);
                view.CleanUp();
                
                Destroy(view.gameObject);
            }

            _createdElements.Clear();
        }

        protected override void OnTurnedOn()
        {
            base.OnTurnedOn();
            
            _closeButton.onClick.AddListener(OnCloseClick);
        }
        
        protected override void OnTurnedOff()
        {
            _closeButton.onClick.RemoveListener(OnCloseClick);
            
            base.OnTurnedOff();
        }
        
        private void OnCloseClick()
        {
            _closeClickCallback?.Invoke();
        }

        protected override void OnCleanedUp()
        {
            ClearScroll();
            _closeClickCallback = default;
            
            base.OnCleanedUp();
        }
    }
}