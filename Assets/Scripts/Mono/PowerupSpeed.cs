using System.Collections;
using UnityEngine;
using UnityEngine.WSA;

public class PowerupSpeed : MonoBehaviour
{
    [Header("Powerup Settings")]
    [SerializeField] private float speedIncreaseAmount = 20;
    [SerializeField] private float powerupDuration = 5;

    [Header("Setup")]
    [SerializeField] private GameObject visualsToDeactivate = null;

    Collider colliderToDeactivate = null;
    bool powerActive = false;

    private void Awake()
    {
        colliderToDeactivate = GetComponent<Collider>();

        EnableRendering();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerShip player = other.gameObject.GetComponent<PlayerShip>();

        if(player != null && !powerActive)
        {
            StartCoroutine(PowerupEffect(player));
        }
    }

    private IEnumerator PowerupEffect(PlayerShip player)
    {
        powerActive = true;

        ActivatePowerup(player);

        DisableRendering();

        yield return new WaitForSeconds(powerupDuration);

        DeactivatePowerup(player);
        EnableRendering();

        powerActive = false;
    }

    private void ActivatePowerup(PlayerShip player)
    {
        if(player != null)
        {
            player.AddSpeed(speedIncreaseAmount);
            player.BoostersState = true;
        }
    }

    private void DeactivatePowerup(PlayerShip player)
    {
        if(player != null)
        {
            player.AddSpeed(-1*speedIncreaseAmount);
            player.BoostersState = false;
        }
    }

    private void DisableRendering()
    {
        colliderToDeactivate.enabled = false;
        visualsToDeactivate.SetActive(false);
    }

    private void EnableRendering()
    {
        colliderToDeactivate.enabled = true;
        visualsToDeactivate.SetActive(true);
    }

}
