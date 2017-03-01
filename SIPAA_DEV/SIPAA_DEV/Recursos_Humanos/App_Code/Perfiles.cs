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
       

        public Dictionary<int,string> ObtenerListPerfiles() {

            Dictionary<int, string> dcPerfiles = new Dictionary<int, string>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT [CVPERFIL]
                                     ,[DESCRIPCION]
                                     ,[USUUMOD]
                                     ,[FHUMOD]
                                     ,[PRGUMOD]
                                     FROM[dbo].[ACCECPERFIL]";
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

           

            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {

                Perfiles objPerfiles = new Perfiles();
                objPerfiles.CVPerfil = reader.GetInt32(reader.GetOrdinal("CVPERFIL"));
                objPerfiles.Descripcion = reader.GetString(reader.GetOrdinal("DESCRIPCION"));

                dcPerfiles.Add(objPerfiles.CVPerfil, objPerfiles.Descripcion);
            }

            objConexion.cerrarConexion();
            return dcPerfiles;

        }


        public DataTable ObtenerPerfilesxBusqueda(string Descripcion)
        {


          SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT CVPERFIL, DESCRIPCION
                                FROM ACCECPERFIL WHERE DESCRIPCION LIKE '%'+ @Descripcion +'%'" ;


            cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = Descripcion;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtPerfiles = new DataTable();
            Adapter.Fill(dtPerfiles);
            return dtPerfiles;

        }


        public List<int> ObtenerPerfilesxUsuario(string cvUsuario)
        {

            List<int> ltPerfilesxUsuario = new List<int>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT [CVUSUARIO]
                                 ,[CVPERFIL]
                                FROM [ACCEAUSUPER] WHERE CVUSUARIO = @cvUsuario";


            cmd.Parameters.Add("@cvUsuario", SqlDbType.VarChar).Value = cvUsuario;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read()) {

                CVPerfil = reader.GetInt32(reader.GetOrdinal("CVPERFIL"));
                ltPerfilesxUsuario.Add(CVPerfil);
            }

            objConexion.cerrarConexion();

            return ltPerfilesxUsuario;

        }


        public int GestionarPerfiles(Perfiles objPerfil,int iOpcion)
        {



            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"sp_GestionPerfiles";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@CvPerfil", SqlDbType.Int).Value = objPerfil.CVPerfil;
            cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = objPerfil.Descripcion;
            cmd.Parameters.Add("@UsuarioUMod", SqlDbType.VarChar).Value = objPerfil.UsuuMod;
            cmd.Parameters.Add("@ProgramaUMod", SqlDbType.VarChar).Value = objPerfil.PrguMod;
            cmd.Parameters.Add("@Opcion", SqlDbType.Int).Value = iOpcion;



            objConexion.asignarConexion(cmd);

            int iResponse = Convert.ToInt32(cmd.ExecuteScalar());


            objConexion.cerrarConexion();

            return iResponse;

        }
    }


}
