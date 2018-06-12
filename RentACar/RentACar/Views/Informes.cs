using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentACar.Views
{
    public partial class Informes : Form
    {
        List<Models.Alquilados> Model;
        public Informes(List<Models.Alquilados> Model)
        {
            InitializeComponent();
            panel1.BackColor = Resources.Colors.Info;
            this.Model = Model;
            dataGridView1.DataSource = Model;
        }

        private void Informes_Load(object sender, EventArgs e)
        {

        }

        public void GenerateExcel2007(DataSet p_dsSrc)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "Guardar el reporte como...";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
                using (ExcelPackage objExcelPackage = new ExcelPackage())
                {
                    var p_strPath = saveFileDialog1.FileName;

                    if (!p_strPath.Contains(".xls")) { p_strPath = p_strPath + ".xls"; }

                    foreach (DataTable dtSrc in p_dsSrc.Tables)
                    {
                        //Create the worksheet    
                        ExcelWorksheet objWorksheet = objExcelPackage.Workbook.Worksheets.Add(dtSrc.TableName);
                        //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1    
                        objWorksheet.Cells["A1"].LoadFromDataTable(dtSrc, true);
                        objWorksheet.Column(4).Style.Numberformat.Format = "yyyy-mm-dd";
                        objWorksheet.Cells.Style.Font.SetFromFont(new Font("Calibri", 10));
                        objWorksheet.Cells.AutoFitColumns();
                        //Format the header    
                        using (ExcelRange objRange = objWorksheet.Cells["A1:XFD1"])
                        {
                            objRange.Style.Font.Bold = true;
                            objRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            objRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            objRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        }
                    }

                    //Write it back to the client    
                    if (File.Exists(p_strPath))
                        File.Delete(p_strPath);

                    //Create excel file on physical disk    
                    FileStream objFileStrm = File.Create(p_strPath);
                    objFileStrm.Close();

                    //Write content to excel file    
                    File.WriteAllBytes(p_strPath, objExcelPackage.GetAsByteArray());
                }
        }        

        private void button1_Click(object sender, EventArgs e)
        {
            var model = Model;

            GenerateExcel2007(Tools.ConvertToDataSet(model, "reporte"));
        }
    }

    public static class Tools
    {
        public static DataSet ConvertToDataSet<T>(this IEnumerable<T> source, string name)
        {
            if (source == null)
                throw new ArgumentNullException("source ");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            var converted = new DataSet(name);
            converted.Tables.Add(NewTable(name, source));
            return converted;
        }

        private static DataTable NewTable<T>(string name, IEnumerable<T> list)
        {
            PropertyInfo[] propInfo = typeof(T).GetProperties();
            DataTable table = Table<T>(name, list, propInfo);
            IEnumerator<T> enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
                table.Rows.Add(CreateRow<T>(table.NewRow(), enumerator.Current, propInfo));
            return table;
        }

        private static DataRow CreateRow<T>(DataRow row, T listItem, PropertyInfo[] pi)
        {
            foreach (PropertyInfo p in pi)
                row[p.Name.ToString()] = p.GetValue(listItem, null);
            return row;
        }

        private static DataTable Table<T>(string name, IEnumerable<T> list, PropertyInfo[] pi)
        {
            DataTable table = new DataTable(name);
            foreach (PropertyInfo p in pi)
                table.Columns.Add(p.Name, p.PropertyType);
            return table;
        }
    }
}
