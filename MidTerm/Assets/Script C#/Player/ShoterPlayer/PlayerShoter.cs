
using UnityEngine;
using TMPro;
public class PlayerShoter : MonoBehaviour
{
    [SerializeField] int maxBullets = 7; // Número máximo de balas que puede disparar
    [SerializeField] float fireRate = 0.5f; // Intervalo de tiempo entre disparos
    [SerializeField] float reloadTime = 2f; // Tiempo para recargar una bala
    [SerializeField] TextMeshProUGUI bulletCountText; // Texto para mostrar el número de balas

    private int currentBullets; // Número actual de balas disponibles
    private float nextFireTime; // Tiempo para el siguiente disparo
    private float nextReloadTime; // Tiempo para la próxima recarga

    private void Start()
    {
        currentBullets = maxBullets; // Inicializar el contador de balas
        nextFireTime = 0f; // Inicializar el tiempo para el siguiente disparo
        nextReloadTime = Time.time + reloadTime; // Inicializar el tiempo para la próxima recarga
        UpdateBulletCountText();
    }

    private void Update()
    {
        // Verificar si se puede disparar
        if (Input.GetKeyDown(KeyCode.Space  ) && currentBullets > 0 && Time.time >= nextFireTime)
        {
            FireBullet();
        }

        // Recargar balas si es necesario
        if (currentBullets < maxBullets && Time.time >= nextReloadTime)
        {
            ReloadBullet();
        }
    }

    private void FireBullet()
    {
        // Obtener una bala del pool
        GameObject playerBullet = PlayerPool.instance.GetBullets();
        if (playerBullet != null)
        {
            playerBullet.transform.position = transform.position;
            playerBullet.SetActive(true);

            currentBullets--; // Disminuir el contador de balas
            nextFireTime = Time.time + fireRate; // Actualizar el tiempo para el siguiente disparo
            nextReloadTime = Time.time + reloadTime; // Actualizar el tiempo para la próxima recarga
            Debug.Log("Fire! Bullets left: " + currentBullets);
            UpdateBulletCountText();
        }
        else
        {
            Debug.LogError("No se pudo obtener una bala del pool.");
        }
    }

    private void ReloadBullet()
    {
        currentBullets++; // Incrementar el contador de balas
        nextReloadTime = Time.time + reloadTime; // Actualizar el tiempo para la próxima recarga
        Debug.Log("Reloading... Bullets left: " + currentBullets);
        UpdateBulletCountText();
    }

    private void UpdateBulletCountText()
    {
        if (bulletCountText != null)
        {
            bulletCountText.text =  currentBullets.ToString();
        }
    }
}