using Unity.VisualScripting;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject ShopButton;
    public GameObject ShopWindow;
    public LevelManager levelManager;
    public int money;


    public void Awake()
    {
        ShopButton.SetActive(true);
        ShopWindow.SetActive(false);
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    public void Update()
    {
        ShowCursorWhenActive();
        UpdateMoney();  
    }
    public void ShowCursorWhenActive()
    {
        if (ShopWindow.activeSelf == true)
        {
            Cursor.visible = true;
        }
        else
        {
           // Cursor.visible = false;
            ShowShopButton();

        }
    }

    public void UpdateMoney()
    {
        money = levelManager.money;
        //Debug.Log("Shop money" + money); 
    }
    public void ShowShopWindow()
    {
        ShopButton.SetActive(false);
        ShopWindow.SetActive(true);
    }
    public void ShowShopButton()
    {
        ShopButton.SetActive(true);
        ShopWindow.SetActive(false);
    }
}
