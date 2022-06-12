using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "passiv items/TestPeriodic")]

public class TestPeriodic : PassiveItem
{
    public override void triggerEffect()
    {
        Debug.Log("periodic test");
    }

    public override void removeEffect()
    {
        Inventory.instance.RemovePeriodiclePassiveItem(this);
    }
}
