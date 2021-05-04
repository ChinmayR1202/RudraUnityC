using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueManager : MonoBehaviour
{

    [SerializeField] private SpriteRenderer moss1;
    [SerializeField] private SpriteRenderer moss2;
    [SerializeField] private SpriteRenderer leaves;
    [SerializeField] private SpriteRenderer redTree;

    public MakePlantsVisible makePlantsVisible;

    public float statuesLit;
    private bool isBotStatueLit;
    private bool isTopStatueLit;

    // Start is called before the first frame update
    void Start()
    {
        statuesLit = 0;
        isBotStatueLit = false;
        isTopStatueLit = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        
        if(statuesLit == 2)
        {
            if (!isBotStatueLit)
            {
                moss1.color = new Color(1, 1, 1, 1);
                leaves.color = new Color(1, 1, 1, 1);
                isBotStatueLit = true;
            }

            GameEvents.lightBotStatue.LightBotStatues();
        }

        if(statuesLit == 4)
        {
            if (!isTopStatueLit)
            { 
                moss2.color = new Color(1, 1, 1, 1);
                redTree.color = new Color(1, 1, 1, 1);
                isTopStatueLit = true;
            }

            GameEvents.lightTopStatue.LightTopStatues();
        }

    }


    
}
