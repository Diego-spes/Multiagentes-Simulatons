using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    [SerializeField] float Speed;

    private void OnEnable()
    {
        Invoke("destruir", 3f);
    }

    // Start is called before the first frame update
    void Start()
    {
        Speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        // Avanzar en la direcci�n en la que est� mirando la bala
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }

    public void Move(Vector2 direction)
    {
        // Ajustar la rotaci�n de la bala para que apunte en la direcci�n especificada
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Ajuste -90 grados si la bala est� apuntando hacia arriba inicialmente
    }

    private void destruir()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
