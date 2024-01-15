using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
    }
}
