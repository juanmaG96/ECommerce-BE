using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Tools
{
    public class Encrypt
    {
        public static string GetSHA256(string str)
        {
            // Crear una instancia del algoritmo SHA-256
            SHA256 sha256 = SHA256Managed.Create();

            // Crear una instancia de codificación ASCII para convertir la cadena en bytes
            ASCIIEncoding encoding = new ASCIIEncoding();

            // Declarar una matriz de bytes para almacenar el resultado del hash
            byte[] stream = null;

            // Declarar un objeto StringBuilder para construir la cadena hexadecimal del hash
            StringBuilder sb = new StringBuilder();

            // Calcular el valor hash SHA-256 de la cadena de entrada
            stream = sha256.ComputeHash(encoding.GetBytes(str));

            // Recorrer la matriz de bytes del hash y convertirla a una cadena hexadecimal
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);

            // Devolver la cadena hexadecimal del hash SHA-256
            return sb.ToString();
        }
    }
}
