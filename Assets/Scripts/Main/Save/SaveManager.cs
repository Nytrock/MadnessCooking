using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursor;
    
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
