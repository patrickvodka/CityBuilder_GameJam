using System;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(Health))]
public class BuildingRespawner : MonoBehaviour {
    [Header("Building Settings")]
    public GameObject buildingHuman;
    public GameObject buildingZombie;
    public bool isHuman=true;
    [Header("VFX_&_Sound")] 
    public AudioClip destroySound;
    public ParticleSystem particleSystem;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Start() {
        // death event 
        GetComponent<Health>().OnDeath += RespawnBuilding;
        _audioSource.clip = destroySound;
    }

    void RespawnBuilding()
    {
        if (isHuman)
        {
            _audioSource.Play();
            // Créer un nouveau bâtiment Humain
            GameObject newBuilding = PrefabUtility.InstantiatePrefab(buildingHuman) as GameObject;
            newBuilding.transform.position = transform.position;
            newBuilding.transform.rotation = transform.rotation;
            
            
        }
        else
        {
            _audioSource.Play();
            // Créer un nouveau bâtiment Zombie
            GameObject newBuilding = PrefabUtility.InstantiatePrefab(buildingZombie) as GameObject;
            newBuilding.transform.position = transform.position;
            newBuilding.transform.rotation = transform.rotation;

        }
    }
}