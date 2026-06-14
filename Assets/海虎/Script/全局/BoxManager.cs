using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    // 字典，键为mapCorrespondenceID，值为具有该mapCorrespondenceID的GameObject列表
    private static Dictionary<int, List<GameObject>> boxGroups = new Dictionary<int, List<GameObject>>();

    // 注册方块到对应的mapCorrespondenceID组里
    public static void RegisterBox(int mapCorrespondenceID, GameObject box)
    {
        if (!boxGroups.ContainsKey(mapCorrespondenceID))
        {
            boxGroups[mapCorrespondenceID] = new List<GameObject>();
        }
        boxGroups[mapCorrespondenceID].Add(box);
    }

    public static List<GameObject> GetBoxesWithID(int mapCorrespondenceID)
    {
        if (boxGroups.ContainsKey(mapCorrespondenceID))
        {
            // 清理销毁的对象
            boxGroups[mapCorrespondenceID].RemoveAll(box => box == null);

            return boxGroups[mapCorrespondenceID];
        }
        return new List<GameObject>();
    }

}