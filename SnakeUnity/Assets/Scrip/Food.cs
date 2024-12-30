
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RandomPosition();
    }

    private void RandomPosition()
    {
        int x = Random.Range(-15, 15);
        int y = Random.Range(-8, 8);

        transform.position = new Vector2(x, y); 
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        int layer = other.gameObject.layer;

        if (layer == LayerMask.NameToLayer("SnakeHead"))
        {
            RandomPosition();
            other.GetComponent<SnakeToroidal>().Grow();
            GameManager.Instance.AddScore();
        }
    }
}
