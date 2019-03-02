using UnityEngine;

public class Interativo : MonoBehaviour
{
    void Awake()
    {
        gameObject.tag = "Interativo";
        gameObject.layer = LayerMask.NameToLayer("Parede");
    }

    public virtual void Interagir() { }
}
