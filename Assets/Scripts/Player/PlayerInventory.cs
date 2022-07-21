using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private ResourcesCreation _resourcesCreation;

    [SerializeField] private int _oreAmount;
    [SerializeField] private int _woodAmount;
    
    [SerializeField] private int _maxOreAmmountInOnventory;
    [SerializeField] private int _maxWoodAmmountInOnventory;
    
    public int MaxOreInInventory => _maxOreAmmountInOnventory - _oreAmount;
    public int MaxWoodInInventory => _maxWoodAmmountInOnventory - _woodAmount;

    public int OreAmount => _oreAmount;
    
    public int WoodAmount => _woodAmount;
    
    private void OnEnable()
    {
        _resourcesCreation.OreIsCollected += AddOre;
        _resourcesCreation.WoodIsCollected += AddWood;
    }

    private void OnDisable()
    {
        _resourcesCreation.OreIsCollected -= AddOre;
        _resourcesCreation.WoodIsCollected -= AddWood;
    }

    private void AddOre(int value)
    {
        _oreAmount += value;
    }

    private void AddWood(int value)
    {
        _woodAmount += value;
    }
}