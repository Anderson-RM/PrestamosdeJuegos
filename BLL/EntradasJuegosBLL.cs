using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using PrestamosdeJuegos.DAL;
using PrestamosdeJuegos.Entidades;

namespace PrestamosdeJuegos.BLL
{
    public class EntradasJuegosBLL
    {
        //——————————————————————————————————————————————[ GUARDAR ]——————————————————————————————————————————————
        public static bool Guardar(EntradasJuegos entradasJuegos)
        {
            if (!Existe(entradasJuegos.EntradaJuegoId))
                return Insertar(entradasJuegos);
            else
                return Modificar(entradasJuegos);
        }
        //——————————————————————————————————————————————[ INSERTAR ]——————————————————————————————————————————————
        private static bool Insertar(EntradasJuegos entradasJuegos)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.EntradasJuegos.Add(entradasJuegos);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }
        //——————————————————————————————————————————————[ MODIFICAR ]——————————————————————————————————————————————
        public static bool Modificar(EntradasJuegos entradasJuegos)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(entradasJuegos).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }
        //——————————————————————————————————————————————[ ELIMINAR ]——————————————————————————————————————————————
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var entradasJuegos = contexto.EntradasJuegos.Find(id);
                if (entradasJuegos != null)
                {
                    contexto.EntradasJuegos.Remove(entradasJuegos);
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }
        //——————————————————————————————————————————————[ BUSCAR ]——————————————————————————————————————————————
        public static EntradasJuegos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            EntradasJuegos entradasJuegos;

            try
            {
                entradasJuegos = contexto.EntradasJuegos.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return entradasJuegos;
        }
        //——————————————————————————————————————————————[ GETLIST ]——————————————————————————————————————————————
        public static List<EntradasJuegos> GetList(Expression<Func<EntradasJuegos, bool>> criterio)
        {
            List<EntradasJuegos> lista = new List<EntradasJuegos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.EntradasJuegos.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }
        //——————————————————————————————————————————————[ EXISTE ]——————————————————————————————————————————————
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.EntradasJuegos.Any(e => e.EntradaJuegoId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }
        //——————————————————————————————————————————————[ GET ]——————————————————————————————————————————————
        public static List<EntradasJuegos> GetEntradasJuegos()
        {
            List<EntradasJuegos> lista = new List<EntradasJuegos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.EntradasJuegos.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }
    }
}
