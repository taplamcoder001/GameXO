using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public static Cell Instance { get; private set; }
    public GameObject gameOverWindow;
    private Transform canvas;
    public Sprite emtyImage;
    public Sprite xImage;
    public Sprite oImage;
    public int row;
    public int column;
    private Image image;
    private Button button;
    private bool onMenu;
    private void Awake()
    {
        // if(Instance!=null && Instance != this)
        // {
        //     Destroy(this);
        // }
        // else
        // {
        //     Instance = this;
        // }
        Instance = this; // Khi bao single ton theo kieu luoi bieng (test) Vi trong 1 bang co nhieu cell
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    private void Start() {
        canvas = FindObjectOfType<Canvas>().transform;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Instantiate(gameOverWindow, canvas);
        }
    }
    public void ChangeImage(string s)
    {
        if (s == "x")
        {
            image.sprite = xImage;
        }
        else if(s=="o")
        {
            image.sprite = oImage;
        }
        else
        {
            image.sprite = emtyImage;
        }
    }

    void OnClick()
    {
        if(Board.Instance.CheckCell(this.row,this.column) && !Board.Instance.result)
        {
            ChangeImage(Board.Instance.currentTurn);
            if(Board.Instance.Check(this.row, this.column))
            {
                GameObject window =Instantiate(gameOverWindow, canvas);
                EndGame.Instance.winnerName.text = Board.Instance.currentTurn;
            }
            if (Board.Instance.currentTurn == "x")
            {
                Board.Instance.currentTurn = "o";
            }
            else
            {
                Board.Instance.currentTurn = "x";
            }
        }
    }
}
