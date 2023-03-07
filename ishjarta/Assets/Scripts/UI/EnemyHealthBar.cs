using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    Slider slider;
    [SerializeField]
    GameObject enemygo;
    Enemy enemy;
    
    
    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        enemy = enemygo.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.maxValue= enemy.MaxHealth;
        slider.value = enemy.CurrentHealth;
    }
}
