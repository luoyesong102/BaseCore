using System;
using System.Collections.Generic;
using System.Text;
using SAAS.FrameWork.Module.Serialization;
using SAAS.FrameWork.Util.Pool;

namespace SAAS.FrameWork.Extensions
{
    public static partial class ArraySegmentByteExtensions
    {

        public static void ReturnToPool(this ArraySegment<byte> data)
        {
            DataPool.BytesReturn(data.Array);
        }




    }
}
