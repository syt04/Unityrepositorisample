//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;
//using System;
//using Random = UnityEngine.Random;
//using UnityEngine.UI;

//public class Main_Stage : MonoBehaviour
//{
//    //[SerializeField]
//    //Main_tile cellFieldPrefab;              //Cell型
//    //[SerializeField]
//    //Main_tile cellForestPrefab;             //Cell型
//    //[SerializeField]
//    //Main_tile cellFieldPrefabA;              //Cell型
//    [SerializeField]
//    Main_tile cellForestPrefab;             //Cell型

//    [SerializeField]
//    Main_tile[] floorTiles;

//    [SerializeField]
//    Main_tile OutWallplefab;

//    public Main_tile Block;

//    [SerializeField]
//    Transform unitContainer;

//    GameObject A;
//    List<Main_tile> cells = new List<Main_tile>();

//    public NewBehaviourScript Turnplayer;

//    Unit.Teams currentTeam;
//    /// <summary>
//    /// 任意のマスを取得します
//    /// </summary>
//    /// <returns>The cell.</returns>
//    /// <param name="x">The x coordinate.</param>
//    /// <param name="y">The y coordinate.</param>
//    public Main_tile GetCells(int x, int y)
//    {
//        return cells.First(c => c.X == x && c.Y == y);

//    }
//    public Main_tile[] GetCellsByDistance(Main_tile baseCell, int distanceMin, int distanceMax)
//    {
//        return cells.Where(x =>
//        {
//            var distance = Math.Abs(baseCell.X - x.X) + Math.Abs(baseCell.Y - x.Y);
//            return distanceMin <= distance && distance <= distanceMax;
//        }).ToArray();
//    }

//    /// <summary>
//    /// 選択中のユニットを返します
//    /// </summary>
//    /// <value>The active unit.</value>
//    public UnitScript FocusingUnit
//    {
//        get { return unitContainer.GetComponentsInChildren<UnitScript>().FirstOrDefault(x => x.IsFocusing); }
//    }

//    /// <summary>
//    /// 対象ユニットに攻撃します
//    /// </summary>
//    /// <param name="fromUnit">From unit.</param>
//    /// <param name="toUnit">To unit.</param>
//    public void AttackTo(UnitScript fromUnit, UnitScript toUnit)
//    {
//        //Battle_SceneController.attacker = fromUnit;
//        //Battle_SceneController.defender = toUnit;
//        //SceneManager.LoadScene("Battle", LoadSceneMode.Additive);
//        ClearHighlight();
//        FocusingUnit.IsMoved = true;
//    }

//    /// <summary>
//    /// マスのハイライトを消します
//    /// </summary>
//    public void ClearHighlight()
//    {
//        foreach (var cell in cells)
//        {
//            if (cell.IsAttackable)
//            {
//                cell.Unit.GetComponent<Button>().interactable = false;
//            }
//            cell.IsMovable = false;
//        }
//    }

//    /// <summary>
//    /// 任意の座標にいるユニットを取得します
//    /// </summary>
//    /// <returns>The unit.</returns>
//    /// <param name="x">The x coordinate.</param>
//    /// <param name="y">The y coordinate.</param>
//    public Unit GetUnit(int x, int y)
//    {
//        return unitContainer.GetComponentsInChildren<Unit>().FirstOrDefault(u => u.x == x && u.y == y);
//    }

//    //public Main_tile GetCell(int x, int y)
//    //{
//    //    return cells.First(c => c.X == x && c.Y == y);
//    //}
//    /// <summary>
//    /// 攻撃可能なマスをハイライトします
//    /// </summary>
//    /// <param name="x">The x coordinate.</param>
//    /// <param name="y">The y coordinate.</param>
//    /// <param name="moveAmount">Move amount.</param>
//    public bool HighlightAttackableCells(int x, int y, int attackRangeMin, int attackRangeMax)
//    {
//        var startCell = cells.First(c => c.X == x && c.Y == y);
//        var hasTarget = false;
//        foreach (var cell in GetCellsByDistance(startCell, attackRangeMin, attackRangeMax))
//        {
//            if (null != cell.Unit && cell.Unit.team != currentTeam)
//            {
//                hasTarget = true;
//                cell.IsAttackable = true;
//                cell.Unit.GetComponent<Button>().interactable = true;
//            }
//        }
//        return hasTarget;
//    }


//    public void Generate(int width, int height)
//    {
//        cells = new List<Main_tile>();

//        for (int y = -1; y < height + 1; y++)
//        {
//            for (int x = -1; x < width + 1; x++)
//            {
//                Main_tile cell;

//                var rand = UnityEngine.Random.Range(0, 4);


//                cell = Instantiate(floorTiles[rand]);

//                if (x > 15 && x <= width / 2 && y < 6 && y > 3) { cell = Instantiate(cellForestPrefab); }

