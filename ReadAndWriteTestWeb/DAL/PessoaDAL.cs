using ReadAndWriteTestWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace ReadAndWriteTestWeb.DAL
{
    public class PessoaDAL
    {
        public void InsertPessoa(Pessoa pessoa)
        {
            try
            {
                string stringConnection = ConfigurationManager.ConnectionStrings["connLocal"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(stringConnection))
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("Insert into TablePessoas values (@ID, @NAME, @DATE)", conn);
                    sqlCommand.Parameters.Add(new SqlParameter("@ID", pessoa.Id));
                    //sqlCommand.Parameters["@ID"].Value = ;

                    sqlCommand.Parameters.Add(new SqlParameter("@NAME", pessoa.Name));
                    //sqlCommand.Parameters["@NAME"].Value = ;

                    SqlParameter sqlParameter = new SqlParameter("@DATE", SqlDbType.DateTime);
                    sqlParameter.Value = pessoa.BormDate;//.ToString("yyyy-MM-dd h:mm tt");
                    sqlCommand.Parameters.Add(sqlParameter);
                    //sqlCommand.Parameters["@DATE"].Value = ;

                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public List<Pessoa> PesquisarPessoas(Pessoa pessoa)
        {
            try
            {
                string stringConnection = ConfigurationManager.ConnectionStrings["connLocal"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(stringConnection))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Select ID, NAME, DATE FROM TablePessoas where (ID = @ID OR @ID = 0) AND (NAME = @NAME OR @NAME = NULL) AND (DATE = @DATE OR @DATE = NULL)");

                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand(sb.ToString(), conn);
                    sqlCommand.Parameters.Add(new SqlParameter("@ID", pessoa.Id));
                    //sqlCommand.Parameters["@ID"].Value = ;

                    sqlCommand.Parameters.Add(new SqlParameter("@NAME", pessoa.Name));
                    //sqlCommand.Parameters["@NAME"].Value = ;

                    SqlParameter sqlParameter = new SqlParameter("@DATE", SqlDbType.DateTime);
                    sqlParameter.Value = pessoa.BormDate;//.ToString("yyyy-MM-dd h:mm tt");
                    sqlCommand.Parameters.Add(sqlParameter);
                    //sqlCommand.Parameters["@DATE"].Value = ;


                    using (IDataReader reader = sqlCommand.ExecuteReader())
                    {
                        List<Pessoa> customers = reader.Select(r => new Pessoa
                        {
                            Id = r["ID"] is DBNull ? null : r["ID"].ToString(),
                            Name = r["NAME"] is DBNull ? null : r["NAME"].ToString(),
                            BormDate = r["DATE"] is DBNull ? null : r["DATE"].ToString()
                        }).ToList();
                    }
                    IDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    //List<Pessoa> lista = sqlDataReader.AutoMap<Pessoa>().ToList();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }
        public static IEnumerable<T> Select<T>(this IDataReader reader,
                                       Func<IDataReader, T> projection)
        {
            while (reader.Read())
            {
                yield return projection(reader);
            }
        }
    }
}