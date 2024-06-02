
using UnityEngine;

public class Shoter : MonoBehaviour
{
    [SerializeField] float repeatRate;
    [SerializeField] GameManager.GameState currentState;
    float timeToNextPattern;

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameManager.GameState newState)
    {
        Debug.Log("Shooter received new state: " + newState);
        currentState = newState;

        // Solo reseteamos el tiempo si es la primera vez que se cambia de estado
        if (timeToNextPattern <= 0f)
        {
            timeToNextPattern = repeatRate;
        }
    }

    void Start()
    {
        // Iniciar en el estado actual
        GameManagerOnGameStateChanged(GameManager.Instance.actualState);
    }

    void Update()
    {
        // Reducimos el tiempo restante hasta el próximo patrón
        timeToNextPattern -= Time.deltaTime;

        // Si el tiempo restante llega a cero, llamamos a la función para generar el patrón correspondiente a la fase actual
        if (timeToNextPattern <= 0f)
        {
            GeneratePattern();

            // Reseteamos el tiempo restante para el próximo patrón
            timeToNextPattern = repeatRate;
        }
    }

    private void GeneratePattern()
    {
        // Determinar qué patrón de disparo generar y con qué parámetros según el estado actual del juego
        switch (currentState)
        {
            case GameManager.GameState.Fase1:
                FirePattern(90, 270, 30); // Patrón para la fase normal
                break;
            case GameManager.GameState.Fase2:
                FirePattern(180, 270, 30); // Patrón para la fase 2
                break;
            case GameManager.GameState.Fase3:
                FirePattern(0, 720, 60); // Patrón para la fase 3
                break;
        }
    }

    private void FirePattern(float startAngle, float endAngle, int amount)
    {
        float angleStep = (endAngle - startAngle) / amount;
        float angle = startAngle;

        for (int i = 0; i < amount + 1; i++)
        {
            float dirx = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float diry = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);
            Vector3 bullDirection = new Vector3(dirx, diry, 0f);
            Vector2 bulldir = (bullDirection - transform.position).normalized;
            GameObject bull = Pool.instance.GetBullets();
            bull.transform.position = transform.position;
            bull.SetActive(true);
            bull.GetComponent<Bullet>().Move(bulldir);

            angle += angleStep;
        }
    }
}