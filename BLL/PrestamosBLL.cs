﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PrestamosdeJuegos.DAL;
using PrestamosdeJuegos.Entidades;

namespace PrestamosdeJuegos.BLL
{
    public class PrestamosBLL
    {
        //—————————————————————————————————————————————————————[ GUARDAR ]—————————————————————————————————————————————————————
        public static bool Guardar(Prestamos prestamos)
        {
            bool paso;

            if (!Existe(prestamos.PrestamoId))
                paso = Insertar(prestamos);
            else
                paso = Modificar(prestamos);

            return paso;
        }
        //—————————————————————————————————————————————————————[ INSERTAR ]—————————————————————————————————————————————————————
        public static bool Insertar(Prestamos prestamos)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                foreach (var item in prestamos.Detalle)
                {
                    item.FK_Juegos.Existencia -= item.CantidadJuegos;
                    contexto.Entry(item.FK_Juegos).State = EntityState.Modified;
                }

                contexto.Prestamos.Add(prestamos);
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
        //—————————————————————————————————————————————————————[ MODIFICAR ]—————————————————————————————————————————————————————
        public static bool Modificar(Prestamos prestamos)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.Database.ExecuteSqlRaw($"DELETE FROM PrestamosDetalle WHERE PrestamoId={prestamos.PrestamoId}");

                foreach (var item in prestamos.Detalle)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }

                contexto.Entry(prestamos).State = EntityState.Modified;
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
        //—————————————————————————————————————————————————————[ ELIMINAR ]—————————————————————————————————————————————————————
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var proyectos = PrestamosBLL.Buscar(id);
                if (proyectos != null)
                {
                    contexto.Prestamos.Remove(proyectos);
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
        //—————————————————————————————————————————————————————[ GETLIST ]—————————————————————————————————————————————————————
        public static List<Prestamos> GetList(Expression<Func<Prestamos, bool>> criterio)
        {
            List<Prestamos> lista = new List<Prestamos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Prestamos.Where(criterio).ToList();
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
        //—————————————————————————————————————————————————————[ EXISTE ]—————————————————————————————————————————————————————
        public static bool Existe(int id)
        {
            bool encontrado = false;
            Contexto contexto = new Contexto();

            try
            {
                encontrado = contexto.Prestamos.Any(p => p.PrestamoId == id);
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
        //—————————————————————————————————————————————————————[ BUSCAR ]————————————————————————————————————————————————————
        public static Prestamos Buscar(int id)
        {
            Prestamos prestamos = new Prestamos();
            Contexto contexto = new Contexto();

            try
            {
                prestamos = contexto.Prestamos
                    .Where(p => p.PrestamoId == id)
                    .Include(p => p.Detalle).ThenInclude(p => p.FK_Juegos)
                    .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return prestamos;
        }
    }
}