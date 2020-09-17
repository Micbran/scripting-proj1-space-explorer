using UnityEngine;
using UnityEngine.Events;

public class WinVolume : MonoBehaviour
{
    [System.Serializable] public class EndStateEvent : UnityEvent<GameManager.EndState> { } 
    [SerializeField] public EndStateEvent onPlayerWin;

    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip = other.gameObject.GetComponent<PlayerShip>();

        if(playerShip != null)
        {
            if(onPlayerWin != null)
            {
                onPlayerWin.Invoke(GameManager.EndState.GAME_WON);
            }
        }
    }
}
