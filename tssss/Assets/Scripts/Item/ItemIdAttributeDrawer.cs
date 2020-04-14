using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text.RegularExpressions;

[CustomPropertyDrawer(typeof(ItemIdAttribute))]
public class ItemIdAttributeDrawer : PropertyDrawer
{
    private ItemIdAttribute itemIdAttribute
    {
        get
        {
            return (ItemIdAttribute)attribute;
        }
    }
    private bool isInitialized = false;
    private int[] itemIds = null;
    private string[] itemLabels = null;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //初期化
        if (!isInitialized)
        {
            Dictionary<int, string> items = GetItemModelLabels();
            itemIds = new int[items.Count];
            itemLabels = new string[items.Count];
            items.Keys.CopyTo(itemIds, 0);
            items.Values.CopyTo(itemLabels, 0);

            isInitialized = true;
        }
        property.intValue = EditorGUI.IntPopup(position, label.text, property.intValue, itemLabels, itemIds);
    }

    public static Dictionary<int, string> GetItemModelLabels()
    {
        Dictionary<int, string> result = new Dictionary<int, string>();

        string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", "MasterItemModel"));
        if (guids.Length == 0)
        {
            return result;
        }
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            MasterItemModel model = AssetDatabase.LoadAssetAtPath<MasterItemModel>(assetPath);

            //フォルダによるしぼりこみ
            Match match = Regex.Match(assetPath, string.Format("Assets/Resources/{0}/(.*?).asset", "MasterItemModel"));
            foreach (Group g in match.Groups)
            {
                if (g.Index == 0)
                {
                    continue;
                }
                int targetId = 0;
                if (int.TryParse(g.Value, out targetId))
                {
                    //表示項目を"ID: アイテム名"にしている
                    result[targetId] = string.Format("{0}: {1}", targetId, model.name);
                }
            }
        }
        return result;
    }
}