using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrosshairCursor : MonoBehaviour
{
    public Sprite defaultSprite;
    public Sprite currentCursor;
    public SpriteRenderer cursorRender;
    public ShopGun heldGun;
    public int heldCost;

    private void Awake()
    {
        Cursor.visible = false; 
        cursorRender = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        CursorMovement();
        CheckForDefaultSwitch();
        DropGunOnClick(heldGun, heldCost);     
    }
    private void CheckForDefaultSwitch()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            cursorRender.sprite = defaultSprite;
            heldGun = null;
        }
    }
    private void CursorMovement()
    {
        Vector2 mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouseCursorPos;
    }
    public void DropGunOnClick(ShopGun _dropGun, int cost)
    {
        if (Input.GetMouseButtonDown(0) && cursorRender.sprite != defaultSprite && heldGun != null)
        {
            Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.CircleCast(currentPosition, .05f, Vector2.right, 0f);

            if (hit.collider == null || hit.collider.gameObject.CompareTag("Armament") || hit.collider.gameObject.CompareTag("Untagged"))
            {
               // Debug.Log("Dropping Gun");
                Vector3 dropPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 ontop = new Vector3(0, 0, 10);
                dropPos += ontop;
                Instantiate(_dropGun.gun, dropPos, Quaternion.identity);
                cursorRender.sprite = defaultSprite;

                LevelManager levelManager = FindFirstObjectByType<LevelManager>();
                levelManager.money -= cost;
                heldCost = 0;
                heldGun = null;
            }
            else if (hit.collider.gameObject.CompareTag("Road"))
            {
                return;
            }
                   
        }
    }
    public void ChangeCursor(ShopGun incomingGun, int cost)
    {
        cursorRender.sprite = incomingGun.icon;
        heldGun = incomingGun;
        heldCost = cost;
    }
}
