using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourcesController : MonoBehaviour
{
    public List<GameResource> resources;
    public int StartingCoins;
    public int StartingFire;
    public int StartingPlant;
    public int StartingWater;
    // Start is called before the first frame update
    void Start()
    {
        resources = new List<GameResource>();
        
        AddResource(ResourceType.GOLD, StartingCoins);
        AddResource(ResourceType.GOLD, StartingCoins);
        AddResource(ResourceType.FIRE, StartingFire);
        AddResource(ResourceType.PLANT, StartingPlant);
        AddResource(ResourceType.WATER, StartingWater);
    }

    private void Resources_ValueChanged(object sender, EventArgs e)
    {
        Debug.Log("Value has changed");
    }

    public bool AddResource(ResourceType resourceType, int amount)
    {
        GameResource find = new GameResource(resourceType, 0);
        if (resources.Contains(find))
            resources[resources.IndexOf(find)].addQuantity(amount);
        else
            resources.Add(new GameResource(resourceType, amount));
            
        return true;
    }
    public int GetResource(ResourceType resourceType)
    {
        GameResource find = new GameResource(resourceType, 0);
        if (resources.Contains(find))
            return resources[resources.IndexOf(find)].getQuantity();
        else
            return 0;
    }
    public bool RemoveResource(ResourceType resourceType, int amount)
    {
        Debug.Log("Se ha intentado quitar:" + amount + " de " + resourceType);
        Debug.Log("Primera evaluacion:" + (GetResource(resourceType) != 0));
        Debug.Log("Segunda evaluacion:" + ((GetResource(resourceType) - amount) >= 0));
        GameResource remove = new GameResource(resourceType, 0);
        if (GetResource(resourceType) != 0 && (GetResource(resourceType) - amount) >= 0)
        {
            resources[resources.IndexOf(remove)].removeQuantity(amount);
            return true;
        }
        else
            return false;
    }
    public int GetResourceByElement(ElementType type)
    {
        switch (type)
        {
            case ElementType.Fire:
                return GetResource(ResourceType.FIRE);
            case ElementType.Grass:
                return GetResource(ResourceType.PLANT);
            case ElementType.Water:
                return GetResource(ResourceType.WATER);
            default: return 0;
        }
    }
    public bool RemoveResourceByElement(ElementType type, int amount)
    {
        switch (type)
        {
            case ElementType.Fire:
                return RemoveResource(ResourceType.FIRE, amount);
            case ElementType.Grass:
                return RemoveResource(ResourceType.PLANT, amount);
            case ElementType.Water:
                return RemoveResource(ResourceType.WATER, amount);
            default: return false;
        }
    }
}
[System.Serializable]
public class GameResource
{
    [SerializeField]
    private ResourceType type;
    [SerializeField]
    private int quantity;
    public GameResource(ResourceType type, int quantity)
    {
        setType(type);
        setQuantity(quantity);
    }
    private void setQuantity(int quantity)
    {
        if (quantity < 0)
            Debug.Log("Se ha intentado establecer una cantidad negativa a un recurso");
        else
            this.quantity = quantity;
    }
    private void setType(ResourceType type)
    {
        this.type = type;
    }
    public ResourceType getType()
    {
        return this.type;
    }
    public int getQuantity()
    {
        return this.quantity;
    }
    public void addQuantity(int quantityToAdd)
    {
        if (quantityToAdd < 0)
            Debug.Log("No se puede añadir una cantidad negativa a un recurso");
        else
            setQuantity(getQuantity() + quantityToAdd);
    }
    public void removeQuantity(int quantityToRemove)
    {
        if (quantityToRemove < 0)
            Debug.Log("No se puede quitar una cantidad negativa a un recurso");
        else
            setQuantity(getQuantity() - quantityToRemove);
    }
    public override bool Equals(object obj)
    {
        return obj is GameResource resource &&
                type == resource.type;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), type);
    }
}
[System.Serializable]
public class ExtendedDictonary<Tkey, Tvalue> : Dictionary<Tkey, Tvalue>
{
    public Dictionary<Tkey, Tvalue> Items = new Dictionary<Tkey, Tvalue>();
    public ExtendedDictonary() : base() { }
    ExtendedDictonary(int capacity) : base(capacity) { }
    //
    // Do all your implementations here...
    //

    public event EventHandler ValueChanged;

    public void OnValueChanged(System.Object sender, EventArgs e)
    {
        EventHandler handler = ValueChanged;
        if (null != handler) handler(this, EventArgs.Empty);
    }

    public void AddItem(Tkey key, Tvalue value)
    {
        try
        {
            Items.Add(key, value);
            OnValueChanged(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    //
    // Similarly Do implementation for Update , Delete and Value Changed checks (Note: Value change can be monitored using a thread)
    //
}