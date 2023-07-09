using MrLule.ExtensionMethods;
using MrLule.General;
using System.Drawing;
using UnityEngine;

namespace MrLule.Systems.GridSystem
{
    public class HexGrid : HexGrid<int> { }

    public class Hex<Type>
    {
        public bool is2D = false;
        public Vector3 position;
        public float size;
        public Type data;

        public Hex(bool is2D, Vector3 position, float size, Type data)
        {
            this.is2D = is2D;
            this.position = position;
            this.size = size;
            this.data = data;
        }

        public Hex(bool is2D, Vector3 position, float size)
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

        // doesn't work always gives true
        //public bool OnArea(Vector3 point)
        //{
        //    size *= 2;
        //    float halfCellSize = size / 2f;
        //    float height = Mathf.Sqrt(3f) / 2f * size;
        //    float x = Mathf.Floor(point.x / halfCellSize);
        //    float y = Mathf.Floor((point.y + (x % 2f) * height / 2f) / height);

        //    Vector2[] polygon = new Vector2[6];
        //    polygon[0] = new Vector2(x * halfCellSize, y * height + ((x % 2f == 0) ? 0f : height / 2f));
        //    polygon[1] = new Vector2((x + 1f) * halfCellSize, y * height + ((x % 2f == 0) ? 0f : height / 2f));
        //    polygon[2] = new Vector2((x + 1.5f) * halfCellSize, (y + 0.5f) * height + ((x % 2f == 0) ? 0f : height / 2f));
        //    polygon[3] = new Vector2((x + 1f) * halfCellSize, (y + 1f) * height + ((x % 2f == 0) ? 0f : height / 2f));
        //    polygon[4] = new Vector2(x * halfCellSize, (y + 1f) * height + ((x % 2f == 0) ? 0f : height / 2f));
        //    polygon[5] = new Vector2((x - 0.5f) * halfCellSize, (y + 0.5f) * height + ((x % 2f == 0) ? 0f : height / 2f));


        //    float minX = polygon[0].x;
        //    float maxX = polygon[0].x;
        //    float minY = polygon[0].y;
        //    float maxY = polygon[0].y;
        //    for (int i = 1; i < polygon.Length; i++)
        //    {
        //        Vector2 q = polygon[i];
        //        minX = Mathf.Min(q.x, minX);
        //        maxX = Mathf.Max(q.x, maxX);
        //        minY = Mathf.Min(q.y, minY);
        //        maxY = Mathf.Max(q.y, maxY);
        //    }

        //    if (point.x < minX || point.x > maxX || point.y < minY || point.y > maxY)
        //    {
        //        return false;
        //    }

        //    bool inside = false;
        //    for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
        //    {
        //        if ((polygon[i].y > point.y) != (polygon[j].y > point.y) &&
        //                point.x < (polygon[j].x - polygon[i].x) * (point.y - polygon[i].y) / (polygon[j].y - polygon[i].y) + polygon[i].x)
        //        {
        //            inside = !inside;
        //        }
        //    }

        //    return inside;
        //}

        public bool OnArea(Vector3 point)
        {
            point = point.SetZ(0);
            float radius = size * Mathf.Sqrt(3) / 2;
            float distance = Vector3.Distance(position, point);
            return distance <= radius;
        }

        public Vector3 GetMiddlePoint()
        {
            return this.position;
        }

        public void Draw(UnityEngine.Color color)
        {
            Gizmos.color = color;
            if (is2D)
            {
                Gizmos.DrawLine(position.SetXY(position.x - (size / 2), position.y - (size * Mathf.Sqrt(3) / 2)),
                    position.SetXY(position.x + (size / 2), position.y - (size * Mathf.Sqrt(3) / 2)));

                Gizmos.DrawLine(position.SetXY(position.x + (size / 2), position.y - (size * Mathf.Sqrt(3) / 2)),
                    position.SetX(position.x + size));

                Gizmos.DrawLine(position.SetX(position.x + size),
                    position.SetXY(position.x + (size / 2), position.y + (size * Mathf.Sqrt(3) / 2)));

                Gizmos.DrawLine(position.SetXY(position.x + (size / 2), position.y + (size * Mathf.Sqrt(3) / 2)),
                    position.SetXY(position.x - (size / 2), position.y + (size * Mathf.Sqrt(3) / 2)));

                Gizmos.DrawLine(position.SetXY(position.x - (size / 2), position.y + (size * Mathf.Sqrt(3) / 2)),
                    position.SetX(position.x - size));

                Gizmos.DrawLine(position.SetX(position.x - size),
                    position.SetXY(position.x - (size / 2), position.y - (size * Mathf.Sqrt(3) / 2)));
            }
            else
            {
                Gizmos.DrawLine(position.SetXZ(position.x - (size / 2), position.z - (size * Mathf.Sqrt(3) / 2)),
                    position.SetXZ(position.x + (size / 2), position.z - (size * Mathf.Sqrt(3) / 2)));

                Gizmos.DrawLine(position.SetXZ(position.x + (size / 2), position.z - (size * Mathf.Sqrt(3) / 2)),
                    position.SetX(position.x + size));

                Gizmos.DrawLine(position.SetX(position.x + size),
                    position.SetXZ(position.x + (size / 2), position.z + (size * Mathf.Sqrt(3) / 2)));

                Gizmos.DrawLine(position.SetXZ(position.x + (size / 2), position.z + (size * Mathf.Sqrt(3) / 2)),
                    position.SetXZ(position.x - (size / 2), position.z + (size * Mathf.Sqrt(3) / 2)));

                Gizmos.DrawLine(position.SetXZ(position.x - (size / 2), position.z + (size * Mathf.Sqrt(3) / 2)),
                    position.SetX(position.x - size));

                Gizmos.DrawLine(position.SetX(position.x - size),
                    position.SetXZ(position.x - (size / 2), position.z - (size * Mathf.Sqrt(3) / 2)));
            }
        }
    }

