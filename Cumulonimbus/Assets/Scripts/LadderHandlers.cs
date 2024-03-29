using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderHandlers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        float width = GetComponent<SpriteRenderer>().size.x;
        float height = GetComponent<SpriteRenderer>().size.y;
        Transform topHandler = transform.GetChild(0).transform;
        Transform bottomHandler = transform.GetChild(1).transform;
        topHandler.position = new Vector3(transform.position.x, transform.position.y + (height / 2), 0);
        bottomHandler.position = new Vector3(transform.position.x, transform.position.y - (height / 2), 0);
        GetComponent<BoxCollider2D>().offset = Vector2.zero;
        GetComponent<BoxCollider2D>().size = new Vector2(width, height);
    }
}
