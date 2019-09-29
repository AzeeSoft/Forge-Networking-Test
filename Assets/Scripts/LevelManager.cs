using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using Cinemachine;
using UnityEngine;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    public bool soloServer = false;
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public Transform spawnPointsHolder;
    public Randomizer<Transform> spawnPointsRandomizer;

    public int totalLaps;
    public HUD hud;

    public HoverCraftModel playerHoverCraftModel;

    new void Awake()
    {
        base.Awake();
        if (totalLaps <= 0)
        {
            totalLaps = 1;
        }

        ResetSpawnPointsRandomizer();

        if (soloServer)
        {
            // Zero connections allowed to this server
            NetWorker server = new UDPServer(0);
//            server.playerAccepted += OnServerAccepted;
            ((UDPServer) server).Connect();

            // Block all connections to skip the validation phase
            ((IServer) server).StopAcceptingConnections();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (NetworkManager.Instance.IsServer)
        {
            NetworkManager.Instance.Networker.IteratePlayers(player =>
            {
                var spawnTransform = spawnPointsRandomizer.GetRandomItem();
                print(spawnTransform.position);
                var hoverCraftModel = NetworkManager.Instance.InstantiateHoverCraft(0,
                    position: spawnTransform.position, rotation: spawnTransform.rotation,
                    sendTransform: true) as HoverCraftModel;
                hoverCraftModel.MoveTo(spawnTransform.position, spawnTransform.rotation);
                hoverCraftModel.networkObject.ownershipChanged += sender =>
                {
                    hoverCraftModel.networkObject.SendRpc(HoverCraftBehavior.RPC_MOVE_TO, Receivers.Owner,
                        spawnTransform.position, spawnTransform.rotation);
                };
                hoverCraftModel.networkObject.AssignOwnership(player);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void ResetSpawnPointsRandomizer()
    {
        spawnPointsRandomizer = new Randomizer<Transform>(new List<Transform>());
        if (spawnPointsHolder == null)
        {
            spawnPointsRandomizer.items.Add(transform);
        }
        else
        {
            for (int i = 0; i < spawnPointsHolder.childCount; i++)
            {
                spawnPointsRandomizer.items.Add(spawnPointsHolder.GetChild(i));
            }
        }

        spawnPointsRandomizer.Shuffle();
    }

    public void SetPlayerModel(HoverCraftModel hoverCraftModel)
    {
        playerHoverCraftModel = hoverCraftModel;
        cinemachineVirtualCamera.Follow = hoverCraftModel.camFollow;
        cinemachineVirtualCamera.LookAt = hoverCraftModel.camFollow;
    }

    public void OnRaceFinished()
    {
        hud.EndGame();
    }
}