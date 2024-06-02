using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyshooter : MonoBehaviour
{
    [SerializeField] private float shootInterval = 1.5f; // Intervalo de disparo en segundos
    [SerializeField] private float bulletSpeed = 5f; // Velocidad de las balas

    private float shootTimer;

    void Start()
    {
        shootTimer = shootInterval;
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0)
        {
            Shoot();
            shootTimer = shootInterval;
        }
    }

    void Shoot()
    {
        GameObject bullet = Pool.instance.GetBullets();
        if (bullet != null)
        {
            bullet.transform.position = transform.position; // Usar el transform del enemigo como punto de origen
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = transform.up * bulletSpeed; // Asignar velocidad en la dirección hacia arriba del transform
            }
        }
    }

}