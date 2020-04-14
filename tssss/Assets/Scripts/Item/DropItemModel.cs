[System.Serializable]
public class DropItemModel
{
    public int rate;

    [ItemIdAttribute]  //追加: ただのintではなくItemIdのint！
    public int itemId;
}