//                /*
//                cell = Instantiate(floorTiles[Random.Range(0, floorTiles.Length)]);
//                */
//                if (x == -1 || x == width || y == -1 || y == height)
//                {
//                    cell = Instantiate(OutWallplefab);
//                }


//                if ((x == width / 2 + 5 && (height / 2 + 5 >= y && y > height / 2 + 2)) || ((width / 2 + 5 > x && x > width / 2 + 2) && y == height / 2 + 5) || (x == width / 2 + 5 && (height / 2 - 5 <= y) && y < height / 2 - 2) || (width / 2 - 5 <= x && x < width / 2 - 2) && y == height / 2 + 5
//                    || x == width / 2 - 5 && (height / 2 - 5 <= y && y < height / 2 - 2) || (width / 2 - 5 <= x && x < width / 2 - 2) && y == height / 2 - 5
//                     || x == width / 2 - 5 && (height / 2 + 5 > y && y > height / 2 + 2) || (width / 2 + 5 >= x && x > width / 2 + 2) && y == height / 2 - 5)
//                {
//                    cell = Instantiate(Block);
//                }

//                if ((x == width / 2 || x == width / 2 - 1 || x == width / 2 + 1) && (y == height / 2 || y == height / 2 - 1 || y == height / 2 + 1))
//                {
//                    cell = Instantiate(Block);
//                }



//                cell.gameObject.SetActive(true);

//                cell.SetCoordinate(x, y);
//                cell.transform.position += new Vector3(x, y);
//                cells.Add(cell);

//            }
//        }
//    }



//    /*
//    void Start()
//    {
//        foreach (var prefab in new Main_tile[] { cellFieldPrefab, cellForestPrefab })
//        {
//            prefab.gameObject.SetActive(false);
//        }
//    }
//    */




//    public void HighlightMovableCells(int x, int y, int moveAmount)
//    {
//        ResetMovableCells();

//        var startCell = cells.First(c => c.X == x && c.Y == y);
//        foreach (var info in GetRemainingMoveAmountInfos(startCell, moveAmount))
//        {
//            // Debug.Log("befor HilightMovableCells" + cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y).IsMovable);
//            cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y).IsMovable = true;

//            //Debug.Log("after HilightMovableCells"+cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y).IsMovable);
//        }
//    }

//    public void ResetMovableCells()
//    {
//        foreach (var cell in cells.Where(c => c.IsMovable))
//        {
//            cell.IsMovable = false;
//            //Debug.Log("REsetMovableCells :"+cell.IsMovable);
//        }
//    }




//    public Main_tile[] CalculateRouteCells(int x, int y, int moveAmount, Main_tile endCell)
//    {
//        //Debug.Log("CalculateRouteCell"+ "x= " + x + " y= " + y+"endcellxy"+endCell.X+endCell.Y);
//        var startCell = cells.First(c => c.X == x && c.Y == y);
//        var infos = GetRemainingMoveAmountInfos(startCell, moveAmount);
//        if (!infos.Any(info => info.coordinate.x == endCell.X && info.coordinate.y == endCell.Y))
//        {

//            throw new ArgumentException(string.Format("endCell(x:{0}, y:{1}) is not movable.", endCell.X, endCell.Y));
//        }

//        var endCellMoveAmountInfo = infos.First(info => info.coordinate.x == endCell.X && info.coordinate.y == endCell.Y);
//        var routeCells = new List<Main_tile>();
//        routeCells.Add(endCell);
//        while (true)
//        {
//            var currentCellInfo = infos.First(info => info.coordinate.x == routeCells[routeCells.Count - 1].X && info.coordinate.y == routeCells[routeCells.Count - 1].Y);
//            var currentCell = cells.First(cell => cell.X == currentCellInfo.coordinate.x && cell.Y == currentCellInfo.coordinate.y);
//            var previousMoveAmount = currentCellInfo.amount + currentCell.Cost;
//            var previousCellInfo = infos.FirstOrDefault(info => (Mathf.Abs(info.coordinate.x - currentCell.X) + Mathf.Abs(info.coordinate.y - currentCell.Y)) == 1 && info.amount == previousMoveAmount);
//            if (null == previousCellInfo)
//            {
//                break;
//            }
//            routeCells.Add(cells.First(c => c.X == previousCellInfo.coordinate.x && c.Y == previousCellInfo.coordinate.y));
//        }
//        routeCells.Reverse();
//        return routeCells.ToArray();
//    }



//    public void PutUnit(int x, int y, Unit unitPrefab, string name)
//    {

//        /*
//        var unit = Instantiate(unitPrefab);
//        unit.gameObject.SetActive(true);
//        unit.transform.SetParent(unitContainer);
//        unit.transform.position = cells.First(c => c.X == x && c.Y == y).transform.position;
//        unit.x = x;
//        unit.y = y;
//*/

