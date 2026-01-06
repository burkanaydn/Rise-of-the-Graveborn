using UnityEngine;

public class MarketManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;

    [SerializeField] private int baseWeaponPrice = 150;
    [SerializeField] private int baseSpeedPrice = 100;
    [SerializeField] private int baseSkillPrice = 300;

    [SerializeField] private int priceAdd = 100;
    [SerializeField] private int speedPriceAdd = 100;

    public void PlayerWeaponUpgrade()
    {
        int price = GetWeaponPrice();

        if (price <= EconomyManager.Instance.GetGold() && !PlayerManager.Instance.WeaponMaxLevel)
        {
            playerManager.UpgradeWeapon();
            EconomyManager.Instance.SpendGold(price);

            GlobalUIManager.Instance?.UpdateGoldText(EconomyManager.Instance.GetGold());
            GlobalUIManager.Instance?.OpenMarket();

            SoundManager.Instance.PlaySuccesfulMarket();

            Debug.Log("Weapon Upgraded");
        }

        if (price > EconomyManager.Instance.GetGold())
        {
            Debug.Log("Insufficient Gold");
        }

        if (PlayerManager.Instance.WeaponMaxLevel)
        {
            Debug.Log("Weapon max level!");
        }
    }

    public void PlayerSpeedUpgrade()
    {
        int price = GetSpeedPrice();

        if (price <= EconomyManager.Instance.GetGold())
        {
            playerManager.UpgradePlayerSpeed();
            EconomyManager.Instance.SpendGold(price);

            GlobalUIManager.Instance?.UpdateGoldText(EconomyManager.Instance.GetGold());
            GlobalUIManager.Instance?.OpenMarket();

            SoundManager.Instance.PlaySuccesfulMarket();

            Debug.Log("Speed Upgraded");
        }
        else
        {
            Debug.Log("Insufficient Gold");
        }
    }


    public void PlayerSkillUpgrade()
    {
        int price = GetSkillPrice();

        if (price <= EconomyManager.Instance.GetGold() && !PlayerManager.Instance.SkillMaxLevel)
        {
            playerManager.UpgradeSkill();
            EconomyManager.Instance.SpendGold(price);

            GlobalUIManager.Instance?.UpdateGoldText(EconomyManager.Instance.GetGold());
            GlobalUIManager.Instance?.OpenMarket();

            SoundManager.Instance.PlaySuccesfulMarket();

            Debug.Log("Skill Upgraded");
        }
        
        if(price > EconomyManager.Instance.GetGold())
        {
            Debug.Log("Insufficient Gold");
        }

        if (PlayerManager.Instance.SkillMaxLevel)
        {
            Debug.Log("Skill max level!");
            GlobalUIManager.Instance.ShowSkillMaxLevelPanel();
        }
    }

    public void CheckUpgradePrices()
    {
        bool canAffordSkill = GetSkillPrice() <= EconomyManager.Instance.GetGold();
        bool canAffordSpeed = GetSpeedPrice() <= EconomyManager.Instance.GetGold();
        bool canAffordWeapon = GetWeaponPrice() <= EconomyManager.Instance.GetGold();

        GlobalUIManager.Instance.SetPriceColor(GlobalUIManager.Instance.skillPriceText, canAffordSkill);
        GlobalUIManager.Instance.SetPriceColor(GlobalUIManager.Instance.speedPriceText, canAffordSpeed);
        GlobalUIManager.Instance.SetPriceColor(GlobalUIManager.Instance.weaponPriceText, canAffordWeapon);

        if (PlayerManager.Instance.SkillMaxLevel)
        {
            Debug.Log("Skill max level!");
            GlobalUIManager.Instance.ShowSkillMaxLevelPanel();
        }

        if (PlayerManager.Instance.WeaponMaxLevel)
        {
            Debug.Log("Weapon max level!");
            GlobalUIManager.Instance.ShowWeaponMaxLevelPanel();
        }
    }

    public int GetWeaponPrice()
    {
        int level = PlayerManager.Instance.GetWeaponLevel();
        return baseWeaponPrice + (level * priceAdd);
    }

    public int GetSpeedPrice()
    {
        int level = PlayerManager.Instance.GetSpeedLevel();
        return baseSpeedPrice + (level * speedPriceAdd);
    }

    public int GetSkillPrice()
    {
        int level = PlayerManager.Instance.GetSkillLevel();
        return baseSkillPrice + (level * priceAdd);
    }
}
