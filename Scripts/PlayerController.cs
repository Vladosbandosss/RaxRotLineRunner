using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    float playerYpos;
    void Start()
    {
        playerYpos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSwithch();
    }
    void ChangeSwithch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerYpos = -playerYpos;
            transform.position = new Vector3(transform.position.x, playerYpos, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.UpdateLives();
            GameManager.instance.Shake();
        }
    }
}
