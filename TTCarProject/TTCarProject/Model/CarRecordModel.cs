using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCarProject
{
    class CarRecordModel
    {
        public string cid;
        public string strNum;
        public DateTime nCapTime;
        public string isWhite;  //用来标记进门还是出门   1：进门  2：出门
        public string goStyle;  //抬杆方式 1：自动  2：手动
        public string manager;
        public string isOffLine;
        public string isUpLoaded;
        public string entrance_guard_position_id;

    }
}
