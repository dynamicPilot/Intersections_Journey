using UnityEngine;

namespace IJ.Core.Managers.GameManagers
{
    [RequireComponent(typeof(Flow))]
    public class GameManager : MonoBehaviour
    {
        private protected PlayerState _playerState;
        private protected Flow _flow;
        private protected void FindPlayerState()
        {
            _playerState = GameObject.FindGameObjectWithTag("PlayerState").GetComponent<PlayerState>();
            _flow = GetComponent<Flow>();

            if (_playerState == null)
            {
                Logging.Log("GameManager: ---- NO PLAYER STATE IN SCENE ---- ");
                _flow.BackToMenu();
                return;
            }

            ISetPlayerState playerstateSetter = _flow as ISetPlayerState;
            if (playerstateSetter != null) playerstateSetter.SetPlayerState(_playerState);
        }
    }
}

