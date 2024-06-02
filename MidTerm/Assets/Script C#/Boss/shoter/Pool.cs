using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] public static Pool instance;
    public static Pool Instance { get { return instance; } }
    [SerializeField] GameObject Bullet;
    [SerializeField] List<GameObject> bullets;
    [SerializeField] bool enougthBullets = false;
    [SerializeField] int cantidad = 150;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
       
        bullets = new List<GameObject>();
    }
    private void Start()
    {
        AddBullets(cantidad);
    }

    private void AddBullets(int cant)
    {
        for (int i = 0; i < cant; i++)
        {
            GameObject proyectil = Instantiate(Bullet);
            proyectil.SetActive(false);
            bullets.Add(proyectil);
            proyectil.transform.parent = transform;
        }
    }


    public GameObject GetBullets()
    {
        if (bullets.Count > 0)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].activeInHierarchy)
                {
                    return bullets[i];
                }
            }
        }
        if (!enougthBullets)
        {
            GameObject bullet = Instantiate(Bullet);
            bullet.SetActive(false);
            bullets.Add(bullet);
            bullet.transform.parent = transform;
            return bullet;
        }
        return null;
    }
}
