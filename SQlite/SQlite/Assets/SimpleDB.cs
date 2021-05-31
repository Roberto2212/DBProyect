using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;


public class NewBehaviourScript : MonoBehaviour
{

    private string dbName = "URI=file:inventory.db";

    // Start is called before the first frame update
    void Start()
    {
        AddItem("Iron Sword", 10, 15);
        AddItem("Golden Sword",20, 30);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        CreateDB();

        if(Input.GetKeyDown(KeyCode.L))
        ShowItems();
    }

    public void CreateDB()
    {
        using (var connection = new SqliteConnection(dbName))  // Se utiliza una variable para crear una conexión con la base de datos a través de la .dll de Mono.Data.Sqlite
        {
            connection.Open();  // Abrimos la conexión

            using (var command = connection.CreateCommand())  // Se utiliza una variable para insertar un comando en SQLite
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS inventory (ItemName VARCHAR(20), STA INT, STR INT);";    //Crea una tabla si no existe una previa, llamada productsTable que contiene en su interior un VARCHAR de 20 caracteres ASCII y un float para determinar el precio.
                command.ExecuteNonQuery();  // Ejecuta el comando en SQL en la query de la DB. 
            }
            connection.Close(); // Cierra la conexión
        }
    }

     public void AddItem(string ItemName, int STA, int STR)
    {
        using (var connection = new SqliteConnection(dbName))  // Se utiliza una variable para crear una conexión con la base de datos a través de la .dll de Mono.Data.Sqlite
        {
            connection.Open();  // Abrimos la conexión

            using (var command = connection.CreateCommand())  // Se utiliza una variable para insertar un comando en SQLite
            {
                command.CommandText = " INSERT INTO inventory (ItemName, STA, STR) VALUES ('" + ItemName + "', '" + STA + "' , '" + STR + "');";    //Crea una tabla si no existe una previa, llamada productsTable que contiene en su interior un VARCHAR de 20 caracteres ASCII y un float para determinar el precio.
                command.ExecuteNonQuery();  // Ejecuta el comando en SQL en la query de la DB. 
            }
            connection.Close(); // Cierra la conexión
        }
    }

    public void ShowItems()
    {
         using (var connection = new SqliteConnection(dbName))  // Se utiliza una variable para crear una conexión con la base de datos a través de la .dll de Mono.Data.Sqlite
        {
            connection.Open();  // Abrimos la conexión

             using (var command = connection.CreateCommand())  // Se utiliza una variable para insertar un comando en SQLite
            {
                command.CommandText = " SELECT * FROM inventory ;";    //Crea una tabla si no existe una previa, llamada productsTable que contiene en su interior un VARCHAR de 20 caracteres ASCII y un float para determinar el precio.
               using (IDataReader reader = command.ExecuteReader())
               {
                   while (reader.Read())
                   {
                       Debug.Log("Nombre: " + reader["ItemName"] + "\tSTA: " + reader["STA"] + "\tSTR: " + reader["STR"]);
                   }
                   reader.Close();
               }
            }
            connection.Close(); // Cierra la conexión
        }
    }
}
