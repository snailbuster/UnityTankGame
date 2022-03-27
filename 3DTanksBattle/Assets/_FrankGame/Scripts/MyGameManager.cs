using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    //≥ı ºÃπøÀŒª÷√
    public Transform posOne;
    public Transform posTwo;
    public GameObject tankPrefab;
    public Color tankOneColor;
    public Color tankTwoColor;

    public MyCamera camerControl;
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
       
        
        TankSpawn();
        if (camerControl != null)
        {
            camerControl.tanks = GameObject.FindGameObjectsWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TankSpawn()
    {
        GameObject tankOne = Instantiate(tankPrefab, posOne.position, posOne.transform.rotation);
        var tankOneControl =  tankOne.GetComponent<TankControl>();
        if (tankOneControl != null)
        {
            tankOneControl.tankType = TankType.Tank_One;
            MeshRenderer[] renderers = tankOne.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = tankOneColor;
            }

        }


        GameObject tankTwo = Instantiate(tankPrefab, posTwo.position, posTwo.transform.rotation);
        print(tankTwo.name + "tank2");
        var tankTwoControl = tankTwo.GetComponent<TankControl>();
        if (tankTwoControl != null)
        {
            tankTwoControl.tankType = TankType.Tank_Two;
            MeshRenderer[] renderers = tankTwo.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = tankTwoColor;
            }
        }

    }
}
