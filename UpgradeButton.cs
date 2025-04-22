using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    public UpgradeManager upgradeManager;
    
    public void ShopButtonPressed()
    {
        //upgradeManager = FindFirstObjectByType<UpgradeManager>();
        //Debug.Log("open upgrade menu");
        upgradeManager.ShowShopWindow();
    }
}
