using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ItemSlot : MonoBehaviour
{
   // //　アイテム情報を表示するテキストUI
   // private Text informationText;

    
   // //　アイテムの名前を表示するテキストUIプレハブ
   // [SerializeField]
   // private GameObject itemSlotTitleUI;

    
   // //　アイテム名を表示するテキストUIをインスタンス化した物を入れておく

   // private GameObject itemSlotTitleUIInstance;
    
   // //　自身のアイテムデータを入れておく
   // MasterItemModel myItemData;


   //// public MasterItemModel Item;


   // public ItemManager Itemmanager;

   // List<MasterItemModel> mylist;

   // void OnDisable()
   // {
   //     if (itemSlotTitleUIInstance != null)
   //     {
   //         Destroy(itemSlotTitleUIInstance);
   //     }
   //     Destroy(gameObject);
   // }

   // public void SetItemData(MasterItemModel itemData)
   // {
   //     myItemData = itemData;

   //      //mylist = myItemData.GetItemKyeList();

   //     //　アイテムのスプライトを設定
   //     //foreach(var item in myItemData)
   //     //transform.GetChild(0).GetComponent<Image>().sprite = Itemmanager.GetItemList(mylist.Find(itemName => itemName.GetName() == "薬草").GetName()).GetSprite;
   //     transform.GetChild(0).GetComponent<Image>().sprite = itemData.GetSprite;
   // }

   // void Start()
   // {
   //     //　アイテムスロットの親の親からInformationゲームオブジェクトを探しTextコンポーネントを取得する

   //    // Debug.Log(transform.parent.parent);
   //     informationText = transform.parent.parent.Find("Panel").GetChild(0).GetComponent<Text>();


   //     /*
   //     itemSlotTitleUIInstance = Instantiate(itemSlotTitleUI);

   //     //　アイテム表示Textを取得
   //     var itemSlotTitleText = itemSlotTitleUIInstance.GetComponent<Text>();
   //     //　アイテム表示テキストに自身のアイテムの名前を表示
   //     itemSlotTitleText.text = myItemData.GetName;

   //     */
        

   //     //Debug.Log(itemSlotTitleText);
   //    // Debug.Log(itemSlotTitleUI);


   // }

    
   // public void MouseOver()
   // {
        

   //     if (itemSlotTitleUIInstance != null)
   //     {
   //         Destroy(itemSlotTitleUIInstance);
   //     }
   //     /*
   //     //　アイテムの名前を表示するUIを位置を調整してインスタンス化
   //     //itemSlotTitleUIInstance = Instantiate<GameObject>(itemSlotTitleUI, new Vector2(transform.position.x - 25f, transform.position.y + 25f), Quaternion.identity, transform.parent.parent);
   //     itemSlotTitleUIInstance = Instantiate(itemSlotTitleUI);
        
   //     //　アイテム表示Textを取得
   //     var itemSlotTitleText = itemSlotTitleUIInstance.GetComponentInChildren<Text>();
   //     //　アイテム表示テキストに自身のアイテムの名前を表示
   //     itemSlotTitleText.text = myItemData.GetName;*/
   //     //　情報表示テキストに自身のアイテムの情報を表示

   //     itemSlotTitleUIInstance = Instantiate(itemSlotTitleUI);

   //     //　アイテム表示Textを取得
   //     var itemSlotTitleText = itemSlotTitleUIInstance.GetComponent<Text>();
   //     //　アイテム表示テキストに自身のアイテムの名前を表示
   //     itemSlotTitleText.text = myItemData.GetName;



   //     informationText.text = myItemData.GetInfo;
   // }
    
}
