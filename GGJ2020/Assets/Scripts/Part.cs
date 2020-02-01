using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to look up if the combined items are used.
/// </summary>
public enum EPart
{
    NONE = -1,

    BALL = 5,
    CAPTAIN_HOOK,
    CLOTH_HANGER,
    FOX_TAIL,
    GARBAGE_BAG,
    JET_PACK,
    KNIFE,
    LATEX_BOOT,
    LOBSTER_CLAW,
    ROBOT_ARM,
    ROWING_THINGY,
    SCREW_DRIVER,
    UMBRELLA,
    WHEEL,
    WOODEN_LEG
}

/// <summary>
/// The combined items.
/// </summary>
public class Part : PickupItem
{
    private static int m_ItemCount = 0;
    private int m_Id;

    [SerializeField] private EPart m_Type;
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
    public int Id => m_Id;
    public EPart Type => m_Type;

    private void Awake()
    {
        m_Id = m_ItemCount;
        ++m_ItemCount;
    }

    public Vector3 this[int i] => m_Pivots[i];

    public static EPart GetPossibleItemCombination(EItem itemLeft, EItem itemRight)
    {
        KeyValuePair<EItem, EItem> key = new KeyValuePair<EItem, EItem>(itemLeft, itemRight);
        if (s_LookUpTable.ContainsKey(key)) return s_LookUpTable[key];

        key = new KeyValuePair<EItem, EItem>(itemRight, itemLeft); //swapped key.
        return !s_LookUpTable.ContainsKey(key) ? EPart.NONE : s_LookUpTable[key];
    }
}
