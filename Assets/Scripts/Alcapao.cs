using UnityEngine;

public class Alcapao : Interativo
{
    public override void Interagir()
    {
        Player player = GameObject.FindObjectOfType<Player>();
        foreach(Item item in player.items)
        {
            if(item != null && item.GetType() == typeof(Key))
            {
                Debug.Log("abrido");
            } else
            {
                Debug.Log("nao tem chave");
            }
        }
    }
}
