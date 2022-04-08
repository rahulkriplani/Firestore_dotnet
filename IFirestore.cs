using System.Collections.Generic;
using Google.Cloud.Firestore;
public interface IFirestore
{
    User Get(User record,FirestoreDb fdb,string collectionName);
    List<User> GetAll(FirestoreDb fdb, string collectionName);
    User Add(User record,FirestoreDb fdb, string collectionName);
    bool Update(User record,FirestoreDb fdb, string collectionName);
    bool Delete(User record,FirestoreDb fdb, string collectionName);
}
