using UnityEngine;

namespace Runtime.UIFramework.Common
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private RectTransform _windowsContainer;

        public RectTransform WindowsContainer => _windowsContainer;
    }
}