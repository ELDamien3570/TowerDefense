using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public ShopGun shopGun;
    public CrosshairCursor crosshairCursor;
    public int cost;
    public ShopManager shopManager;

    private void Awake()
    {
        shopManager = FindFirstObjectByType<ShopManager>();  
    }
    public void buyButtonPress()
    {
        shopManager = FindFirstObjectByType<ShopManager>();
        if (shopManager.money >= cost)
        {
            crosshairCursor = FindFirstObjectByType<CrosshairCursor>();
            crosshairCursor.ChangeCursor(shopGun, cost);
        }
        else
        {
            return;
        }
        
    }
}
