using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float currentHealth;

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

    void TakeDamage(float damage)
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


}
