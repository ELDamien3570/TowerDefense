using UnityEngine;

public class StartWaveButton : MonoBehaviour
{
    public LevelManager levelManager;

    public void Update()
    {
        if (this.gameObject.activeSelf)
        {
            Cursor.visible = true;
        }
    }
    public void StartWave()
    {
        levelManager.StartWave();
        Cursor.visible = false;
        this.gameObject.SetActive(false);
    }


}
