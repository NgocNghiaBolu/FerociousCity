using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAI : MonoBehaviour
{
    public GameObject[] prefabAI;
    public int AiToSpawn;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        int count = 0;
        while(count < AiToSpawn)
        {
            int randomIndex = Random.Range(0, prefabAI.Length);

            GameObject obj = Instantiate(prefabAI[randomIndex]);
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            obj.GetComponent<WaypointNavi>().currentWp = child.GetComponent<Waypoint>();//random vi tri xuat hien cua character

            obj.transform.position = child.position;
            yield return new WaitForSeconds(1f);

            count++;
        }
    }
}
