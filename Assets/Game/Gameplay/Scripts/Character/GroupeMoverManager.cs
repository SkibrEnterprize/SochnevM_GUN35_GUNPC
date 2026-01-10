using Entities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GroupeMoverManager : MonoBehaviour
{
    private List<CharacterEntity> _character;

    private void Awake()
    {
        _character = new List<CharacterEntity>();
    }

    private void Update()
    {
        //if (_character.Count > 1)
        //{
        //    foreach (CharacterEntity character in _character) {
                
        //}
    }
    public void AddInGroup(CharacterEntity entity)
    { 
        _character.Add(entity);
        entity.GetComponent<CharacterGroupMover>().enabled = true;
    }
    public void ClearGroup()
    { _character.Clear(); }
}
