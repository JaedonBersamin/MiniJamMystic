using Pathfinding;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [SerializeField]
    private GameObject spell;
    [SerializeField]
    private GameObject enemy;
    private int spellsPickedUp;
    [SerializeField]
    private TMP_Text spellsCollected;
    private int lives;
    private float time;
    [SerializeField]
    private TMP_Text timeText;
    private int seconds;
    private string difficulty;
    private float spellDelay;
    private float enemyDelay;
    private int sigilCount;
    [SerializeField]
    private GameObject sigil;
    private int sigilRadius;
    private GameObject[] sigils;
    GameObject gameCanvas;
    GameObject pauseCanvas;
    [SerializeField]
    private SceneController sceneController;
    [SerializeField]
    private GameObject boundaryObject;
    private GameObject player;
    [SerializeField]
    private GameObject[] sigilSpawns;
    [SerializeField]
    private GameObject sigilSpawn;
    // Start is called before the first frame update
    void Start()
    {
        spellsPickedUp = 0;
        lives = 3;
        spellDelay = 3f;
        enemyDelay = 5f;
        sigilCount = 1;
        sigilRadius = 3;
        spawnSigils();
        StartCoroutine(spawnSpell());
        StartCoroutine(spawnEnemy());
        time = 0;
        seconds = 0;
        player = GameObject.Find("Player");
        difficulty = Combo.DIFFICULTY_EASY;
        gameCanvas = GameObject.FindGameObjectWithTag("GameCanvas");
        pauseCanvas = GameObject.FindGameObjectWithTag("PauseCanvas");
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 1f)
        {
            seconds++;
            timeText.text = seconds + "";
            time = 0f;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            foreach (Transform child in gameCanvas.transform)
            {
                child.gameObject.SetActive(false);
            }
            foreach (Transform child in pauseCanvas.transform)
            {
                child.gameObject.SetActive(true);
            }
            Time.timeScale = 0;
        }

        if (lives <= 0)
        {
            SceneManager.LoadScene("Lose");
        }

        if (difficulty == Combo.DIFFICULTY_EASY && seconds >= 45)
        {
            difficulty = Combo.DIFFICULTY_MEDIUM;
        } else if (difficulty == Combo.DIFFICULTY_MEDIUM && seconds >= 90)
        {
            difficulty = Combo.DIFFICULTY_HARD;
        } else if (difficulty == Combo.DIFFICULTY_HARD && seconds >= 150)
        {
            difficulty = Combo.DIFFICULTY_MYSTIC;
        }

        if (difficulty == Combo.DIFFICULTY_EASY)
        {
            spellDelay = 3f;
            enemyDelay = 5f;
        } else if (difficulty == Combo.DIFFICULTY_MEDIUM)
        {
            spellDelay = 4f;
            enemyDelay = 4f;
        } else if (difficulty == Combo.DIFFICULTY_HARD)
        {
            spellDelay = 5f;
            enemyDelay = 3f;
        } else if (difficulty == Combo.DIFFICULTY_MYSTIC)
        {
            enemyDelay = 2f;
            spellDelay = 7f;
        }

        sigils = GameObject.FindGameObjectsWithTag("Sigil");
        bool sigilRemaining = false;
        foreach (GameObject sigil in sigils) { 
            if (!sigil.GetComponent<Activate>().getCompleted())
            {
                sigilRemaining = true;
            }
        }
        if (!sigilRemaining && sigils.Length > 0)
        {
            if (sigilCount >= 8)
            {
                sceneController.OnWin();
            }
            sigilCount += 2;
            spawnSigils();
        }
    }

    IEnumerator spawnSpell()
    {
        Bounds bounds = boundaryObject.GetComponent<Renderer>().bounds;

        Vector2 minBounds = new Vector2(bounds.min.x, bounds.min.y);
        Vector2 maxBounds = new Vector2(bounds.max.x, bounds.max.y);

        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);

        Vector3 spellPos = new Vector3(randomX, randomY, 0);
        Instantiate(spell, spellPos, Quaternion.identity);
        yield return new WaitForSeconds(spellDelay);
        StartCoroutine(spawnSpell());
    }

    IEnumerator spawnEnemy()
    {
        Bounds bounds = boundaryObject.GetComponent<Renderer>().bounds;

        Vector2 minBounds = new Vector2(bounds.min.x, bounds.min.y);
        Vector2 maxBounds = new Vector2(bounds.max.x, bounds.max.y);

        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);

        Vector3 enemyPos = new Vector3(randomX, randomY, 0);
        GameObject player = GameObject.Find("Player");
        GameObject newEnemy = Instantiate(enemy, enemyPos, Quaternion.identity);
        newEnemy.GetComponent<AIDestinationSetter>().target = player.transform;

        yield return new WaitForSeconds(enemyDelay);
        StartCoroutine(spawnEnemy());
    }
    public int getSpellsPickedUp()
    {
        return spellsPickedUp;
    }

    public void setSpellsPickedUp(int spells)
    {
        spellsPickedUp = spells;
        spellsCollected.text = getSpellsPickedUp() + "";
    }

    public int getLives()
    {
        return lives;
    }

    public void setLives(int lives)
    {
        this.lives = lives;
    }

    public string getDifficulty()
    {
        return difficulty;
    }

    public void setDifficulty(string difficulty)
    {
        this.difficulty = difficulty;
    }

    public int getSeconds()
    {
        return seconds;
    }

    public void spawnSigils()
    {
        for (int i = 0; i < sigilCount; i++)
        {
            if (GameObject.FindGameObjectsWithTag("Sigil").Length >= 8)
            {
                break;
            }

            int randomSpawn;
            do
            {
                randomSpawn = Random.Range(0, sigilSpawns.Length);
            } while (sigilSpawns[randomSpawn] == null);

            GameObject newSigil = Instantiate(sigil, sigilSpawns[randomSpawn].transform.position, Quaternion.identity, sigilSpawn.transform);
            sigilSpawns[randomSpawn] = null;
            int random = Random.Range(1, 6);
            Sprite sigilSprite = Resources.Load<Sprite>("Sprites/Sygil/complete_sygil" + random);
            newSigil.GetComponent<SpriteRenderer>().sprite = sigilSprite;
        }
    }
}
