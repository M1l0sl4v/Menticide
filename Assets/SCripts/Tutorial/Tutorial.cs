using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public static bool startWithTutorial;
    public static Tutorial instance;
    public Phase currentPhase;

    [Header("General")]
    public GameObject tutorialObjects;
    public TMP_Text tutorialText;
    private bool phaseShiftTriggered;
    public bool disabledForTutorial;

    [Header("Movement")]
    public GameObject movementObjects;
    private Image leftButton;
    private Image rightButton;
    public Sprite[] leftKeySprites;
    public Sprite[] rightKeySprites;
    public float scaleAmountWhenPressed;
    private bool leftPressed;
    private bool rightPressed;
    public float graphicSwapSpeed;
    private float timeUntilSwap;
    private int currentSet;
    public string movementTooltip;
    public float delayBeforeDrawPhase;

    [Header("Line Drawing")]
    public GameObject lineDrawingObjects;
    public float fillSpeed;
    public int drawAmountToProgress;
    [HideInInspector] public int drawAmount;
    public string lineDrawTooltip;
    public float delayBeforeEnemyPhase;
    private Image demoLine;
    public float refillCooldown;
    private float refillCooldownLeft;

    [Header("Enemies")]
    public GameObject enemiesObjects;
    public enemyspawner dogSpawner1;
    public enemyspawner dogSpawner2;
    public enemyspawner spitterSpawner;
    public float dog1Delay;
    public float spitterDelay;
    public float dog2Delay;
    public float delayBeforeComplete; 
    private bool enemiesQueued;
    public string enemiesTooltip;

    

    public enum Phase
    {
        None = 0,
        Movement,
        Drawing,
        Enemies,
        Attacking,
        Health,
        Complete
    }

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (startWithTutorial) SetPhase(Phase.Movement);
        else SetPhase(Phase.None);

        // Movement
        leftButton = movementObjects.transform.Find("Left Key").GetComponent<Image>();
        rightButton = movementObjects.transform.Find("Right Key").GetComponent<Image>();
        timeUntilSwap = graphicSwapSpeed;
        // Line Drawing
        demoLine = lineDrawingObjects.transform.Find("Demo Line").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (currentPhase)
        {

            case Phase.None:
                tutorialObjects.SetActive(false);
                break;
            case Phase.Movement:
                // Activate these
                tutorialObjects.SetActive(true);
                movementObjects.SetActive(true);
                // Deactivate these
                lineDrawingObjects.SetActive(false);
                enemiesObjects.SetActive(false);
                disabledForTutorial = true;
                // Set text
                tutorialText.text = movementTooltip;

                // Pulse input buttons
                if (Input.GetAxis("Horizontal") < -0.2)
                {
                    leftButton.rectTransform.localScale = new Vector3(scaleAmountWhenPressed, scaleAmountWhenPressed, scaleAmountWhenPressed);
                    rightButton.rectTransform.localScale = Vector3.one;
                    leftPressed = true;
                }
                else if (Input.GetAxis("Horizontal") > 0.2)
                {
                    rightButton.rectTransform.localScale = new Vector3(scaleAmountWhenPressed, scaleAmountWhenPressed, scaleAmountWhenPressed);
                    leftButton.rectTransform.localScale = Vector3.one;
                    rightPressed = true;
                }
                else
                {
                    leftButton.rectTransform.localScale = Vector3.one;
                    rightButton.rectTransform.localScale = Vector3.one;
                }

                // Swap input buttons
                timeUntilSwap -= Time.deltaTime;
                if (timeUntilSwap <= 0)
                {
                    currentSet = ++currentSet == leftKeySprites.Length ? 0 : currentSet;
                    leftButton.sprite = leftKeySprites[currentSet];
                    rightButton.sprite = rightKeySprites[currentSet];
                    timeUntilSwap = graphicSwapSpeed;
                }
                
                // Next phase
                if (leftPressed && rightPressed && !phaseShiftTriggered)
                {
                    Invoke("NextPhase", delayBeforeDrawPhase);
                    phaseShiftTriggered = true;
                }
                break;
            case Phase.Drawing:
                // Activate these
                tutorialObjects.SetActive(true);
                lineDrawingObjects.SetActive(true);
                // Deactivate these
                movementObjects.SetActive(false);
                enemiesObjects.SetActive(false);
                disabledForTutorial = true;
                // Set text
                tutorialText.text = lineDrawTooltip;

                // Fill and flip line
                demoLine.fillAmount += fillSpeed * Time.deltaTime;
                if (demoLine.fillAmount >= 1 && refillCooldownLeft <= 0)
                {
                    demoLine.fillAmount = 0;
                    refillCooldownLeft = refillCooldown;
                    float rotationAmount = demoLine.rectTransform.localEulerAngles.z == 325 ? 220 : 325;
                    demoLine.fillOrigin = demoLine.fillOrigin == 1 ? 0 : 1;
                    demoLine.rectTransform.localEulerAngles = new Vector3 (0, 0, rotationAmount);
                }
                else if (demoLine.fillAmount >= 1)
                {
                    refillCooldownLeft -= Time.deltaTime;
                }

                // Next phase
                if (drawAmount >= drawAmountToProgress && !phaseShiftTriggered)
                {
                    Invoke("NextPhase", delayBeforeEnemyPhase);
                    phaseShiftTriggered = true;
                }
                break;
            case Phase.Enemies:
                // Activate thses
                tutorialObjects.SetActive(true);
                enemiesObjects.SetActive(true);
                // Deactivate these
                movementObjects.SetActive(false);
                lineDrawingObjects.SetActive(false);
                disabledForTutorial = true;
                // Set text
                tutorialText.text = enemiesTooltip;

                // Spawn enemies
                if (!enemiesQueued)
                {
                    Invoke("SpawnDog1", dog1Delay);
                    Invoke("SpawnSpitter", spitterDelay);
                    Invoke("SpawnDog2", dog2Delay);
                    enemiesQueued = true;
                    Invoke("EndOfTutorial", delayBeforeComplete);
                    phaseShiftTriggered = true;
                }
                break;
            case Phase.Complete:
                disabledForTutorial = false;
                tutorialObjects.SetActive(false);
                break;
            default:
                break;
        }

        
    }

    public void NextPhase()
    {
        if (currentPhase == Phase.Complete || currentPhase == Phase.None) return;
        currentPhase += 1;
        phaseShiftTriggered = false;
    }

    public void SetPhase(Phase phase)
    {
        currentPhase = phase;
    }

    public Phase CurrentPhase()
    {
        return currentPhase;
    }

    private void Complete()
    {
        SetPhase(Phase.Complete);
        startWithTutorial = false;
        TutorialToggle.instance.SyncState();
        playermovement.instance.AddHealth(playermovement.instance.health - playermovement.maxHealth);
        hands.instance.Activate();
    }

    private void SpawnDog1()
    {
        dogSpawner1.Spawn();
    }
    private void SpawnSpitter()
    {
        spitterSpawner.Spawn();
    }

    private void SpawnDog2()
    {
        dogSpawner1.Spawn();
        dogSpawner2.Spawn();
    }

    private void EndOfTutorial()
    {
        enemiesTooltip = "Good luck!";
        Invoke("Complete", 3f);
    }
}
