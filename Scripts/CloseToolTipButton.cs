using UnityEngine;

public class CloseToolTipButton : MonoBehaviour
{
    public GameObject parent;

    public void CloseParent()
    {
        parent.SetActive(false);
    }
    public void CloseShopWindow()
    {
        ShopManager shopManager = FindFirstObjectByType<ShopManager>();
        shopManager.ShowShopButton();
        Cursor.visible = false;
    }
}
