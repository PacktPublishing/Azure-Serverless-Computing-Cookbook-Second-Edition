using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;


namespace ExcelImport.DurableFunctions
{
    class EPPLusExcelManager
    {
        public List<Employee> ReadExcelData(Stream stream)
        {
            List<Employee> employees = new List<Employee>();
            //FileInfo existingFile = new FileInfo("EmployeeInformation.xlsx");
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet excelWorksheet = package.Workbook.Worksheets[0];
                for (int EmployeeIndex = 2; EmployeeIndex < excelWorksheet.Dimension.Rows + 1; EmployeeIndex++)
                {
                    employees.Add(new Employee()
                    {
                        EmpId = Convert.ToString(excelWorksheet.Cells[EmployeeIndex, 1].Value),
                        Name = Convert.ToString(excelWorksheet.Cells[EmployeeIndex, 2].Value),
                        Email = Convert.ToString(excelWorksheet.Cells[EmployeeIndex, 3].Value),
                        PhoneNumber = Convert.ToString(excelWorksheet.Cells[EmployeeIndex, 4].Value)
                    });
                }
            }
            return employees;
        }
    }
    public class Employee
    {
        public string EmpId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

}
