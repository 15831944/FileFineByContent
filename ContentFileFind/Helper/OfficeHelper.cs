using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContentFileFind.Helper
{
    public class OfficeHelper
    {
        /// <summary>
        /// word内容查找
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <param name="searchValue">查找内容</param>
        /// <returns></returns>
        public static bool CheckWordContent(string fileFullPath,string searchValue)
        {
            object filename = fileFullPath; //要打开的文档路径 
            string strKey = searchValue; //要搜索的文本 
            object MissingValue = Type.Missing;
            bool checkResult = false;
            int i = 0, iCount = 0;

            if (fileFullPath.Contains("~$"))
                return checkResult;

            Microsoft.Office.Interop.Word.Find wfnd;

            Microsoft.Office.Interop.Word.Application wp = new Microsoft.Office.Interop.Word.ApplicationClass();
            Type wordType = wp.GetType();
            wp.Visible = false;
            Document wd = wp.Documents.Open(ref filename, ref MissingValue,
            ref MissingValue, ref MissingValue,
            ref MissingValue, ref MissingValue,
            ref MissingValue, ref MissingValue,
            ref MissingValue, ref MissingValue,
            ref MissingValue, ref MissingValue,
            ref MissingValue, ref MissingValue,
            ref MissingValue, ref MissingValue);

            //查找
            if (wd.Paragraphs != null && wd.Paragraphs.Count > 0)
            {
                iCount = wd.Paragraphs.Count;
                for (i = 1; i <= iCount; i++)
                {
                    wfnd = wd.Paragraphs[i].Range.Find;
                    wfnd.ClearFormatting();
                    wfnd.Text = strKey;
                    if (wfnd.Execute(ref MissingValue, ref MissingValue,
                        ref MissingValue, ref MissingValue,
                        ref MissingValue, ref MissingValue,
                        ref MissingValue, ref MissingValue,
                        ref MissingValue, ref MissingValue,
                        ref MissingValue, ref MissingValue,
                        ref MissingValue, ref MissingValue,
                        ref MissingValue))
                    {
                        checkResult = true;
                        break;
                    }
                }
            }
            //Application退出
            wp.Quit(ref MissingValue, ref MissingValue, ref MissingValue);
            return checkResult;
        }

        /// <summary>
        /// word内容查找
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <param name="searchValue">查找内容</param>
        /// <returns></returns>
        public static bool CheckExcelContent(string fileFullPath, string searchValue)
        {
            object filename = fileFullPath; //要打开的文档路径 
            string strKey = searchValue; //要搜索的文本 
            object MissingValue = Type.Missing;
            bool checkResult = false;
            int i = 0;

            if (fileFullPath.Contains("~$"))
                return checkResult;


            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.ApplicationClass();
            excel.Visible = false;
            Workbook wb = excel.Workbooks.Open(filename.ToString(), MissingValue,
             MissingValue,  MissingValue,
             MissingValue,  MissingValue,
             MissingValue,  MissingValue,
             MissingValue,  MissingValue,
             MissingValue,  MissingValue,
             MissingValue,  MissingValue,
             MissingValue);

            Microsoft.Office.Interop.Excel.Worksheet ews;
            int iEWSCnt = wb.Worksheets.Count;
            Microsoft.Office.Interop.Excel.Range oRange;
            object oText = searchValue.Trim().ToUpper();

            for (i = 1; i <= iEWSCnt; i++)
            {
                ews = null;
                ews = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[i];
                oRange = null;
                oRange = (ews.UsedRange).Find(
                oText, MissingValue, MissingValue,
                MissingValue, MissingValue, Microsoft.Office.Interop.Excel.XlSearchDirection.xlNext,
                MissingValue, MissingValue, MissingValue);
                if (oRange != null && oRange.Cells.Rows.Count >= 1 && oRange.Cells.Columns.Count >= 1)
                {
                    checkResult = true;
                    break;
                }
            }
            
            excel.Quit();
            return checkResult;
        }
    }
}
