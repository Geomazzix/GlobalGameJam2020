using UnityEngine;

/// <summary>
/// Item types.
/// </summary>
public enum EItem
{
    METAL = 0,
    WOOD,
    CLOTH,
    RUBBER,
    PLASTIC
}

/// <summary>
/// Represents an interactive item in the game for the player, used in order to create parts to repair toys.
/// </summary>
public class Item : PickupItem
{
    private static int m_ItemCount = 0;
    private int m_Id;

    //[SerializeField] private string m_Name;
    [SerializeField] private EItem m_Type;

    public int Id => m_Id;
    //public string Name => m_Name;
    public EItem Type => m_Type;

    private void Awake()
    {
        m_Id = m_ItemCount;
        ++m_ItemCount;
    }
}
