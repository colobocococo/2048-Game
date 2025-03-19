using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FieldScript : MonoBehaviour
{

    bool moved;
    int score;
    public CellScript[] cells { get; private set; }

    private TextMeshProUGUI text;

    private void Awake()
    {
        cells = GetComponentsInChildren<CellScript>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        int times = 2;
        while (times > 0) {
            Awake();
            times--;
            Select();
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = score.ToString();
        text.color = Color.black;

        Move();
    }

    void Select() {
        int size = 0;

        for (int i = 0; i < cells.Length; i++) {
            if (cells[i].Empty()) {
                size++;
            }
        }

        int cur = Random.Range(0, size);
        //cur = 0;
        for (int i = 0; i < cells.Length; i++) {
            if (cells[i].Empty()) {
                if (cur == 0) {
                    cells[i].OnValueChanged();
                    score += cells[i].value;
                    break;
                }

                cur--;
            }
        }
    }

    bool Same(int i, int j) {
        return cells[i].value == cells[j].value;
    }

    void push(int i, int j) {
        cells[j].value = cells[i].value;
        cells[j].time = cells[i].time;
        cells[j].ChangeColor();
    }

    void Move() {
        moved = false;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            Down();
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            Left();
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            Up();
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            Right();
        }

        if (moved) Select();
    }

    void Right() {
        for (int y = 3; y >= 0; y--) {
            for (int x = 0; x < 4; x++) {
                MoveNext(x, y, 0, 1, false);
            }
        }
    }

    void Left() {
        for (int y = 0; y < 4; y++) {
            for (int x = 0; x < 4; x++) {
                MoveNext(x, y, 0, -1, false);
            }
        }
    }

    void Up() {
        for (int x = 3; x >= 0; x--) {
            for (int y = 0; y < 4; y++) {
                MoveNext(x, y, 1, 0, false);
            }
        }
    }

    void Down() {
        for (int x = 0; x < 4; x++) {
            for (int y = 0; y < 4; y++) {
                MoveNext(x, y, -1, 0, false);
            }
        }
    }

    int Pos(int i, int j) {
        return 4*i + j;
    }

    bool Fit(int x, int y) {
        return 0 <= x && x < 4 && 0 <= y && y < 4;
    }
    
    void MoveNext(int x, int y, int dx, int dy, bool merge = false) {
        int i = Pos(x, y);
        int j = Pos(x+dx, y+dy);

        if (!Fit(x+dx, y+dy) || cells[i].Empty()) return;

        if (cells[j].Empty()) {
            moved = true;
            push(i, j);
            cells[i].SetZero();
            MoveNext(x+dx, y+dy, dx, dy, merge);
            return;
        }

        if (Same(i, j) && !merge) {
            //score += 2*cells[i].value;
            moved = true;
            cells[i].SetZero();
            cells[j].Increase();
            MoveNext(x+dx, y+dy, dx, dy, true);
            return;
        }
    }
}
