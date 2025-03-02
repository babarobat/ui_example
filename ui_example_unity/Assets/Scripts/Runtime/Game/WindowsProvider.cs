using System;
using System.Collections.Generic;
using UIFramework.Common;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Runtime.Game.Windows
{
    public class WindowsProvider : IInstanceProvider
    {
        private readonly Dictionary<Type, string> _assetPathMap = new()
        {
            [typeof(MenuWindowView)] = "Game/Windows/MenuWindow",
            [typeof(ProfileWindowView)] = "Game/Windows/ProfileWindow",
            [typeof(AvatarShopWindowView)] = "Game/Windows/AvatarShopWindow",
        };
        
        public TInstance Get<TInstance>(Transform container) where TInstance : Object
        {
            var path = _assetPathMap[typeof(TInstance)];
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, container).GetComponent<TInstance>();
        }

        public void Release(GameObject instance)
        {
            Object.Destroy(instance);
        }
    }
}