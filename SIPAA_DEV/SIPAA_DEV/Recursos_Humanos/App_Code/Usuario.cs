using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPAA_DEV.Recursos_Humanos.App_Code
{
    class Usuario
    {

        public string CVUsuario;
        public int Idtrab;
        public string Nombre;
        public string Pass;
        public int StUsuario;
        public string UsuuMod;
        public DateTime FhumMod;
        public string PrguMod;
       



        public List<Usuario> ObtenerListaUsuarios() {
            List<Usuario> ltUsuarios = new List<Usuario>();

                SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT [CVUSUARIO]
                              ,[IDTRAB]
                              ,[NOMBRE]
                              ,[PASSW]
                              ,[STUSUARIO]
                              ,[USUUMOD]
                              ,[FHUMOD]
                              ,[PRGUMOD]
                          FROM [dbo].[ACCECUSUARIO]";
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {

                Usuario objUsuario = new Usuario();
                objUsuario.CVUsuario = reader.GetString(reader.GetOrdinal("CVUSUARIO"));
                objUsuario.Idtrab = reader.GetInt32(reader.GetOrdinal("IDTRAB"));
                objUsuario.Nombre = reader.GetString(reader.GetOrdinal("NOMBRE"));
                objUsuario.Pass = reader.GetString(reader.GetOrdinal("PASSW"));
                objUsuario.UsuuMod = reader.GetString(reader.GetOrdinal("USUUMOD"));
                objUsuario.FhumMod = reader.GetDateTime(reader.GetOrdinal("FHUMOD"));
                objUsuario.PrguMod = reader.GetString(reader.GetOrdinal("PRGUMOD"));


                ltUsuarios.Add(objUsuario);
            }

            objConexion.cerrarConexion();

            return ltUsuarios;

        }


        public DataTable ObtenerDataTableUsuarios(List<Usuario> ltUsuario) {


            DataTable dtUsuarios = new DataTable();
            dtUsuarios.Columns.Add("CvUsuario");
            dtUsuarios.Columns.Add("IdTrab");
            dtUsuarios.Columns.Add("Nombre");

            for (int iContador = 0; iContador < ltUsuario.Count(); iContador++)
            {

                Usuario objUsuarioActual = new Usuario();
                objUsuarioActual = ltUsuario.ElementAt(iContador);
                DataRow row = dtUsuarios.NewRow();
                row["CvUsuario"] = objUsuarioActual.CVUsuario.ToString();
                row["IdTrab"] = objUsuarioActual.Idtrab.ToString();
                row["Nombre"] = objUsuarioActual.Nombre;

                dtUsuarios.Rows.Add(row);

            }


            return dtUsuarios;

        }


        public void AsignarPerfilaUsuario(string CVUsuario,int CVPerfil,string UsuuMod,string PrguMod) {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_AsignarPerfil";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CVUsuario", SqlDbType.VarChar).Value = CVUsuario;
            cmd.Parameters.Add("@CvPerfil", SqlDbType.Int).Value = CVPerfil;
            cmd.Parameters.Add("@USUUMOD", SqlDbType.VarChar).Value = UsuuMod;
            cmd.Parameters.Add("@PRGUMOD", SqlDbType.VarChar).Value = PrguMod;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.ExecuteNonQuery();
            
            objConexion.cerrarConexion();


        }

        public DataTable ObtenerUsuariosxBusqueda(string Nombre,string idTrab)
        {


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT [CVUsuario]
                                      ,[IdTrab]
                                      ,[Nombre]
                                     
                                  FROM [dbo].[ACCECUSUARIO] 
                                    WHERE NOMBRE LIKE '%'+ @NOMBRE +'%'
                                     AND IDTRAB LIKE  '%'+ @IDTRAB +'%'  ";


            cmd.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = Nombre;
            cmd.Parameters.Add("@IDTRAB", SqlDbType.VarChar).Value = idTrab;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtPerfiles = new DataTable();
            Adapter.Fill(dtPerfiles);
            return dtPerfiles;

        }

    }
}
