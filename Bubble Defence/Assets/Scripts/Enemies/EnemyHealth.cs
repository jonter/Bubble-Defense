using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hp = 20;
    float maxHp;

    bool alive = true;

    [SerializeField] int coinsForKill = 3;

    public bool GetAlive() { return alive; }

    [SerializeField] Slider healthBar;

    public void GetDamage(float damage)
    {
        if (alive == false) return;
        hp -= damage; 
        healthBar.gameObject.SetActive(true);
        healthBar.value = hp / maxHp;
        if(hp <= 0.001f)
        {
            Death();
        }
    }

    void Death()
    {
        GameCoins.AddCoins(coinsForKill);
        alive = false;
        healthBar.gameObject.SetActive(false);
        GetComponent<Animator>().SetTrigger("death");
        GetComponent<EnemyLogic>().enabled = false;
        Destroy(gameObject, 3);
    }


    // Start is called before the first frame update
    void Start()
    {
        maxHp = hp;
        healthBar.gameObject.SetActive(false);
    }

    
}
