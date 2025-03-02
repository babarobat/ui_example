using Runtime.Configs;
using TMPro;
using UIFramework.SimpleViews;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game
{
    public class MenuWindowView : ABaseView<(Routing Routing, Config Configs)>
    {
        [SerializeField] private Button _profileButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private TMP_Text _header;
        
        private void Awake()
        {
            _profileButton.onClick.AddListener(OnProfileClick);
            _shopButton.onClick.AddListener(OnShopClick);
        }

        protected override void OnConnected()
        {
            _header.text = Context.Configs.Localization.MenuHeader;
        }

        protected override void OnDestroy()
        {
            _profileButton.onClick.RemoveListener(OnProfileClick);
            _shopButton.onClick.RemoveListener(OnShopClick);
            
            base.OnDestroy();
        }

        private void OnProfileClick()
        {
            Context.Routing.OpenProfileWindow();
        }
        
        private void OnShopClick()
        {
            Context.Routing.OpenShopWindow();
        }
    }
}