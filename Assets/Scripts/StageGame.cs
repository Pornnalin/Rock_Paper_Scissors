using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StageGame : MonoBehaviour
{
    public enum stageGames
    {
        none,
        playerTurn,
        comTurn,
        check,
        reset
    }
    public stageGames myStageGames;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI playerText;
    [SerializeField] private TextMeshProUGUI comText;
    [SerializeField] private TextMeshProUGUI resultsText;
    [SerializeField] private TextMeshProUGUI pressText;

    [SerializeField] private List<InfoCard> infoCards;

    [SerializeField] private string playerInput;
    [SerializeField] private string comInput;  

    public float timeWorld = 20f;
    public float timeTurn = 1f;

    [SerializeField] private bool comTurnExecuted = false;
    [SerializeField] private bool isReset = false;
    [SerializeField] private AnimationUI animationUI;
    [SerializeField] private Image imageCom;
    // Start is called before the first frame update
    void Start()
    {
        myStageGames = stageGames.none;
        animationUI.AnimationPressText(pressText.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = timeWorld.ToString("F1");
     
        switch (myStageGames)
        {
            case stageGames.none:

                playerText.gameObject.SetActive(false);

                if (Input.GetMouseButtonDown(0) && !timeText.gameObject.activeSelf)
                {
                    myStageGames = stageGames.playerTurn;
                    ToggleCard(false);

                }
                else
                {
                    pressText.gameObject.SetActive(true);
                    resultsText.text = "";
                    timeText.gameObject.SetActive(false);

                    foreach (var item in infoCards)
                    {
                        item.imageCard.gameObject.SetActive(false);
                    }

                }

                
                break;

            case stageGames.playerTurn:

                pressText.gameObject.SetActive(false);
                timeText.gameObject.SetActive(true);
                DisbleTextTurn(playerText.gameObject);
                isReset = false;

                foreach (var item in infoCards)
                {
                    item.imageCard.gameObject.SetActive(true);
                }



                if (timeWorld <= 0)
                {
                    ToggleCard(false);
                    StartCoroutine(waitToChange(stageGames.comTurn, .5f));
                    timeWorld = 0;
                }
                else
                {
                    ToggleCard(true);

                    timeWorld -= 1 * Time.deltaTime;
                }

                break;

            case stageGames.comTurn:

                if (!comTurnExecuted)
                {
                    comTurnExecuted = true;

                    DisbleTextTurn(comText.gameObject);
                    int index = Random.Range(0, 3);
                    comInput = infoCards[index].shortName;
                    imageCom.sprite = infoCards[index].imageCard.sprite;
                    animationUI.ScaleBigImCom(imageCom.gameObject);


                }
                StartCoroutine(waitToChange(stageGames.check, 1f));

                break;

            case stageGames.check:


                if (playerInput == comInput)
                {
                    resultsText.text = "DRAW!!";
                }
                else if ((playerInput == "p" && comInput == "r") ||
                    (playerInput == "s" && comInput == "p") ||
                    (playerInput == "r" && comInput == "s")
                )
                {
                    resultsText.text = "Player1 Win!!";
                }

                else
                {
                    resultsText.text = "Player2 Win!!";
                }
                if (playerInput == "")
                {
                    resultsText.text = "Player2 Win!!";
                }

                StartCoroutine(waitToChange(stageGames.reset, 3f));

                break;

            case stageGames.reset:

                if (!isReset)
                {
                    isReset = true;
                    animationUI.ScaleSmallImCom(imageCom.gameObject);
                    animationUI.MoveToOrigin();
                    timeWorld = 5f;
                    timeTurn = 1f;
                    resultsText.text = "wait Reset...";
                    comInput = "";
                    playerInput = "";
                    comTurnExecuted = false;
                }
                StartCoroutine(waitToChange(stageGames.none, 2f));

                break;
        }
    }

    IEnumerator waitToChange(stageGames stage, float time)
    {
        yield return new WaitForSeconds(time);
        myStageGames = stage;

    }

    private void DisbleTextTurn(GameObject obj)
    {
        if (timeTurn > 0)
        {
            obj.SetActive(true);
            timeTurn -= 1 * Time.deltaTime;
        }
        else
        {
            obj.SetActive(false);

        }
    }
    public void GET_PlayerInput(string key)
    {
        playerInput = key;
    }

    private void ToggleCard(bool set)
    {
        for (int i = 0; i < infoCards.Count; i++)
        {
            var b = infoCards[i].imageCard.GetComponent<Button>();
            b.enabled = set;
        }
    }
   


}
[System.Serializable]
public class InfoCard
{
    public Image imageCard;
    public string shortName;
}
