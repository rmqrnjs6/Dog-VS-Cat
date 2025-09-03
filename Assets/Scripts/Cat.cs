using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{

    public RectTransform front;

    public GameObject hungryCat;
    public GameObject FullCat;

    float full = 5.0f;
    float energy = 0.0f;
    float speed = 0.05f;

    public int type;



    bool isFull = false;

    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(-9.0f, 9.0f);
        float y = 30.0f;
        transform.position = new Vector2(x, y);

        if (type == 1)
        {
            speed = 0.05f;
            full = 5f;
        }
        else if (type == 2)
        {
            speed = 0.02f;
            full = 10f;
        }
        else if (type == 3)
        {
            speed = 0.04f;
            full = 3f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (energy < full)
        {
            transform.position += Vector3.down * speed;
            if (transform.position.y < -16.0f)
            {
                GameManager.Instance.GameOver();
            }
        }
        else
        {
            if(transform.position.x > 0)
            {
                transform.position += Vector3.right * speed;
            }
            else
            {
                transform.position += Vector3.left * speed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.CompareTag("Food"))
        {
            if(energy < full)
            {
                energy += 1.0f;
                front.localScale = new Vector3(energy / full, 1, 1);
                Destroy(collision.gameObject);

                if(energy == 5.0f)
                {
                    if (!isFull)
                    {
                        isFull = true;
                        hungryCat.SetActive(false);
                        FullCat.SetActive(true);
                        Destroy(gameObject, 3.0f);
                        GameManager.Instance.AddScore();
                    }
                }
            }
        }
    }
}
