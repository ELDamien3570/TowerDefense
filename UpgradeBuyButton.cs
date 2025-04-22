using UnityEngine;

public class UpgradeBuyButton : MonoBehaviour
{
    public int cost;
    private bool purchased = false;
    public UpgradeType upgradeType;
    private LevelManager levelManager;  
    private UpgradeManager upgradeManager;
    public GameObject shopWindow;
    public enum UpgradeType
    {
        Ammo,
        Reload,
        Weapon
    }

    public void Update()
    {
        if (purchased)
        {
            this.gameObject.SetActive(false);
        }

    }
    public void UpgradeButtonPressed()
    {
        levelManager = FindFirstObjectByType<LevelManager>();
        upgradeManager = GetComponentInParent<UpgradeManager>();    
        shopWindow.SetActive(false);
        if (cost <= levelManager.money && !purchased)
        {
            purchased = true;
            levelManager.money -= cost;
            switch (upgradeType)
            {
                case UpgradeType.Ammo:
                    upgradeManager.UpgradeAmmo();
                    break;
                case UpgradeType.Reload:
                    upgradeManager.UpgradeReload();
                    break;
                case UpgradeType.Weapon:
                    upgradeManager.UpgradeWeapon();
                    break;
            }
        }
    }
}