    public class HexGrid<DataType> : MonoBehaviour
    {
        [SerializeField] private bool is2D = false;
        [SerializeField] private int verticalCount = 10;
        [SerializeField] private int horizontalCount = 10;
        [SerializeField] private float cellSize = 10f;
        [SerializeField] private float verticalCellSpacing = 1f;
        [SerializeField] private float horizontalCellSpacing = 1f;
        [SerializeField] private UnityEngine.Color drawColor = UnityEngine.Color.white;

        public Hex<DataType>[,] grids;

        public HexGrid() { }

        public HexGrid(bool is2D, int verticalCount, int horizontalCount, float cellSize, float verticalCellSpacing, float horizontalCellSpacing, UnityEngine.Color drawColor)
        {
            this.is2D = is2D;
            this.verticalCount = verticalCount;
            this.horizontalCount = horizontalCount;
            this.cellSize = cellSize;
            this.verticalCellSpacing = verticalCellSpacing;
            this.horizontalCellSpacing = horizontalCellSpacing;
            this.drawColor = drawColor;
            CreateGrids();
        }

        void Start()
        {
            CreateGrids();
        }

        private void CreateGrids()
        {
            grids = new Hex<DataType>[horizontalCount, verticalCount];
            for (int x = 0; x < horizontalCount; x++)
            {
                for (int y = 0; y < verticalCount; y++)
                {
                    Hex<DataType> hex;
                    if (y % 2 == 0)
                    {
                        if (is2D)
                        {
                            hex = new Hex<DataType>(is2D, transform.position + new Vector3(3 * x * (cellSize + verticalCellSpacing), y * (cellSize * Mathf.Sqrt(3) / 2 + horizontalCellSpacing), 0), cellSize);
                        }
                        else
                        {
                            hex = new Hex<DataType>(is2D, transform.position + new Vector3(3 * x * (cellSize + verticalCellSpacing), 0, y * (cellSize * Mathf.Sqrt(3) / 2 + horizontalCellSpacing)), cellSize);
                        }
                    }
                    else
                    {
                        if (is2D)
                        {
                            hex = new Hex<DataType>(is2D, transform.position + new Vector3(3 * x * (cellSize + verticalCellSpacing) + (1.5f * (cellSize + verticalCellSpacing)), y * (cellSize * Mathf.Sqrt(3) / 2 + horizontalCellSpacing), 0), cellSize);
                        }
                        else
                        {
                            hex = new Hex<DataType>(is2D, transform.position + new Vector3(3 * x * (cellSize + verticalCellSpacing) + (1.5f * (cellSize + verticalCellSpacing)), 0, y * (cellSize * Mathf.Sqrt(3) / 2 + horizontalCellSpacing)), cellSize);
                        }
                    }
                    grids[x, y] = hex;
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
                    if (y % 2 == 0)
                    {
                        if (is2D)
                        {
                            Hex<DataType> hex = new Hex<DataType>(is2D, transform.position + new Vector3(3 * x * (cellSize + verticalCellSpacing), y * (cellSize * Mathf.Sqrt(3) / 2 + horizontalCellSpacing), 0), cellSize);
                            hex.Draw(drawColor);
                            //Debugger.DrawString($"{x} {y}", hex.GetMiddlePoint(), drawColor);
                        }
                        else
                        {
                            Hex<DataType> hex = new Hex<DataType>(is2D, transform.position + new Vector3(3 * x * (cellSize + verticalCellSpacing), 0, y * (cellSize * Mathf.Sqrt(3) / 2 + horizontalCellSpacing)), cellSize);
                            hex.Draw(drawColor);
                            //Debugger.DrawString($"{x} {y}", hex.GetMiddlePoint(), drawColor);
                        }
                    }
                    else
                    {
                        if (is2D)
                        {
                            Hex<DataType> hex = new Hex<DataType>(is2D, transform.position + new Vector3(3 * x * (cellSize + verticalCellSpacing) + (1.5f * (cellSize + verticalCellSpacing)), y * (cellSize * Mathf.Sqrt(3) / 2 + horizontalCellSpacing), 0), cellSize);
                            hex.Draw(drawColor);
                            //Debugger.DrawString($"{x} {y}", hex.GetMiddlePoint(), drawColor);
                        }
                        else
                        {
                            Hex<DataType> hex = new Hex<DataType>(is2D, transform.position + new Vector3(3 * x * (cellSize + verticalCellSpacing) + (1.5f * (cellSize + verticalCellSpacing)), 0, y * (cellSize * Mathf.Sqrt(3) / 2 + horizontalCellSpacing)), cellSize);
                            hex.Draw(drawColor);
                            //Debugger.DrawString($"{x} {y}", hex.GetMiddlePoint(), drawColor);
                        }
                    }
                }
            }
        }
    }
}
