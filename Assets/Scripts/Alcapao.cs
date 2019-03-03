using UnityEngine;

public class Alcapao : Interativo
{
    public override void Interagir()
    {
        Player player = GameObject.FindObjectOfType<Player>();
        for(int i = 0; i < player.items.Length; i++)
        {
            if(player.items[i] != null && player.items[i].GetType() == typeof(Key))
            {
                Debug.Log("abrido");
                player.items[i] = null;
                GetComponent<Animator>().SetTrigger("Open");
                gameObject.layer = LayerMask.NameToLayer("Default");
                player.SlotKey.SetActive(false);
                return;
            }
        }

        Debug.Log("nao tem chave");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.SetActive(false);
        }
    }
}
