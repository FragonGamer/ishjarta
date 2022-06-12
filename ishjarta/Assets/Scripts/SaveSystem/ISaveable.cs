using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    abstract object SaveState();
    abstract void LoadState(object state);
}
