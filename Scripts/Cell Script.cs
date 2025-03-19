using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellScript : MonoBehaviour
{
    public int value;
    public float speed = 0.1f, time = 0;

    private TextMeshProUGUI text;

    void Awake() {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    //private float fixed_move = 1.2f;
    void Start()
    {
        value = 1;
        //transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update() {
        //OnPositionChanged();
        //OnValueChanged();

        text.text = value.ToString();
        text.color = Color.black;
        if (value == 1){ 
            float col_time = 0.5f;
            text.color = Color.Lerp(Color.black, Color.white, col_time);
        }
    }

    void OnPositionChanged() {
        if (Input.GetKeyDown(KeyCode.O)) {
            Increase();
        }
    
    }

    public void OnValueChanged() {
        int dice = Random.Range(0, 5);  // creates a number between 1 and 6
        if (dice == 3){
            Increase();
        }

        Increase();
    }

    public void SetZero() {
        float col_time = 1.0f;
        GetComponent<Renderer>().material.color = Color.Lerp(Color.black, Color.white, col_time);
        value = 1;
        time = 0;
    }

    public bool Empty() {
        return value == 1;
    }

    public void Increase() {
        value *= 2;
        time += speed;
        //GetComponent<Renderer>().material.color = Color.yellow;
        ChangeColor();
    }

    public void ChangeColor() {
        GetComponent<Renderer>().material.color = Color.Lerp(Color.Lerp(Color.white, Color.yellow, 0.7f), Color.red, time);
    }

    public Color GetColor() {
        return GetComponent<Renderer>().material.color;
    }
}
