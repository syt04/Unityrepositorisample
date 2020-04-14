using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Main_Stage : MonoBehaviour
{
    [SerializeField]
    Sprite cellForestSprite;             //Cell型


    [SerializeField]
    Sprite[] TilesSprite;
    [SerializeField]
    Main_tile TilePrefab;

    [SerializeField]
    Main_tile OutWallplefab;


    public Main_tile Block;
    Transform unitContainer;
   public List<Main_tile> cells = new List<Main_tile>();

   static UnitObject focusingunit;

    private int[,] map;

    const int wall = 9;
    const int road = 0;
    const int room = 1;

    public int[,] Map { get => map; set => map = value; }

    /// <summary>
    /// 任意のマスを取得します
    /// </summary>
    /// <returns>The cell.</returns>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    public Main_tile GetCells(int x, int y)
    {
        return cells.First(c => c.X == x && c.Y == y);

    }

    public Main_tile[] GetCellsByDistance(Main_tile baseCell, int distanceMin, int distanceMax)
    {
        return cells.Where(x =>
        {
            var distance = Math.Abs(baseCell.X - x.X) + Math.Abs(baseCell.Y - x.Y);
            return distanceMin <= distance && distance <= distanceMax;
        }).ToArray();
    }

    MoveAmountInfo[] GetCellsByDistanceAtack(Main_tile startCell, int distanceMax)
    {
        //Debug.Log("残移動力けいさん実行");
        var infos = new List<MoveAmountInfo>();      //moveamountinfo型のリスト　infos
        infos.Add(new MoveAmountInfo(startCell.X, startCell.Y, distanceMax));    // startcellの位置を加える
        for (var i = distanceMax; i >= 0; i--)         //移動力まで行う   iは移動する数
        {
            var appendInfos = new List<MoveAmountInfo>();              //appendinfos はmoveamountiinfo型のリスト
            foreach (var calcTargetInfo in infos.Where(info => info.amount == i))    //infosの中のamountとiが一致するものをcalcTargetInfo　移動力と一致する位置
            {
                // 四方のマスの座標配列を作成
                var calcTargetCoordinate = calcTargetInfo.coordinate;    //calcTargetCoordinate = moveamountマス先,,,~1マス先 最初の位置から移動力0のところまで
                var aroundCellCoordinates = new Coordinate[]
                {
                    new Coordinate(calcTargetCoordinate.x - 1, calcTargetCoordinate.y),//左
                    new Coordinate(calcTargetCoordinate.x + 1, calcTargetCoordinate.y),//→
                    new Coordinate(calcTargetCoordinate.x, calcTargetCoordinate.y - 1),//上
                    new Coordinate(calcTargetCoordinate.x, calcTargetCoordinate.y + 1),//下
                };
                // 四方のマスの残移動力を計算
                foreach (var aroundCellCoordinate in aroundCellCoordinates)   //周りの座標配列を繰り返す
                {
                    var targetCell = cells.FirstOrDefault(c => c.X == aroundCellCoordinate.x && c.Y == aroundCellCoordinate.y);
                    //  targetに最初の周りのセルと等しい物を入れる
                    if (null == targetCell || //周りのセルがないor周りのセルと等しいorappendInfosが周りのセルと等しい
                        infos.Any(info => info.coordinate.x == aroundCellCoordinate.x && info.coordinate.y == aroundCellCoordinate.y) ||
                        appendInfos.Any(info => info.coordinate.x == aroundCellCoordinate.x && info.coordinate.y == aroundCellCoordinate.y))
                    {
                        // マップに存在しない、または既に計算済みの座標はスルー
                        continue;
                    }
                    if (!targetCell.canAtack)
                    {
                        var remainingMoveAmount = 0; //remaingmoveamount　に移動力-移動コストを入れる
                        appendInfos.Add(new MoveAmountInfo(aroundCellCoordinate.x, aroundCellCoordinate.y, remainingMoveAmount));
                    }
                    else
                    {
                        var remainingMoveAmount = i - 1; //remaingmoveamount　に移動力-移動コストを入れる
                        appendInfos.Add(new MoveAmountInfo(aroundCellCoordinate.x, aroundCellCoordinate.y, remainingMoveAmount));
                    }
                   
                        //appendinfosに周りのセルの座標と残移動力を入れる。
                    
                }
            }
            infos.AddRange(appendInfos);
        }
        // 残移動力が0以上（移動可能）なマスの情報だけを返す
        return infos.Where(x => x.amount >= 0).ToArray();
    }

    /// <summary>
    /// 選択中のユニットを返します
    /// </summary>
    /// <value>The active unit.</value>
    public UnitObject FocusingUnit
    {
        set {               
            focusingunit = value;
            SkilButtonScript.Instance.CloseSkilRan();
        }
        get { return focusingunit; }//unitContainer.GetComponentsInChildren<UnitScript>().FirstOrDefault(x => x.IsFocusing); }
    }

    /// <summary>
    /// マスのハイライトを消します
    /// </summary>
    public void ClearHighlight()
    {
        foreach (var cell in cells)
        {

            if (cell.IsAttackable)
            {
                // cell.Unit.GetComponent<Button>().interactable = false;
            }
            
            cell.IsMovable = false;
            cell.IsFirstFocusable = false;
            cell.IsSecondFocusable = false;
            cell.IsSkilable = false;
            cell.IsSecondSkilable = false;
            cell.IsSkilAttackable = false;
        }
    }

    


    /// <summary>
    /// 任意の座標にいるユニットを取得します
    /// </summary>
    /// <returns>The unit.</returns>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    public UnitObject GetUnit(int x, int y)
    {
        unitContainer = GameObject.Find("UnitContainer").transform;
        return unitContainer.GetComponentsInChildren<UnitObject>().FirstOrDefault(u => u.X == x && u.Y == y);
    }

    //public Main_tile GetCell(int x, int y)
    //{
    //    return cells.First(c => c.X == x && c.Y == y);
    //}
    /// <summary>
    /// 攻撃可能なマスをハイライトします
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="moveAmount">Move amount.</param>
    public bool HighlightAttackableCells(UnitObject unit)
    {
        var startCell = cells.First(c => c.X == unit.X && c.Y == unit.Y);
        var hasTarget = false;
        foreach (var cell in GetCellsByDistance(startCell, unit.Unit.AttackRangeMin, unit.Unit.AttackRangeMax))
        {
            if (null != cell.Unit && cell.Unit.team != GameState.Instance.currentTeam && unit.team != cell.Unit.team)
            {
                hasTarget = true;
                cell.IsAttackable = true;
                //cell.Unit.GetComponent<Button>().interactable = true;
            }
        }
        return hasTarget;
    }

    //public bool HighlightAttackableCells2(int x, int y, int attackRangeMin, int attackRangeMax)
    //{
    //    var startCell = cells.First(c => c.X == x && c.Y == y);
    //    var hasTarget = false;
    //    foreach (var info in GetRemainingMoveAmountInfos(startCell, attackRangeMax))
    //    {
    //        // Debug.Log("befor HilightMovableCells" + cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y).IsMovable);
    //        if (null != cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y).Unit && cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y).Unit.team != GameState.Instance.currentTeam)
    //        {
    //            hasTarget = true;
    //            cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y).IsAttackable = true;
    //        }

    //        //Debug.Log("after HilightMovableCells"+cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y).IsMovable);
    //    }
    //    return hasTarget;
    //}

    //public void Attackablebutton(int x,int y, int attackRangeMin,int attackRangeMax)
    //{
    //    //var startCell = cells.First(c => c.X == x && c.Y == y);
    //    //foreach (var info in GetRemainingMoveAmountInfos(startCell, attackRangeMax))
    //    //{
    //    //    // Debug.Log("befor HilightMovableCells" + cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y).IsMovable);
    //    //    cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y).IsAttackable = true;

    //    //    //Debug.Log("after HilightMovableCells"+cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y).IsMovable);
    //    //}

    //    var startCell = cells.First(c => c.X == x && c.Y == y);
      
    //    foreach (var cell in GetCellsByDistance(startCell, attackRangeMin, attackRangeMax))
    //    {

               
    //            cell.IsAttackable = true;
    //            //cell.Unit.GetComponent<Button>().interactable = true;
            
    //    }

    //}

    public void Generate(int width, int height, int roomMinHeight, int roomMaxHeight, int roomMinWidth, int roomMaxWidth,
                         int RoomCountMin, int RoomCountMax,int corridermin,int corridermax)
    {
        


        cells = new List<Main_tile>();

        Transform TileContainer = GameObject.Find("TileContainer").transform;

        for (int y = -1; y < height + 1; y++)
        {
            for (int x = -1; x < width + 1; x++)
            {
                Main_tile cell;

                var rand = UnityEngine.Random.Range(0, 4);


                    cell = Instantiate(TilePrefab, TileContainer);
                    cell.GetComponent<SpriteRenderer>().sprite = TilesSprite[rand];

                //if (x > 15 && x <= width / 2 && y < 6 && y > 3) { cell = Instantiate(cellForestPrefab); }

                int Cellrand = UnityEngine.Random.Range(0, 100);

                if (x == -1 || x == width || y == -1 || y == height)
                {
                    cell = Instantiate(OutWallplefab, TileContainer);
                }
                else if (map[y + 1, x + 1] == room)
                {
                    cell = Instantiate(TilePrefab, TileContainer);
                    cell.GetComponent<SpriteRenderer>().sprite = cellForestSprite;
                    cell.Cost = 2;
                }
                else if (map[y + 1, x + 1] == road)
                {
                    //cell = Instantiate(TilePrefab, TileContainer);
                    //cell.GetComponent<SpriteRenderer>().sprite = cellForestSprite;
                    //cell.Cost = 2;
                }
                //else if (Cellrand <= 80)
                //{

                //}
                //else if (Cellrand <= 90)
                //{
                //    cell = Instantiate(TilePrefab, TileContainer);
                //    cell.GetComponent<SpriteRenderer>().sprite = cellForestSprite;
                //    cell.Cost = 2;
                //}
                //else if (Cellrand <= 100) { cell = Instantiate(Block, TileContainer); }


                else if (Tiles[x][y] != null)
                {
                    if (Tiles[x][y].Room)
                    {
                        cell = Instantiate(TilePrefab, TileContainer);
                        cell.GetComponent<SpriteRenderer>().sprite = cellForestSprite;
                        cell.Cost = 2;
                    }
                }




                //if ((x == width / 2 + 5 && (height / 2 + 5 >= y && y > height / 2 + 2)) || ((width / 2 + 5 > x && x > width / 2 + 2) && y == height / 2 + 5) || (x == width / 2 + 5 && (height / 2 - 5 <= y) && y < height / 2 - 2) || (width / 2 - 5 <= x && x < width / 2 - 2) && y == height / 2 + 5
                //    || x == width / 2 - 5 && (height / 2 - 5 <= y && y < height / 2 - 2) || (width / 2 - 5 <= x && x < width / 2 - 2) && y == height / 2 - 5
                //     || x == width / 2 - 5 && (height / 2 + 5 > y && y > height / 2 + 2) || (width / 2 + 5 >= x && x > width / 2 + 2) && y == height / 2 - 5)
                //{
                //    cell = Instantiate(Block);
                //}

                //if ((x == width / 2 || x == width / 2 - 1 || x == width / 2 + 1) && (y == height / 2 || y == height / 2 - 1 || y == height / 2 + 1))
                //{
                //    cell = Instantiate(Block, TileContainer);
                //}





                cell.gameObject.SetActive(true);

                cell.SetCoordinate(x, y);
                cell.transform.position += new Vector3(x, y);
                cells.Add(cell);

            }
        }
    }



    void Start()
    {

        unitContainer = GameObject.Find("UnitContainer").transform;
        //ResetMapData(NewBehaviourScript.Instance.width,NewBehaviourScript.Instance.height);


    }



    public void HighlightMovableCells(int x, int y, int moveAmount)
    {
        ResetMovableCells();

        var startCell = cells.First(c => c.X == x && c.Y == y);
        foreach (var info in GetRemainingMoveAmountInfos(startCell, moveAmount))
        {
            // Debug.Log("befor HilightMovableCells" + cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y).IsMovable);
            cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y).IsMovable = true;

            //Debug.Log("after HilightMovableCells"+cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y).IsMovable);
        }
    }

    public Main_tile FirstFocusingcells(UnitObject foucusunit)
    {
            cells.First(c => c.X == foucusunit.X && c.Y == foucusunit.Y).IsFirstFocusable = true;
            return cells.First(c => c.X == foucusunit.X && c.Y == foucusunit.Y);
    }

    public Main_tile SecondFocusingcells(int x, int y)
    {
        if (focusingunit != null && !focusingunit.IsMoved)
        {
            foreach (var cell in cells.Where(c => c.IsSecondFocusable))
            {
                cell.IsSecondFocusable = false;
                cell.IsMovable = true;
            }             
            cells.First(c => c.X == x && c.Y == y).IsSecondFocusable = true;
            //StatusManager.Instance.RanOpen();
            //Debug.Log("x" + x + "y" + y);
            return cells.First(c => c.X == x && c.Y == y);
        }

        Debug.Log("null");
        return null;
    }

    public Main_tile SerchSecondFocusing()
    {
        if(cells.Any(c => c.IsSecondFocusable == true))
        return cells.First(c => c.IsSecondFocusable);

        return null;
    }


    /// <summary>
    /// スキル対象1マスを用
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Main_tile AttackSkilFocusingcells(int x, int y)
    {
        if (focusingunit != null && !focusingunit.Atacked)
        {
            foreach (var cell in cells.Where(c => c.IsSkilAttackable))
            {
                cell.IsSkilAttackable = false;
                cell.IsSecondSkilable = true;
            }
            if(SkilManager.Instance.UseSkil.Skil != null)
            cells.First(c => c.X == x && c.Y == y).IsSkilAttackable = true;          

            return cells.First(c => c.X == x && c.Y == y);
        }

        Debug.Log("null");
        return null;
    }


    /// <summary>
    /// スペルの基準タイルを返す
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Main_tile SpellAttackFocusingcells(int x, int y)
    {
        
            foreach (var cell in cells.Where(c => c.IsSpellAttackable))
            {
                cell.IsSpellAttackable = false;
                cell.IsSpellable = true;
            }
            if (SkilManager.Instance.UseSkil.Skil != null)
                cells.First(c => c.X == x && c.Y == y).IsSpellAttackable = true;
            return cells.First(c => c.X == x && c.Y == y);
        
    }

    /// <summary>
    /// スキル対象1マスを返す &UseButtun表示
    /// </summary>
    /// <returns></returns>
    public Main_tile SerchAttackSkilFocusing()
    {
        if (cells.Any(c => c.IsSkilAttackable == true))
        {
            return cells.First(c => c.IsSkilAttackable);
        }

        return null;
    }





    public void ResetMovableCells()
    {
        foreach (var cell in cells.Where(c => c.IsMovable))
        {
            cell.IsMovable = false;
            cell.IsFirstFocusable = false;
            cell.IsSecondFocusable = false;
            //Debug.Log("REsetMovableCells :"+cell.IsMovable);
        }
    }




    public Main_tile[] CalculateRouteCells(int x, int y, int moveAmount, Main_tile endCell)
    {
        //Debug.Log("CalculateRouteCell"+ "x= " + x + " y= " + y+"endcellxy"+endCell.X+endCell.Y);
        var startCell = cells.First(c => c.X == x && c.Y == y);
        var infos = GetRemainingMoveAmountInfos(startCell, moveAmount);
        if (!infos.Any(info => info.coordinate.x == endCell.X && info.coordinate.y == endCell.Y))
        {

            throw new ArgumentException(string.Format("endCell(x:{0}, y:{1}) is not movable.", endCell.X, endCell.Y));
        }

        var endCellMoveAmountInfo = infos.First(info => info.coordinate.x == endCell.X && info.coordinate.y == endCell.Y);
        var routeCells = new List<Main_tile>();
        routeCells.Add(endCell);
        while (true)
        {
            var currentCellInfo = infos.First(info => info.coordinate.x == routeCells[routeCells.Count - 1].X && info.coordinate.y == routeCells[routeCells.Count - 1].Y);
            var currentCell = cells.First(cell => cell.X == currentCellInfo.coordinate.x && cell.Y == currentCellInfo.coordinate.y);
            var previousMoveAmount = currentCellInfo.amount + currentCell.Cost;
            var previousCellInfo = infos.FirstOrDefault(info => (Mathf.Abs(info.coordinate.x - currentCell.X) + Mathf.Abs(info.coordinate.y - currentCell.Y)) == 1 && info.amount == previousMoveAmount);
            if (null == previousCellInfo)
            {
                break;
            }
            routeCells.Add(cells.First(c => c.X == previousCellInfo.coordinate.x && c.Y == previousCellInfo.coordinate.y));
        }
        routeCells.Reverse();
        return routeCells.ToArray();
    }


    ///// <summary>
    ///// 指定座標にユニットを配置します
    ///// </summary>
    ///// <param name="x">The x coordinate.</param>
    ///// <param name="y">The y coordinate.</param>
    ///// <param name="unitPrefab">Unit prefab.</param>
    //public void PutUnit(int x, int y, UnitObject unitPrefab, UnitTypes type, Teams team)
    //{
    //    var unit = Instantiate(unitPrefab);
    //    unit.team = team;
    //    switch (unit.team)
    //    {
    //        case Teams.Player2:
    //            // 敵ユニットはちょっと色を変えて反転
    //            var image = unit.GetComponent<SpriteRenderer>();
    //            image.color = new Color(1f, 0.7f, 0.7f);
    //            var scale = image.transform.localScale;
    //            scale.x *= -1f;
    //            image.transform.localScale = scale;
    //            break;
    //    }
    //    unitContainer = GameObject.Find("UnitContainer").transform;
    //    unit.gameObject.SetActive(true);
    //    unit.transform.SetParent(unitContainer);
    //    unit.transform.position = cells.First(c => c.X == x && c.Y == y).transform.position;
    //    unit.X = x;
    //    unit.Y = y;

    //    UnitScript unitScript = unit.Unit;

    //    unitScript.UnitType = type;
    //    unitScript.LvType = unitPrefab.LvType;
    //    unitScript.Exp = unitPrefab.Exp;
    //    unitScript.Strength = unitPrefab.Strength;
    //    unitScript.Armor = unitPrefab.Armor;
    //    unitScript.Intellect = unitPrefab.Intellect;
    //    unitScript.Agility = unitPrefab.Agility;

    //    if (unit.team == Teams.Player1)
    //    {
    //        unit.tag = "Player";
    //    }
    //    else
    //        unit.tag = "Enemy";


    //    //Debug.Log("putunit"+unit.name);
    //}

    /// <summary>
    /// 残移動量計算
    /// </summary>
    /// <param name="startCell"></param>
    /// <param name="moveAmount"></param>
    /// <returns></returns>
   public MoveAmountInfo[] GetRemainingMoveAmountInfos(Main_tile startCell, int moveAmount)
    {
        var infos = new List<MoveAmountInfo>();     
        infos.Add(new MoveAmountInfo(startCell.X, startCell.Y, moveAmount));    // startcellの位置を加える
        for (var i = moveAmount; i >= 0; i--)         //移動力まで行う   iは移動する数
        {
            var appendInfos = new List<MoveAmountInfo>();              
            foreach (var calcTargetInfo in infos.Where(info => info.amount == i))    //infosの中のamountとiが一致するものをcalcTargetInfo　移動力と一致する位置
            {
                // 四方のマスの座標配列を作成
                var calcTargetCoordinate = calcTargetInfo.coordinate;    //calcTargetCoordinate = moveamountマス先,,,~1マス先 最初の位置から移動力0のところまで
                var aroundCellCoordinates = new Coordinate[]
                {
                    new Coordinate(calcTargetCoordinate.x - 1, calcTargetCoordinate.y),//左
                    new Coordinate(calcTargetCoordinate.x + 1, calcTargetCoordinate.y),//→
                    new Coordinate(calcTargetCoordinate.x, calcTargetCoordinate.y - 1),//上
                    new Coordinate(calcTargetCoordinate.x, calcTargetCoordinate.y + 1),//下
                };
                // 四方のマスの残移動力を計算
                foreach (var aroundCellCoordinate in aroundCellCoordinates)   //周りの座標配列を繰り返す
                {
                    var targetCell = cells.FirstOrDefault(c => c.X == aroundCellCoordinate.x && c.Y == aroundCellCoordinate.y);
                    //  targetに最初の周りのセルと等しい物を入れる
                    if (null == targetCell || //周りのセルがないor周りのセルと等しいorappendInfosが周りのセルと等しい
                        infos.Any(info => info.coordinate.x == aroundCellCoordinate.x && info.coordinate.y == aroundCellCoordinate.y) ||
                        appendInfos.Any(info => info.coordinate.x == aroundCellCoordinate.x && info.coordinate.y == aroundCellCoordinate.y))
                    {
                        // マップに存在しない、または既に計算済みの座標はスルー
                        continue;
                    }
                    var remainingMoveAmount = i - targetCell.Cost; //remaingmoveamount　に移動力-移動コストを入れる
                    appendInfos.Add(new MoveAmountInfo(aroundCellCoordinate.x, aroundCellCoordinate.y, remainingMoveAmount));
                    //appendinfosに周りのセルの座標と残移動力を入れる。
                }
            }
            infos.AddRange(appendInfos);
        }
        // 残移動力が0以上（移動可能）なマスの情報だけを返す
        return infos.Where(x => x.amount >= 0).ToArray();
    }


    /// <summary>
    /// 残移動力情報クラス
    /// </summary>
   public class MoveAmountInfo
    {
        public readonly Coordinate coordinate;
        public readonly int amount;

        public MoveAmountInfo(int x, int y, int amount)
        {
            this.coordinate = new Coordinate(x, y);
            this.amount = amount;
        }
    }




    /// <summary>
    /// 座標と数値情報を紐付けるためのクラス
    /// </summary>
    public class CoordinateAndValue
    {
        public readonly Coordinate coordinate;
        public readonly int value;

        public CoordinateAndValue(int x, int y, int value)
        {
            this.coordinate = new Coordinate(x, y);
            this.value = value;
        }
    }

    /// <summary>
    /// 座標クラス
    /// </summary>
    public class Coordinate
    {
        public readonly int x;
        public readonly int y;

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    ///// <summary>
    ///// 自軍のユニットを取得します
    ///// </summary>
    ///// <returns>The own units.</returns>
    //public UnitScript[] GetOwnUnits()
    //{
    //    unitContainer = GameObject.Find("UnitContainer").transform;

    //    return unitContainer.GetComponentsInChildren<UnitScript>().Where(x => x.team == currentTeam).ToArray();
    //}

    ///// <summary>
    ///// 敵軍のユニットを取得します
    ///// </summary>
    ///// <returns>The enemy units.</returns>
    //public UnitScript[] GetEnemyUnits()
    //{
    //    while (unitContainer == null)
    //    {
    //        unitContainer = GameObject.Find("UnitContainer").transform;
    //    }
    //    return unitContainer.GetComponentsInChildren<UnitScript>().Where(x => x.team != currentTeam).ToArray();
    //}

    /// <summary>
    /// 指定座標から各マスまで、移動コストいくつで行けるかを計算します
    /// </summary>
    /// <returns>The move amount to cells.</returns>
    /// <param name="from">From.</param>
    public List<CoordinateAndValue> GetMoveCostToAllCells(Main_tile from)
    {
        var infos = new List<CoordinateAndValue>();
        infos.Add(new CoordinateAndValue(from.X, from.Y, 0)); //最初のマスを加える
        var i = 0;
        while (true)
        {
            var appendInfos = new List<CoordinateAndValue>();
            foreach (var calcTargetInfo in infos.Where(info => info.value == i))
            {
                // 四方のマスの座標配列を作成
                var calcTargetCoordinate = calcTargetInfo.coordinate;
                var aroundCellCoordinates = new Coordinate[] 
                {
                    new Coordinate(calcTargetCoordinate.x - 1, calcTargetCoordinate.y),
                    new Coordinate(calcTargetCoordinate.x + 1, calcTargetCoordinate.y),
                    new Coordinate(calcTargetCoordinate.x, calcTargetCoordinate.y - 1),
                    new Coordinate(calcTargetCoordinate.x, calcTargetCoordinate.y + 1),
                };
                // 四方のマスの残移動力を計算
                foreach (var aroundCellCoordinate in aroundCellCoordinates)
                {
                    var targetCell = cells.FirstOrDefault(c => c.X == aroundCellCoordinate.x && c.Y == aroundCellCoordinate.y);
                    if (null == targetCell ||
                        infos.Any(info => info.coordinate.x == aroundCellCoordinate.x && info.coordinate.y == aroundCellCoordinate.y) ||
                        appendInfos.Any(info => info.coordinate.x == aroundCellCoordinate.x && info.coordinate.y == aroundCellCoordinate.y))
                    {
                        // マップに存在しない、または既に計算済みの座標はスルー
                        continue;
                    }
                    var remainingMoveAmount = i + targetCell.Cost;
                    appendInfos.Add(new CoordinateAndValue(aroundCellCoordinate.x, aroundCellCoordinate.y, remainingMoveAmount));
                }
            }
            infos.AddRange(appendInfos);

            i++;
            if (i > infos.Max(x => x.value < 999 ? x.value : 0))
            {
                break;
            }
        }
        return infos.Where(x => x.value < 999).ToList();
    }




    /// <summary>
    /// 指定位置までの移動ルートと移動コストを返します
    /// </summary>
    /// <returns>The route coordinates and move amount.</returns>
    /// <param name="from">From.</param>
    /// <param name="to">To.</param>
    public List<CoordinateAndValue> CalcurateRouteCoordinatesAndMoveAmount(Main_tile from, Main_tile to)
    {
        var costs = GetMoveCostToAllCells(from);
        if (!costs.Any(info => info.coordinate.x == to.X && info.coordinate.y == to.Y))
        {
            throw new ArgumentException(string.Format("x:{0}, y:{1} is not movable.", to.X, to.Y));
        }

        var toCost = costs.First(info => info.coordinate.x == to.X && info.coordinate.y == to.Y);
        var route = new List<CoordinateAndValue>();
        route.Add(toCost);
        while (true)
        {
            var currentCost = route.Last();
            var currentCell = cells.First(cell => cell.X == currentCost.coordinate.x && cell.Y == currentCost.coordinate.y);
            var prevMoveCost = currentCost.value - currentCell.Cost;
            var previousCost = costs.FirstOrDefault(info => (Mathf.Abs(info.coordinate.x - currentCell.X) + Mathf.Abs(info.coordinate.y - currentCell.Y)) == 1 && info.value == prevMoveCost);
            if (null == previousCost)
            {
                break;
            }
            route.Add(previousCost);
        }
        route.Reverse();
        return route.ToList();
    }

    public Main_tile[] GetAttackableCells()
    {
        return cells.Where(x => x.IsAttackable).ToArray();
    }


    public Main_tile[] GetMovableCells()
    {
        return cells.Where(x => x.IsMovable).ToArray();
    }


    /// <summary>
    /// ユニットを対象のマスに移動させます
    /// </summary>
    /// <param name="cell">Cell.</param>
    public void MoveTo(UnitObject unit, Main_tile cell)
    {
        ClearHighlight();
       
        var routeCells = CalculateRouteCells(unit.X, unit.Y, unit.Unit.MoveAmount, cell);
        var sequence = DOTween.Sequence();
        for (var i = 1; i < routeCells.Length; i++)
        {
            var routeCell = routeCells[i];
            sequence.Append(unit.transform.DOMove(routeCell.transform.position, 0.1f).SetEase(Ease.Linear));
        }
        sequence.OnComplete(() =>
        {
            unit.X = routeCells[routeCells.Length - 1].X;
            unit.Y = routeCells[routeCells.Length - 1].Y;
            // 攻撃可能範囲のチェック
            bool isAttackable = false;
            if (GameState.Instance.currentTeam == Teams.Player2)
            {
                isAttackable = HighlightAttackableCells(unit);
            }                                                 
            if (!isAttackable)
            {
                //unit.IsMoved = true;
            }
        });

        //unit.IsMoved = true;
       
        
    }


    public void ResetMapData(int MapWidth, int MapHeight)
    {
        Map = new int[MapHeight + 2, MapWidth + 2];
        for (int i = 0; i < MapHeight + 1; i++)
        {
            for (int j = 0; j < MapWidth + 1; j++)
            {
                Map[i, j] = wall;
            }
        }
    }


    public void CreateSpaceData(int MapWidth, int MapHeight, int roomMinHeight, int roomMaxHeight, int roomMinWidth, int roomMaxWidth
        , int RoomCountMin, int RoomCountMax, int meetPointCount)
    {
        int roomCount = Random.Range(RoomCountMin, RoomCountMax);

        int[] meetPointsX = new int[meetPointCount];
        int[] meetPointsY = new int[meetPointCount];
        for (int i = 0; i < meetPointsX.Length; i++)
        {
            meetPointsX[i] = Random.Range(MapWidth / 4, MapWidth * 3 / 4);
            meetPointsY[i] = Random.Range(MapHeight / 4, MapHeight * 3 / 4);
            Map[meetPointsY[i], meetPointsX[i]] = room;
        }

        for (int i = 0; i < roomCount; i++)
        {
            int roomHeight = Random.Range(roomMinHeight, roomMaxHeight);
            int roomWidth = Random.Range(roomMinWidth, roomMaxWidth);
            int roomPointX = Random.Range(0, MapWidth - roomMaxWidth - 2);
            int roomPointY = Random.Range(0, MapHeight - roomMaxHeight - 2);

            int roadStartPointX = Random.Range(roomPointX, roomPointX + roomWidth);
            int roadStartPointY = Random.Range(roomPointY, roomPointY + roomHeight);

            bool isRoad = CreateRoomData(roomHeight, roomWidth, roomPointX, roomPointY);

            if (isRoad == false)
            {
                CreateRoadData(roadStartPointX, roadStartPointY, meetPointsX[Random.Range(0, 0)], meetPointsY[Random.Range(0, 0)]);
            }
        }


    }

    /// <summary>
    /// 部屋データを生成。すでに部屋がある場合はtrueを返し、道を作らないようにする
    /// </summary>
    /// <param name="roomHeight">部屋の高さ</param>
    /// <param name="roomWidth">部屋の横幅</param>
    /// <param name="roomPointX">部屋の始点(x)</param>
    /// <param name="roomPointY">部屋の始点(y)</param>
    /// <returns></returns>
    private bool CreateRoomData(int roomHeight, int roomWidth, int roomPointX, int roomPointY)
    {
        bool isRoad = false;
        for (int i = 0; i < roomHeight; i++)
        {
            for (int j = 0; j < roomWidth; j++)
            {
                if (Map[roomPointY + i, roomPointX + j] == room)
                {
                    isRoad = true;
                }
                else
                {
                    Map[roomPointY + i, roomPointX + j] = room;
                }
            }
        }
        return isRoad;
    }

    /// <summary>
    /// 道データを生成
    /// </summary>
    /// <param name="roadStartPointX"></param>
    /// <param name="roadStartPointY"></param>
    /// <param name="meetPointX"></param>
    /// <param name="meetPointY"></param>
    private void CreateRoadData(int roadStartPointX, int roadStartPointY, int meetPointX, int meetPointY)
    {

        bool isRight;
        if (roadStartPointX > meetPointX)
        {
            isRight = true;
        }
        else
        {
            isRight = false;
        }
        bool isUnder;
        if (roadStartPointY > meetPointY)
        {
            isUnder = false;
        }
        else
        {
            isUnder = true;
        }

        if (Random.Range(0, 2) == 0)
        {

            while (roadStartPointX != meetPointX)
            {

                Map[roadStartPointY, roadStartPointX] = road;
                if (isRight == true)
                {
                    roadStartPointX--;
                }
                else
                {
                    roadStartPointX++;
                }

            }

            while (roadStartPointY != meetPointY)
            {

                Map[roadStartPointY, roadStartPointX] = road;
                if (isUnder == true)
                {
                    roadStartPointY++;
                }
                else
                {
                    roadStartPointY--;
                }
            }

        }
        else
        {

            while (roadStartPointY != meetPointY)
            {

                Map[roadStartPointY, roadStartPointX] = road;
                if (isUnder == true)
                {
                    roadStartPointY++;
                }
                else
                {
                    roadStartPointY--;
                }
            }

            while (roadStartPointX != meetPointX)
            {

                Map[roadStartPointY, roadStartPointX] = road;
                if (isRight == true)
                {
                    roadStartPointX--;
                }
                else
                {
                    roadStartPointX++;
                }

            }

        }
    }

    public void SetUpTilesArray(int width, int height)
    {
        Tiles = new TileType[width+2][];


        for (int i = 0; i < width + 2; i++)
        {
            Tiles[i] = new TileType[height+2];

        }

    }

    public void CreateRoomsAndCorridors(int width, int height, IntRange numRooms, IntRange roomWidth, IntRange roomHeight, IntRange corridorLength)
    {

        rooms = new Room[numRooms.Random];

        corridors = new Corridor[rooms.Length - 1];

        rooms[0] = new Room();
        corridors[0] = new Corridor();

        rooms[0].SetupRoom(roomWidth, roomHeight, width, height);

        corridors[0].SetupCorridor(rooms[0], corridorLength, roomWidth, roomHeight, width, height, true);

        for (int i = 1; i < rooms.Length; i++)
        {
            rooms[i] = new Room();

            rooms[i].SetupRoom(roomWidth, roomHeight, width, height, corridors[i - 1]);

            if (i < corridors.Length)
            {
                corridors[i] = new Corridor();

                corridors[i].SetupCorridor(rooms[i], corridorLength, roomWidth, roomHeight, width, height, false);

            }

            if (i == rooms.Length * .5f)
            {
                Vector3 playerPos = new Vector3(rooms[i].xPos, rooms[i].xPos, 0);
                //Instantiate(player, playerPos,Quaternion.identity);
                //EnemyGanareter.Instance.PutUnit(rooms[i].xPos, rooms[i].yPos, UnitManager.Instance.GetAtRandom(), EnemyGanareter.Instance.RandomType(), Teams.Player2, EnemyGanareter.Instance.Kai);
                //EnemyGanareter.Instance.PutUnit(rooms[i].xPos, rooms[i].yPos+1, UnitManager.Instance.GetAtRandom(), EnemyGanareter.Instance.RandomType(), Teams.Player2, EnemyGanareter.Instance.Kai);

            }
        }
    }

    public void SetTilesValuesForRooms()
    {

        for (int i = 0; i < rooms.Length; i++)
        {
            Room currentRoom = rooms[i];

            for (int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoord = currentRoom.xPos + j;

                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPos + k;


                    Tiles[xCoord][yCoord] = new TileType(i);
                }

            }

        }


    }

    public void SetTilesValuesForCorridors()
    {

        for (int i = 0; i < corridors.Length; i++)
        {
            Corridor currentCorridor = corridors[i];

            for (int j = 0; j < currentCorridor.corridorLength; j++)
            {
                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;

                switch (currentCorridor.direction)
                {
                    case Direction.N:
                        yCoord += j;
                        break;
                    case Direction.E:
                        xCoord += j;
                        break;
                    case Direction.S:
                        yCoord -= j;
                        break;
                    case Direction.W:
                        xCoord -= j;
                        break;

                }

                Tiles[xCoord][yCoord]  = new TileType(false);

            }

        }

    }


    private TileType[][] tiles;
    private Room[] rooms;
    private Corridor[] corridors;
    public TileType[][] Tiles { get => tiles; set => tiles = value; }

}

public class TileType
{
    private bool room;
    private int roomNumber;

    public bool Room { get => room;}

   public TileType(bool Isroom)
    {
        this.room = Isroom;
    }
   public TileType(int roomNumber)
    {
        this.roomNumber = roomNumber;
        room = true;
    }

}