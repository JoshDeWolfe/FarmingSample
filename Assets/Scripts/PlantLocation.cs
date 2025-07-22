using UnityEngine;

public class PlantLocation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer waterIcon;

    public Sprite[] growingGraphics;
    public Sprite emptyGraphic;
    public Sprite pumpkinGraphic;
    public Sprite cabbageGraphic;
    public Sprite carrotGraphic;

    public const float SEED_TIME = 3f;

    private int _seedStep = 0;
    private bool _needsWater = false;
    private float _seedTimer = -1f;

    private SEED_TYPE _plantedSeed = SEED_TYPE.NONE;

    private enum PLANT_STATE { NONE, UNPLANTED, GROWING, HARVESTABLE }
    private PLANT_STATE _currentState = PLANT_STATE.NONE;

    void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer> ();
        ResetPlant ();
        GameManager.instance.onGameWin += ResetPlant;
    }

    public void ResetPlant ()
    {
        _currentState = PLANT_STATE.UNPLANTED;
        _plantedSeed = SEED_TYPE.NONE;
        spriteRenderer.sprite = emptyGraphic;
        waterIcon.gameObject.SetActive (false);
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        Player p = collision.gameObject.GetComponent<Player> ();
        if (p == null)
            return;

        Tool t = p.GetHeldTool ();
        if (t == null)
            return;

        switch (_currentState)
        {
            case PLANT_STATE.UNPLANTED:
                if (t.toolType == TOOL.SEED1)
                {
                    PlantSeed (SEED_TYPE.PUMPKIN, p);
                }
                else if (t.toolType == TOOL.SEED2)
                {
                    PlantSeed (SEED_TYPE.CABBAGE, p);
                }
                else if (t.toolType == TOOL.SEED3)
                {
                    PlantSeed (SEED_TYPE.CARROT, p);
                }
                break;

            case PLANT_STATE.GROWING:
                if (_needsWater && t.toolType == TOOL.BUCKET)
                {
                    SetNeedsWater (false);
                }
                break;

            case PLANT_STATE.HARVESTABLE:
                if (t.toolType == TOOL.SCYTHE)
                {
                    HarvestLocation ();
                }
                break;
        }
    }

    private void PlantSeed (SEED_TYPE seed, Player playerRef)
    {
        _currentState = PLANT_STATE.GROWING;
        _plantedSeed = seed;
        SetGrowingStep (0);
        playerRef.ResetTool ();
    }

    private void SetNeedsWater (bool needsWater)
    {
        _needsWater = needsWater;
        waterIcon.gameObject.SetActive (_needsWater);
    }

    private void AdvanceStep ()
    {
        if (_seedStep < growingGraphics.Length - 1)
        {
            SetGrowingStep (_seedStep + 1);
        }
        else if (_seedStep == growingGraphics.Length - 1)
        {
            SetHarvestable ();
        }
    }

    private void SetGrowingStep (int newStep)
    {
        _seedStep = newStep;
        _seedTimer = SEED_TIME;
        SetNeedsWater (true);
        spriteRenderer.sprite = growingGraphics[_seedStep];
    }

    private void SetHarvestable ()
    {
        switch (_plantedSeed)
        {
            case SEED_TYPE.PUMPKIN:
                spriteRenderer.sprite = pumpkinGraphic;
                break;

            case SEED_TYPE.CABBAGE:
                spriteRenderer.sprite = cabbageGraphic;
                break;

            case SEED_TYPE.CARROT:
                spriteRenderer.sprite = carrotGraphic;
                break;
        }
        _currentState = PLANT_STATE.HARVESTABLE;
    }

    private void HarvestLocation ()
    {
        GameManager.instance.OnPlantHarvested (_plantedSeed);
        ResetPlant ();
    }

    void Update()
    {
        if (_currentState == PLANT_STATE.GROWING)
        {
            if (!_needsWater)
            {
                _seedTimer -= Time.deltaTime;
                if (_seedTimer <= 0)
                {
                    AdvanceStep ();
                }
            }
        }
    }
}
