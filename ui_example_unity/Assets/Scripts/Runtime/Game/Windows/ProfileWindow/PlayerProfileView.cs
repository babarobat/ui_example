using Runtime.UIFrameworks.MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game
{
    public class PlayerProfileViewModel : ViewModelBase
    {
        public readonly ViewModelValue<Sprite> Avatar = new();
        public readonly ViewModelValue<string> Name = new();
        public readonly ViewModelValue<string> Money = new();
    }
    
    public class PlayerProfileView : AViewBase<PlayerProfileViewModel>
    {
        [SerializeField] private Image _avatarIcon;
        [SerializeField] private TMP_Text _nameLabel;
        [SerializeField] private TMP_Text _moneyLabel;
        
        protected override void OnViewModelChanged()
        {
            ViewModel.Avatar.Bind(x => _avatarIcon.sprite = x);
            ViewModel.Name.Bind(x => _nameLabel.text = x);
            ViewModel.Money.Bind(x => _moneyLabel.text = x);
        }
    }
}