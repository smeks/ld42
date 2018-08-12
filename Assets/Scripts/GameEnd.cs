using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public GameObject SurvivedText;
    public GameObject DiedText;


    public void Survived()
    {
        DiedText.SetActive(false);
        SurvivedText.SetActive(true);
    }


    public void Died()
    {
        DiedText.SetActive(true);
        SurvivedText.SetActive(false);
    }
}
