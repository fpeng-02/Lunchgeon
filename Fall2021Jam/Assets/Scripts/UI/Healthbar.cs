using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Image healthbarFill;
    private Player player;

    void Start()
    {
        healthbarFill = GetComponent<Image>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healthbarFill.fillAmount = player.GetHealth() / player.GetMaxHealth();
    }
}
