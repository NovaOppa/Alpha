using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Other Elements references")]

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private MoveBehaviour playerMovementScript;



    [SerializeField]
    public float currentHealth;

    [SerializeField]
    private float maxHealth = 100f;

    [SerializeField]
    private Image healthBarFill;
    //relier un script a ce scirpt pour pouvoir lutiler en bas
    public AimBehaviourBasic playerAimScript;

    public BasicBehaviour playerSprintScript;

    public Inventory inventoryScript;

    public ThirdPersonOrbitCamBasic playerCamScript;

    public FlyBehaviour playerFlyScript;

    // relier un autre empty a ce script pour pouvoir lutiler en bas
    public GameObject DeathMenu;

    [HideInInspector]
    public bool isDead = false;

    


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

        if(currentHealth <= 0 && !isDead)
        {
            Die();
            
        }
        UpdateHealthBarFill();
    }

    private void Die()
    {

        Debug.Log("Player Mort !");
        isDead = true;
        // quand le joueur est mort les movement sont desactiver
        playerMovementScript.canMove = false;
        // il a appele le script dun autre object et la desactiver
        //desactive Aim du perso 
        playerAimScript.enabled = false;
        // desactiver le script qui gere le sprint 
        playerSprintScript.enabled = false;
        // desactiver le script de linventaire quand est mort
        inventoryScript.enabled = false;
        //desactive le movement de cam apres la mort
        playerCamScript.enabled = false;
        // desactiver le vol
        playerFlyScript.enabled = false;
        // desactiver un autre empty
        DeathMenu.SetActive(true);



       
        
        
        animator.SetTrigger("Die");
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
