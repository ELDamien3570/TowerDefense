using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public ShopManager ShopManager;
    public void ShopButtonPressed()
    {
        

        ShopManager.ShowShopWindow();
    }
}
