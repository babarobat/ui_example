using System;
using Runtime.UIFrameworks.MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game
{
    public class ProfileWindowViewModel : ViewModelBase
    {
        public readonly ViewModelValue<PlayerProfileViewModel> PlayerProfile = new(new ());
        public readonly ViewModelValue<Action> CloseClickCallback = new();
        public readonly ViewModelValue<string> Header = new();
    }

    public class ProfileWindowView : AViewBase<ProfileWindowViewModel>
    {
        [SerializeField] private PlayerProfileView _profileView;
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _headerLabel;

        private Action _closeClickCallback;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            
            _profileView.Initialize();
            
            AddChild(_profileView);
        }

        protected override void OnViewModelChanged()
        {
            ViewModel.PlayerProfile.Bind(x => _profileView.Connect(x));
            ViewModel.CloseClickCallback.Bind(x => _closeClickCallback = x);
            ViewModel.Header.Bind(x => _headerLabel.text = x);
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
            _closeClickCallback = default;
            _profileView.CleanUp();
            
            base.OnCleanedUp();
        }
    }
}