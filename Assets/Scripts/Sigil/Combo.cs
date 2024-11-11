using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    private List<string> keys;
    [SerializeField]
    private GameObject keyObject;
    private GameState gameState;
    // Easy => smallLetters
    // Medium => mediumLetters
    // Hard => hardLetters
    // Mystic => allLetters
    public const string DIFFICULTY_EASY = "Easy";
    public const string DIFFICULTY_MEDIUM = "Medium";
    public const string DIFFICULTY_HARD = "Hard";
    public const string DIFFICULTY_MYSTIC = "Mystic";
    private string[] smallLetters = new string[] {
        "H",
        "J"
    };
    private string[] mediumLetters = new string[] {
        "H",
        "J",
        "K",
        "L"
    };
    private string[] largeLetters = new string[] {
        "F",
        "G",
        "H",
        "J",
        "K",
        "L"
    };
    private string[] allLetters = new string[] {
        "B",
        "C",
        "E",
        "F",
        "G",
        "H",
        "I",
        "J",
        "K",
        "L",
        "M",
        "N",
        "O",
        "P",
        "Q",
        "R",
        "T",
        "U",
        "V",
        "X",
        "Y",
        "Z"
    };

    Dictionary<string, int> keyComboCount = new Dictionary<string, int>()
    {
        {DIFFICULTY_EASY, 2}, 
        {DIFFICULTY_MEDIUM, 3}, 
        {DIFFICULTY_HARD, 4},
        {DIFFICULTY_MYSTIC, 5}
    };
    private string difficulty;

    private void Start()
    {
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
        difficulty = gameState.getDifficulty();

    }
    public void generateCombo(string difficulty = DIFFICULTY_EASY)
    {
        keys = new List<string>();
        this.difficulty = difficulty;
        if (difficulty == DIFFICULTY_EASY)
        {
            setKeys(smallLetters);
        }
        else if (difficulty == DIFFICULTY_MEDIUM)
        {
            setKeys(mediumLetters);
        }
        else if (difficulty == DIFFICULTY_HARD)
        {
            setKeys(largeLetters);
        }
        else if (difficulty == DIFFICULTY_MYSTIC)
        {
            setKeys(allLetters);
        }
    }
    public List<string> getKeys()
    {
        return keys;
    }

    public List<string> removeKey(string key)
    {
        keys.Remove(key);
        Debug.Log("removeKey");
        for (int i = 0; i < keys.Count; i++) {
            Debug.Log(i + ": " + keys[i]);
        }
        return keys;
    }

    public GameObject getKeyObject()
    {
        return keyObject;
    }

    private void setKeys(string[] letterSet)
    {
        int count = keyComboCount[difficulty];
        for (int i = 0; i < count; i++)
        {
            int random = Random.Range(0, letterSet.Length);
            keys.Add(letterSet[random]);
        }
    }

    public Dictionary<string, int> getKeyComboCount()
    {
        return keyComboCount;
    }

    public string getDifficulty()
    {
        return difficulty;
    }
}
