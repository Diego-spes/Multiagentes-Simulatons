using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float x, y, w, h; // variables para los ejes
    [SerializeField] float speed; // velocidad normal
    [SerializeField] float focusSpeed; // velocidad en modo focus
    private bool isFocused = false; // estado para el modo focus

    private Vector2 screenBounds;
    private float playerWidth;
    private float playerHeight;
    private Rigidbody2D rb;

    void Start()
    {
        // Calcular los l�mites de la pantalla
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        playerWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x + w; // Extensi�n horizontal del jugador
        playerHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y + h; // Extensi�n vertical del jugador

        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float currentSpeed = isFocused ? focusSpeed : speed; // usar la velocidad seg�n el estado de focus

        Vector2 move = new Vector2(x, y) * currentSpeed * Time.deltaTime;
        Vector2 newPosition = rb.position + move;

        // Limitar la posici�n del jugador dentro de los l�mites de la pantalla
        newPosition.x = Mathf.Clamp(newPosition.x, screenBounds.x * -1 + playerWidth, screenBounds.x - playerWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, screenBounds.y * -1 + playerHeight, screenBounds.y - playerHeight);

        rb.MovePosition(newPosition);
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal"); // obtener el input de la flecha derecha o izquierda
        y = Input.GetAxisRaw("Vertical"); // obtener el input de la flecha arriba o abajo

             isFocused = Input.GetKey(KeyCode.X); // verifica si la tecla "x" est� siendo mantenida presionada
    }

 
}