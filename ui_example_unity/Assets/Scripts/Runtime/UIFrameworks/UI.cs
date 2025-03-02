using Runtime.Game.Windows;
using Runtime.UIFramework.Common;
using Runtime.UIFrameworks.MVVM;
using UIFramework.SimpleViews;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UIFramework
{
    public static class UI
    {
        public static SimpleWindowsManager SimpleWindows;
        public static MVVMWindowsManager MvvmWindows;

        private static UIRoot _root;

        private static UIRoot Root
        {
            get
            {
                if (_root == null)
                {
                    var prefab = Resources.Load<UIRoot>("UIFramework/UIRoot");
                    _root = Object.Instantiate(prefab);
                    Object.DontDestroyOnLoad(_root.gameObject);
                }

                return _root;
            }
        }

        public static void Init(WindowsProvider windowsProvider)
        {
            SimpleWindows = new SimpleWindowsManager(Root.WindowsContainer, windowsProvider);
            MvvmWindows = new MVVMWindowsManager(Root.WindowsContainer, windowsProvider);
        }
    }
}