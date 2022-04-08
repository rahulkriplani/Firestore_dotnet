using System;
using Google.Cloud.Firestore;
using System.Threading.Tasks;
using coreLogic;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FireStore
{
    class Program
    {
        public static FirestoreDb fireStoreDb;
        public static string project = "firesharp";
        public static string collectionName = "Users";
        public static void Main(string[] args)
            {
                Environment.SetEnvironmentVariable("FIRESTORE_EMULATOR_HOST", "localhost:8080");
                fireStoreDb = new FirestoreDbBuilder
                    {
                        ProjectId = project,
                        EmulatorDetection = Google.Api.Gax.EmulatorDetection.EmulatorOnly
                    }.Build();
            Console.WriteLine(fireStoreDb);
            CollectionReference col = fireStoreDb.Collection(collectionName);
            Console.WriteLine(col);
            UserRepository userRepository = new UserRepository();
            City city = new City{Name = "Farrukhabad"};
            User user = new User{Name = "Rahul", Surname = "Kriplani", Gender = "M", From = city};
            // Console.WriteLine(user.Name);
            userRepository.Add(user,fireStoreDb,collectionName);

            List<User> users = userRepository.GetAll(fireStoreDb,collectionName);
            foreach (User u in users){
                Console.WriteLine(JsonConvert.SerializeObject(u, Formatting.Indented));
            }

        }
    }
}
