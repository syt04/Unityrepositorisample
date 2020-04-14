using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageCleaText : MonoBehaviour
{

    private Text damageText;
    //　フェードアウトするスピード
    [SerializeField]
    private float fadeOutSpeed = 10f;
    //　移動値
    [SerializeField]
    private float moveSpeed = 0.4f;

    //public string text;
    private CanvasGroup canvasGroup;


    void Start()
    {
        damageText = GetComponentInChildren<Text>();
        canvasGroup = GetComponentInChildren<CanvasGroup>();


    }

    void LateUpdate()
    {

            transform.rotation = Camera.main.transform.rotation;
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;

            damageText.color = Color.Lerp(damageText.color, new Color(1f, 0f, 0f, 0f), fadeOutSpeed * Time.deltaTime);

            if (damageText.color.a <= 0.1f)
            {
                Destroy(gameObject);
            }
        
    }

}
