using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid2d : MonoBehaviour
{
    GameObject[,] gridArray;
    public int rowLength = 4;
    public int colLength = 4;
    private float tileGapX;
    private float tileGapY;
    public float userTileGap;
    public GameObject boxPrefab;
    public UIMngr uIMngr;
    public Button btn;
    public Image fillImg;
    private Vector3 lastTouchPosition;
    private Vector3 firstTouchPosition;
    public GameObject selectedObj;
    RaycastHit2D hit;
    public Vector2 mouseLocation;
    private bool loading;
    private bool inProcess = false;
    public int seletTileIndexX;
    public int seletTileIndexY;
    private Vector2[,] gridArrayPosition;
    public List<Customcolor> colorList;
    public List<GameObject> matchedList;
    public List<GameObject> cellsToBeMatched;

    int matchCount = 0;
    public int MatchCount
    {
        get { return matchCount; }
        set
        {
            matchCount = value;
            if (matchCount == 0)
            {
                matchedList = new List<GameObject>();
            }
        }
    }
    void Start()
    {
        fillImg = btn.GetComponent<Image>();
        tileGapX = userTileGap + boxPrefab.transform.localScale.x;
        tileGapY = userTileGap + boxPrefab.transform.localScale.y;

        StartCoroutine(CreateGrid());
    }
    void Update()
    {
        Click();
    }
    IEnumerator CreateGrid()
    {
        if (gridArray != null)
        {
            foreach (var item in gridArray)
            {
                Destroy(item);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        SetCameraPosition();
        gridArrayPosition = new Vector2[rowLength, colLength];
        gridArray = new GameObject[rowLength, colLength];
        for (int x = 0; x < rowLength; x++)
        {
            for (int y = 0; y < colLength; y++)
            {
                GameObject newBox = Instantiate(boxPrefab, transform);
                newBox.transform.position = new Vector3(x * tileGapX, y * tileGapY, 0);
                newBox.name = x + " " + y;
                gridArray[x, y] = newBox;
                int colorIndex;
                Customcolor newCustColor = GetColor(out colorIndex);
                newBox.GetComponent<Tile>().ChangeColor(newCustColor, colorIndex);
                newBox.GetComponent<Tile>().cellIndex = new Vector2(x,y);
                newBox.GetComponent<Tile>().colorIndex = colorIndex;
                
                gridArrayPosition[x, y] = newBox.transform.position;
                yield return new WaitForSeconds(Time.deltaTime * 0.1f);
            }
        }
        List<GameObject> tempList = new List<GameObject>();
        foreach (var item in gridArray)
        {
            tempList.Add(item);
        }
        AddToColorSetList(tempList);
        //RemoveUnwantedColor();
        SetCameraPosition();
    }

    void Click()
    {
        if (inProcess)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            mouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(mouseLocation, Vector2.zero);
            if (hit)
            {
                if (hit.collider.gameObject.GetComponent<Tile>().isMatched)
                {
                    return;
                }
                selectedObj = hit.collider.gameObject;
                seletTileIndexX = (int)selectedObj.GetComponent<Tile>().cellIndex.x;
                seletTileIndexY = (int)selectedObj.GetComponent<Tile>().cellIndex.y;
                firstTouchPosition = mouseLocation;
            }
        }

        if (Input.GetMouseButtonUp(0) && selectedObj !=null)
        { 
            lastTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //SwipeDirection swipeDirection = SwipeGesture.GetSwipeDirection(lastTouchPosition.y - firstTouchPosition.y, lastTouchPosition.x - firstTouchPosition.x);
            Vector2 dir = SwipeGesture.SwipeVector(lastTouchPosition.y - firstTouchPosition.y, lastTouchPosition.x - firstTouchPosition.x);

            float distance = Vector2.Distance(lastTouchPosition, firstTouchPosition);
            if (distance < 0.3f)
            {
                return;
            }
            Swipe(dir);
            //RegisterMove(selectedObj, direction, matchedTileList);
            //RegisterMove(selectedObj, dir);
        }
    }
    Vector2 direction;
    void Swipe(Vector2 dir)
    {
        direction = dir;
        int cellX = seletTileIndexX + (int)direction.x;
        int cellY = seletTileIndexY + (int)direction.y;
        if (cellY < colLength && cellY >= 0 && cellX < rowLength && cellX >= 0)
        {
            StartCoroutine(ISwitchCells(cellX, cellY));
        }
        else
        {
            selectedObj = null;
        }
    }
    IEnumerator ISwitchCells(int cellX,int cellY)
    {
        inProcess = true;
        Vector2 seletedObjectPosition = gridArrayPosition[seletTileIndexX, seletTileIndexY];
        GameObject nextGameObject = gridArray[cellX, cellY];

        if (nextGameObject.GetComponent<Tile>().isMatched)
        {
            selectedObj = null;
            yield break;
        }

        //swapping positions
        //selectedObj.transform.position = gridArrayPosition[cellX, cellY];
        //nextGameObject.transform.position = seletedObjectPosition;
        swipe.Invoke();
        yield return StartCoroutine(ISwitchPosition(selectedObj, nextGameObject));
        
        //Checking for match
        seletTileIndexX = cellX;
        seletTileIndexY = cellY;

        cellsToBeMatched = new List<GameObject>();
        yield return StartCoroutine(CheckVertAndHor(true));
        yield return StartCoroutine(CheckVertAndHor(false));

        foreach (var item in cellsToBeMatched)
        {
            item.GetComponent<Tile>().Matched();
            onMatch.Invoke();
        }
        RemoveFromColorSetList(cellsToBeMatched);
        Score(cellsToBeMatched.Count);
        RegisterMove(selectedObj, direction, cellsToBeMatched);
        //RegisterMatchObject(matchedTileList);
        selectedObj = null;
        inProcess = false;
    }

    IEnumerator CheckVertAndHor(bool vert)
    {
        MatchCount = 0;
        int rowsCols = vert ? colLength : rowLength;

        Tile currentMovedTile = gridArray[seletTileIndexX, seletTileIndexY].GetComponent<Tile>();

        // code for vertical matching
        for (int iter = 0; iter < rowsCols; iter++)
        {
            Tile currentTile = vert ? gridArray[seletTileIndexX, iter].GetComponent<Tile>() : gridArray[iter, seletTileIndexY].GetComponent<Tile>();
            if (currentMovedTile.colorIndex == currentTile.colorIndex && !currentTile.isMatched)
            {
                MatchList(currentTile.gameObject);
                MatchCount++;

            }
            else if (currentTile.Equals(currentMovedTile))
            {
                MatchList(currentTile.gameObject);
                MatchCount++;
            }
            else
            {
                if (MatchCount >= 3)
                {
                    CheckMatchedObjs();
                    //selectedObj = null;
                    yield break;

                }
                MatchCount = 0;
            }
            if (iter == rowsCols - 1)
            {
                if (MatchCount >= 3)
                {
                    CheckMatchedObjs();
                }
            }
        }
    }
    void TileToBeMatchList(GameObject obj)
    {
        foreach (var item in cellsToBeMatched)
        {
            if (item.Equals(obj))
            {
                return;
            }
        }
        cellsToBeMatched.Add(obj);
    }
    void MatchList(GameObject obj)
    {
        foreach (var item in matchedList)
        {
            if (item.Equals(obj))
            {
                return;
            }
        }
        matchedList.Add(obj);
    }
    void CheckMatchedObjs()
    {
        if (matchedList.Count >= 3)
        {
            foreach (var item in matchedList)
            {
                TileToBeMatchList(item);
            }
        }
    }
    
    IEnumerator ISwitchPosition(GameObject one, GameObject two)
    {
        Tile tileOne = one.GetComponent<Tile>();
        Tile tileTwo = two.GetComponent<Tile>();

        //swiping cell array
        gridArray[(int)tileOne.cellIndex.x, (int)tileOne.cellIndex.y] = two;
        gridArray[(int)tileTwo.cellIndex.x, (int)tileTwo.cellIndex.y] = one;
        //swiping cell index
        Vector2 tempIndex = tileOne.cellIndex;
        tileOne.cellIndex = tileTwo.cellIndex;
        tileTwo.cellIndex = tempIndex;

        Vector2 onePosition = one.transform.position;
        Vector2 twoPosition = two.transform.position;

        float distance = Vector2.Distance(twoPosition, onePosition);
        while (distance > 0.3f)
        {
            one.transform.position = Vector2.Lerp(one.transform.position, twoPosition, Time.deltaTime * 7f);
            two.transform.position = Vector2.Lerp(two.transform.position, onePosition, Time.deltaTime * 7f);
            distance = Vector2.Distance(one.transform.position, twoPosition);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        one.transform.position = twoPosition;
        two.transform.position = onePosition;
    }

    public Customcolor GetColor(out int colorIndex)
    {
        colorIndex = Random.Range(0, colorList.Count);
        return colorList[colorIndex];
    }
    public void ResetColor()
    {
        StartCoroutine("ColorReseter");
        ResetUndo();
        //Score(0);
        ResetScore?.Invoke();
    }
    IEnumerator ColorReseter()
    {
        Loading = true;
        float totalTiles = (float)gridArray.Length;
        float loadedTiles = 0;
        ResetColorList();
        for (int x = 0; x < rowLength; x++)
        {
            for (int y = 0; y < colLength; y++)
            {
                gridArray[x, y].GetComponent<SpriteRenderer>().color = Color.black;
                yield return new WaitForSeconds(Time.deltaTime * 5f);
                int colorIndex;
                Customcolor newCustColor = GetColor(out colorIndex);
                gridArray[x, y].GetComponent<Tile>().ChangeColor(newCustColor, colorIndex);
                gridArray[x, y].GetComponent<Tile>().UnMatched();
                loadedTiles++;
                float percentage = loadedTiles / totalTiles;
                fillImg.fillAmount = percentage;
            }
        }
        List<GameObject> tempList = new List<GameObject>();
        foreach (var item in gridArray)
        {
            tempList.Add(item);
        }
        AddToColorSetList(tempList);
        //RemoveUnwantedColor();
        Loading = false;
    }
    public bool Loading
    {
        get { return loading; }
        set
        {
            loading = value;
            if (loading)
            {
                btn.interactable = false;
            }
            else
            {
                btn.interactable = true;
            }
        }
    }
    void SetCameraPosition()
    {
        float cx = (((float)rowLength - 1f) * tileGapX) / 2f;
        float cy = (((float)colLength - 1f) * tileGapY) / 2f;
        Camera.main.transform.position = new Vector3(cx, cy, -14f);

        Camera.main.orthographicSize = cx * 2 + tileGapX / 2f + 16 / 9;
    }
    int scoreValue = 0;
    public OnIntChange scoreCal;
    public OnChange ResetScore;
    void Score(int totalMatchCount)
    {
        scoreValue = totalMatchCount * 2 * (Mathf.Abs(totalMatchCount) - 1);
        scoreCal.Invoke(scoreValue);
    }
    public void CreateGridBtn(int rows, int col)
    {
        if (rows <= 1 && col <= 1)
        {
            return;
        }
        rowLength = rows;
        colLength = col;
        ResetUndo();
        //Score(0);
        ResetScore.Invoke();    
        StartCoroutine(CreateGrid());
    }
    #region music

    public OnChange swipe;
    public OnChange onMatch;

    #endregion

    #region Undo
    [Header("Undo Variables")]
    [Space(50f)]

    //public GameObject[] undoSelectedGameObject = new GameObject[0];
    //public Direction[] undoSelectedObjDirection = new Direction[0];
    //public List<GameObject>[] undoMatchGameObject = new List<GameObject>[0];
    public UndoClass[] undoClassArray;

    GameObject undoSeletedObject;
    Vector2 undoSelectedObjDir;
    Tile undoSelectTile;
    UndoClass undoClass;
    List<GameObject> undoSelectedList;

    public OnStringChange undoCountText;
    public int maxCount;
    int undoCount = 0;
    public int UndoCount
    {
        get { return undoCount; }
        set
        {
            undoCount = value;
            if (undoCount > maxCount)
            {
                undoCount = maxCount;
            }
            undoCountText.Invoke("UNDO "+"(" + undoCount.ToString() + ")");

        }
    }
    void ResetUndo()
    {
        undoClassArray = new UndoClass[maxCount];
        UndoCount = 0;
    }
    void RegisterMove(GameObject obj, Vector2 dir, List<GameObject> matchedList)
    {
        // undoSelectedGameObject.Push(obj);
        //if (undoSelectedGameObject.Length != maxCount)
        //{
        //    undoSelectedGameObject = new GameObject[maxCount];
        //}
        //UndoScript<GameObject>.AddObjectToUndoArray(undoSelectedGameObject, obj);

        //// undoSelectedGameObjectDirection.Push(-dir);
        //if (undoSelectedObjDirection.Length != maxCount)
        //{
        //    undoSelectedObjDirection = new Direction[maxCount];
        //}
        //UndoScript<Direction>.AddObjectToUndoArray(undoSelectedObjDirection, new Direction(dir));
        
        if (undoClassArray.Length != maxCount)
        {
            undoClassArray = new UndoClass[maxCount];
        }

        UndoClass undoClassObj = new UndoClass(obj, -dir, matchedList);
        UndoScript<UndoClass>.AddObjectToUndoArray(undoClassArray, undoClassObj);
        UndoCount++;
    }

    //void RegisterMatchObject(List<GameObject> matchGameObject)
    //{
    //    //undoMatchGameObject.Push(matchGameObject);
    //    if (undoMatchGameObject.Length != maxCount)
    //    {
    //        undoMatchGameObject = new List<GameObject>[maxCount];
    //    }
    //    UndoScript<List<GameObject>>.AddObjectToUndoArray(undoMatchGameObject, matchGameObject);
    //}
    public void UndoBtn()
    {
        if (inProcess || UndoCount == 0)
        {
            return;
        }
        undoClass = UndoScript<UndoClass>.GetObjectFromUndoArray(undoClassArray);
        if (undoClass.undoObject != null)
        {
            undoSeletedObject = undoClass.undoObject;
            undoSelectedObjDir = undoClass.undoDirection;
            undoSelectedList = undoClass.undoMatchedObjectList;
            undoSelectTile = undoSeletedObject.GetComponent<Tile>();
             
            StartCoroutine(IUndo());
        }
    }

    IEnumerator IUndo()
    {   
        inProcess = true;
        //undoSelectedObjDir = undoSelectedGameObjectDirection.Pop();

        int undoSelectedTileObjectX = (int)undoSelectTile.cellIndex.x;
        int undoSelectedTileObjectY = (int)undoSelectTile.cellIndex.y;
        //Vector2 undoSwipeDirection = -undoSelectedObjDir.dir;

        int undoTargertTileObjectX = (int)undoSelectedTileObjectX + (int)undoSelectedObjDir.x;
        int undoTargertTileObjectY = (int)undoSelectedTileObjectY + (int)undoSelectedObjDir.y;

        GameObject undoTargetObject = gridArray[undoTargertTileObjectX, undoTargertTileObjectY];
        yield return StartCoroutine(ISwitchPosition(undoSeletedObject, undoTargetObject));

        //List<GameObject> matchTile = UndoScript<List<GameObject>>.GetObjectFromUndoArray(undoMatchGameObject);
        foreach (var item in undoSelectedList)
        {
            item.GetComponent<Tile>().UnMatched();
        }
        Score(-undoSelectedList.Count);
        AddToColorSetList(undoSelectedList);
        UndoCount--;
        DisplayColorInfo();

        inProcess = false;
    }
    #endregion

    #region GameOver
    public List<ColorSet> colorSetList;
    public OnStringChange colorValInfo;
    public void ResetColorList()
    {
        colorSetList = new List<ColorSet>();
    }
    public void DisplayColorInfo()
    {
        string info = "";
        foreach (ColorSet colorSet in colorSetList)
        {
            info += colorSet.colorName + " : " + colorSet.count.ToString() + " \n"; 
        }
        colorValInfo.Invoke(info);
    }
    public void AddToColorSetList(List<GameObject> tileList)
    {
        foreach (GameObject obj in tileList)
        {
            int colorIndex = obj.GetComponent<Tile>().colorIndex;
            string colorName = obj.GetComponent<Tile>().colorName;

            bool createNewColorList = false;
            foreach (ColorSet item in colorSetList)
            {
                if (item.colorIndex == colorIndex)
                {
                    item.AddToList(obj);
                    DisplayColorInfo();
                    createNewColorList = true;
                    break;
                }
            }
            if (!createNewColorList)
            {
                ColorSet newColorSet = new ColorSet(colorIndex, colorName, rowLength);
                newColorSet.AddToList(obj);
                colorSetList.Add(newColorSet);
            }
        }
        RemoveUnwantedColor();
        DisplayColorInfo();
    }
    void RemoveUnwantedColor()
    {
        for (int i = 0; i < colorSetList.Count; i++)
        {
            int colorSetCount = colorSetList.Count;
            foreach (ColorSet colorSet in colorSetList)
            {
                if (colorSet.Count < 3)
                {
                    colorSetList.Remove(colorSet);
                    break;
                }
            }
        }
    }
    public void RemoveFromColorSetList(List<GameObject> tileList)
    {
        foreach (GameObject obj in tileList)
        {
            int colorIndex = obj.GetComponent<Tile>().colorIndex;
            string colorName = obj.GetComponent<Tile>().colorName;
            foreach (ColorSet item in colorSetList)
            {
                if (item.colorIndex == colorIndex)
                {
                    item.RemoveFromList(obj);
                    break;
                }
            }
        }

        if (CheckIfAllMatched())
        {
            
            if (CheckIfCountZero())
            {
                //Invoke("GameWin", 0.5f);
                onGameEnd.Invoke("You Win");
            }
            else
            {
                //Invoke("GameLost", 0.5f);
                onGameEnd.Invoke("You Lost");
            }
        }
        DisplayColorInfo();
    }
    public OnStringChange onGameEnd;
    bool CheckIfAllMatched()
    {
        int i = colorSetList.Count;
        foreach (var item in colorSetList)
        {
            if (item.canBeMatched == false)
            {
                i--;
            }
        }
        return (i == 0);
    }
    bool CheckIfCountZero()
    {
        int i = colorSetList.Count;
        foreach (var item in colorSetList)
        {
            if (item.Count == 0)
            {
                i--;
            }
        }
        return (i == 0);
    }
    //private void GameWin()
    //{
    //    uIMngr.GameWin();
    //}
    //private void GameLost()
    //{
    //    uIMngr.GameLost();
    //}
    #endregion
}

// Direction class

[System.Serializable]
public class Direction : MonoBehaviour
{
    [Space(20f)]
    public Vector2 dir;

    //Constructor
    public Direction(Vector2 direction)
    {
        dir = direction;
    }
}

[System.Serializable]
public class UndoClass : ScriptableObject
{
    [Space(20f)]
    public GameObject undoObject;
    public Vector2 undoDirection;
    public List<GameObject> undoMatchedObjectList;

    //Constructor
    public UndoClass(GameObject obj, Vector2 dir, List<GameObject> listObjs)
    {
        undoObject = obj;
        undoDirection = dir;
        undoMatchedObjectList = listObjs;
    }
}

[System.Serializable]
public class Customcolor
{
    public string colorName;
    public Color color;
}

[System.Serializable]
public class ColorSet
{
    [Space(20f)]
    public string colorName;
    public int count;
    public int colorIndex;
    public bool canBeMatched = true;
    int rows;
    public List<GameObject> objList;

    public int Count
    {
        get { return count; }
        set
        {
            count = value;
            if (count < 0)
            {
                count = 0;
            }
            canBeMatched = count >= 3;
        }
    }
    public ColorSet(int _colorIndex, string _name, int _rows)
    {
        colorIndex = _colorIndex;
        colorName = _name;
        rows = _rows;
        objList = new List<GameObject>();
    }
    public void AddToList(GameObject obj)
    {
        objList.Add(obj);
        Count++;
        if (rows > 2 && rows < 5)
        {
            if (Count == rows + 1)
            {
                Count--;
            }
        }
    }
    public void RemoveFromList(GameObject obj)
    {
        if (objList.Contains(obj))
        {
            objList.Remove(obj);
        }
        Count--;
    }
}