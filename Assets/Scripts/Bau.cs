using UnityEngine;

public class Bau : Interativo
{
    public override void Interagir()
    {
        Player player = GameObject.FindObjectOfType<Player>();
        for (int i = 0; i < player.items.Length; i++)
        {
            if(player.items[i] == null)
            {
                GameObject chave = new GameObject("Chave");
                chave.transform.parent = player.transform;

                player.items[i] = chave.AddComponent<Key>();
                Destroy(this);
                return;
            }
        }

        Debug.Log("não tem espaço");
    }
}
