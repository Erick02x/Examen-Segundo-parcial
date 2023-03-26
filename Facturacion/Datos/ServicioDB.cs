using Entidades;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

namespace Datos
{
    public class ServicioDB
    {
        string cadena = "server=localhost; user=root; database=ticket; password=123456;";

        public bool Insertar(servicio servicio)
        {
            bool inserto = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" INSERT INTO servicio VALUES ");
                sql.Append(" (@Codigo, @Descripcion, @DescripcionRespuesta, @Precio, @EstaDisponible); ");
                using (MySqlConnection _conexion = new MySqlConnection(cadena))
                {
                    _conexion.Open();
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 80).Value = servicio.Codigo;
                        comando.Parameters.Add("@Descripcion", MySqlDbType.VarChar, 200).Value = servicio.Descripcion;
                        comando.Parameters.Add("@DescripcionRespuesta", MySqlDbType.VarChar, 200).Value = servicio.DescripcionRespuesta;
                        comando.Parameters.Add("@Precio", MySqlDbType.Decimal).Value = servicio.Precio;
                        comando.Parameters.Add("@EstaDisponible", MySqlDbType.Bit).Value = servicio.EstaDisponible;
                        comando.ExecuteNonQuery();
                        inserto = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
            }
            return inserto;
        }

        public bool Editar(servicio servicio)
        {
            bool edito = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" UPDATE servicio SET ");
                sql.Append(" Descripcion = @Descripcion, DescripcionRespuesta = @DescripcionRespuesta, Precio = @Precio, EstaDisponible = @EstaDiponible ");
                sql.Append(" WHERE Codigo = @Codigo; ");

                using (MySqlConnection _conexion = new MySqlConnection(cadena))
                {
                    _conexion.Open();
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 80).Value = servicio.Codigo;
                        comando.Parameters.Add("@Descripcion", MySqlDbType.VarChar, 200).Value = servicio.Descripcion;
                        comando.Parameters.Add("@DescripcionRespuesta", MySqlDbType.VarChar, 200).Value = servicio.DescripcionRespuesta;
                        comando.Parameters.Add("@Precio", MySqlDbType.Decimal).Value = servicio.Precio;
                        comando.Parameters.Add("@EstaDisponible", MySqlDbType.Bit).Value = servicio.EstaDisponible;
                        comando.ExecuteNonQuery();
                        edito = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
            }
            return edito;
        }

        public bool Eliminar(string codigo)
        {
            bool elimino = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" DELETE FROM servicio ");
                sql.Append(" WHERE Codigo = @Codigo; ");

                using (MySqlConnection _conexion = new MySqlConnection(cadena))
                {
                    _conexion.Open();
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 80).Value = codigo;
                        comando.ExecuteNonQuery();
                        elimino = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
            }
            return elimino;
        }

        public DataTable DevolverServicio()
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM servicio ");
                using (MySqlConnection _conexion = new MySqlConnection(cadena))
                {
                    _conexion.Open();
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        MySqlDataReader dr = comando.ExecuteReader();
                        dt.Load(dr);
                    }
                }
            }
            catch (System.Exception ex)
            {
            }
            return dt;
        }

        //public byte[] DevolverFoto(string codigo)
        //{
        //    byte[] foto = new byte[0];
        //    try
        //    {
        //        StringBuilder sql = new StringBuilder();
        //        sql.Append(" SELECT Foto FROM servicio WHERE Codigo = @Codigo;");
        //        using (MySqlConnection _conexion = new MySqlConnection(cadena))
        //        {
        //            _conexion.Open();
        //            using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
        //            {
        //                comando.CommandType = CommandType.Text;
        //                comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 80).Value = codigo;
        //                MySqlDataReader dr = comando.ExecuteReader();
        //                if (dr.Read())
        //                {
        //                    foto = (byte[])dr["Foto"];
        //                }
        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //    }
        //    return foto;
        //}

        //public servicio DevolverProductoPorCodigo(string codigo)
        //{
        //    servicio producto = null;
        //    try
        //    {
        //        StringBuilder sql = new StringBuilder();
        //        sql.Append(" SELECT * FROM producto WHERE Codigo = @Codigo; ");
        //        using (MySqlConnection _conexion = new MySqlConnection(cadena))
        //        {
        //            _conexion.Open();
        //            using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
        //            {
        //                comando.CommandType = CommandType.Text;
        //                comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 80).Value = codigo;
        //                MySqlDataReader dr = comando.ExecuteReader();
        //                if (dr.Read())
        //                {
        //                    producto = new servicio();
        //                    producto.Codigo = codigo;
        //                    producto.Descripcion = dr["Descripcion"].ToString();
        //                    producto.Existencia = Convert.ToInt32(dr["Existencia"]);
        //                    producto.Precio = Convert.ToDecimal(dr["Precio"]);
        //                    producto.EstaActivo = Convert.ToBoolean(dr["EstaActivo"]);
        //                }
        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //    }
        //    return producto;
        //}

        public DataTable DevolverServicioPorDescripcion(string descripcion)
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM servicio WHERE Descripcion LIKE '%" + descripcion + "%'");
                using (MySqlConnection _conexion = new MySqlConnection(cadena))
                {
                    _conexion.Open();
                    using (MySqlCommand comando = new MySqlCommand(sql.ToString(), _conexion))
                    {
                        comando.CommandType = CommandType.Text;
                        MySqlDataReader dr = comando.ExecuteReader();
                        dt.Load(dr);
                    }
                }
            }
            catch (System.Exception ex)
            {
            }
            return dt;
        }


    }
}
