using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] public List<GameObject> rowsGO;
    [SerializeField] public List<TextMeshProUGUI> playerTexts;
    [SerializeField] private List<TextMeshProUGUI> playerPings;
    [SerializeField] public TextMeshProUGUI lobbyId;
    [SerializeField] public GameObject idGO;
    [SerializeField] private Player parent;
    private List<Player> players;

    public void SetNickName(int index, string nickname)
    {
        playerTexts[index].text = nickname;
    }

    public void SetPing(int index, double ping)
    {
        string formattedPing = ping.ToString("F2");

        playerPings[index].text = "Ping: " + formattedPing;
    }

    private void Start()
    {
        lobbyId.text = Singleton<GameHandler>.instance.lobbyName;
    }
    private void Update()
    {
        players = Singleton<GameHandler>.instance.players;
        foreach(var player in players)
        {
            rowsGO[player.index].SetActive(true);
            foreach (var player2 in players)
            {
                if (player2)
                {
                    player.UIController.SetNickName(player2.index, player2.netPlayer.playerNickText.text);
                }
            }
        }
    }
}
