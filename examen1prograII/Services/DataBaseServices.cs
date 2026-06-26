using examen1prograII.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace examen1prograI.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection db;

        async Task Init()
        {
            if (db != null)
                return;

            string path =
                Path.Combine(
                FileSystem.AppDataDirectory,
                "Sitios.db3");

            db = new SQLiteAsyncConnection(path);

            await db.CreateTableAsync<Sitio>();
        }

        public async Task<List<Sitio>> ObtenerSitios()
        {
            await Init();

            return await db.Table<Sitio>()
                .ToListAsync();
        }

        public async Task<int> GuardarSitio(
            Sitio sitio)
        {
            await Init();

            return await db.InsertAsync(sitio);
        }

        public async Task<int> ActualizarSitio(
            Sitio sitio)
        {
            await Init();

            return await db.UpdateAsync(sitio);
        }

        public async Task<int> EliminarSitio(
            Sitio sitio)
        {
            await Init();

            return await db.DeleteAsync(sitio);
        }
    }
}
