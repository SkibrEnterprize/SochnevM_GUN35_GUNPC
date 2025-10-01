using System.Collections.Generic;
using UnityEngine;


public sealed class PlayerHolder : MonoBehaviour
{
    
    private static PlayerHolder _instance;

    public static PlayerHolder Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerHolder>();

                if (_instance == null)
                {
                    var go = new GameObject("PlayerHolder");
                    _instance = go.AddComponent<PlayerHolder>();
                }
            }
            return _instance;
        }
    }
   
    [Header("Список всех игроков")]
    [SerializeField] private List<Transform> _players = new List<Transform>();

    public IReadOnlyList<Transform> Players => _players;

   
    public void RegisterPlayer(Transform player)
    {
        if (player == null) return;
        if (!_players.Contains(player))
            _players.Add(player);
    }
       
    public void UnregisterPlayer(Transform player)
    {
        if (player == null) return;
        _players.Remove(player);
    }
        
    public void AutoRegister(Transform player)
    {
        RegisterPlayer(player);
    }

    public void AutoUnregister(Transform player)
    {
        UnregisterPlayer(player);
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}