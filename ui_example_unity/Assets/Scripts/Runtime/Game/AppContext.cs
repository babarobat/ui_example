using Runtime.Configs;
using Runtime.Game.Windows;
using Runtime.Models;
using UIFramework;

namespace Runtime.Game
{
    public static class AppContext
    {
        public static Model Model { get; private set; }
        public static Config Configs { get; private set; }
        public static Routing Routing { get; private set; }
        
        public static void Init()
        {
            UI.Init(new WindowsProvider());
            
            Configs = new Config();
            Model = new Model();
            Routing = new Routing(Model, Configs);
        }
    }
}