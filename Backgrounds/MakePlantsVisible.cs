using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePlantsVisible : MonoBehaviour
{

    [SerializeField] private float whatIsNumber;
    private SpriteRenderer plantAlpha;



    // Start is called before the first frame update
    void Start()
    {
        plantAlpha = GetComponent<SpriteRenderer>();
        
        if(whatIsNumber == 1)
        {
            GameEvents.lightBotStatue.OnBotStatuesLit += IncreaseAlpha;
        }

        if(whatIsNumber == 2)
        {
            GameEvents.lightTopStatue.OnTopStatuesLit += IncreaseAlpha;
        }
    }


    public void IncreaseAlpha()
    {
        plantAlpha.color = new Color(1, 1, 1, 1);
    }
}
