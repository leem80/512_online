using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using PROnline.Models;
using PROnline.Models.Users;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace PROnline.Controllers.Users
{
    public class ClassImport:IUserImport
    {
        private const ushort START_ROW = 1;
        private const ushort NAME_COL = 1;
        private const ushort SEX_COL = 2;
        private const ushort BIRTH_COL = 3;
        private const ushort USERNAME_COL = 0;
        //其他

        //民族
        private const ushort MZ = 4;
        //职务
        private const ushort ZW = 5;
        //家庭人数
        private const ushort JTRS = 6;
        //个人特长
        private const ushort GRTC = 7;


        //家庭通讯方式
        private const ushort TXFS = 8;

        //是否留守
        private const ushort SFLS = 9;
        //家里是否具备上网条件
        private const ushort SFJBSWTJ = 10;
        //每周固定上网时间
        private const ushort MZGDSWSJ = 11;
        //个人受伤情况
        private const ushort GRSSQK = 12;
        //亲朋亡故情况
        private const ushort QPWG = 13;
        //家庭财产损失情况
        private const ushort JTSS = 14;
        //数学
        private const ushort SX = 15;
        //语文
        private const ushort YW = 16;
        //英语
        private const ushort YY = 17;
        //物理
        private const ushort WL = 18;
        //化学
        private const ushort HX = 19;
        //生物
        private const ushort SW = 20;
        //地理
        private const ushort DL = 21;
        //历史
        private const ushort LS = 22;

        public List<ImportResult> import(HSSFWorkbook input)
        {
            List<ImportResult> result=new List<ImportResult>();
            HSSFWorkbook doc = input;
            if (doc != null)
            {
                PROnlineContext db = new PROnlineContext();
                Sheet sheet = doc.GetSheetAt(0);
                int rowCount = sheet.LastRowNum;
                //校验提交的表中是否有相同的用户名
                List<String> uNames = new List<String>();

                for (ushort i = START_ROW; i <= rowCount; i++)
                {
                    HSSFRow row = (HSSFRow)sheet.GetRow(i);

                    //用户名判断
                    String UserName;
                    try
                    {
                        UserName = row.Cells[USERNAME_COL].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (UserName == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (uNames.Count == 0)
                    {
                        uNames.Add(UserName);
                    }
                    else
                    {
                        bool flag = true;
                        foreach (String s in uNames)
                        {
                            if (UserName.CompareTo(s) == 0)
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行用户名与之前用户名有重复" });
                            continue;
                        }
                        else
                        {
                            uNames.Add(UserName);
                        }
                    }
                    if (db.Users.Where(r => r.UserName == UserName.Trim()).Count() > 0)
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行用户名已存在" });
                        continue;
                    }

                    //姓名判断
                    String User;
                    try
                    {
                        User = row.Cells[NAME_COL].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (User == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }

                    //性别判断
                    String Sex;
                    try
                    {
                        Sex = row.Cells[SEX_COL].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (Sex == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (Sex != "男" && Sex != "女")
                    {

                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行，性别只能为‘男’或‘女’" });
                        continue;

                    }

                    //生日判断
                    DateTime Birthday;
                    try
                    {
                        Birthday = row.Cells[BIRTH_COL].DateCellValue;
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }

                    //民族
                    String MZx;
                    try
                    {
                        MZx = row.Cells[MZ].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (MZx == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }

                    //职务
                    String ZWx;
                    try
                    {
                        ZWx = row.Cells[ZW].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (ZWx == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }

                    //家庭人数
                    int JTRSx;
                    try
                    {
                        JTRSx = (Int32)row.Cells[JTRS].NumericCellValue;
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (JTRSx < 0)
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }

                    //个人特长
                    String GRTCx;
                    try
                    {
                        GRTCx = row.Cells[GRTC].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (GRTCx == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }

                    //家庭通讯方式
                    String TXFSx;
                    try
                    {
                        TXFSx = row.Cells[TXFS].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (TXFSx == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }

                    //是否留守
                    bool SFLSx;
                    String SFx;
                    try
                    {
                        SFx = row.Cells[SFLS].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (SFx == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    SFLSx = SFx == "是";

                    //家里是否具备上网条件
                    bool SFJBSWTJx;
                    String SFJBx;
                    try
                    {
                        SFJBx = row.Cells[SFJBSWTJ].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (SFJBx == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    SFJBSWTJx = SFJBx == "是";

                    //每周固定上网时间
                    String MZGDSWSJx;
                    try
                    {
                        MZGDSWSJx = row.Cells[MZGDSWSJ].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (MZGDSWSJx == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    String[] SJArr = MZGDSWSJx.Split(':');
                    if (SJArr.Length < 2)
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行“每周固定上网时间”为:“星期x:xx的形式”" });
                        continue;
                    }
                    //星期合法性判断
                    if (!(SJArr[0] == "星期一" || SJArr[0] == "星期二" || SJArr[0] == "星期三" || SJArr[0] == "星期四" || SJArr[0] == "星期五" || SJArr[0] == "星期六" || SJArr[0] == "星期天"))
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行只能为星期一到星期天" });
                        continue;
                    }
                    //时间合法性判断
                    if (!(SJArr[1] == "09" || SJArr[1] == "14" || SJArr[1] == "16" || SJArr[1] == "19" || SJArr[1] == "21"))
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行“每周固定上网时间”只能为[09,14,16,19,21]" });
                        continue;
                    }
                    //针对特定日期的时间判断
                    if (SJArr[0] == "星期一" || SJArr[0] == "星期二" || SJArr[0] == "星期三" || SJArr[0] == "星期四" || SJArr[0] == "星期五")
                    {
                        if (!(SJArr[1] == "19" || SJArr[1] == "21"))
                        {
                            result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行“每周一至周五固定上网时间”只能为[19,21]" });
                            continue;
                        }
                    }
                    else if (SJArr[0] == "星期六" || SJArr[0] == "星期天")
                    {
                        if (!(SJArr[1] == "09" || SJArr[1] == "14" || SJArr[1] == "16" || SJArr[1] == "19" || SJArr[1] == "21"))
                        {
                            result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行“每周六或周日固定上网时间”只能为[09,14,16,19,21]" });
                            continue;
                        }
                    }

                    //个人受伤情况
                    String GRSSQKx;
                    try
                    {
                        GRSSQKx = row.Cells[GRSSQK].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (GRSSQKx == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }

                    //亲朋亡故情况
                    String QPWGx;
                    try
                    {
                        QPWGx = row.Cells[QPWG].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (QPWGx == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }

                    //家庭财产损失情况
                    String JTSSx;
                    try
                    {
                        JTSSx = row.Cells[JTSS].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (JTSSx == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }

                    //数学
                    String SXx;
                    try
                    {
                        SXx = row.Cells[SX].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (SXx == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }

                    //语文
                    String YWx;
                    try
                    {
                        YWx = row.Cells[YW].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (YWx == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }

                    //英语
                    String YYx;
                    try
                    {
                        YYx = row.Cells[YY].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (YYx == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }

                    //物理
                    String WLx;
                    try
                    {
                        WLx = row.Cells[WL].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (WLx == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }

                    //化学
                    String HXx;
                    try
                    {
                        HXx = row.Cells[HX].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (HXx == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }

                    //生物
                    String SWx;
                    try
                    {
                        SWx = row.Cells[SW].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (SWx == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }

                    //地理
                    String DLx = row.Cells[DL].StringCellValue;
                    try
                    {
                        DLx = row.Cells[DL].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (DLx == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }

                    //历史
                    String LSx;
                    try
                    {
                        LSx = row.Cells[LS].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }
                    if (LSx == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行数据有误" });
                        continue;
                    }

                    ClassImportModel cm = new ClassImportModel
                    {
                        Name = User,
                        Sex = Sex,
                        BirthDay = Birthday,
                        UserName = UserName,
                        People = MZx,
                        Career = ZWx,
                        HomeNum = JTRSx,
                        Hobby = GRTCx,
                        Telephone = TXFSx,
                        IsAlone = SFLSx,
                        CanSurf = SFJBSWTJx,
                        DayOfWeek = SJArr[0],
                        SurfTime = SJArr[1],
                        Hurt = GRSSQKx,
                        Die = QPWGx,
                        Lose = JTSSx,
                        Math = SXx,
                        Chinese = YWx,
                        English = YYx,
                        Physics = WLx,
                        Chemistry = HXx,
                        Sw = SWx,
                        Dl = DLx,
                        Ls = LSx
                    };

                    result.Add(new ImportResult { Status = ImportResult.SUCCESS, Description = "第" + i + "行验证成功", Data = cm });

                }

            }
            else
            {
                result.Add(new ImportResult { Status = ImportResult.PROBLEM, Description = "文件不合法！" });
            }

            return result;
        }
    }
}