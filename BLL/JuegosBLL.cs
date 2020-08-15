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
    public class JuegosBLL
    {
        //——————————————————————————————————————————————[ GUARDAR ]——————————————————————————————————————————————
        public static bool Guardar(Juegos juegos)
        {
            if (!Existe(juegos.JuegoId))
                return Insertar(juegos);
            else
                return Modificar(juegos);
        }
        //——————————————————————————————————————————————[ INSERTAR ]——————————————————————————————————————————————
        private static bool Insertar(Juegos juegos)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Juegos.Add(juegos);
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
        public static bool Modificar(Juegos juegos)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(juegos).State = EntityState.Modified;
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
                var juegos = contexto.Juegos.Find(id);
                if (juegos != null)
                {
                    contexto.Juegos.Remove(juegos);
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
        public static Juegos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Juegos juegos;

            try
            {
                juegos = contexto.Juegos.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return juegos;
        }
        //——————————————————————————————————————————————[ GETLIST ]——————————————————————————————————————————————
        public static List<Juegos> GetList(Expression<Func<Juegos, bool>> criterio)
        {
            List<Juegos> lista = new List<Juegos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Juegos.Where(criterio).ToList();
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
                encontrado = contexto.Juegos.Any(j => j.JuegoId == id);
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
        public static List<Juegos> GetJuegos()
        {
            List<Juegos> lista = new List<Juegos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Juegos.ToList();
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
        public static List<Juegos> GetList()
        {
            List<Juegos> juegos = new List<Juegos>();
            Contexto contexto = new Contexto();

            try
            {
                juegos = contexto.Juegos.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return juegos;
        }
        //——————————————————————————————————————————————[ SumarJuegos ]——————————————————————————————————————————————
        public static void SumarJuegos(int id, double cantidad)
        {
            Juegos juegos = Buscar(id);

            juegos.Existencia += cantidad;

            Modificar(juegos);
        }
        //——————————————————————————————————————————————[ RestarJuegos ]——————————————————————————————————————————————
        public static void RestarJuegos(int id, double cantidad)
        {
            Juegos juegos = Buscar(id);

            juegos.Existencia -= cantidad;

            if (juegos.Existencia >= 0)
            {
                Modificar(juegos);
            }
            else
            {
                MessageBox.Show("No puedes dar salida a esta catidad de juegos, porque es menor que 0.\n\nVerifique la existencia actual del libro.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }
    }
}