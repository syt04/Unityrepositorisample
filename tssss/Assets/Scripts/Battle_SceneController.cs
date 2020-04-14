//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;
//using DG.Tweening;

//public class Battle_SceneController : MonoBehaviour
//{
    
//    public static UnitScript attacker;
//    public static UnitScript defender;

//    [SerializeField]
//    List<Image> attackerImages;
//    [SerializeField]
//    List<Image> defenderImages;

//    IEnumerator Start()
//    {
//        // 攻撃側・防衛側の画像を反映
//        RefreshImages(attackerImages, attacker);
//        foreach (var image in attackerImages)
//        {
//            var unitImage = attacker.GetComponent<SpriteRenderer>(); 
//            image.sprite = unitImage.sprite;
//            image.color = unitImage.color; 
//        }
//        RefreshImages(defenderImages, defender);
//        foreach (var image in defenderImages)
//        {
//            var unitImage = defender.GetComponent<SpriteRenderer>(); 
//            image.sprite = unitImage.sprite;
//            image.color = unitImage.color; 
//        }

//        yield return new WaitForSeconds(0.5f);

//        // 攻撃アニメーション
//        foreach (var image in attackerImages)
//        {
//            image.transform.DOLocalMoveX(image.transform.localPosition.x - 30f, 0.2f)
//                .SetLoops(2, LoopType.Yoyo);
//        }

//        yield return new WaitForSeconds(0.2f);

//        // 防衛側がダメージを受ける
//        int life = defender.life;
//        defender.Damage(attacker);
//        int x = life - defender.life;
//        Debug.Log("Defenderダメージ:"+ x);
//        StatusManager.Instance.Show(defender);
//        RefreshImages(defenderImages, defender, true);

//        yield return new WaitForSeconds(1f);

//        // 反撃可能な距離であれば、防衛側の反撃
//        var distance = Mathf.Abs(attacker.x - defender.x) + Mathf.Abs(attacker.y - defender.y);
//        if (defender.AttackRangeMin <= distance && distance <= defender.AttackRangeMax)
//        {
//            foreach (var image in defenderImages)
//            {
//                image.transform.DOLocalMoveX(image.transform.localPosition.x + 30f, 0.2f)
//                    .SetLoops(2, LoopType.Yoyo);
//            }

//            yield return new WaitForSeconds(0.2f);

//            life = attacker.life;

//            attacker.Damage(defender);

//            x = life - attacker.life;
//            Debug.Log("attackerダメージ:" + x);


//            RefreshImages(attackerImages, attacker, true);
//        }

//        yield return new WaitForSeconds(1f);

//        // ライフが0になるとユニット消滅
//        if (attacker.Life <= 0)
//        {
//            attacker.DestroyWithAnimate();
//        }
//        if (defender.Life <= 0)
//        {
//            defender.DestroyWithAnimate();
//        }

//        SceneManager.UnloadSceneAsync("Battle");
//    }

//    void RefreshImages(List<Image> images, UnitScript unit, bool needToAnimate = false)
//    {
//       for (var i = images.Count; i > Mathf.CeilToInt((float)unit.Life / (float)unit.LifeMax * 10f); i--)
//        {
//            var index = Random.Range(0, images.Count);
//            if (needToAnimate)
//            {
//                var image = images[index];
//                image.transform.DOLocalMoveY(image.transform.localPosition.y - 100f, 0.3f)
//                    .OnComplete(() =>
//                    {
//                        Destroy(image.gameObject);
//                    });
//            }
//            else
//            {
//                Destroy(images[index].gameObject);
//            }
//            images.RemoveAt(index);
//        }
//    }


//    public void Damage()
//    {
//        int life = defender.life;
//        defender.Damage(attacker);
//        int x = life - defender.life;
//        Debug.Log("Defenderダメージ:" + x);
//        StatusManager.Instance.Show(defender);
//    }


//}
