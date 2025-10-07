using UnityEngine;

public class GasBlockDestructStrategy : IDestructStrategy
{
    public void Destruct(Block block)
    {
        Debug.Log("GasBlockDestructStrategy Destructed");
        // Implement gas block specific destruction logic here
    }
}
