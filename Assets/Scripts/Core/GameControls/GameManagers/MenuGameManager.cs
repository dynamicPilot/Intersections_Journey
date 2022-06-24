using UnityEngine;


namespace IJ.Core.Managers.GameManagers
{
    //[DefaultExecutionOrder(-1)]
    [RequireComponent(typeof(MenuFlow))]
    public class MenuGameManager : GameManager
    {
        private void Awake()
        {
            FindPlayerState();
        }

        private void Start()
        {
            MenuFlow menuFlow = _flow as MenuFlow;
            menuFlow.SetMenu();
        }
    }
}

