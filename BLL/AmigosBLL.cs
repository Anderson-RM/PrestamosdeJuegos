using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using PrestamosdeJuegos.DAL;
using PrestamosdeJuegos.Entidades;
using System.Windows;
using System.Windows.Documents;

namespace PrestamosdeJuegos.BLL
{
    public class AmigosBLL
    {
        //——————————————————————————————————————————————[ GUARDAR ]——————————————————————————————————————————————
        public static bool Guardar(Amigos amigos)
        {
            if (!Existe(amigos.AmigoId))
                return Insertar(amigos);
            else
                return Modificar(amigos);
        }
        //——————————————————————————————————————————————[ INSERTAR ]——————————————————————————————————————————————
        private static bool Insertar(Amigos amigos)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Amigos.Add(amigos);
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
        public static bool Modificar(Amigos amigos)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(amigos).State = EntityState.Modified;
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
                var amigos = contexto.Amigos.Find(id);
                if (amigos != null)
                {
                    contexto.Amigos.Remove(amigos);
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
        public static Amigos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Amigos amigos;

            try
            {
                amigos = contexto.Amigos.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return amigos;
        }
        //——————————————————————————————————————————————[ GETLIST ]——————————————————————————————————————————————
        public static List<Amigos> GetList(Expression<Func<Amigos, bool>> criterio)
        {
            List<Amigos> lista = new List<Amigos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Amigos.Where(criterio).ToList();
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
                encontrado = contexto.Amigos.Any(j => j.AmigoId == id);
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
        public static List<Amigos> GetAmigos()
        {
            List<Amigos> lista = new List<Amigos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Amigos.ToList();
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
        //——————————————————————————————————————————————[ GetList ]——————————————————————————————————————————————
        public static List<Amigos> GetList()
        {
            List<Amigos> amigos = new List<Amigos>();
            Contexto contexto = new Contexto();

            try
            {
                amigos = contexto.Amigos.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return amigos;
        }
    }
}