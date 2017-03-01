using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SIPAA_DEV.Recursos_Humanos.App_Code
{
    class Modulos
    {

        public string CVModulo;
        public string Descripcion;
        public string CVModPadre;
        public int Orden;
        public string Ambiente;
        public string Modulo;
        public string UsuuMod;
        public DateTime FhuMod;
        public string PrguMod;
        Conexion objConexion = new Conexion();

        public List<Modulos> ObtenerListModulos()
        {

            List<Modulos> ltModulos = new List<Modulos>();
      SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT [CVMODULO]
                                  ,[DESCRIPCION]
                                  ,[CVMODPAD]
                                  ,[ORDEN]
                                  ,[AMBIENTE]
                                  ,[MODULO]
                                  ,[USUUMOD]
                                  ,[FHUMOD]
                                  ,[PRGUMOD]
                              FROM [dbo].[ACCECMODULO]";



            objConexion.asignarConexion(cmd);

            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {

                Modulos objModulo = new Modulos();
                objModulo.CVModulo = reader.GetString(reader.GetOrdinal("CVPERFIL"));
                objModulo.Descripcion = reader.GetString(reader.GetOrdinal("DESCRIPCION"));
                objModulo.CVModPadre = reader.GetString(reader.GetOrdinal("CVMODPAD"));
                objModulo.Orden = reader.GetInt32(reader.GetOrdinal("ORDEN"));
                objModulo.Ambiente = reader.GetString(reader.GetOrdinal("AMBIENTE"));
                objModulo.Modulo = reader.GetString(reader.GetOrdinal("MODULO"));
                objModulo.UsuuMod = reader.GetString(reader.GetOrdinal("USUUMOD"));
                objModulo.FhuMod = reader.GetDateTime(reader.GetOrdinal("FHUMOD"));
                objModulo.PrguMod = reader.GetString(reader.GetOrdinal("PRGUMOD"));

                ltModulos.Add(objModulo);
            }

            objConexion.cerrarConexion();

            return ltModulos;

        }

    }

}
