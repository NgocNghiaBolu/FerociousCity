using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapUI : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        //swap
        Vector3 newPosition = player.position;//tao bien gan vo vi tri player
        newPosition.y = transform.position.y;// vij tri moi cua y se duoc gan vo tranform
        transform.position = newPosition;// tran foem dc gan lai cho bien da tao


        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
