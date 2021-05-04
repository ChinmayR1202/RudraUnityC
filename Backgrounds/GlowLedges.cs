using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowLedges : MonoBehaviour
{

    [SerializeField] private SpriteRenderer glow;
    private float alpha;
    private bool isGone;

    // Start is called before the first frame update
    void Start()
    {
        alpha = 0;
        isGone = true;
    }

    private void Update()
    {
        if (isGone && alpha > 0.1f)
        {
            alpha -= 0.1f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            isGone = false;

            if(alpha < 1)
            {
                alpha += 0.1f;
            }
        }
    }

}
