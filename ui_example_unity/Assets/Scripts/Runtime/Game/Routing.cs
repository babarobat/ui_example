using Runtime.Configs;
using Runtime.Models;
using UIFramework;

namespace Runtime.Game
{
    public class Routing
    {
        private readonly Model _model;
        private readonly Config _configs;

        public Routing(Model model, Config configs)
        {
            _model = model;
            _configs = configs;
        }

        public void OpenMenuWindow()
        {
            UI.MvvmWindows.CloseCurrent();
            
            UI.SimpleWindows.Show<MenuWindowView, (Routing, Config)>((this, _configs));
        }
        
        public void OpenProfileWindow()
        {
            UI.SimpleWindows.CloseCurrent();
            
            UI.MvvmWindows.Show<ProfileScreen, ProfileWindowView, ProfileWindowViewModel, ProfileScreenContext>(new ProfileScreenContext
            {
                Model = _model,
                Configs = _configs,
                Routing = this
            });
        }

        public void OpenShopWindow()
        {
            UI.SimpleWindows.CloseCurrent();
            
            UI.MvvmWindows.Show<AvatarShopScreen, AvatarShopWindowView, AvatarShopWindowViewModel, AvatarShopContext>(new AvatarShopContext
            {
                Model = _model,
                Configs = _configs,
                Routing = this
            });
        }
    }
}