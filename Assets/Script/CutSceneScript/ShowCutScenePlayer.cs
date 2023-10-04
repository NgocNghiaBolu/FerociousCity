using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCutScenePlayer : MonoBehaviour
{
    public GameObject playerCutScene;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bus")
        {
            playerCutScene.SetActive(true);
        }
    }

}
