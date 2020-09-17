using CentralDeErros.Core;
using CentralDeErros.Model.Models;
using CentralDeErros.Services.Base;
using CentralDeErros.Services.Interfaces;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CentralDeErros.Services
{
    public class MicrosserviceService : ServiceBase<Microsservice>, IMicrosserviceService
    {
        public MicrosserviceService(CentralDeErrosDbContext context) : base(context) 
        {
        }

        public void Delete(Guid? clientId)
        {
            if (clientId == null)
            {
                throw new Exception("O registro informado para exclusão não existe na base de dados");
            }

            var relationship = Context.Errors.Count(x => x.MicrosserviceClientId == clientId);

            if (relationship > 0)
            {
                throw new Exception("O registro não pode ser excluído por ser relacionar com mais de um registro de error");
            }

            else

            {
                var register = Context.Microsservices.FirstOrDefault(x => x.ClientId == clientId);
                Context.Remove(register);
                Context.SaveChanges();
            }
        }

        public Microsservice Register(string name)
        {

            var microsservice = new Microsservice
            {
                Name = name.ToLower()
            };

            GenerateClientSecret(microsservice);

            Context.Add(microsservice);
            Context.SaveChanges();

            return microsservice;
        }

        public override Microsservice Update(Microsservice microsservice)
        {

            var fullData = Fetch(microsservice.ClientId);
            fullData.Name = microsservice.Name.ToLower();

            Context.Update(fullData);
            Context.SaveChanges();

            return fullData;
        }

        public Microsservice Fetch(Guid id)
        {
            Microsservice rsFetch = Context.Microsservices.Find(id);

            return rsFetch;
        }

        public Microsservice GenerateClientSecret(Microsservice microsservice)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                var hashBase = microsservice.ClientId.ToString() + 
                                DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
                string hash = GetMd5Hash(md5Hash, hashBase);
                microsservice.ClientSecret = hash;
            }
            return microsservice;
        }

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public bool ValidateMicrosserviceCredentials(Microsservice microsservice)
        {
            Microsservice ms = Fetch(microsservice.ClientId);
            if (ms != null)
            {
                if (microsservice.ClientSecret.Equals(ms.ClientSecret))
                {
                    return true;
                }
            }
            return false;
        }

        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
