using System.Collections.Generic;
using System.Linq;
using Runtime.Models;
using Runtime.Configs;
using Runtime.UIFrameworks.MVVM;
using UnityEngine;
using Avatar = Runtime.Configs.Avatar;

namespace Runtime.Game
{
    public class AvatarShopContext
    {
        public Model Model;
        public Config Configs;
        public Routing Routing;
    }
    
    public class AvatarShopScreen : AScreen<AvatarShopWindowView, AvatarShopWindowViewModel, AvatarShopContext>
    {
        private readonly Dictionary<string, ShopAvatarElementViewModel> _shopElementsById = new();
        
        protected override void OnConnected()
        {
            ViewModel.CloseClickCallback.Value = OnCloseClick;
            
            ViewModel.Header.Value = Context.Configs.Localization.AvatarShopWindowHeader;

            ViewModel.Elements.Value = Context.Configs.Avatars.Select(CreateViewModel).ToList();
        }

        private void OnCloseClick()
        { 
            Context.Routing.OpenMenuWindow();
        }

        protected override void OnTurnedOn()
        {
            base.OnTurnedOn();

            Context.Model.PlayerProfile.Money.OnChanged += OnMoneyChanged;
            Context.Model.PlayerProfile.Avatar.OnChanged += OnAvatarChanged;
            
            OnMoneyChanged();
            OnAvatarChanged();
        }

        protected override void OnTurnedOff()
        {
            Context.Model.PlayerProfile.Money.OnChanged -= OnMoneyChanged;
            Context.Model.PlayerProfile.Avatar.OnChanged -= OnAvatarChanged;
            
            base.OnTurnedOff();
        }

        private void OnMoneyChanged()
        {
            ViewModel.Money.Value = string.Format(Context.Configs.Localization.PlayerMoneyFormat, Context.Model.PlayerProfile.Money.Value);
        }
        
        private void OnAvatarChanged()
        {
            foreach (var (id, viewModel) in _shopElementsById)
            {
                viewModel.IsSelected.Value = id == Context.Model.PlayerProfile.Avatar.Value;
            }
        }

        private ShopAvatarElementViewModel CreateViewModel(Avatar avatar)
        {
            var result = new ShopAvatarElementViewModel
            {
                Avatar = { Value = Resources.Load<Sprite>(avatar.AssetPath) },
                ButtonLabel = { Value = $"Select {avatar.SelectionPrice}$" },
                Name = { Value = avatar.Name },
                ClickCallback = { Value = () => OnSelectClick(avatar)},
                IsSelected = { Value = Context.Model.PlayerProfile.Avatar.Value == avatar.Id},
            };
            
            _shopElementsById.Add(avatar.Id, result);

            return result;
        }
        
        private void OnSelectClick(Avatar avatar)
        {
            var price = avatar.SelectionPrice;
            if (Context.Model.PlayerProfile.Money.Value <= price)
            {
                Debug.Log("Not enough money!");
                
                return;
            }

            Context.Model.PlayerProfile.Money.Value -= price;
            Context.Model.PlayerProfile.Avatar.Value = avatar.Id;
        }
        
        public override void CleanUp()
        {
            _shopElementsById.Clear();
            
            base.CleanUp();
        }
    }
}