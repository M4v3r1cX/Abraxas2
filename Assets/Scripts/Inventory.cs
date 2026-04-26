using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found D:");
            return;
        }

        instance = this;
    }
    #endregion

    public List<Item> items = new List<Item>();
    public int space = 20;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public bool Add(Item item)
    {
        bool ret = false;
        if (!item.isDefaultItem)
        {
            if (items.Count <= space)
            {
                items.Add(item);
                if (onItemChangedCallback != null)
                {
                    onItemChangedCallback.Invoke();        
                }
                ret = true;
            }
            else
            {
                Debug.Log("Not enough space on inventory");
            }
        }
        return ret;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();        
        }
    }
}
