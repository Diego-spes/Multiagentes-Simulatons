using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPool : MonoBehaviour
{
    [SerializeField] public static PlayerPool instance;
    public static PlayerPool Instance { get { return instance; } }
    [SerializeField] GameObject proyectilprefab;
    [SerializeField] List<GameObject> bullets;
    [SerializeField] int cantidad = 0;
    private void Awake()
    {
        if (instance == null)
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
        return null;
    }
    private void AddBullets(int cant)
    {
        for (int i = 0; i < cant; i++)
        {
            GameObject proyectil = Instantiate(proyectilprefab);
            proyectil.SetActive(false);
            bullets.Add(proyectil);
            proyectil.transform.parent = transform;
        }
    }
}
