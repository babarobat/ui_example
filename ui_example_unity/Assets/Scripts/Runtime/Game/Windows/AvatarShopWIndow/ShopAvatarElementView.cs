using System;
using Runtime.UIFrameworks.MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game
{
    public class ShopAvatarElementViewModel : ViewModelBase
    {
        public readonly ViewModelValue<Sprite> Avatar = new();
        public readonly ViewModelValue<string> Name = new();
        public readonly ViewModelValue<Action> ClickCallback = new();
        public readonly ViewModelValue<string> ButtonLabel = new();
        public readonly ViewModelValue<bool> IsSelected = new();
    }

    public class ShopAvatarElementView : AViewBase<ShopAvatarElementViewModel>
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _buttonLabel;
        [SerializeField] private GameObject _selectedFrame;
        [SerializeField] private GameObject _defaultFrame;
        
        private Action _clickCallback;
        
        protected override void OnViewModelChanged()
        {
            ViewModel.Avatar.Bind(x => _icon.sprite = x);
            ViewModel.Name.Bind(x => _name.text = x);
            ViewModel.ClickCallback.Bind(x => _clickCallback = x);
            ViewModel.ButtonLabel.Bind(x => _buttonLabel.text = x);
            ViewModel.IsSelected.Bind(OnSelectionChanged);
        }

        protected override void OnTurnedOn()
        {
            base.OnTurnedOn();
            
            _button.onClick.AddListener(OnClick);
        }

        protected override void OnTurnedOff()
        {
            _button.onClick.RemoveListener(OnClick);
            
            base.OnTurnedOff();
        }

        private void OnClick()
        {
            _clickCallback?.Invoke();
        }
        
        private void OnSelectionChanged(bool isSelected)
        {
            _selectedFrame.SetActive(isSelected);
            _defaultFrame.SetActive(!isSelected);
        }
    }
}