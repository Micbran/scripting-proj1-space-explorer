using UnityEngine;
using UnityEngine.Events;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private int pointValue = 20;
    [System.Serializable] public class IntEvent : UnityEvent<int> {};
    [SerializeField] public IntEvent coinCollected;

    private void Awake()
    {
        coinCollected.AddListener(GameManager.Instance.OnCoinCollected);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerShip player = other.gameObject.GetComponent<PlayerShip>();

        if (player != null)
        {
            SoundManager.Instance.PlaySoundEffect(SoundEffect.CoinCollect);
            if(coinCollected != null)
            {
                coinCollected.Invoke(pointValue);
            }
            Destroy(gameObject);
        }
    }
}
