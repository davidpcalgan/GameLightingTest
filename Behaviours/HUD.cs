using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject health, energy, asthma, inventory;

    // Start is called before the first frame update
    void Start()
    {
        GameTime.isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        health.transform.localScale = new Vector3(Player.Instance.health / 100, 1, 1);
        energy.transform.localScale = new Vector3(Player.Instance.energy / 100, 1, 1);
        asthma.transform.localScale = new Vector3(Player.Instance.asthma / 100, 1, 1);

        inventory.SetActive (GameTime.isPaused);
    }
}
