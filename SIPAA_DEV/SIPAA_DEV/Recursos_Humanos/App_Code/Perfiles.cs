using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SIPAA_DEV.Recursos_Humanos.App_Code
{
    class Perfiles
    {

        public int CVPerfil;
        public string Descripcion;
        public string UsuuMod;
        public DateTime FhumMod;
        public string PrguMod;
      //  Conexion objConexion = new Conexion();

        public Dictionary<int,string> ObtenerListPerfiles() {

            Dictionary<int, string> dcPerfiles = new Dictionary<int, string>();

         //   ConnectionStringSettings strConexion = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            SqlConnection sqlConnection1 = new SqlConnection("Data Source=192.168.9.77;Initial Catalog=sipaa_d;User ID=Desarrollo;Password=Desa17");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT [CVPERFIL]
                                     ,[DESCRIPCION]
                                     ,[USUUMOD]
                                     ,[FHUMOD]
                                     ,[PRGUMOD]
                                     FROM[dbo].[ACCECPERFIL]";

            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {

                Perfiles objPerfiles = new Perfiles();
                objPerfiles.CVPerfil = reader.GetInt32(reader.GetOrdinal("CVPERFIL"));
                objPerfiles.Descripcion = reader.GetString(reader.GetOrdinal("DESCRIPCION"));

                dcPerfiles.Add(objPerfiles.CVPerfil, objPerfiles.Descripcion);
            }

            sqlConnection1.Close();

            return dcPerfiles;

        }


        public DataTable ObtenerPerfilesxBusqueda(string Descripcion)
        {


            //   ConnectionStringSettings strConexion = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            SqlConnection sqlConnection1 = new SqlConnection("Data Source=192.168.9.77;Initial Catalog=sipaa_d;User ID=Desarrollo;Password=Desa17");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT CVPERFIL, DESCRIPCION
                                FROM ACCECPERFIL WHERE DESCRIPCION LIKE '%'+ @Descripcion +'%'";


            cmd.Parameters.Add(@"Descripcion", SqlDbType.VarChar).Value = Descripcion;

            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            sqlConnection1.Close();

            DataTable dtPerfiles = new DataTable();
            Adapter.Fill(dtPerfiles);
            return dtPerfiles;

        }


        public int intGestionarPerfiles(Perfiles objPerfil,int iOpcion)
        {



            //   ConnectionStringSettings strConexion = ConfigurationManager.ConnectionStrings["DefaultConnection"];

         //   Conexion objConexion = new Conexion();

            SqlConnection sqlConnection1 = new SqlConnection("Data Source=192.168.9.77;Initial Catalog=sipaa_d;User ID=Desarrollo;Password=Desa17");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"sp_GestionPerfiles";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = sqlConnection1;

            cmd.Parameters.Add("@CvPerfil", SqlDbType.Int).Value = objPerfil.CVPerfil;
            cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = objPerfil.Descripcion;
            cmd.Parameters.Add("@UsuarioUMod", SqlDbType.VarChar).Value = objPerfil.UsuuMod;
            cmd.Parameters.Add("@ProgramaUMod", SqlDbType.VarChar).Value = objPerfil.PrguMod;
            cmd.Parameters.Add("@Opcion", SqlDbType.Int).Value = iOpcion;

  
            sqlConnection1.Open();

            int iResponse = Convert.ToInt32(cmd.ExecuteScalar());

            sqlConnection1.Close();

            return iResponse;

        }
    }


}
