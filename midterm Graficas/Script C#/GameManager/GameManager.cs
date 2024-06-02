using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState actualState;
    public static event Action<GameState> OnGameStateChanged;
    [SerializeField] GameObject player;
    [SerializeField] GameObject Boss;
    [SerializeField] GameObject GameOverScreen;
    [SerializeField] GameObject Victory;
    [SerializeField] GameObject[] enemies; // Array de enemigos normales

    [SerializeField] float part1Duration = 90f; // Duración de la parte 1 en segundos (1 minuto y medio)
    [SerializeField] float part1Timer;
    private EnemyPool enemyPool; // Referencia al EnemyPool

    private AudioSource audioSource; // Referencia al AudioSource

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Existe más de un game manager en la escena");
        }

        // Encuentra el AudioSource en el GameManager
        audioSource = GetComponent<AudioSource>();

        // Encuentra la EnemyPool en la escena
        enemyPool = FindObjectOfType<EnemyPool>();

        if (enemyPool == null)
        {
            Debug.LogError("EnemyPool no encontrado en la escena");
        }

        ChangeState(GameState.Setup);
    }

    private void Start()
    {
        if (enemyPool == null)
        {
            enemyPool = FindObjectOfType<EnemyPool>();

            if (enemyPool == null)
            {
                Debug.LogError("EnemyPool no encontrado en la escena");
            }
        }

        // Comenzar en el estado Part1 cuando inicia el juego
        ChangeState(GameState.Part1);
        PlayMusic(); // Reproduce la música al inicio del juego
    }

    private void Update()
    {
        if (actualState == GameState.Part1)
        {
            part1Timer -= Time.deltaTime;
            if (part1Timer <= 0f)
            {
                ChangeState(GameState.Fase1);
            }
        }
    }

    public void ChangeState(GameState newState)
    {
        actualState = newState;
        switch (newState)
        {
            case GameState.Setup:
                SetupState();
                break;
            case GameState.Part1:
                Part1();
                break;
            case GameState.Fase1:
                Fase1();
                break;
            case GameState.Fase2:
                Fase2();
                break;
            case GameState.Fase3:
                Fase3();
                break;
            case GameState.Victory:
                Win();
                break;
            case GameState.Lose:
                Lose();
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }

    void SetupState()
    {
        // Configuración inicial del juego
        GameOverScreen.SetActive(false);
        Boss.SetActive(false);
    }

    void Win()
    {
        Time.timeScale = 0;
        // Lógica cuando el jugador gana
        Victory.SetActive(true);
    }

    void Lose()
    {
        Time.timeScale = 0;
        // Lógica cuando el jugador pierde
        GameOverScreen.SetActive(true);
    }

    void Part1()
    {
        part1Timer = part1Duration;

        // Desactivar al jefe y activar a los enemigos normales
        Boss.SetActive(false);
    }

    void Fase1()
    {
        // Activar al jefe y desactivar a los enemigos normales
        Boss.SetActive(true);
    }

    void Fase2()
    {
        // Implementar lógica para la fase 2
    }

    void Fase3()
    {
        // Implementar lógica para la fase 3
    }

    void PlayMusic()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogError("No AudioSource found on GameManager.");
        }
    }

    public enum GameState
    {
        Setup,
        Part1,
        Fase1,
        Fase2,
        Fase3,
        Victory,
        Lose
    }
}