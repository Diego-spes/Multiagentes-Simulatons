
using UnityEngine;
using UnityEngine.UI;
public class HealthPlayer : MonoBehaviour
{
    [SerializeField] public float ActualHealth = 100;
    [SerializeField] public float MaxHealth = 100;
    [SerializeField] Image barraVida;
    [SerializeField] ScreenShake cameraShake; // Referencia al script CameraShake
    [SerializeField] float shakeDuration = 0.5f;
    [SerializeField] float shakeMagnitude = 0.2f;

    public void Damaged(float damage)
    {
        ActualHealth -= damage;
        Debug.Log("Player health is: " + ActualHealth);
        StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "BossProyectile" || other.transform.tag == "Boss"|| other.transform.tag == "Enemy")
        {
            Damaged(5f);
        }
    }

    private void Update()
    {
        barraVida.fillAmount = ActualHealth / MaxHealth;

        if (ActualHealth <= 0)
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Lose);
        }
    }
}