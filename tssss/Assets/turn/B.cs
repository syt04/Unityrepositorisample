using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B : MonoBehaviour
{

    public A player;
    public A enemy;

    bool IsPlayerTuren;

    // Start is called before the first frame update
    void Start()
    {
        IsPlayerTuren = true;
    }

    float second = 0f;
    // Update is called once per frame
    void Update()
    {
        if (!IsPlayerTuren)
        {
            second += Time.deltaTime;
            if (second >= 1f)
            {
                second = 0;


                IsPlayerTuren = true;
                player.OnDamage(enemy.at);
                Debug.Log("playerhp" + player.hp);
            }
        }
    }

    public void PushAttackButton()
    {
        enemy.OnDamage(player.at);
        IsPlayerTuren = false;

        Debug.Log("enemyhp"+enemy.hp);
    }

}
