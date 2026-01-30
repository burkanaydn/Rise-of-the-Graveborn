using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private int currentWeaponIndex = 0; // Baþlangýç silahý
    [SerializeField] private GameObject[] weapons;       // Silahlar sýralý olacak
    //[SerializeField] private GameObject laserBullet;     // Lazer objesi
    //[SerializeField] private int extraLaserParticles = 50; // Eklenecek particle miktarý

    //private ParticleSystem laserParticleSystem;

    private void Awake()
    {
        // Baþlangýç silahýný aktif yap
        //weapons[currentWeaponIndex].SetActive(true);

        // Lazer sistemi referansý al

        /*
        if (laserBullet != null)
        {
            laserParticleSystem = laserBullet.GetComponent<ParticleSystem>();
            laserBullet.SetActive(false);
        }
        */
    }

    public void SwitchToNextWeapon()
    {
        if (currentWeaponIndex < weapons.Length - 1)
        {
            weapons[currentWeaponIndex].SetActive(false);
            currentWeaponIndex++;
            weapons[currentWeaponIndex].SetActive(true);

            /*
            if (currentWeaponIndex == weapons.Length - 1 && laserBullet != null)
            {
                laserBullet.SetActive(true);
            }
            */
        }
        else
        {
            /*

            // Zaten 5. silahtayýz, sadece laserBullet'ýn particles'ýný artýr
            if (laserParticleSystem != null && laserBullet.activeSelf)
            {
                var main = laserParticleSystem.main;
                main.maxParticles += extraLaserParticles;
            } */
        }

        CheckWeaponMaxLevel();
    }

    public void DisableCurrentWeaponAndBullet()
    {
        if (weapons != null && weapons.Length > currentWeaponIndex)
        {
            weapons[currentWeaponIndex].SetActive(false);
        }

        /*
        // Sadece 5. silah aktifse laserBullet olabilir
        if (currentWeaponIndex == weapons.Length - 1 && laserBullet != null)
        {
            laserBullet.SetActive(false);
        } */
    }

    // Þu anki silah ve varsa lazer objesini aç
    public void EnableCurrentWeaponAndBullet()
    {
        if (weapons != null && weapons.Length > currentWeaponIndex)
        {
            weapons[currentWeaponIndex].SetActive(true);
        }

        /*if (currentWeaponIndex == weapons.Length - 1 && laserBullet != null)
        {
            laserBullet.SetActive(true);
        } */
    }

    public void SetWeaponLevel(int weaponLevel)
    {
        currentWeaponIndex = weaponLevel;

        if (weaponLevel >= weapons.Length -1)
        {
            currentWeaponIndex = weapons.Length - 1;
            /*laserBullet.SetActive(true);

            var main = laserParticleSystem.main;
            main.maxParticles += (weaponLevel - (weapons.Length - 1)) * extraLaserParticles; */
        }

        weapons[currentWeaponIndex].SetActive(true);

        CheckWeaponMaxLevel();
    }

    private void CheckWeaponMaxLevel()
    {
        bool isMax = currentWeaponIndex >= weapons.Length - 1;
        //bool isMax = false;
        PlayerManager.Instance?.SetWeaponMaxLevel(isMax);
    }
}
