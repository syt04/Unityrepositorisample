﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/*
using namespace abc
{
    public class Main_Map : MonoBehaviour
    {
        [SerializeField]
        Main_Cell cellFieldPrefab;              //Cell型
        [SerializeField]
        Main_Cell cellForestPrefab;             //Cell型
        [SerializeField]
        Main_Cell cellRockPrefab;               //Cell型
        [SerializeField]
        Transform unitContainer;                //Transform型

        List<Main_Cell> cells = new List<Main_Cell>();       //List Cell型

        /// <summary>
        /// 選択中のユニットを返します
        /// </summary>
        /// <value>The active unit.</value>
        public Main_Unit ActiveUnit
        {
            get { return unitContainer.GetComponentsInChildren<Main_Unit>().First(); }  //unitContainer
        }

        void Start()
        {
            foreach (var prefab in new Main_Cell[] { cellFieldPrefab, cellForestPrefab, cellRockPrefab })
            {
                prefab.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// マップを生成します
        /// </summary>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        public void Generate(int width, int height)
        {
            foreach (var cell in cells)
            {
                Destroy(cell.gameObject);
            }

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    Main_Cell cell;
                    var rand = UnityEngine.Random.Range(0, 10);
                    if (rand == 0)
                    {
                        cell = Instantiate(cellRockPrefab);
                    }
                    else if (rand <= 2)
                    {
                        cell = Instantiate(cellForestPrefab);
                    }
                    else
                    {
                        cell = Instantiate(cellFieldPrefab);
                    }
                    cell.gameObject.SetActive(true);
                    cell.transform.SetParent(transform);
                    cell.SetCoordinate(x, y);
                    cells.Add(cell);
                }
            }
        }

        /// <summary>
        /// 移動可能なマスをハイライトします
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="moveAmount">Move amount.</param>
        public void HighlightMovableCells(int x, int y, int moveAmount)
        {
            ResetMovableCells();
            var startCell = cells.First(c => c.X == x && c.Y == y);
            foreach (var info in GetRemainingMoveAmountInfos(startCell, moveAmount))
            {
                cells.First(c => c.X == info.coordinate.x && c.Y == info.coordinate.y).IsMovable = true;
            }
        }

        /// <summary>
        /// 移動可能なマスのハイライトを消します
        /// </summary>
        public void ResetMovableCells()
        {
            foreach (var cell in cells.Where(c => c.IsMovable))
            {
                cell.IsMovable = false;
            }
        }

        /// <summary>
        /// 移動経路となるマスを返します
        /// </summary>
        /// <returns>The route cells.</returns>
        /// <param name="startCell">Start cell.</param>
        /// <param name="moveAmount">Move amount.</param>
        /// <param name="endCell">End cell.</param>
        public Main_Cell[] CalculateRouteCells(int x, int y, int moveAmount, Main_Cell endCell)
        {
            var startCell = cells.First(c => c.X == x && c.Y == y);
            var infos = GetRemainingMoveAmountInfos(startCell, moveAmount);
            if (!infos.Any(info => info.coordinate.x == endCell.X && info.coordinate.y == endCell.Y))
            {
                throw new ArgumentException(string.Format("endCell(x:{0}, y:{1}) is not movable.", endCell.X, endCell.Y));
            }

            var endCellMoveAmountInfo = infos.First(info => info.coordinate.x == endCell.X && info.coordinate.y == endCell.Y);
            var routeCells = new List<Main_Cell>();
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

        /// <summary>
        /// 指定座標にユニットを配置します
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="unitPrefab">Unit prefab.</param>
        public void PutUnit(int x, int y, Main_Unit unitPrefab)
        {
            var unit = Instantiate(unitPrefab);
            unit.gameObject.SetActive(true);
            unit.transform.SetParent(unitContainer);
            unit.transform.position = cells.First(c => c.X == x && c.Y == y).transform.position;
            unit.x = x;
            unit.y = y;
        }

        /// <summary>
        /// 移動力を元に移動可能範囲の計算を行います
        /// </summary>
        /// <returns>The remaining move amount infos.</returns>
        /// <param name="startCell">Start cell.</param>
        /// <param name="moveAmount">Move amount.</param>
        MoveAmountInfo[] GetRemainingMoveAmountInfos(Main_Cell startCell, int moveAmount)
        {
            var infos = new List<MoveAmountInfo>();      //moveamountinfo型のリスト　infos
            infos.Add(new MoveAmountInfo(startCell.X, startCell.Y, moveAmount));    // startcellの位置を加える
            for (var i = moveAmount; i >= 0; i--)         //移動力まで行う   iは移動する数
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
        class MoveAmountInfo
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
        /// 座標クラス
        /// </summary>
        class Coordinate
        {
            public readonly int x;
            public readonly int y;

            public Coordinate(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
    }
}*/