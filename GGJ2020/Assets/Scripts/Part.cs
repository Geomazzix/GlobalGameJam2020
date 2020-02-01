using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to look up if the combined items are used.
/// </summary>
public enum EPart
{
    NONE = -1,

    LOBSTER_CLAW = 0,
    JET_PACK,
    ROBOT_ARM,
    ROWING_THINGY,
    KNIFE,
    WOODEN_LEG,
    CLOTH_HANGER,
    CAPTAIN_HOOK,
    UMBRELLA,
    FOX_TAIL,
    GARBAGE_BAG,
    SCREW_DRIVER,
    WHEEL,
    LATEX_BOOT,
    BALL,
}

/// <summary>
/// The combined items.
/// </summary>
public class Part : Item
{
    [SerializeField] private Vector3[] m_Pivots;
    private static readonly Dictionary<KeyValuePair<EItem, EItem>, EPart> s_LookUpTable = new Dictionary<KeyValuePair<EItem, EItem>, EPart>()
    {
        //Single matches without inverses.
        { new KeyValuePair<EItem, EItem>(EItem.METAL, EItem.CLOTH), EPart.CAPTAIN_HOOK},
        { new KeyValuePair<EItem, EItem>(EItem.METAL, EItem.RUBBER), EPart.SCREW_DRIVER },
        { new KeyValuePair<EItem, EItem>(EItem.METAL, EItem.WOOD), EPart.KNIFE },
        { new KeyValuePair<EItem, EItem>(EItem.PLASTIC, EItem.CLOTH), EPart.CLOTH_HANGER},
        { new KeyValuePair<EItem, EItem>(EItem.PLASTIC, EItem.METAL), EPart.JET_PACK },
        { new KeyValuePair<EItem, EItem>(EItem.PLASTIC, EItem.RUBBER), EPart.GARBAGE_BAG },
        { new KeyValuePair<EItem, EItem>(EItem.CLOTH, EItem.RUBBER), EPart.LATEX_BOOT },
        { new KeyValuePair<EItem, EItem>(EItem.PLASTIC, EItem.WOOD), EPart.ROWING_THINGY },
        { new KeyValuePair<EItem, EItem>(EItem.CLOTH, EItem.WOOD), EPart.UMBRELLA },
        { new KeyValuePair<EItem, EItem>(EItem.RUBBER, EItem.WOOD), EPart.WHEEL },

        //Duplicates.
        { new KeyValuePair<EItem, EItem>(EItem.CLOTH, EItem.CLOTH), EPart.FOX_TAIL },
        { new KeyValuePair<EItem, EItem>(EItem.PLASTIC, EItem.PLASTIC), EPart.LOBSTER_CLAW },
        { new KeyValuePair<EItem, EItem>(EItem.METAL, EItem.METAL), EPart.ROBOT_ARM },
        { new KeyValuePair<EItem, EItem>(EItem.RUBBER, EItem.RUBBER), EPart.BALL },
        { new KeyValuePair<EItem, EItem>(EItem.WOOD, EItem.WOOD), EPart.WOODEN_LEG }
    };

    public Vector3 this[int i] => m_Pivots[i];

    public EPart GetPossibleItemCombination(EItem itemLeft, EItem itemRight)
    {
        KeyValuePair<EItem, EItem> key = new KeyValuePair<EItem, EItem>(itemLeft, itemRight);
        if (s_LookUpTable.ContainsKey(key)) return s_LookUpTable[key];

        key = new KeyValuePair<EItem, EItem>(itemRight, itemLeft); //swapped key.
        return s_LookUpTable.ContainsKey(key) ? EPart.NONE : s_LookUpTable[key];
    }
}
