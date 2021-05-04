using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeFriction : MonoBehaviour
{
    [SerializeField] private CharacterController2D playerController;
    [SerializeField] private Rigidbody2D player;
    private PhysicsMaterial2D iniMaterial;

    // Start is called before the first frame update
    void Start()
    {
        iniMaterial = player.sharedMaterial;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerController.m_Grounded)
            {
                player.transform.position = new Vector2(transform.position.x, player.transform.position.y);
            }
        }
    }
}
