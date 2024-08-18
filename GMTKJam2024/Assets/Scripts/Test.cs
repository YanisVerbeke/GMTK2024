using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : Obstacle
{
    
    protected override void Move()
    {
        transform.Translate(Vector2.left * 2f * Time.deltaTime);

        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }
}
