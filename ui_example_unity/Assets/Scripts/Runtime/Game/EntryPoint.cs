using UnityEngine;

namespace Runtime.Game
{
    public class EntryPoint : MonoBehaviour
    {
        private void Start()
        {
            AppContext.Init();
            
            AppContext.Routing.OpenMenuWindow();
        }
    }
}