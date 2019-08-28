using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Printing;
using System.Globalization;
using System.ServiceModel.Web;
using System.Net;
using Newtonsoft.Json;

namespace FAA_SGH
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            
         
            lbError.Visible = false;
            // ...or with DirectoryInfo instance method.
            //System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(@"C:\Users\Administrador\Documents\Teste");
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(@"C:\Users\hospub\Documents\Teste");
            // Delete this dir and all subdirs.
            try
            {
                di.Delete(true);
            }
            catch (System.IO.IOException e)
            {
                error = e.Message;
            }
        }
        //MyService.DadosFAA dFaa;
        //MyService.Services sv;
        long _faa;
        String error;
        string enderecoCompleto = "";
        Consulta dFaa;
        Meta metadados;
     
        public class Meta
        {
            public int code { get; set; }
            public string status { get; set; }
            public string method_name { get; set; }
        }

        

        public class Procedimento
        {
            public int cd_procedimento { get; set; }
            public string dc_procedimento { get; set; }
            
        }

        public class Consulta
        {
            public int prontuatrio { get; set; }
            public string nome { get; set; }
            public DateTime dataNascimento { get; set; }
            public string sexo { get; set; }
           
            public int consulta{ get; set; }
            public DateTime dataConsulta{ get; set; }
            public string nomeEspecialidade { get; set; }
            public string nomeEquipe { get; set; }
            public string nomeProfissional{ get; set; }
            public string orgao { get; set; }
            public List<Procedimento> procedimentos { get; set; }
        }

           

        

      
        public class RootObject
        {
            public Meta meta { get; set; }
            public Consulta consulta { get; set; }
        }



        private int MontagemTxt( Int64 cod_consulta)
        {
            int status = 0;
            string url = "http://intranethspm:5000/hspmsgh-api/pacientes/paciente/consulta/" + cod_consulta;
            WebRequest request = WebRequest.Create(url);
            try
            {
                using (var twitpicResponse = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new StreamReader(twitpicResponse.GetResponseStream()))
                    {
                        JsonSerializer json = new JsonSerializer();
                        var objText = reader.ReadToEnd();
                        dFaa = JsonConvert.DeserializeObject<Consulta>(objText);

                    }
                }
                const string ESC = "\u001B";
                const string v = "\u0076";
                string lines = ESC + "&f1y2X";


                string lines2 = ESC + "&f2y2X";
                string duplex = ESC + "&l1S";
                string fonte1 = ESC + "(8U" + ESC + "(s1p12" + v + "0s3b16602T";

                string fonte2 = ESC + "(8U" + ESC + "(s1p8" + v + "0s0b16602T";

                string fonte3 = ESC + "(8U" + ESC + "(s1p8" + v + "0s3b16602T";


                string espaço1 = ESC + "*p140Y"; //RH
                string espaço = ESC + "*p190Y"; //Nome
                string espaço2 = ESC + "*p240Y"; //Data Nascimento - Sexo
                string espaço3 = ESC + "*p290Y"; //FAA - Hora Impressão
                string espaço10 = ESC + "*p340Y"; //Data Consulta
                string espaço4 = ESC + "*p390Y"; // Especialidade
                string espaço5 = ESC + "*p440Y"; //Profissional
                string espaço6 = ESC + "*p400Y"; //Clínica    
                string espaço7 = ESC + "*p865Y"; // 1º Código de procedimento - 1ºDescrição do Procedimento
                string espaço8 = ESC + "*p095Y"; // Espaço Código em diante - 2º Descrição do Procedimento


                string espaçohor1 = ESC + "*p1320X";  // RH - Nome - Data - FAA - Data Consulta - Especialidade -Profissional
                string espaçohor2 = ESC + "*p1720X";  // Sexo
                string espaçohor3 = ESC + "*p1720X";  // Data Consulta
                string espaçohor4 = ESC + "*p420X";  // Codigo de procedimento
                string espaçohor5 = ESC + "*p750X";  // Descrição do procedimento
                string espaçohor6 = ESC + "*p500X";  // Clinica



                DateTime dataImpressão = DateTime.Now;
                string strDataImpressao = dataImpressão.Hour.ToString() + ":" + dataImpressão.Minute.ToString().PadLeft(2, '0');

                Random randNum = new Random();

                //string pathString = "C:\\Users\\Administrador\\Documents\\Teste";
                string pathString = "C:\\Users\\hospub\\Documents\\Teste";

                if (!System.IO.Directory.Exists(pathString))
                {
                    System.IO.Directory.CreateDirectory(pathString);
                }

                //enderecoCompleto = "C:\\Users\\Administrador\\Documents\\Teste\\docteste" + randNum.Next().ToString() + ".txt";
                enderecoCompleto = "C:\\Users\\hospub\\Documents\\Teste\\docteste" + randNum.Next().ToString() + ".txt";

                // Write the string to a file.

                //System.IO.StreamWriter file = new System.IO.StreamWriter(enderecoCompleto, false, Encoding.GetEncoding(28591));
                System.IO.StreamWriter file = new System.IO.StreamWriter(enderecoCompleto);

                file.WriteLine(duplex + fonte1 + espaço1 + espaçohor1 + "RH: " + dFaa.prontuatrio); // Rh
                //file.WriteLine( fonte1 + espaço1 + espaçohor1 + "RH: " + dFaa.Rh); // Rh
                file.WriteLine(fonte2 + espaço + espaçohor1 + "Nome: " + dFaa.nome); // Nome

                file.WriteLine(espaço2 + espaçohor1 + "Data Nasc.: " + dFaa.dataNascimento.ToShortDateString() + espaçohor2 + "Sexo: " + dFaa.sexo); // Data de Nascimento - Sexo



                // file.WriteLine(espaço3 + espaçohor1 + "FAA: " + dFaa.NumFAA + espaçohor3 + "Data Consulta: " + dFaa.Data); // Número do FAA 
                file.WriteLine(espaço3 + espaçohor1 + "FAA: " + dFaa.consulta + espaçohor3 + "Hora da Impressao: " + strDataImpressao); // Número do FAA 
                file.WriteLine(espaço10 + fonte3 + espaçohor1 + "Data Consulta: " + dFaa.dataConsulta.ToShortDateString()); // Data Hora 



                file.WriteLine(espaço4 + fonte2 + espaçohor1 + "Especialidade: " + dFaa.nomeEspecialidade + " - " + dFaa.nomeEquipe);// Especialidade



                /*   String[] espearr = dFaa.Especialidade.Split('-');

                   g.DrawString(espearr[0], fntRH, System.Drawing.Brushes.Black, 180, 145);
                   */
                file.WriteLine(espaço5 + espaçohor1 + "Profissional: " + dFaa.nomeProfissional);  // Profissional

                file.WriteLine(fonte1 + espaço6 + espaçohor6 + removerAcentos(dFaa.nomeEspecialidade)); //Clinica

                //file.WriteLine(fonte1 + espaço6 + espaçohor6 + dFaa.Clinica); //Clinica
                //file.WriteLine(fonte1 + espaço6 + espaçohor6 + "í Í é É");




                //Image myimg = Code128Rendering.MakeBarcodeImage(txtFaa.Text, 1, true);


                foreach (Procedimento row in dFaa.procedimentos)
                {
                    String codigo = row.cd_procedimento.ToString();
                    if (codigo.Length < 8)
                        codigo = codigo.PadLeft(8, '0');
                    file.WriteLine(espaço7 + espaçohor4 + fonte2 + codigo + espaçohor5 + row.dc_procedimento); //Código - Descrição de Procedimento

                    int soma = Int32.Parse(espaço7.Substring(3, 4).Replace("Y", "")) + Int32.Parse(espaço8.Substring(3, 3));
                    espaço7 = ESC + "*p" + soma + "Y";
                }

                file.WriteLine(lines);




                file.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");

                file.WriteLine(lines2);
                file.Close();
            }
     
                   catch (Exception ex)
                {
                    error = ex.Message;
                    lbError.Visible = true;
                    lbError.Text = error.ToString();

                    btImprimir.Enabled = true;
                    txtFAA.Enabled = true;




                    status = 1;
                }

            return status;


        }

        public class RawPrinterHelper
        {
            // Structure and API declarions:
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public class DOCINFOA
            {
                [MarshalAs(UnmanagedType.LPStr)]
                public string pDocName;
                [MarshalAs(UnmanagedType.LPStr)]
                public string pOutputFile;
                [MarshalAs(UnmanagedType.LPStr)]
                public string pDataType;
            }
            [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

            [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool ClosePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

            [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool EndDocPrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool StartPagePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool EndPagePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

            // SendBytesToPrinter()
            // When the function is given a printer name and an unmanaged array
            // of bytes, the function sends those bytes to the print queue.
            // Returns true on success, false on failure.
            public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
            {
                Int32 dwError = 0, dwWritten = 0;
                IntPtr hPrinter = new IntPtr(0);
                DOCINFOA di = new DOCINFOA();
                bool bSuccess = false; // Assume failure unless you specifically succeed.

                di.pDocName = "My C#.NET RAW Document";
                di.pDataType = "RAW";

                // Open the printer.
                if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
                {
                    // Start a document.
                    if (StartDocPrinter(hPrinter, 1, di))
                    {
                        // Start a page.
                        if (StartPagePrinter(hPrinter))
                        {
                            // Write your bytes.
                            bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                            EndPagePrinter(hPrinter);
                        }
                        EndDocPrinter(hPrinter);
                    }
                    ClosePrinter(hPrinter);
                }
                // If you did not succeed, GetLastError may give more information
                // about why not.
                if (bSuccess == false)
                {
                    dwError = Marshal.GetLastWin32Error();
                }
                return bSuccess;
            }

            public static bool SendFileToPrinter(string szPrinterName, string szFileName)
            {
                // Open the file.
                FileStream fs = new FileStream(szFileName, FileMode.Open);
                // Create a BinaryReader on the file.
                BinaryReader br = new BinaryReader(fs);
                // Dim an array of bytes big enough to hold the file's contents.
                Byte[] bytes = new Byte[fs.Length];
                bool bSuccess = false;
                // Your unmanaged pointer.
                IntPtr pUnmanagedBytes = new IntPtr(0);
                int nLength;

                nLength = Convert.ToInt32(fs.Length);
                // Read the contents of the file into the array.
                bytes = br.ReadBytes(nLength);
                // Allocate some unmanaged memory for those bytes.
                pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
                // Copy the managed byte array into the unmanaged array.
                Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
                // Send the unmanaged bytes to the printer.
                bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
                // Free the unmanaged memory that you allocated earlier.
                Marshal.FreeCoTaskMem(pUnmanagedBytes);
                return bSuccess;
            }
            public static bool SendStringToPrinter(string szPrinterName, string szString)
            {
                IntPtr pBytes;
                Int32 dwCount;
                // How many characters are in the string?
                dwCount = szString.Length;
                // Assume that the printer is expecting ANSI text, and then convert
                // the string to ANSI text.
                pBytes = Marshal.StringToCoTaskMemAnsi(szString);
                // Send the converted ANSI string to the printer.
                SendBytesToPrinter(szPrinterName, pBytes, dwCount);
                Marshal.FreeCoTaskMem(pBytes);
                return true;
            }
        }


        private void btImprimir_Click(object sender, EventArgs e)
        {
            int status = 0;
            lbError.Visible = false;
            try
            {
                btImprimir.Enabled = false;
                _faa = Convert.ToInt64(txtFAA.Text);
                txtFAA.Enabled = false;
                try
                {
                    
                        int retorno = MontagemTxt(_faa);
                        if (retorno.ToString() == "0")
                        {


                            //string endereco = "C:\\Users\\hospub\\Documents\\docteste.txt";
                            // Allow the user to select a printer.
                            PrintDialog pd = new PrintDialog();
                            pd.PrinterSettings = new PrinterSettings();

                            // Print the file to the printer.
                            RawPrinterHelper.SendFileToPrinter(pd.PrinterSettings.PrinterName, enderecoCompleto);

                            //excluir arquivo
                            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(@"C:\Users\hospub\Documents\Teste");
                            // Delete this dir and all subdirs.
                            try
                            {
                                di.Delete(true);
                            }
                            catch (System.IO.IOException ev)
                            {
                                error = ev.Message;
                                lbError.Visible = true;
                                lbError.Text = error.ToString();
                                btImprimir.Enabled = true;
                                txtFAA.Enabled = true;
                                status = 1;
                            }





                            btImprimir.Enabled = true;
                            txtFAA.Enabled = true;

                            lbError.Text = "";
                            status = 0;
                        }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    lbError.Visible = true;
                    lbError.Text = error.ToString();

                    btImprimir.Enabled = true;
                    txtFAA.Enabled = true;
                    status = 1;
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
                lbError.Visible = true;
                lbError.Text = error.ToString();

                btImprimir.Enabled = true;
                txtFAA.Enabled = true;
                status = 1;
            }
        }
      

        public static string removerAcentos(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }

    }
}
