using UnityEngine;

public class Interativo : MonoBehaviour
{
    void Awake()
    {
        gameObject.tag = "Interativo";
        gameObject.layer = LayerMask.NameToLayer("Parede");
    }

    public void Interagir()
    {
        ///
        /// Colocar lógica para interação com o baú
        ///
        Destroy(gameObject);
    }
}
