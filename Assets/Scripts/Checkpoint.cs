using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject display;

    private CheckpointSystem checkpointSystem;

    void Awake()
    {
        checkpointSystem = GetComponentInParent<CheckpointSystem>();
        Deactivate();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            var hoverCraftModel = collider.GetComponent<HoverCraftModel>();
            if (hoverCraftModel.networkObject.IsOwner)
            {
                checkpointSystem.MoveToNextCheckpoint();
            }
        }
    }
}
