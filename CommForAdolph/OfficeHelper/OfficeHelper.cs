using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Xls;

namespace CommForAdolph
{
    public class OfficeHelper
    {
        /// <summary>
        /// word 内容查找
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public bool CheckWordContent(string path, string content)
        {
            bool result = false;
            Document document = new Document();

            document.LoadFromFile(path);

            TextSelection[] textSelections = document.FindAllString(content, false, true);
            if (textSelections!=null&&textSelections.Count() > 0)
                result = true;
            return result;
        }

        /// <summary>
        /// Excel 内容查找
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public bool CheckExcelContent(string path, string content)
        {
            bool result = false;
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(path);
            Worksheet worksheet = workbook.Worksheets[0];

            CellRange[] ranges = worksheet.FindAllString(content, true, true);

            if (ranges.Count() > 0)
                result = true;
            return result;
        }
    }
}
