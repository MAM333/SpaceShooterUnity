using UnityEngine;

public class TrophySpawner : MonoBehaviour
{
    public static TrophySpawner instance;

    public GameObject trophy;
    public Sprite trophyS;
    public Sprite trophyA;
    public Sprite trophyB;
    public Sprite trophyC;
    public Sprite trophyD;
    public Sprite trophyE;

    private void Awake()
    {
        instance = this;
    }

    public Sprite GetSpriteByLetter(string letter)
    {
        Sprite trophySprite = null;

        switch (letter)
        {
            case "s": trophySprite = trophyS; break;
            case "a": trophySprite = trophyA; break;
            case "b": trophySprite = trophyB; break;
            case "c": trophySprite = trophyC; break;
            case "d": trophySprite = trophyD; break;
            case "e": trophySprite = trophyE; break;
        }

        return trophySprite;
    }

    public void SpawnTrophy(string letter, Vector3 position)
    {
        Sprite trophySprite = GetSpriteByLetter(letter);

        GameObject spawnedTrophy = Instantiate(trophy, position, Quaternion.identity);
        SpriteRenderer trophySpr = spawnedTrophy.GetComponent<SpriteRenderer>();
        trophySpr.sprite = trophySprite;

        Trophy trp = spawnedTrophy.GetComponent<Trophy>();
        trp.SetLetter(letter);
    }
}
