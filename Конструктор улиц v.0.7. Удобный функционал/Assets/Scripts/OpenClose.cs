using UnityEngine;

public class OpenClose : MonoBehaviour
{
    public GameObject @object;
    public bool isActive = false;

    public GameObject[] gameObjects;

    private void Start()
    {
        ChangeState();
    }

    public void ChangeState()
    {
        @object.SetActive(isActive);
        foreach (var item in gameObjects)
        {
            item.SetActive(isActive);
        }
        isActive = !isActive;
    }
}
