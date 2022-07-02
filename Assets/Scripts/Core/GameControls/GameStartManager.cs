using UnityEngine;

namespace IJ.Core.Managers.GameManagers
{
    [RequireComponent(typeof(GameStartFlow))]
    public class GameStartManager : GameManager
    {
        private void Awake()
        {
            FindPlayerState();
        }

        private void Start()
        {
            GameStartFlow menuFlow = _flow as GameStartFlow;
            menuFlow.SetGameStart();
        }
    }
}
