using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            // Si l'instance n'existe pas encore, la créer
            if (_instance == null)
            {
                // Rechercher une instance existante dans la scène
                _instance = FindObjectOfType<GameManager>();

                // Si aucune instance n'existe dans la scène, créer une nouvelle instance
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    _instance = singletonObject.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }

    // Empêcher la création d'une nouvelle instance via le constructeur
    private GameManager()
    {
    }


    // Compteurs pour les différents types de zombies
    private int zombieBasicCount = 0;
    private int zombieBoomCount = 0;
    private int zombieTankCount = 0;

    // Référence à l'UI pour afficher les compteurs
    public Text zombieBasicText;
    public Text zombieBoomText;
    public Text zombieTankText;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        zombieBasicCount = 0;
        zombieBoomCount = 0;
        zombieTankCount = 0;
        UpdateUiZombie();
    }

    public void UpdateUiZombie()
    {
        // Mettre à jour le texte de l'UI avec les nouveaux compteurs
        zombieBasicText.text = "Zombies Basiques: " + zombieBasicCount.ToString();
        zombieBoomText.text = "Zombies Boom: " + zombieBoomCount.ToString();
        zombieTankText.text = "Zombies Tank: " + zombieTankCount.ToString();
    }

    public void SubtractZombie(ZombieType type)
    {
        // Vérifier si le compteur est supérieur à 0 avant de le soustraire
        switch (type)
        {
            case ZombieType.Basic:
                if (zombieBasicCount > 0)
                    zombieBasicCount--;
                break;
            case ZombieType.Boom:
                if (zombieBoomCount > 0)
                    zombieBoomCount--;
                break;
            case ZombieType.Tank:
                if (zombieTankCount > 0)
                    zombieTankCount--;
                break;
            default:
                break;
        }

        UpdateUiZombie();
    }
    public void AddZombie(ZombieType type)
    {
        // Incrémenter le compteur correspondant
        switch (type)
        {
            case ZombieType.Basic:
                zombieBasicCount++;
                break;
            case ZombieType.Boom:
                zombieBoomCount++;
                break;
            case ZombieType.Tank:
                zombieTankCount++;
                break;
            default:
                break;
           
        }
        UpdateUiZombie();
    }
}