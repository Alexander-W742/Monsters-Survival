using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    // current data
    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;

    private void Awake()
    {
        // Assign the variables
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
    }

    // Experience and level of the player
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 100;

    // class for defining a level range and the corresponding experience cap increas for that range
    [System.Serializable]

    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncreas;
    }

    // I-Frames
    [Header("I-Frames")]
    public float invincibilityDuration;
    public float invincibilityTimer;
    bool isInvicibile;

    public List<LevelRange> levelRanges;

    void Start()
    {
        // initialze the experience cap as the first experience cap increase
        experienceCap = levelRanges[0].experienceCapIncreas;
    }

    void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        // if the invicibility timer has reached 0, set the invicibility flag to false
        else if(isInvicibile)
        {
            isInvicibile = false;
        }
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;

        levelUpChecker();
    }

    void levelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;
            foreach(LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncreas;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
        }
    }

    public void TakeDamage(float dam)
    {
        // if the player is not currently invicible, reduce health and start inviciblity
        if (!isInvicibile)
        {
            currentHealth -= dam;

            invincibilityTimer = invincibilityDuration;
            isInvicibile = true;

            if (currentHealth <= 0)
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        Debug.Log("Player is dead");
    }

    public void RestoreHealth(float amount)
    {
        // only heal if the player is hurt
        if (currentHealth < characterData.MaxHealth)
        {
            currentHealth += amount;
            // makes sure the players health doesn't exceed their max health
            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
    }
}
