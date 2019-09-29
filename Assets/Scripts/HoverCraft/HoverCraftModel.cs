using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

public class HoverCraftModel : HoverCraftBehavior
{
    public class ServerData
    {
        public HashSet<uint> finishedHashSet = new HashSet<uint>();
        public int nextPosition = 1;
    }

    public Transform camFollow;
    public ServerData serverData;

    protected override void NetworkStart()
    {
        base.NetworkStart();

        OnOwnershipUpdated();
        networkObject.ownershipChanged += sender => { OnOwnershipUpdated(); };
    }

    // Start is called before the first frame update
    void Start()
    {
        if (networkObject.IsOwner)
        {
            CheckpointSystem.Instance.onLapCompleted += () =>
            {
                networkObject.lapsCompleted = CheckpointSystem.Instance.lapsCompleted;
            };
        }

        if (networkObject.IsServer)
        {
            serverData = new ServerData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (networkObject.IsServer)
        {
            print("Checking Finish: " + networkObject.lapsCompleted);
            if (!serverData.finishedHashSet.Contains(networkObject.NetworkId) &&
                networkObject.lapsCompleted >= LevelManager.Instance.totalLaps)
            {
                print("Finished");
                networkObject.SendRpc(RPC_FINISHED_RACE, Receivers.Owner, serverData.nextPosition);
                serverData.finishedHashSet.Add(networkObject.NetworkId);
                serverData.nextPosition++;
            }
        }
    }

    void OnOwnershipUpdated()
    {
        if (networkObject.IsOwner)
        {
            LevelManager.Instance.SetPlayerModel(this);
        }
    }

    public override void MoveTo(RpcArgs args)
    {
        MainThreadManager.Run(() =>
        {
            var position = args.GetNext<Vector3>();
            var rotation = args.GetNext<Quaternion>();
            MoveTo(position, rotation);
        });
    }

    public override void FinishedRace(RpcArgs args)
    {
        MainThreadManager.Run(() =>
        {
            print("Finished RPC");
            var racePosition = args.GetNext<int>();
            networkObject.racePosition = racePosition;
            LevelManager.Instance.OnRaceFinished();
        });
    }

    public void MoveTo(Vector3 pos, Quaternion rot)
    {
        print("Move to called: " + networkObject.IsOwner);
        transform.position = pos;
        transform.rotation = rot;
        print(transform.position);

        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;
    }
}