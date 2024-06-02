
using UnityEngine;
using TMPro;
public class ConteoBalas : MonoBehaviour
{// Asigna el TextMeshPro desde el Inspector
    [SerializeField] private TextMeshProUGUI bulletCounterText;

    // Tags para las balas del jugador y del Boss
    [SerializeField] private string playerBulletTag = "Proyectil";
    [SerializeField] private string bossBulletTag = "BossProyectile";

    // Update is called once per frame
    void Update()
    {
        // Obtener todas las balas del jugador y del Boss
        GameObject[] playerBullets = GameObject.FindGameObjectsWithTag(playerBulletTag);
        GameObject[] bossBullets = GameObject.FindGameObjectsWithTag(bossBulletTag);

        // Contar el total de balas
        int totalBullets = playerBullets.Length + bossBullets.Length;

        // Actualizar el texto en la UI
        bulletCounterText.text = "Balas: " + totalBullets;
    }
}
