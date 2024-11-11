using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Activate : MonoBehaviour
{
    private bool colliding = false;
    private SoundController soundController;
    [SerializeField]
    private GameObject comboObject;
    private float xOffset = 0.5f;
    private float yOffset = 0.25f;
    private KeyCode keyCode;
    private GameObject currentCombo;
    private float totalWidth;
    private bool sigilCompleted = false;
    List<string> keys;
    private int spellsNeeded;
    GameState gameStateScript;
    private int randomSprite;

    // Update is called once per frame
    private void Start()
    {
        spellsNeeded = 3;
        gameStateScript = GameObject.Find("GameState").GetComponent<GameState>();
        randomSprite = Random.Range(1, 6);
        Sprite sigilSprite = Resources.Load<Sprite>("Sprites/Sygil/complete_sygil" + randomSprite);
        this.GetComponent<SpriteRenderer>().sprite = sigilSprite;
        soundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
    }

    void Update()
    {
        int spellsPickedUp = gameStateScript.getSpellsPickedUp();
        if (colliding && !sigilCompleted && spellsPickedUp >= spellsNeeded)
        {
            if (Input.GetKey(KeyCode.Space) && transform.childCount == 0)
            {
                displayCombo();
            }

            if (transform.childCount > 0)
            {
                keys = comboObject.GetComponent<Combo>().getKeys();
                string target = keys[0];
                if (System.Enum.TryParse(target, true, out keyCode))
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        keys = comboObject.GetComponent<Combo>().removeKey(target);
                        if (keys.Count == 0)
                        {
                            completeSigil();
                        } else
                        {
                            hideCombo();
                            Vector3 currentComboPos = this.gameObject.transform.localPosition + new Vector3(0, yOffset, 0);
                            currentCombo = Instantiate(comboObject, currentComboPos, this.gameObject.transform.rotation, this.gameObject.transform);
                            formatKeys(currentCombo, xOffset, totalWidth, keys);
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            colliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            colliding = false;
            hideCombo();
            soundController.StopSigilFormation();
        }
    }

    private void displayCombo()
    {
        Combo combo = comboObject.GetComponent<Combo>();
        combo.generateCombo(gameStateScript.getDifficulty());
        soundController.PlaySigilFormation();

        List<string> keys = combo.getKeys();
        Vector3 currentComboPos = this.gameObject.transform.localPosition + new Vector3(0, yOffset, 0);
        currentCombo = Instantiate(comboObject, currentComboPos, this.gameObject.transform.rotation, this.gameObject.transform);
        totalWidth = (keys.Count - 1) * xOffset;
        formatKeys(currentCombo, xOffset, totalWidth, keys);
    }

    private void hideCombo()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void formatKeys(GameObject currentCombo, float xOffset, float totalWidth, List<string> keys)
    {
        for (int i = 0; i < keys.Count; i++)
        {
            GameObject keyObject = currentCombo.GetComponent<Combo>().getKeyObject();
            GameObject key = Instantiate(keyObject, currentCombo.transform.transform.localPosition, currentCombo.transform.rotation, currentCombo.transform);
            float offset = -totalWidth / 2 + i * xOffset;
            key.transform.localPosition = currentCombo.transform.localPosition + new Vector3(offset, 0, 0);
            Sprite letter = Resources.Load<Sprite>("Sprites/Keys/" + keys[i]);
            if (letter != null)
            {
                key.GetComponent<SpriteRenderer>().sprite = letter;
            }
        }
    }

    private void completeSigil()
    {
        hideCombo();
        gameStateScript.setSpellsPickedUp(gameStateScript.getSpellsPickedUp() - spellsNeeded);
        this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Sygil/sygil" + randomSprite);
        sigilCompleted = true;
        soundController.PlaySigilCompletion();
    }

    public bool getCompleted()
    {
        return sigilCompleted;
    }
}
