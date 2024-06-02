using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
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
    private void Update()
    {
        gameObject.transform.Translate(Vector3.up * Time.deltaTime * Speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Boss")
        {
            gameObject.SetActive(false);
        }
        
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