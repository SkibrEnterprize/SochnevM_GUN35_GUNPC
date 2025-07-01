using UnityEngine;

public class EndOfField : MonoBehaviour
{
    [SerializeField] private Team _endOfFieldFor;
    public Team EndOfTeamFor => _endOfFieldFor;
}
