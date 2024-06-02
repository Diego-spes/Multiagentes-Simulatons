using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool instance;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int poolSize = 15;
    [SerializeField] private int initialActiveEnemies = 5; // Número de enemigos activos al inicio
    [SerializeField] private float respawnTime = 5f;
    private List<GameObject> enemyPool = new List<GameObject>();

    private Transform[] respawnPoints;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Encontrar los puntos de reaparición
        GameObject[] points = GameObject.FindGameObjectsWithTag("Respawn");
        if (points.Length == 0)
        {
            Debug.LogError("No se encontraron puntos de reaparición con el tag 'Respawn'.");
            return;
        }

        respawnPoints = new Transform[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            respawnPoints[i] = points[i].transform;
        }

        // Crear la pool de enemigos
        for (int i = 0; i < poolSize; i++)
        {
            if (enemyPrefab == null)
            {
                Debug.LogError("Enemy prefab no asignado en el inspector.");
                return;
            }

            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
            Debug.Log($"Enemigo {i + 1} creado y añadido a la pool.");
        }

        if (enemyPool.Count != poolSize)
        {
            Debug.LogError("La cantidad de enemigos creados no coincide con el tamaño de la pool.");
        }
        else
        {
            Debug.Log($"Se crearon {enemyPool.Count} enemigos correctamente.");
        }

        // Activar enemigos iniciales
        for (int i = 0; i < initialActiveEnemies; i++)
        {
            ActivateEnemyAtRandomRespawnPoint();
        }
    }

    public GameObject GetEnemy()
    {
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
        }
        return null; // Si todos los enemigos están activos
    }

    public void RespawnEnemy(GameObject enemy)
    {
        StartCoroutine(RespawnEnemyCoroutine(enemy));
    }

    private IEnumerator RespawnEnemyCoroutine(GameObject enemy)
    {
        yield return new WaitForSeconds(respawnTime);

        Transform spawnPoint = respawnPoints[Random.Range(0, respawnPoints.Length)];
        enemy.transform.position = spawnPoint.position;
        enemy.SetActive(true);
    }

    private void ActivateEnemyAtRandomRespawnPoint()
    {
        GameObject enemy = GetEnemy();
        if (enemy != null)
        {
            Transform spawnPoint = respawnPoints[Random.Range(0, respawnPoints.Length)];
            enemy.transform.position = spawnPoint.position;
            enemy.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No hay enemigos disponibles en la pool para activar.");
        }
    }
}