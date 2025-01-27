using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DamageType
{
    PHYSIC,
    MAGIC,
    FIRE
}

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hp = 20;
    float maxHp;

    bool alive = true;

    [SerializeField] int coinsForKill = 3;

    public bool GetAlive() { return alive; }

    [SerializeField] Slider healthBar;

    [Header("Защита от типов урона")]
    [SerializeField][Range(0, 1)] float physicResist = 0;
    [SerializeField][Range(0, 1)] float magicResist = 0;
    [SerializeField][Range(0, 1)] float fireResist = 0;

    public virtual void GetDamage(float damage, DamageType damagetype = DamageType.PHYSIC)
    {
        if (alive == false) return;
        if (damagetype == DamageType.PHYSIC) damage = damage * (1 - physicResist);
        else if (damagetype == DamageType.MAGIC) damage = damage * (1 - magicResist);
        else if (damagetype == DamageType.FIRE) damage = damage * (1 - fireResist);
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

    public void IncreaseHP(float mult)
    {
        hp *= mult;
        maxHp = hp;
    }

    public void IncreaseCoins(float mult)
    {
        coinsForKill = (int)(coinsForKill * mult);
    }
    
}
