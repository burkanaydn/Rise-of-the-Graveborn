using System.Collections;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [Header("Skill Weapons")]
    [SerializeField] private GameObject[] skillWeapons;

    [Header("Skill Bullets")]
    [SerializeField] private GameObject[] skillBullets;

    [Header("References")]
    [SerializeField] private WeaponManager weaponManager; // Normal silah sistemine eriþim

    [Header("Skill Settings")]
    [SerializeField] private float skillDuration = 5f; // Skill süresi (saniye)
    [SerializeField] private float skillCooldown = 20f; // Cooldown süresi
    
    private bool isOnCooldown = false;

    private int currentSkillWeaponIndex = -1;
    private int currentSkillBulletIndex = -1;

    private float cooldownTimer = 0f;

    private GameObject currentSkillWeapon;
    private GameObject currentSkillBullet;

    /// <summary>
    /// Bir sonraki skill weapon ve bullet'ý current olarak ayarlar.
    /// Ýlk çalýþtýrmada ilk weapon/bullet atanýr, sonrakiler sýrayla deðiþir.
    /// </summary>
    /// 
    private void OnEnable()
    {
        GlobalUIManager.Instance?.ShowSkillCooldownPanel();
        GlobalUIManager.Instance?.HideSkillCooldownLock();
    }

    private void OnDisable()
    {
        GlobalUIManager.Instance?.HideSkillCooldownPanel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)
        && currentSkillWeapon != null
        && currentSkillBullet != null)
        {
            ActivateCurrentSkill(); //Skilli aktif et.
        }
    }

    public void SwitchToNextSkill()
    {
        // Weapon ilerlet
        if (currentSkillWeaponIndex < skillWeapons.Length - 1)
        {
            currentSkillWeaponIndex++;
            currentSkillWeapon = skillWeapons[currentSkillWeaponIndex];
        }

        // Bullet ilerlet
        if (currentSkillBulletIndex < skillBullets.Length - 1)
        {
            currentSkillBulletIndex++;
            currentSkillBullet = skillBullets[currentSkillBulletIndex];
        }
        CheckSkillMaxLevel();
        GlobalUIManager.Instance?.ShowSkillCooldownPanel();
    }

    /// <summary>
    /// Mevcut silahý devre dýþý býrakýr, skill weapon ve bullet'ý aktif eder.
    /// Belirlenen süre sonra geri dönüþ yapar.
    /// </summary>
    public void ActivateCurrentSkill()
    {
        if (isOnCooldown)
        {
            Debug.Log("Skill cooldown'da.");
            return;
        }

        if (currentSkillWeapon == null || currentSkillBullet == null)
        {
            Debug.LogWarning("Skill weapon veya bullet ayarlanmadý.");
            return;
        }

        StartCoroutine(SkillRoutine());
        StartCoroutine(SkillCooldownRoutine());
    }

    private IEnumerator SkillRoutine()
    {
        // Normal silahlarý devre dýþý býrak
        weaponManager.DisableCurrentWeaponAndBullet();

        // Skill weapon & bullet aktif et
        currentSkillWeapon.SetActive(true);
        currentSkillBullet.SetActive(true);

        // Belirli süre bekle
        yield return new WaitForSecondsRealtime(skillDuration);

        // Skill weapon & bullet kapat
        currentSkillWeapon.SetActive(false);
        currentSkillBullet.SetActive(false);

        // Normal silahlarý geri getir
        weaponManager.EnableCurrentWeaponAndBullet();
    }

    private IEnumerator SkillCooldownRoutine()
    {
        isOnCooldown = true;
        cooldownTimer = skillCooldown;

        // UI kilidini göster
        GlobalUIManager.Instance?.ShowSkillCooldownLock();

        while (cooldownTimer > 0f)
        {
            GlobalUIManager.Instance?.UpdateSkillCooldownPanel(Mathf.CeilToInt(cooldownTimer));
            yield return new WaitForSeconds(1f);
            cooldownTimer -= 1f;
        }

        // UI'ý sýfýrla
        GlobalUIManager.Instance?.UpdateSkillCooldownPanel(0f);

        // UI kilidini gizle
        GlobalUIManager.Instance?.HideSkillCooldownLock();

        isOnCooldown = false;
    }



    public void SetSkillLevel(int skillLevel)
    {
        if (skillLevel <= 0)
        {
            currentSkillWeapon = null;
            currentSkillBullet = null;
            currentSkillWeaponIndex = -1;
            currentSkillBulletIndex = -1;

            CheckSkillMaxLevel();
            return;
        }

        currentSkillWeaponIndex = Mathf.Clamp(skillLevel - 1, 0, skillWeapons.Length - 1);
        currentSkillWeapon = skillWeapons[currentSkillWeaponIndex];

        currentSkillBulletIndex = Mathf.Clamp(skillLevel - 1, 0, skillBullets.Length - 1);
        currentSkillBullet = skillBullets[currentSkillBulletIndex];
        CheckSkillMaxLevel();
    }
    private void CheckSkillMaxLevel()
    {
        bool isMax = currentSkillBulletIndex >= skillBullets.Length - 1;
        PlayerManager.Instance?.SetSkillMaxLevel(isMax);
    }
}
