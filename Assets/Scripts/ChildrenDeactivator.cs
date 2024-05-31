using UnityEngine;

public class ChildrenDeactivator : MonoBehaviour
{
    [SerializeField] private Transform _parent;

    public void Deactivate()
    {
        foreach (Transform child in _parent)
        {
            child.gameObject.SetActive(false);
        }
    }
}

