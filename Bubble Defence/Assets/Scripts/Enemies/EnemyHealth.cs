using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hp = 20;
    float maxHp;

    bool alive = true;

    public bool GetAlive() { return alive; }


    public void GetDamage(float damage)
    {
        if (alive == false) return;
        hp -= damage; 
        if(hp <= 0.001f)
        {
            Death();
        }
    }

    void Death()
    {
        alive = false;
        GetComponent<Animator>().SetTrigger("death");
        GetComponent<EnemyLogic>().enabled = false;
        Destroy(gameObject, 3);
    }


    // Start is called before the first frame update
    void Start()
    {
        maxHp = hp;

    }

    
}
