using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    public float currentHealth;

    [SerializeField]
    private float maxHealth = 100f;

    [SerializeField]
    private Image healthBarFill;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.K))
       {
           TakeDamage(15f);
       }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Debug.Log("Player Mort !");
        }
        UpdateHealthBarFill();
    }

    void UpdateHealthBarFill()
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }


   public void ConsumeItem(float health)
    {
        // la vie actuel + les stats de litem
        currentHealth += health;

        // si la vie actuel > a la vie max 
        if(currentHealth > maxHealth)
        {
            // alors la vie actuel sera = a vie max
            currentHealth = maxHealth;
        }

        UpdateHealthBarFill();
    }


}
