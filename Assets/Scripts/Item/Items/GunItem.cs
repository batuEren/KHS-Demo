using UnityEngine;
using System.Collections;

public enum FireMode { Single, FullAuto }

public class GunItem : Item, IUsable
{
    public override bool CanEquipIn(EquipmentSlot slot)
        => slot == EquipmentSlot.LeftHand || slot == EquipmentSlot.RightHand;

    public FireMode fireMode = FireMode.Single;
    public float fireRate = 10f; // bullets per second
    public Transform muzzle;
    public GameObject bulletPrefab;

    private int bulletsInClip = 0;
    private bool isFiring;

    //public void LoadClip(AmmoClipItem clip)
    //{
    //    bulletsInClip = clip.bulletCount;
    //}

    public void Use(PlayerEquipment owner, EquipmentSlot slot)
    {
        if (fireMode == FireMode.Single)
        {
            TryShoot();
        }
        else // FullAuto
        {
            // toggle full auto firing with each press
            isFiring = !isFiring;

            if (isFiring)
                StartCoroutine(FullAutoCoroutine());
        }
    }

    private void TryShoot()
    {
        if (bulletsInClip <= 0) return;

        bulletsInClip--;

        // spawn bullet or cast ray
        Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
    }

    private IEnumerator FullAutoCoroutine()
    {
        float delay = 1f / fireRate;
        while (isFiring)
        {
            TryShoot();
            yield return new WaitForSeconds(delay);
        }
    }
}
