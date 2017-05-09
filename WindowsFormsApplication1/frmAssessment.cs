using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1
{
    public partial class frmAssessment : Form
    {
        string sourceFile = "";
        string sourcePath = "";
        string list1Path = "";
        string list2Path = "";
        string sSQL = "";
        DataTable dtList1 = new DataTable();
        System.Data.Odbc.OdbcConnection conn;


        public frmAssessment()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close the application?", "Close Application", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        private Boolean PrepareCSVFile()
        {
            try
            {
                UpdateStatus("Preparing Data...");


                sourceFile = txtCSVFile.Text;
                sourcePath = sourceFile.Substring(0, sourceFile.LastIndexOf("\\"));
                var delimiter = ",";
                var firstLineContainsHeaders = chkHeaders.Checked;
                var tempPath = Path.GetTempFileName();
                var lineNumber = 0;

                var splitExpression = new Regex(@"(" + delimiter + @")(?=(?:[^""]|""[^""]*"")*$)");

                dtList1.Columns.Add("NameField", typeof(string));
                using (var writer = new StreamWriter(tempPath))
                using (var reader = new StreamReader(sourceFile))
                {
                    string line = null;
                    string[] headers = null;
                    int spaceIndex = 0;
                    if (firstLineContainsHeaders)
                    {

                        line = reader.ReadLine();
                        lineNumber++;

                        if (string.IsNullOrEmpty(line))
                        {
                            UpdateStatus("The specified file is empty");
                            return false; // file is empty;
                        }

                        headers = splitExpression.Split(line).Where(s => s != delimiter).ToArray();

                        line = line.Replace(",Address,", ",StreetNumber,Address,");
                        writer.WriteLine(line); // write the original header to the temp file.
                    }

                    while ((line = reader.ReadLine()) != null)
                    {
                        lineNumber++;

                        var columns = splitExpression.Split(line).Where(s => s != delimiter).ToArray();

                        // check valid format of the CSV to make sure it always has the same number of columns in a line
                        if (headers == null) headers = new string[columns.Length];

                        if (columns.Length != headers.Length) throw new InvalidOperationException(string.Format("Line {0} is missing one or more columns.", lineNumber));

                        // fill the list1 with names and surnames while looping through the file already
                        DataRow oRow = dtList1.NewRow();
                        oRow["NameField"] = columns[0];
                        dtList1.Rows.Add(oRow);
                        oRow = dtList1.NewRow();
                        oRow["NameField"] = columns[1];
                        dtList1.Rows.Add(oRow);

                        // add a comma between street number and name to split them into separate fields
                        // if there are no numbers in the address, slot in the comma without a street number value.  For cases such as corner addresses
                        spaceIndex = columns[2].IndexOf(" ");
                        if (spaceIndex < 0)
                        {
                            columns[2] = columns[2].Insert(0, ",");
                        }
                        else
                        {
                            columns[2] = columns[2].Remove(spaceIndex, 1);
                            columns[2] = columns[2].Insert(spaceIndex, ",");
                        }

                        writer.WriteLine(string.Join(delimiter, columns));
                    }

                }


                File.Delete(sourceFile);
                File.Move(tempPath, sourceFile);
                UpdateStatus("Data Preparation done...");

            }
            catch (IOException e)
            {
                MessageBox.Show("The following error occurred during the preparation of the data:\n" + e.Message);
                return false;
            }
            return true;
        }

        private Boolean ConnectCSV()
        {
            try
            {
                conn = new System.Data.Odbc.OdbcConnection(@"Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" + sourcePath + ";Extensions=asc,csv,tab,txt;Persist Security Info=False");
            }
            catch (IOException e)
            {
                UpdateStatus("Unable to secure data connection to CSV file");
                MessageBox.Show("The following error occurred while securing a data connection to the CSV file:\n" + e.Message);
                return false;
            }
            return true;
        }

        private Boolean CreateList1()
        {
            try
            {
                UpdateStatus("Creating List 1 (Frequency)...");

                list1Path = sourcePath + "\\List1.csv";
                var query =(from row in dtList1.AsEnumerable()
                            group row by row.Field<string>("NameField") into names
                            select new
                            {
                                NameFld = names.Key,
                                Cnt = names.Count()
                            }).OrderByDescending(x => x.Cnt).ThenBy(x => x.NameFld);

                string csv = "";
                foreach (var nameField in query)
                {
                    csv += string.Format("{0},{1}\n", nameField.NameFld, nameField.Cnt);
                }
                File.WriteAllText(list1Path, csv);

            }
            catch (IOException e)
            {
                MessageBox.Show("The following error occurred during the creation of List 1:\n" + e.Message);
                return false;
            }
            UpdateStatus("List 1 done...");
            return true;
        }

        private Boolean CreateList2()
        {
            try
            {
                UpdateStatus("Creating List 2 (Aphabetical Order)...");

                string csvFileName = sourceFile.Substring(sourceFile.LastIndexOf("\\") + 1);
                list2Path = sourceFile.Substring(0, sourceFile.LastIndexOf("\\")) + "\\List2.csv";
                DataTable dt = new DataTable();
                System.Data.Odbc.OdbcDataAdapter da;

                sSQL = "";
                sSQL += "SELECT StreetNumber & ' ' & Address as Addresses";
                sSQL += "  FROM [" + csvFileName + "]";
                sSQL += " ORDER BY Address";

                da = new System.Data.Odbc.OdbcDataAdapter(sSQL, conn);
                da.Fill(dt);

                WriteDataTableToCSV(dt, list2Path);
            }
            catch (IOException e)
            {
                MessageBox.Show("The following error occurred during the creation of List 2:\n" + e.Message);
                return false;
            }
            UpdateStatus("List 2 done...");

            return true;
        }

        private void WriteDataTableToCSV(DataTable dt, string filePath)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }

            File.WriteAllText(filePath, sb.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtCSVFile.Text == "")
            {
                MessageBox.Show("Please enter a valid filename");
                return;
            }
            if (!File.Exists(txtCSVFile.Text))
            {
                MessageBox.Show("The selected file does not exist. Please check the file path and try again.");
                return;
            }
            if (!PrepareCSVFile())
            {
                MessageBox.Show("Preparation of the CSV file was not successful");
                return;
            }
            if (!ConnectCSV())
            {
                MessageBox.Show("A data connection to the CSV file could not be secured");
                return;
            }
            if (!CreateList1())
            {
                MessageBox.Show("Creation of the first list was not successful");
                return;
            }
            if (!CreateList2())
            {
                MessageBox.Show("Creation of the first list was not successful");
                return;
            }
            UpdateStatus("Process Completed...");
            conn.Close();
            MessageBox.Show("Process has completed successfully.\n\nThe created lists can be found as List1.csv and List2.csv in the following folder:\n\n" + sourcePath);
        }

        private void UpdateStatus(string UpdateText)
        {
            lblStatus.Text = UpdateText;
            System.Threading.Thread.Sleep(100);
        }

    }
}

