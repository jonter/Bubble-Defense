using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    Queue<Arrow> pool;

    public static ArrowPool instance;
    [SerializeField] int count = 10;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        pool = new Queue<Arrow>();
        for (int i = 0; i < count; i++) 
        {
            Create();
        }

    }

    void Create()
    {
        GameObject newArrow = Instantiate(arrowPrefab, transform);
        newArrow.transform.position = new Vector3 (0, 0, 0);
        Arrow a = newArrow.GetComponent<Arrow>();
        newArrow.SetActive(false);
        pool.Enqueue(a);
    }

    public Arrow Get()
    {
        if(pool.Count == 0)
        {
            Create();
        }
        Arrow a = pool.Dequeue();
        a.gameObject.SetActive(true);
        return a;
    }

    public void Return(Arrow a)
    {
        a.gameObject.SetActive(false);
        a.transform.parent = transform;
        a.transform.position = new Vector3();
        pool.Enqueue(a);
    }

    
}
