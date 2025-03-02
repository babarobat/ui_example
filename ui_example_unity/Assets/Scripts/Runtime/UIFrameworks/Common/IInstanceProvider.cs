using UnityEngine;

namespace UIFramework.Common
{
    public interface IInstanceProvider
    {
        public TInstance Get<TInstance>(Transform container) where TInstance : Object;
        public void Release(GameObject instance);
    }
}