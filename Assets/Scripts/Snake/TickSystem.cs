using System;
using UnityEngine;

public class TickSystem : MonoBehaviour
{
    public static event Action OnTick; //one shared event__notify

    [SerializeField] private float tickInterval = 0.5f; //delay
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime; //keep track of time
        if (timer >= tickInterval)
        {
            timer -= tickInterval;
            OnTick?.Invoke(); //inform
        }
    }
}