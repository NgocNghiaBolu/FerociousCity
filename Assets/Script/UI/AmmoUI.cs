using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public Text ammoAmountText;
    public Text magAmountText;

    public static AmmoUI instance;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateAmmo(int ammoAmount)
    {
        ammoAmountText.text = "Ammo " + ammoAmount;
    }

    public void UpdateMag(int magAmount)
    {
        magAmountText.text = "Mag " + magAmount;
    }

}
