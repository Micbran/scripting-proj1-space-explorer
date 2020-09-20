using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupInvincibility : MonoBehaviour
{
    [Header("Powerup Settings")]
    [SerializeField] private float powerupDuration = 5;
    [SerializeField] private Color invincibilityColor;

    [Header("Setup")]
    [SerializeField] private GameObject visualsToDeactivate = null;

    Collider colliderToDeactivate = null;
    bool powerActive = false;

    private MeshRenderer[] renderSave;

    private void Awake()
    {
        colliderToDeactivate = GetComponent<Collider>();

        EnableRendering();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerShip player = other.gameObject.GetComponent<PlayerShip>();

        if (player != null && !powerActive)
        {
            SoundManager.Instance.PlaySoundEffect(SoundEffect.InvincbilityPowerupCollect);
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
        if (player != null)
        {
            player.InvincibleState = true;
            player.MakeColor(invincibilityColor);
        }
    }

    private void DeactivatePowerup(PlayerShip player)
    {
        if (player != null)
        {
            player.InvincibleState = false;
            player.ReturnToDefaultColor();
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
