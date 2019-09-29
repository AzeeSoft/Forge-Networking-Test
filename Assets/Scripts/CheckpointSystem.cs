using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : SingletonMonoBehaviour<CheckpointSystem>
{
    public Checkpoint[] checkpoints;
    public int lapsCompleted = 0;

    public event Action onLapCompleted;

    private int curCheckpoint = 0;

    new void Awake()
    {
        base.Awake();
        checkpoints = GetComponentsInChildren<Checkpoint>(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        checkpoints[curCheckpoint].Activate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToNextCheckpoint()
    {
        checkpoints[curCheckpoint].Deactivate();
        curCheckpoint++;

        if (curCheckpoint >= checkpoints.Length)
        {
            curCheckpoint %= checkpoints.Length;
            lapsCompleted++;
            onLapCompleted?.Invoke();
        }

        checkpoints[curCheckpoint].Activate();
    }
}
