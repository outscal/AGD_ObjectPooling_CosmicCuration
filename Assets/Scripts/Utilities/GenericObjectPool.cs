using System;
using System.Collections.Generic;

namespace CosmicCuration.Utilities
{
    public class PooledItem<T>
    {
        public T Item;
        public bool IsUsed;
    }
    
    public class GenericObjectPool<T> where T:class
    {
        private List<PooledItem<T>> pooledItems = new();

        protected T GetItem()
        {
            if (pooledItems.Count > 0)
            {
                PooledItem<T> item = pooledItems.Find(item => !item.IsUsed);
                if (item != null)
                {
                    item.IsUsed = true;
                    return item.Item;
                }
            }
            return CreateNewPooledItem();
        }

        private T CreateNewPooledItem()
        {
            PooledItem<T> newItem = new PooledItem<T>();
            newItem.Item = CreateItem();
            newItem.IsUsed = true;
            pooledItems.Add(newItem);
            return newItem.Item;
        }

        protected T CreateItem()
        {
            throw new NotImplementedException("Not implement by child class!");
        }
        
    }
}
