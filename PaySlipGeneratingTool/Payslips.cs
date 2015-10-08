using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Configuration;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO.Compression;
using System.Net.Mail;
using System.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Xml.Linq;
using System.Web;
using System.Web.UI;



namespace PaySlipGeneratingTool
{
    public partial class Payslips : Form
    {
        public Payslips()
        {
            InitializeComponent();
        }
        public string NameOfPDF;
        public string PayslipMonth;
        public string PayslipYear;
        public string PayslipMonthYear;
        public string EmpName;
        public int NetpayinWords;
        private void btnBrowseExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog op1 = new OpenFileDialog();
            op1.Multiselect = true;
            op1.ShowDialog();
            op1.Filter = "allfiles|*.*";
            txtExcel.Text = op1.FileName;
        }
       
        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (cmbYear.Text == "--Select--" && cmbMonth.Text == "--Select--")
            {
                MessageBox.Show("Please Select Month and Year... ");
                cmbMonth.Focus();
            }
            else if (cmbMonth.Text == "--Select--")
            {
                MessageBox.Show("Please Select Month");
                cmbMonth.Focus();
            }
            else if (cmbYear.Text == "--Select--")
            {
                MessageBox.Show("Please Select Year... ");
                cmbYear.Focus();
            }
            
            else if (txtExcel.Text == "")
            {
                MessageBox.Show("Please Browse Excel file");
                txtExcel.Focus();
            }
            else
            {
                PayslipMonth = cmbMonth.Text;
                PayslipYear = cmbYear.Text;
                PayslipMonthYear = PayslipMonth + ", " + PayslipYear;
                var confirmtext = "Genarate Payslips for the Month " + PayslipMonth + " " + PayslipYear;
                var confirmResult = MessageBox.Show(confirmtext,
                                         "Please Confirm !!",
                                         MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    // Reference to Excel Application.
                    Excel.Application xlApp = new Excel.Application();

                    // Open the Excel file.
                    // You have pass the full path of the file.
                    // In this case file is stored in the Bin/Debug application directory.
                    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(Path.GetFullPath(txtExcel.Text));

                    // Get the first worksheet.
                    Excel.Worksheet xlWorksheet = (Excel.Worksheet)xlWorkbook.Sheets.get_Item(1);

                    // Get the range of cells which has data.
                    Excel.Range xlRange = xlWorksheet.UsedRange;

                    // Get an object array of all of the cells in the worksheet with their values.
                    object[,] valueArray = (object[,])xlRange.get_Value(
                                Excel.XlRangeValueDataType.xlRangeValueDefault);

                    // iterate through each cell and display the contents.
                    for (int row = 2; row <= xlWorksheet.UsedRange.Rows.Count; ++row)
                    {
                        for (int col = 1; col <= xlWorksheet.UsedRange.Columns.Count; ++col)
                        {

                            // Print value of the cell to Console.
                            var EmpNo = valueArray[row, 1] == null ? "" : valueArray[row, 1].ToString();
                            var Dep = valueArray[row, 2] == null ? "" : valueArray[row, 2].ToString();
                            var Fname = valueArray[row, 3] == null ? "" : valueArray[row, 3].ToString();
                            var Place = valueArray[row, 4] == null ? "" : valueArray[row, 4].ToString();
                            var Desgn = valueArray[row, 5] == null ? "" : valueArray[row, 5].ToString();
                            var PayDays = valueArray[row, 6] == null ? "" : valueArray[row, 6].ToString();
                            var DOJ = valueArray[row, 7] == null ? "" : valueArray[row, 7].ToString();
                            var PANNo = valueArray[row, 8] == null ? "" : valueArray[row, 8].ToString();
                            var BankName = valueArray[row, 9] == null ? "" : valueArray[row, 9].ToString();
                            var BankAcnt = valueArray[row, 10] == null ? "" : valueArray[row, 10].ToString();
                            var PFNo = valueArray[row, 11] == null ? "" : valueArray[row, 11].ToString();
                            var email = valueArray[row, 12] == null ? "" : valueArray[row, 12].ToString();
                            var BASIC = valueArray[row, 13] == null ? "" : valueArray[row, 13].ToString();
                            var ARREARS = valueArray[row, 14] == null ? "" : valueArray[row, 14].ToString(); ;
                            var HRA = valueArray[row, 15] == null ? "" : valueArray[row, 15].ToString();
                            var CONVEYANCE = valueArray[row, 16] == null ? "" : valueArray[row, 16].ToString();
                            var MEDICALALLOWANCE = valueArray[row, 17] == null ? "" : valueArray[row, 17].ToString();
                            var OTHERSGROSS = valueArray[row, 18] == null ? "" : valueArray[row, 18].ToString();
                            var VARIABLEPAY = valueArray[row, 19] == null ? "" : valueArray[row, 19].ToString();
                            var NIGHTSHIFTALLOWANCE = valueArray[row, 20] == null ? "" : valueArray[row, 20].ToString();
                            var INCOMETAX = valueArray[row, 21] == null ? "" : valueArray[row, 21].ToString();
                            var EPF = valueArray[row, 22] == null ? "" : valueArray[row, 22].ToString();
                            var PROFESSIONALTAX = valueArray[row, 23] == null ? "" : valueArray[row, 23].ToString();
                            var OTHERSDEDUCTIONS = valueArray[row, 24] == null ? "" : valueArray[row, 24].ToString();
                            var GROSS = valueArray[row, 25] == null ? "" : valueArray[row, 25].ToString();
                            var DEDUCTIONS = valueArray[row, 26] == null ? "" : valueArray[row, 26].ToString();
                            var NETPAY = valueArray[row, 28] == null ? "" : valueArray[row, 28].ToString();
                            var MEDICALINSURANCE = valueArray[row, 27] == null ? "" : valueArray[row, 27].ToString();
                            EmpName = Fname;
                            NameOfPDF = Fname + "_" + PayslipMonthYear + " _ Payslip.pdf";
                            NetpayinWords = int.Parse(NETPAY);
                            var NETPAYWORDS = NumbersToWords(NetpayinWords);
                            string temp = File.ReadAllText("../../adt.txt");
                            temp = temp.Replace("{EmpNo}", EmpNo);
                            temp = temp.Replace("{Dep}", Dep);
                            temp = temp.Replace("{Fname}", Fname);
                            temp = temp.Replace("{Place}", Place);
                            temp = temp.Replace("{Desgn}", Desgn);
                            temp = temp.Replace("{PayDays}", PayDays);
                            temp = temp.Replace("{DOJ}", DOJ);
                            temp = temp.Replace("{PANNo}", PANNo);
                            temp = temp.Replace("{BankName}", BankName);
                            temp = temp.Replace("{BankAcnt}", BankAcnt);
                            temp = temp.Replace("{PFNo}", PFNo);
                            temp = temp.Replace("{BASIC}", BASIC);
                            temp = temp.Replace("{ARREARS}", ARREARS);
                            temp = temp.Replace("{HRA}", HRA);
                            temp = temp.Replace("{CONVEYANCE}", CONVEYANCE);
                            temp = temp.Replace("{MEDICALALLOWANCE}", MEDICALALLOWANCE);
                            temp = temp.Replace("{OTHERSGROSS}", OTHERSGROSS);
                            temp = temp.Replace("{VARIABLEPAY}", VARIABLEPAY);
                            temp = temp.Replace("{NIGHTSHIFTALLOWANCE}", NIGHTSHIFTALLOWANCE);
                            temp = temp.Replace("{INCOMETAX}", INCOMETAX);
                            temp = temp.Replace("{EPF}", EPF);
                            temp = temp.Replace("{PROFESSIONALTAX}", PROFESSIONALTAX);
                            temp = temp.Replace("{OTHERSDEDUCTIONS}", OTHERSDEDUCTIONS);
                            temp = temp.Replace("{GROSS}", GROSS);
                            temp = temp.Replace("{DEDUCTIONS}", DEDUCTIONS);
                            temp = temp.Replace("{NETPAY}", NETPAY);
                            temp = temp.Replace("{MEDICALINSURANCE}", MEDICALINSURANCE);
                            temp = temp.Replace("{NETPAYWORDS}", NETPAYWORDS);
                            temp = temp.Replace("{Month}", PayslipMonthYear);
                            SendPDFEmail(temp, email);
                            break;
                        }
                    }
                }
                else
                {
                    this.Close();
                }
            }
           
        }

        public void SendPDFEmail(string dt, string Email)
        {
            using (StringWriter sw = new StringWriter())
            {

                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringReader sr = new StringReader(dt.ToString());

                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {

                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);

                        pdfDoc.Open();
                        htmlparser.Parse(sr);
                        pdfDoc.Close();
                        byte[] bytes = memoryStream.ToArray();
                        memoryStream.Close();

                        using (MemoryStream inputData = new MemoryStream(bytes))
                        {
                            using (MemoryStream outputData = new MemoryStream())
                            {
                                string PDFFileword = "adt";//you can also generate Dynamic word  
                                PdfReader reader = new PdfReader(inputData);
                                PdfEncryptor.Encrypt(reader, outputData, true, "123", "adt", PdfWriter.ALLOW_SCREENREADERS);
                                bytes = outputData.ToArray();
                                try
                                {
                                    var MsgBody = "         Please find the attached pay slip for the month of " + PayslipMonth +" "+ PayslipYear + ". Any queries, feel free to contact me." + Environment.NewLine + Environment.NewLine + "Thanks," + Environment.NewLine + "Sravani Tamma" + Environment.NewLine + "Executive-HR";
                                    MailMessage message = new MailMessage();
                                    SmtpClient smtp = new SmtpClient();

                                    message.From = new MailAddress("chitra.vinjamuri@adroitent.com");
                                    message.To.Add(new MailAddress(Email));
                                    message.Subject = "Payslip for " + PayslipMonth;
                                    message.Body = "Hi " + EmpName + "," + Environment.NewLine + Environment.NewLine + MsgBody;
                                    message.Attachments.Add(new Attachment(new MemoryStream(bytes), NameOfPDF));
                                    smtp.Port = 80;
                                    smtp.Host = "smtpout.secureserver.net";
                                    smtp.EnableSsl = false;
                                    smtp.UseDefaultCredentials = false;
                                    smtp.Credentials = new NetworkCredential("chitra.vinjamuri@adroitent.com", "123456aA");
                                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    smtp.Send(message);
                                    lblMsg.Text = "Mail sent successfully";
                                }
                                catch (Exception ex)
                                {
                                    lblMsg.Text = "err: " + ex.Message;

                                }
                            }
                        }
                    }
                }
            }
        }
        public string NumbersToWords(int inputNumber)
        {
            int inputNo = inputNumber;

            if (inputNo == 0)
                return "Zero";

            int[] numbers = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (inputNo < 0)
            {
                sb.Append("Minus ");
                inputNo = -inputNo;
            }

            string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
            "Five " ,"Six ", "Seven ", "Eight ", "Nine "};
            string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
            "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
            string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
            "Seventy ","Eighty ", "Ninety "};
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };

            numbers[0] = inputNo % 1000; // units
            numbers[1] = inputNo / 1000;
            numbers[2] = inputNo / 100000;
            numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
            numbers[3] = inputNo / 10000000; // crores
            numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs

            for (int i = 3; i > 0; i--)
            {
                if (numbers[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (numbers[i] == 0) continue;
                u = numbers[i] % 10; // ones
                t = numbers[i] / 10;
                h = numbers[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("and ");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }
    }
}
