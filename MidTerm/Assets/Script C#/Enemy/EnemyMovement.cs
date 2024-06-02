using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f; // Velocidad de movimiento del enemigo
    [SerializeField] Transform targetPoint; // Punto de destino actual
     List<Transform> movePoints = new List<Transform>(); // Lista de puntos de movimiento
    [SerializeField] GameManager.GameState currentState;
    private void GameManagerOnGameStateChanged(GameManager.GameState newState)
    {
        currentState = newState;
    }
    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
    private void Start()
    {
        // Encontrar todos los puntos de movimiento en la escena y añadirlos a la lista
        GameObject[] points = GameObject.FindGameObjectsWithTag("MovePoints");
        foreach (GameObject point in points)
        {
            movePoints.Add(point.transform);
        }

        if (movePoints.Count > 0)
        {
            ChooseNewTargetPoint();
        }
    }

    private void Update()
    {
        MoveToTargetPoint();
    }

    void ChooseNewTargetPoint()
    {
        int randomIndex = Random.Range(0, movePoints.Count);
        targetPoint = movePoints[randomIndex];
    }

    void MoveToTargetPoint()
    {
        if (targetPoint == null) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        // Si el enemigo ha llegado al punto de destino, elige un nuevo punto después de disparar
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            StartCoroutine(WaitBeforeChoosingNewPoint());
        }
    }

    IEnumerator WaitBeforeChoosingNewPoint()
    {
        yield return new WaitForSeconds(2f); // Ajusta el tiempo según sea necesario
        ChooseNewTargetPoint();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Proyectil"))
        {
            gameObject.SetActive(false); // Desactivar el enemigo al ser golpeado por una bala del jugador
           if(currentState == GameManager.GameState.Part1)
            EnemyPool.instance.RespawnEnemy(gameObject); // Programar la reaparición del enemigo
        }
    }
}