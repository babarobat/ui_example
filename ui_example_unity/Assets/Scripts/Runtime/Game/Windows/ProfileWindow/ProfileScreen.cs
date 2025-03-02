using System.Linq;
using Runtime.Configs;
using Runtime.Models;
using Runtime.UIFrameworks.MVVM;
using UnityEngine;

namespace Runtime.Game
{
    public class ProfileScreenContext
    {
        public Model Model;
        public Config Configs;
        public Routing Routing;
    }
    
    public class ProfileScreen : AScreen<ProfileWindowView, ProfileWindowViewModel, ProfileScreenContext>
    {
        protected override void OnConnected()
        {
            ViewModel.CloseClickCallback.Value = OnCloseClick;
        }

        private void OnCloseClick()
        { 
            Context.Routing.OpenMenuWindow();
        }

        protected override void OnTurnedOn()
        {
            base.OnTurnedOn();

            ViewModel.Header.Value = Context.Configs.Localization.ProfileWindowHeader;

            var playerMoney = Context.Model.PlayerProfile.Money.Value;
            var moneyText = string.Format(Context.Configs.Localization.PlayerMoneyFormat, playerMoney);
            ViewModel.PlayerProfile.Value.Money.Value = moneyText;

            var avatarId = Context.Model.PlayerProfile.Avatar.Value;
            var avatarSpritePath = Context.Configs.Avatars.First(x => x.Id == avatarId).AssetPath;
            var avatarSprite = Resources.Load<Sprite>(avatarSpritePath);
            ViewModel.PlayerProfile.Value.Avatar.Value = avatarSprite;
            
            ViewModel.PlayerProfile.Value.Name.Value = Context.Model.PlayerProfile.Name;
        }
    }
}