using System;
using UnityEngine;
using UnityEngine.Events;

public class ResourcesCreation : MonoBehaviour
{
    [SerializeField] private float _timeBetweenOreSpawn;

    [SerializeField] private int _amountOfOre;
    
    [SerializeField] private int _maxAmountOfOre;

    [SerializeField] private float _timeBetweenWoodSpawn;
    
    [SerializeField] private int _amountOfWood;

    [SerializeField] private int _maxAmountOfWood;

    [SerializeField] private CollectResource _collectResource;

    [SerializeField] private PlayerInventory _playerInventory;
    
    public event Action<int> OreAmountChanged;
    public event Action<int> WoodAmountChanged;

    public event Action<int> OreIsCollected;
    public event Action<int> WoodIsCollected;
    
    private float _currentOreTime;
    private float _currentWoodTime;

    private void FixedUpdate()
    {
        _currentOreTime += Time.fixedDeltaTime;
        _currentWoodTime += Time.fixedDeltaTime;
        
        if (_currentOreTime >= _timeBetweenOreSpawn && _amountOfOre < _maxAmountOfOre)
        {
            _amountOfOre++;
            _currentOreTime = 0;
            OreAmountChanged?.Invoke(_amountOfOre);
        }
        if (_currentWoodTime >= _timeBetweenWoodSpawn && _amountOfWood < _maxAmountOfWood)
        {
            _amountOfWood++;
            _currentWoodTime = 0;
            WoodAmountChanged?.Invoke(_amountOfWood);
        }
        if (_amountOfOre > _maxAmountOfOre)
        {
            _amountOfOre = _maxAmountOfOre;
        }
        if (_amountOfWood > _maxAmountOfWood)
        {   
            _amountOfWood = _maxAmountOfWood;
        }
    }

    private void OnEnable()
    {
        _collectResource.OreCollected += CollectOre;
        _collectResource.WoodCollected += CollectWood;
    }
    private void OnDisable()
    {
        _collectResource.OreCollected -= CollectOre;
        _collectResource.WoodCollected -= CollectWood;
    }
    
    private void CollectOre()
    {
        if (_amountOfOre > 0 && _amountOfOre <= _playerInventory.MaxOreInInventory)
        {
            OreIsCollected?.Invoke(_amountOfOre);
            _amountOfOre = 0;
            OreAmountChanged?.Invoke(_amountOfOre);
        }
        else if (_amountOfOre > 0 && _amountOfOre > _playerInventory.MaxOreInInventory)
        {
            _amountOfOre -= _playerInventory.MaxOreInInventory;
            OreIsCollected?.Invoke(_playerInventory.MaxOreInInventory);
            OreAmountChanged?.Invoke(_amountOfOre);
        }
    }
    private void CollectWood()
    {
        if (_amountOfWood > 0 && _amountOfWood <= _playerInventory.MaxWoodInInventory)
        {
            WoodIsCollected?.Invoke(_amountOfWood);
            _amountOfWood = 0;
            WoodAmountChanged?.Invoke(_amountOfWood);
        }
        else if (_amountOfWood > 0 && _amountOfWood > _playerInventory.MaxWoodInInventory)
        {
            _amountOfWood -= _playerInventory.MaxWoodInInventory;
            WoodIsCollected?.Invoke(_playerInventory.MaxWoodInInventory);
            WoodAmountChanged?.Invoke(_amountOfWood);
        }
    }
}
