using Google.Cloud.Firestore;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace coreLogic
{
    public class UserRepository : IFirestore
    {
        // FirestoreDb fDb = repo.fireStoreDb;

        // CollectionReference repo = fireStoreDb.Collection(collectionName);
        public User Add(User record,FirestoreDb fdb, string collectionName)
                {
                    Console.WriteLine("in");
                    CollectionReference colRef = fdb.Collection(collectionName);
                    DocumentReference doc = colRef.AddAsync(record).GetAwaiter().GetResult();
                    Console.WriteLine(doc);
                    record.Id = doc.Id;
                    return record;
                    // return Task;
                }
        public bool Update(User record,FirestoreDb fdb,string collectionName)
                {
                    DocumentReference recordRef = fdb.Collection(collectionName).Document(record.Id);
                    recordRef.SetAsync(record, SetOptions.MergeAll).GetAwaiter().GetResult();
                    return true;
                }
        public bool Delete(User record,FirestoreDb fdb,string collectionName)
                {
                    DocumentReference recordRef = fdb.Collection(collectionName).Document(record.Id);
                    recordRef.DeleteAsync().GetAwaiter().GetResult();
                    return true;
                }
        public User Get(User record,FirestoreDb fdb,string collectionName)
    {
                    DocumentReference docRef = fdb.Collection(collectionName).Document(record.Id);
                    DocumentSnapshot snapshot = docRef.GetSnapshotAsync().GetAwaiter().GetResult();
        if (snapshot.Exists)
                    {
                        User usr = snapshot.ConvertTo<User>();
                        usr.Id = snapshot.Id;
                        return usr;
                    }
                    else
                    {
                        return null;
                    }
        }
        public List<User> GetAll(FirestoreDb fdb, string collectionName)
                {
        // Query query = fdb.Collection(collectionName);
        CollectionReference col = fdb.Collection(collectionName);
        QuerySnapshot querySnapshot = col.GetSnapshotAsync().GetAwaiter().GetResult();
                    List<User> list = new List<User>();
        foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
                    {
                        if (documentSnapshot.Exists)
                        {
                            Dictionary<string, object> city = documentSnapshot.ToDictionary();
                            string json = JsonConvert.SerializeObject(city);
                            User newItem = JsonConvert.DeserializeObject<User>(json);
                            newItem.Id = documentSnapshot.Id;
        list.Add(newItem);
                        }
                    }
        return list;
        }
        public List<User> QueryRecords(Query query)
                {
                    QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();
                    List<User> list = new List<User>();
        foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
                    {
                        if (documentSnapshot.Exists)
                        {
                            Dictionary<string, object> city = documentSnapshot.ToDictionary();
                            string json = JsonConvert.SerializeObject(city);
                            User newItem = JsonConvert.DeserializeObject<User>(json);
                            newItem.Id = documentSnapshot.Id;
        list.Add(newItem);
                        }
                    }
        return list;
        }

        // public User Add(User record,FirestoreDb fdb, string collectionName) => Add(record,fdb,collectionName);
        // public bool Update(User record,FirestoreDb fdb, string collectionName) => Update(record,fdb,collectionName);
        // public bool Delete(User record,FirestoreDb fdb, string collectionName) => Delete(record,fdb,collectionName);
        // public User Get(User record,FirestoreDb fdb, string collectionName) => Get(record,fdb,collectionName);
        // public List<User> GetAll(FirestoreDb fdb, string collectionName) => GetAll<User>(fdb, collectionName);
        public List<User> GetUserWhereCity(FirestoreDb fdb, string collectionName)
        {
            List<City> cities = new List<City>()
                {
                    new City()
                    {   
                        Name="Test City"
                    }
                };
            Query query = fdb.Collection(collectionName).WhereIn(nameof(User.From), cities);
            return QueryRecords(query);
        }
    }
}