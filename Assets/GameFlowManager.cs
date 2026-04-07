using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public enum GameState
{
    StartScreen,
    Intro,
    Playing
}
public class GameFlowManager : MonoBehaviour
{
    [Header("State")]
    public GameState currentState = GameState.StartScreen;

    [Header("UI")]
    public GameObject startUI;
    public GameObject introTextObject;
    public TMP_Text introText;
    [TextArea] public string introLine = "";

    [Header("Mother Intro")]
    public GameObject motherObject;
    public Transform motherTransform;
    public Transform motherStopPoint;
    public float motherRunSpeed = 2f;
    public Animator motherAnimator;

    [Header("Gameplay References")]
    public CarriageController2D carriageController;
    public Rigidbody2D carriageRb;
    public Rigidbody2D babyRb;

    private void Start()
    {
        currentState = GameState.StartScreen;

        if (startUI != null)
            startUI.SetActive(true);

        if (introTextObject != null)
            introTextObject.SetActive(false);

        if (motherObject != null)
            motherObject.SetActive(false);

        if (introText != null)
            introText.text = introLine;

        if (babyRb != null)
        {
            babyRb.simulated = false;
            babyRb.linearVelocity = Vector2.zero;
            babyRb.angularVelocity = 0f;
        }

        if (carriageController != null)
            carriageController.StopGame();

        if (carriageRb != null)
        {
            carriageRb.linearVelocity = Vector2.zero;
            carriageRb.angularVelocity = 0f;
        }
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.StartScreen:
                HandleStartScreen();
                break;

            case GameState.Intro:
                HandleIntro();
                break;
        }
    }

    private void HandleStartScreen()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            StartIntro();
    }

    private void StartIntro()
    {
        currentState = GameState.Intro;

        if (startUI != null)
            startUI.SetActive(false);

        if (motherObject != null)
            motherObject.SetActive(true);

        if (introTextObject != null)
            introTextObject.SetActive(true);

        if (introText != null)
            introText.text = introLine;

        if (carriageController != null)
            carriageController.StopGame();

        if (carriageRb != null)
        {
            carriageRb.linearVelocity = Vector2.zero;
            carriageRb.angularVelocity = 0f;
        }

        if (babyRb != null)
        {
            babyRb.simulated = false;
            babyRb.linearVelocity = Vector2.zero;
            babyRb.angularVelocity = 0f;
        }

        if (motherAnimator != null)
            motherAnimator.SetBool("isRunning", true);
    }

    private void HandleIntro()
    {
        if (motherTransform == null || motherStopPoint == null)
            return;

        motherTransform.position = Vector2.MoveTowards(
            motherTransform.position,
            motherStopPoint.position,
            motherRunSpeed * Time.deltaTime
        );

        float distance = Vector2.Distance(motherTransform.position, motherStopPoint.position);

        if (distance <= 0.05f)
            StartGameplay();
    }

    private void StartGameplay()
    {
        currentState = GameState.Playing;

       
        if (introTextObject != null)
            introTextObject.SetActive(false);

       
        if (motherAnimator != null)
            motherAnimator.SetBool("isRunning", false);

        if (babyRb != null)
        {
            babyRb.simulated = true;
            babyRb.linearVelocity = Vector2.zero;
            babyRb.angularVelocity = 0f;
        }

        if (carriageController != null)
            carriageController.StartGame();
    }
}