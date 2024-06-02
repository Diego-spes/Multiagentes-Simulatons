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
        // Avanzar en la dirección en la que está mirando la bala
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }

    public void Move(Vector2 direction)
    {
        // Ajustar la rotación de la bala para que apunte en la dirección especificada
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Ajuste -90 grados si la bala está apuntando hacia arriba inicialmente
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
