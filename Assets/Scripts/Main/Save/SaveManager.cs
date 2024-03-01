using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    [Header("Списки всех объектов")]
    [SerializeField] private Ingredient[] _allIngredients;
    [SerializeField] private Food[] _allFood;
    [SerializeField] private Technic[] _allTechnic;
    [SerializeField] private BedType[] _allBedTypes;
    [SerializeField] private BaseUpgrade[] _allUpgrades;
    [SerializeField] private Texture2D cursor;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void SaveAll()
    {

    }

    public List<Ingredient> GetIngredientsOfOneBedType(BedType bedType)
    {
        var result = new List<Ingredient>();
        foreach (var ingredient in _allIngredients)
            if (ingredient.TypeIngredient == bedType.AcceptableType)
                result.Add(ingredient);
        return result;
    }
}
