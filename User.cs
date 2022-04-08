using Google.Cloud.Firestore;
using System.Collections.Generic;
[FirestoreData]
public class Base
{
    [FirestoreProperty]
    public string Id { get; set; } 
}

[FirestoreData]
public class City:Base
{
    [FirestoreProperty]
    public string Name { get; set; } 
}

[FirestoreData]
public class User : Base
{  
    [FirestoreProperty]
    public string Name { get; set; }
    [FirestoreProperty]
    public string Surname { get; set; }
    [FirestoreProperty]
    public string Gender { get; set; }
   [FirestoreProperty]
   public City From { get; set; }
   public string NotBeingSaved { get; set; }
}