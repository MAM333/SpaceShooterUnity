using UnityEngine;

public class TrophyMenu : MonoBehaviour
{
    public bool inMenu = true;
    public GameObject goldenParticles;

    private TrophySpawner trophySpw;
    private SpriteRenderer sprR;

    void Awake()
    {
        trophySpw = GetComponent<TrophySpawner>();
        sprR = GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (inMenu) goldenParticles.SetActive(true);
        else goldenParticles.SetActive(false);

        SaveData svd = SaveSystem.Load();
        
        if (svd.trophy != "z" && svd.trophy != null)
        {
            Sprite spr = trophySpw.GetSpriteByLetter(svd.trophy);
            sprR.sprite = spr;
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
