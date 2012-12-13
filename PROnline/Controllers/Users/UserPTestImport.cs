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
    public class UserPTestImport : IUserImport
    {
        private const ushort START_ROW = 1;
        private const ushort NAME_COL = 0;

        //其他

        //想法总与别人不一样
        private const ushort Ta = 2;
        //情绪不稳定（常发脾气或容易激动或容易哭泣）
        private const ushort Tb = 3;
        //讨厌上学、讨厌写作业
        private const ushort Tc = 4;
        //伤害他人或打人
        private const ushort Td = 5;
        //从不与异性在一起
        private const ushort Te = 6;
        //平时成绩和考试成绩差距较大
        private const ushort Tf = 7;
        //时常与人争论、抬杠
        private const ushort Tg = 8;
        //不喜欢与他人交往
        private const ushort Th = 9;
        //学习成绩忽上忽下，很不稳定
        private const ushort Ti = 10;
        //和老师发生冲突
        private const ushort Tj = 11;


        public List<ImportResult> import(HSSFWorkbook input)
        {
            List<ImportResult> result = new List<ImportResult>();
            HSSFWorkbook doc = input;
            if (doc != null)
            {
                PROnlineContext db = new PROnlineContext();
                Sheet sheet = doc.GetSheetAt(0);
                int rowCount = sheet.LastRowNum;
                for (ushort i = START_ROW; i <= rowCount; i++)
                {
                    HSSFRow row = (HSSFRow)sheet.GetRow(i);


                    //用户名判断
                    String UserName = row.Cells[NAME_COL].StringCellValue;
                    try
                    {
                        UserName = row.Cells[NAME_COL].StringCellValue.Trim();
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行输入有误" });
                        break;
                    }
                    if (UserName == "")
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行用户名为空" });
                        break;
                    }

                    var usrList = db.Student.Where(r => r.User.UserName == UserName.Trim()).ToList();
                    if (usrList.Count() == 0)
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行不存在拥有该用户名的学生" });
                        continue;
                    }
                    else
                    {
                        UserName = usrList[0].StudentID.ToString();
                    }



                    int Tax = 0;
                    try
                    {
                        Tax = Convert.ToInt32(row.Cells[Ta].NumericCellValue);
                        if (Tax <= 0 || Tax % 2 != 0)
                        {
                            result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行有错,每列只能为数字[2,4,6,8,10]" });
                            continue;
                        }
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行有错,必须填写数字" });
                        continue;
                    }


                    int Tbx = 0;
                    try
                    {
                        Tbx = Convert.ToInt32(row.Cells[Tb].NumericCellValue);
                        if (Tbx <= 0 || Tbx % 2 != 0)
                        {
                            result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行有错,每列只能为数字[2,4,6,8,10]" });
                            continue;
                        }
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行有错,必须填写数字" });
                        continue;
                    }


                    int Tcx = 0;
                    try
                    {
                        Tcx = Convert.ToInt32(row.Cells[Tc].NumericCellValue);
                        if (Tcx <= 0 || Tcx % 2 != 0)
                        {
                            result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行有错,每列只能为数字[2,4,6,8,10]" });
                            continue;
                        }
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行有错,必须填写数字" });
                        continue;
                    }


                    int Tdx = 0;
                    try
                    {
                        Tdx = Convert.ToInt32(row.Cells[Td].NumericCellValue);
                        if (Tdx <= 0 || Tdx % 2 != 0)
                        {
                            result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行有错,每列只能为数字[2,4,6,8,10]" });
                            continue;
                        }
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行有错,必须填写数字" });
                        continue;
                    }

                    int Tex = 0;
                    try
                    {
                        Tex = Convert.ToInt32(row.Cells[Te].NumericCellValue);
                        if (Tex <= 0 || Tex % 2 != 0)
                        {
                            result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行有错,每列只能为数字[2,4,6,8,10]" });
                            continue;
                        }
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行有错,必须填写数字" });
                        continue;
                    }

                    int Tfx = 0;
                    try
                    {
                        Tfx = Convert.ToInt32(row.Cells[Tf].NumericCellValue);
                        if (Tfx <= 0 || Tfx % 2 != 0)
                        {
                            result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行有错,每列只能为数字[2,4,6,8,10]" });
                            continue;
                        }
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行有错,必须填写数字" });
                        continue;
                    }

                    int Tgx = 0;
                    try
                    {
                        Tgx = Convert.ToInt32(row.Cells[Tg].NumericCellValue);
                        if (Tgx <= 0 || Tgx % 2 != 0)
                        {
                            result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行有错,每列只能为数字[2,4,6,8,10]" });
                            continue;
                        }
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行有错,必须填写数字" });
                        continue;
                    }

                    int Thx = 0;
                    try
                    {
                        Thx = Convert.ToInt32(row.Cells[Th].NumericCellValue);
                        if (Thx <= 0 || Thx % 2 != 0)
                        {
                            result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行有错,每列只能为数字[2,4,6,8,10]" });
                            continue;
                        }
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行有错,必须填写数字" });
                        continue;
                    }
                    int Tix = 0;
                    try
                    {
                        Tix = Convert.ToInt32(row.Cells[Ti].NumericCellValue);
                        if (Tix <= 0 || Tix % 2 != 0)
                        {
                            result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行有错,每列只能为数字[2,4,6,8,10]" });
                            continue;
                        }
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行有错,必须填写数字" });
                        continue;
                    }

                    int Tjx = 0;
                    try
                    {
                        Tjx = Convert.ToInt32(row.Cells[Tj].NumericCellValue);
                        if (Tjx <= 0 || Tjx % 2 != 0)
                        {
                            result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行有错,每列只能为数字[2,4,6,8,10]" });
                            continue;
                        }
                    }
                    catch
                    {
                        result.Add(new ImportResult { Status = ImportResult.FAILURE, Description = "第" + i + "行有错,必须填写数字" });
                        continue;
                    }

                    int tol = Tax + Tbx + Tcx + Tdx + Tex + Tfx + Tgx + Thx + Tix + Tjx;
                    String res;
                    if (tol < 40)
                    {
                        res = "心理素质很好";
                    }
                    else if (tol < 60)
                    {
                        res = "心理素质较好";
                    }
                    else if (tol < 80)
                    {
                        res = "心理素质一般";
                    }
                    else
                    {
                        res = "心理素质很差";
                    }

                    UserPTest cm = new UserPTest
                    {
                        StudentID = new Guid(UserName),
                        Ta = Tax,
                        Tb = Tbx,
                        Tc = Tcx,
                        Td = Tdx,
                        Te = Tex,
                        Tf = Tfx,
                        Tg = Tgx,
                        Th = Thx,
                        Ti = Tix,
                        Tj = Tjx,
                        total = tol,
                        result = res
                    };

                    result.Add(new ImportResult { Status = ImportResult.SUCCESS, Description = "第" + i + "行验证成功",Data=cm });

                }

            }
            else{
                result.Add(new ImportResult{Status=ImportResult.PROBLEM,Description="文件不合法！"});
            }

            return result;
        }
    }
}