//        var unit = Instantiate(unitPrefab);
//        unit.gameObject.SetActive(true);
//        unit.transform.position += new Vector3(x, y);
//        unit.x = x;
//        unit.y = y;
//        unit.unitname = name;
//        unit.name = name;
//    }

//    public void PutItem(int x, int y, GameObject Item)
//    {
//        Item.gameObject.SetActive(true);
//        Item.transform.position += new Vector3(x, y);

//    }

//    public void PutWepon(int x, int y, ItemObject Item)
//    {
//        Item.gameObject.SetActive(true);
//        Item.transform.position += new Vector3(x, y);
//        Item.SetX(x);
//        Item.SetY(y);

//    }



//    MoveAmountInfo[] GetRemainingMoveAmountInfos(Main_tile startCell, int moveAmount)
//    {
//        //Debug.Log("残移動力けいさん実行");
//        var infos = new List<MoveAmountInfo>();      //moveamountinfo型のリスト　infos
//        infos.Add(new MoveAmountInfo(startCell.X, startCell.Y, moveAmount));    // startcellの位置を加える
//        for (var i = moveAmount; i >= 0; i--)         //移動力まで行う   iは移動する数
//        {
//            var appendInfos = new List<MoveAmountInfo>();              //appendinfos はmoveamountiinfo型のリスト
//            foreach (var calcTargetInfo in infos.Where(info => info.amount == i))    //infosの中のamountとiが一致するものをcalcTargetInfo　移動力と一致する位置
//            {
//                // 四方のマスの座標配列を作成
//                var calcTargetCoordinate = calcTargetInfo.coordinate;    //calcTargetCoordinate = moveamountマス先,,,~1マス先 最初の位置から移動力0のところまで
//                var aroundCellCoordinates = new Coordinate[]
//                {
//                    new Coordinate(calcTargetCoordinate.x - 1, calcTargetCoordinate.y),//左
//                    new Coordinate(calcTargetCoordinate.x + 1, calcTargetCoordinate.y),//→
//                    new Coordinate(calcTargetCoordinate.x, calcTargetCoordinate.y - 1),//上
//                    new Coordinate(calcTargetCoordinate.x, calcTargetCoordinate.y + 1),//下
//                };
//                // 四方のマスの残移動力を計算
//                foreach (var aroundCellCoordinate in aroundCellCoordinates)   //周りの座標配列を繰り返す
//                {
//                    var targetCell = cells.FirstOrDefault(c => c.X == aroundCellCoordinate.x && c.Y == aroundCellCoordinate.y);
//                    //  targetに最初の周りのセルと等しい物を入れる
//                    if (null == targetCell || //周りのセルがないor周りのセルと等しいorappendInfosが周りのセルと等しい
//                        infos.Any(info => info.coordinate.x == aroundCellCoordinate.x && info.coordinate.y == aroundCellCoordinate.y) ||
//                        appendInfos.Any(info => info.coordinate.x == aroundCellCoordinate.x && info.coordinate.y == aroundCellCoordinate.y))
//                    {
//                        // マップに存在しない、または既に計算済みの座標はスルー
//                        continue;
//                    }
//                    var remainingMoveAmount = i - targetCell.Cost; //remaingmoveamount　に移動力-移動コストを入れる
//                    appendInfos.Add(new MoveAmountInfo(aroundCellCoordinate.x, aroundCellCoordinate.y, remainingMoveAmount));
//                    //appendinfosに周りのセルの座標と残移動力を入れる。
//                }
//            }
//            infos.AddRange(appendInfos);
//        }
//        // 残移動力が0以上（移動可能）なマスの情報だけを返す
//        return infos.Where(x => x.amount >= 0).ToArray();
//    }


//    /// <summary>
//    /// 残移動力情報クラス
//    /// </summary>
//    class MoveAmountInfo
//    {
//        public readonly Coordinate coordinate;
//        public readonly int amount;

//        public MoveAmountInfo(int x, int y, int amount)
//        {
//            this.coordinate = new Coordinate(x, y);
//            this.amount = amount;
//        }
//    }




//    /// <summary>
//    /// 座標と数値情報を紐付けるためのクラス
//    /// </summary>
//    public class CoordinateAndValue
//    {
//        public readonly Coordinate coordinate;
//        public readonly int value;

//        public CoordinateAndValue(int x, int y, int value)
//        {
//            this.coordinate = new Coordinate(x, y);
//            this.value = value;
//        }
//    }

//    /// <summary>
//    /// 座標クラス
//    /// </summary>
//    public class Coordinate
//    {
//        public readonly int x;
//        public readonly int y;

//        public Coordinate(int x, int y)
//        {
//            this.x = x;
//            this.y = y;
//        }
//    }

