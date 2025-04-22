using UnityEngine;

public class UpgradeManager : ShopManager
{

    public AutoGunController childGun;
    public Sprite gunUpgrade;
    public GameObject bulletUpgrade;
    public int upgradeRotFix;

    public new void Awake()
    {
        ShopButton.SetActive(true);
        ShopWindow.SetActive(false);
        levelManager = FindFirstObjectByType<LevelManager>();
        
    }
    private void LateUpdate()
    {
        ShowCursorWhenActive();
        ShowButtonWhenWindowClosed();
       // MoveWindowForward();
    }
    public void MoveWindowForward()
    {
        ShopWindow.transform.position += new Vector3(0, 0, -3);
    }
    public void ShowButtonWhenWindowClosed()
    {
        if (!ShopWindow.activeSelf)
            ShopButton.SetActive(true);
    }
    public void UpgradeAmmo()
    {
        childGun.bullet = bulletUpgrade;
    }
    public void UpgradeReload()
    {
        childGun.timeBetweenFiring = childGun.timeBetweenFiring / 2;
    }
    public void UpgradeWeapon()
    {
        childGun.gunIcon.sprite = gunUpgrade;
        childGun.timeBetweenFiring = childGun.timeBetweenFiring * .6f;     
        childGun.rotFix = upgradeRotFix;
    }

}
