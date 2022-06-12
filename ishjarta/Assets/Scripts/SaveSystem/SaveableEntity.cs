using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveableEntity : MonoBehaviour
{

    [SerializeField] private string id;
    public string Id => id;
    
    [ContextMenu("Generate Id")]
    private void GenerateId()
    {
        id = Guid.NewGuid().ToString();
    }
    //find all ISaveable components on gameobject
    public object SaveState()
    {
        var state = new Dictionary<string, object>();
        foreach (var saveable in GetComponents<ISaveable>())
        {
            state[saveable.GetType().ToString()] = saveable.SaveState();
        }
        return state;
        
    }

    public void LoadState(object state)
    {
        var stateDict = (Dictionary<string, object>)state;
        foreach (var saveable in GetComponents<ISaveable>())
        {
            var typeString = saveable.GetType().ToString();
            if (stateDict.TryGetValue(typeString,out object savedState))
            {
                saveable.LoadState(savedState);
            }
        }
    }
}
