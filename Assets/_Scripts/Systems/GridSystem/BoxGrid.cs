using MrLule.ExtensionMethods;
using MrLule.General;
using UnityEngine;

namespace MrLule.Systems.GridSystem
{
    public class BoxGrid : BoxGrid<int> { }

    public class Box<Type>
    {
        public bool is2D = false;
        public Vector3 position;
        public Vector2 size;
        public Type data;

        public Box(bool is2D, Vector3 position, Vector2 size, Type data)
        {
            this.is2D = is2D;
            this.position = position;
            this.size = size;
            this.data = data;
        }

        public Box(bool is2D, Vector3 position, Vector2 size)
        {
            this.is2D = is2D;
            this.position = position;
            this.size = size;
        }

        public void SetData(Type data)
        {
            this.data = data;
        }

        public Type GetData()
        {
            return this.data;
        }

        public bool OnArea(Vector3 point)
        {
            if (point.x >= position.x && point.x <= position.x + size.x)
            {
                if (is2D && point.y >= position.y && point.y <= position.y + size.y)
                {
                    return true;
                }
                else if (!is2D && point.z >= position.z && point.z <= position.z + size.y)
                {
                    return true;
                }
            }
            return false;
        }

        public Vector3 GetPosition()
        {
            return this.position;
        }

        public Vector3 GetMiddlePoint()
        {
            Vector3 middlePoint = new Vector3(position.x + (size.x / 2), position.y + (size.y / 2), position.z + (size.y / 2));
            middlePoint = is2D ? middlePoint.SetZ(0) : middlePoint.SetY(0);
            return middlePoint;
        }

        public Vector3 GetPointByAnchor(Vector2 anchor)
        {
            Vector3 anchored = this.position;
            anchored = is2D ? anchored.SetXY(anchored.x + (anchor.x * size.x), anchored.y + (anchor.y * size.y)) :
                anchored.SetXZ(anchored.x + (anchor.x * size.x), anchored.z + (anchor.y * size.y));
            return anchored;
        }

        public void Draw(Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(position, position.SetX(position.x + size.x));
            if (is2D)
            {
                Gizmos.DrawLine(position, position.SetY(position.y + size.y));
                Gizmos.DrawLine(position.SetX(position.x + size.x), position.SetXY(position.x + size.x, position.y + size.y));
                Gizmos.DrawLine(position.SetY(position.y + size.y), position.SetXY(position.x + size.x, position.y + size.y));
            }
            else
            {
                Gizmos.DrawLine(position, position.SetZ(position.z + size.y));
                Gizmos.DrawLine(position.SetX(position.x + size.x), position.SetXZ(position.x + size.x, position.z + size.y));
                Gizmos.DrawLine(position.SetZ(position.z + size.y), position.SetXZ(position.x + size.x, position.z + size.y));
            }
        }
    }

    public class BoxGrid<DataType> : MonoBehaviour
    {
        public bool is2D = false;
        public int horizontalCount = 10;
        public int verticalCount = 10;
        public Vector2 cellSize = new Vector2(10f, 10f);
        public float verticalCellSpacing = 1f;
        public float horizontalCellSpacing = 1f;
        public Color drawColor = Color.white;
        public Box<DataType>[,] grids;

        public BoxGrid() { }

        public BoxGrid(bool is2D, int horizontalCount, int verticalCount, Vector2 cellSize, float horizontalCellSpacing, float verticalCellSpacing, Color drawColor)
        {
            this.is2D = is2D;
            this.horizontalCount = horizontalCount;
            this.verticalCount = verticalCount;
            this.cellSize = cellSize;
            this.horizontalCellSpacing = horizontalCellSpacing;
            this.verticalCellSpacing = verticalCellSpacing;
            this.drawColor = drawColor;
            CreateGrids();
        }

        public virtual void Start()
        {
            CreateGrids();
        }

        protected void CreateGrids()
        {
            grids = new Box<DataType>[horizontalCount, verticalCount];
            for (int x = 0; x < horizontalCount; x++)
            {
                for (int y = 0; y < verticalCount; y++)
                {
                    Box<DataType> box;
                    if (is2D)
                    {
                        box = new Box<DataType>(is2D, transform.position + new Vector3(x * (cellSize.x + verticalCellSpacing), y * (cellSize.y + horizontalCellSpacing), 0), cellSize);
                    }
                    else
                    {
                        box = new Box<DataType>(is2D, transform.position + new Vector3(x * (cellSize.x + verticalCellSpacing), 0, y * (cellSize.y + horizontalCellSpacing)), cellSize);
                    }
                    grids[x, y] = box;
                }
            }
        }

        public bool TryGetGridIndex(Vector3 worldPosition, out int x, out int y)
        {
            for (int i = 0; i < horizontalCount; i++)
            {
                for (int j = 0; j < verticalCount; j++)
                {
                    bool onArea = grids[i, j].OnArea(worldPosition);
                    if (onArea)
                    {
                        x = i;
                        y = j;
                        return true;
                    }
                }
            }
            x = -1;
            y = -1;
            return false;
        }

        public void SetValue(Vector3 worldPosition, DataType value)
        {
            if (TryGetGridIndex(worldPosition, out int x, out int y))
            {
                grids[x, y].SetData(value);
            }
        }

        public void SetValue(int x, int y, DataType value)
        {
            if (x >= horizontalCount || y >= verticalCount)
            {
                return;
            }
            grids[x, y].SetData(value);
        }

        public void SetAllValues(DataType data)
        {
            for (int x = 0; x < horizontalCount; x++)
            {
                for (int y = 0; y < verticalCount; y++)
                {
                    grids[x, y].SetData(data);
                }
            }
        }

        public bool TryGetValue(Vector3 worldPosition, out DataType value)
        {
            if (TryGetGridIndex(worldPosition, out int x, out int y))
            {
                value = grids[x, y].GetData();
                return true;
            }
            else
            {
                value = default(DataType);
                return false;
            }
        }

        public DataType GetValue(int x, int y)
        {
            if (x >= horizontalCount || y >= verticalCount)
            {
                return default;
            }
            return grids[x, y].GetData();
        }

        private void OnDrawGizmos()
        {
            for (int x = 0; x < horizontalCount; x++)
            {
                for (int y = 0; y < verticalCount; y++)
                {
                    if (is2D)
                    {
                        Box<DataType> box = new Box<DataType>(is2D, transform.position + new Vector3(x * (cellSize.x + verticalCellSpacing), y * (cellSize.y + horizontalCellSpacing), 0), cellSize);
                        box.Draw(drawColor);
                        Debugger.DrawString($"{x} {y}", box.GetMiddlePoint(), drawColor);
                    }
                    else
                    {
                        Box<DataType> box = new Box<DataType>(is2D, transform.position + new Vector3(x * (cellSize.x + verticalCellSpacing), 0, y * (cellSize.y + horizontalCellSpacing)), cellSize);
                        box.Draw(drawColor);
                        Debugger.DrawString($"{x} {y}", box.GetMiddlePoint(), drawColor);
                    }
                }
            }
        }
    }
}
