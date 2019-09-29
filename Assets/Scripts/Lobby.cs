using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public int mainSceneBuildIndex = 2;

    public TextMeshProUGUI playerList;
    public GameObject startBtnObject;

    // Start is called before the first frame update
    void Start()
    {
        startBtnObject.SetActive(NetworkManager.Instance.IsServer);
    }

    // Update is called once per frame
    void Update()
    {
        if (NetworkManager.Instance)
        {
            playerList.text = "Players Joined: " +
                              (NetworkManager.Instance.Networker.Players.Count +
                               (NetworkManager.Instance.IsServer ? 0 : 1)).ToString();
        }
    }

    public void StartGame()
    {
        if (NetworkManager.Instance.IsServer)
        {
            SceneManager.LoadScene(mainSceneBuildIndex, LoadSceneMode.Single);
            ((IServer) NetworkManager.Instance.Networker).StopAcceptingConnections();
        }
    }
}