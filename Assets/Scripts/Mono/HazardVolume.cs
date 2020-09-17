﻿using UnityEngine;

public class HazardVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip = other.gameObject.GetComponent<PlayerShip>();

        if(playerShip != null)
        {
            SoundManager.Instance.PlaySoundEffect(SoundEffect.PlayerDeath);
            playerShip.Kill();
        }
    }
}
