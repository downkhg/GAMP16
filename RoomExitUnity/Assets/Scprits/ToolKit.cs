using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolKit : RoomObject
{
    public override bool CheckItem(Dynamic dynamic)
    {
        //키조각을 모두 삭제하고 가지고 있는 아이템을 넘겨준다.
        return base.CheckItem(dynamic);
    }
}
