using UnityEngine;
using System.Collections;
using System.Threading;

public enum FireMode { Single, FullAuto }

public class GunItem : Item, IUsable
{
    public override bool CanEquipIn(EquipmentSlot slot)
        => slot == EquipmentSlot.LeftHand || slot == EquipmentSlot.RightHand;

    public FireMode fireMode = FireMode.Single;
    public float fireRate = 10f; // bullets per second

    public Transform muzzle;
    public GameObject bulletPrefab;

    private int bulletsInClip = 10;
    private float fireTimer = 0;

    //public void LoadClip(AmmoClipItem clip)
    //{
    //    bulletsInClip = clip.bulletCount;
    //}

    private void Update()
    {
        if(fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
        }
    }

    public void Use(PlayerEquipment owner, EquipmentSlot slot)
    {
        if (fireMode == FireMode.Single && fireTimer <= 0)
        {
            TryShoot();
            fireTimer = 1 / fireRate;
        }
    }

    public void UseHold(PlayerEquipment owner, EquipmentSlot slot)
    {
        if (fireMode == FireMode.FullAuto && fireTimer<=0)
        {
            TryShoot();
            fireTimer = 1/fireRate;
        }
    }

    private void TryShoot()
    {
        if (bulletsInClip <= 0)
        {
            Debug.Log("Out of ammo");
            return;
        }

        bulletsInClip--;

        // spawn bullet or cast ray
        Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
    }

}
