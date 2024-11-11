using UnityEngine;

public class SpellPickup : MonoBehaviour
{
    [SerializeField]
    private GameObject gameState;
    private GameState gameStateScript;
    private SoundController soundController;

    private void Start()
    {
        gameStateScript = gameState.GetComponent<GameState>();
        soundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spell")
        {
            int currentSpells = gameStateScript.getSpellsPickedUp();
            gameStateScript.setSpellsPickedUp(currentSpells + 1);
            soundController.PlaySpellCollect();
            Destroy(collision.gameObject);
        }
    }
}