//    /// <summary>
//    /// 自軍のユニットを取得します
//    /// </summary>
//    /// <returns>The own units.</returns>
//    public Unit[] GetOwnUnits()
//    {
//        return unitContainer.GetComponentsInChildren<Unit>().Where(x => x.team == currentTeam).ToArray();
//    }

//    /// <summary>
//    /// 敵軍のユニットを取得します
//    /// </summary>
//    /// <returns>The enemy units.</returns>
//    public Unit[] GetEnemyUnits()
//    {
//        return unitContainer.GetComponentsInChildren<Unit>().Where(x => x.team != currentTeam).ToArray();
//    }

//    /// <summary>
//    /// 指定座標から各マスまで、移動コストいくつで行けるかを計算します
//    /// </summary>
//    /// <returns>The move amount to cells.</returns>
//    /// <param name="from">From.</param>
//    public List<CoordinateAndValue> GetMoveCostToAllCells(Main_tile from)
//    {
//        var infos = new List<CoordinateAndValue>();
//        infos.Add(new CoordinateAndValue(from.X, from.Y, 0));
//        var i = 0;
//        while (true)
//        {
//            var appendInfos = new List<CoordinateAndValue>();
//            foreach (var calcTargetInfo in infos.Where(info => info.value == i))
//            {
//                // 四方のマスの座標配列を作成
//                var calcTargetCoordinate = calcTargetInfo.coordinate;
//                var aroundCellCoordinates = new Coordinate[]
//                {
//                    new Coordinate(calcTargetCoordinate.x - 1, calcTargetCoordinate.y),
//                    new Coordinate(calcTargetCoordinate.x + 1, calcTargetCoordinate.y),
//                    new Coordinate(calcTargetCoordinate.x, calcTargetCoordinate.y - 1),
//                    new Coordinate(calcTargetCoordinate.x, calcTargetCoordinate.y + 1),
//                };
//                // 四方のマスの残移動力を計算
//                foreach (var aroundCellCoordinate in aroundCellCoordinates)
//                {
//                    var targetCell = cells.FirstOrDefault(c => c.X == aroundCellCoordinate.x && c.Y == aroundCellCoordinate.y);
//                    if (null == targetCell ||
//                        infos.Any(info => info.coordinate.x == aroundCellCoordinate.x && info.coordinate.y == aroundCellCoordinate.y) ||
//                        appendInfos.Any(info => info.coordinate.x == aroundCellCoordinate.x && info.coordinate.y == aroundCellCoordinate.y))
//                    {
//                        // マップに存在しない、または既に計算済みの座標はスルー
//                        continue;
//                    }
//                    var remainingMoveAmount = i + targetCell.Cost;
//                    appendInfos.Add(new CoordinateAndValue(aroundCellCoordinate.x, aroundCellCoordinate.y, remainingMoveAmount));
//                }
//            }
//            infos.AddRange(appendInfos);

//            i++;
//            if (i > infos.Max(x => x.value < 999 ? x.value : 0))
//            {
//                break;
//            }
//        }
//        return infos.Where(x => x.value < 999).ToList();
//    }




//    /// <summary>
//    /// 指定位置までの移動ルートと移動コストを返します
//    /// </summary>
//    /// <returns>The route coordinates and move amount.</returns>
//    /// <param name="from">From.</param>
//    /// <param name="to">To.</param>
//    public List<CoordinateAndValue> CalcurateRouteCoordinatesAndMoveAmount(Main_tile from, Main_tile to)
//    {
//        var costs = GetMoveCostToAllCells(from);
//        if (!costs.Any(info => info.coordinate.x == to.X && info.coordinate.y == to.Y))
//        {
//            throw new ArgumentException(string.Format("x:{0}, y:{1} is not movable.", to.X, to.Y));
//        }

//        var toCost = costs.First(info => info.coordinate.x == to.X && info.coordinate.y == to.Y);
//        var route = new List<CoordinateAndValue>();
//        route.Add(toCost);
//        while (true)
//        {
//            var currentCost = route.Last();
//            var currentCell = cells.First(cell => cell.X == currentCost.coordinate.x && cell.Y == currentCost.coordinate.y);
//            var prevMoveCost = currentCost.value - currentCell.Cost;
//            var previousCost = costs.FirstOrDefault(info => (Mathf.Abs(info.coordinate.x - currentCell.X) + Mathf.Abs(info.coordinate.y - currentCell.Y)) == 1 && info.value == prevMoveCost);
//            if (null == previousCost)
//            {
//                break;
//            }
//            route.Add(previousCost);
//        }
//        route.Reverse();
//        return route.ToList();
//    }

//    public Main_tile[] GetAttackableCells()
//    {
//        return cells.Where(x => x.IsAttackable).ToArray();
//    }


//    public Main_tile[] GetMovableCells()
//    {
//        return cells.Where(x => x.IsMovable).ToArray();
//    }

//}
