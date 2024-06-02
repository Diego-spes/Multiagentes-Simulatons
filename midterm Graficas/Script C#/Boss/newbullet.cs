using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newbullet : MonoBehaviour
{
    public Camera MainCamera;
    [SerializeField] public float cam_height;
    [SerializeField] public float cam_width;
    [SerializeField] Vector3 bullet;
    [SerializeField] float bullet_velocity;

    void Start()
    {
        cam_width = cam_height * MainCamera.aspect;
        cam_height = MainCamera.orthographicSize;
        bullet = new Vector3(0,1,0);
        bullet_velocity = 5f;
    }


    void Update()
    {

        transform.Translate( bullet* bullet_velocity * Time.deltaTime);


        if (transform.position.x > cam_width || transform.position.x < -cam_width)
        {

            bullet_velocity *= -1f;
        } else if (transform.position.y > cam_height || transform.position.y < -cam_height)
        {

            bullet_velocity *= -1f;
        }





    }
}

