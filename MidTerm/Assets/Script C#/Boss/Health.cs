using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] public float ActualHealth = 100;
    [SerializeField] public float MaxHealth = 100;
    [SerializeField] float moveSpeed = 2f; // Velocidad de movimiento del enemigo
    [SerializeField] Image barraVida;
    List<Transform> movePoints = new List<Transform>(); // Lista de puntos de movimiento
    [SerializeField] Transform targetPoint; // Punto de destino actual

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
    public void Damaged(float damage)
    {
        ActualHealth = ActualHealth - damage;
        Debug.Log("Boss health is: " + ActualHealth);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Proyectil")
        {
            Damaged(4f);
        }

    }
    void ChooseNewTargetPoint()
    {
        int randomIndex = UnityEngine.Random.Range(0, movePoints.Count);
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

    private void Update()
    {
        barraVida.fillAmount = ActualHealth / MaxHealth;
        if (ActualHealth <= 0)
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Victory);
        }

        if (ActualHealth <= 70 && ActualHealth > 50)
        {
            MoveToTargetPoint();
            GameManager.Instance.ChangeState(GameManager.GameState.Fase2);

        }

        if (ActualHealth <= 50 && ActualHealth > 0)
        {
            MoveToTargetPoint();
            GameManager.Instance.ChangeState(GameManager.GameState.Fase3);
        }
    }
}
