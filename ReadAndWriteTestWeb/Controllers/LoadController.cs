using ReadAndWriteTestWeb.DAL;
using ReadAndWriteTestWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ReadAndWriteTestWeb.Controllers
{
    public class LoadController : Controller
    {
        // GET: Load
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Load()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult LoadFile(string txtCaminhoArquivo)
        {
            GravarDados(txtCaminhoArquivo);

            ViewBag.result = "Dados carregados com sucesso.";

            return View("~/Views/Load/Load.cshtml");
        }

        private static void GravarDados(string txtCaminhoArquivo)
        {
            PessoaDAL pessoaDAL = new PessoaDAL();
            Pessoa pessoaInsert = new Pessoa();
            try
            {
                using (StreamReader sr = new StreamReader(txtCaminhoArquivo))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        //String[] elements = Regex.Split()


                        pessoaInsert.Id = Convert.ToInt32(line.Split(';')[0]);
                        pessoaInsert.Name = line.Split(';')[1];
                        pessoaInsert.BormDate = DateTime.Parse(line.Split(';')[2], new CultureInfo("pt-BR"));

                        pessoaDAL.InsertPessoa(pessoaInsert);
                    }
                }
            }
            catch (SqlException se)
            {
                throw;
            }
        }

        private static List<Pessoa> ObterDados()
        {
            List<Pessoa> listaPessoa = new List<Pessoa>();
            PessoaDAL pessoaDAL = new PessoaDAL();
            try
            {

                using (StreamReader sr = new StreamReader())
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        //String[] elements = Regex.Split()


                        pessoaInsert.Id = Convert.ToInt32(line.Split(';')[0]);
                        pessoaInsert.Name = line.Split(';')[1];
                        pessoaInsert.BormDate = DateTime.Parse(line.Split(';')[2], new CultureInfo("pt-BR"));

                        pessoaDAL.InsertPessoa(pessoaInsert);
                    }
                }
            }
            catch (SqlException se)
            {
                throw;
            }



            return listaPessoa;
        }
    }
}