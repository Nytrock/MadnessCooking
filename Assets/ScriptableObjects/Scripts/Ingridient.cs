using UnityEngine;

[CreateAssetMenu(menuName = "Ingridient")]

public class Ingridient : BaseObject
{
    public IngridientType TypeIngridient;
    public Sprite IngridientSprite;
    public Sprite PlantSprite;
    public int TimeGrow;
    public int MaxCount;
}
