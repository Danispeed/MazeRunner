using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    public static playerManager Ins { get; private set; }

    private List<playerMovement> players = new List<playerMovement>();

    void Awake()
    {
        if (Ins == null)
        {
            Ins = this;
        }
        else if (Ins != this)
        {
            Destroy(gameObject);
        }
    }

    public void RegisterPlayer(playerMovement player)
    {
        if (!players.Contains(player))
        {
            players.Add(player);
        }
    }

    public void UnregisterPlayer(playerMovement player)
    {
        if (players.Contains(player))
        {
            players.Remove(player);
        }
    }

    public void EnableAllPlayersMovement(int direction)
    {
        foreach (playerMovement player in players)
        {
            player.canMove(direction);
        }
    }
}
