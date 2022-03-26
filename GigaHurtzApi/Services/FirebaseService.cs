using System.Collections.Immutable;
using System.Text;
using GigaHurtzApi.Models;
using Google.Cloud.Firestore;
using Host = GigaHurtzApi.Models.Host;

namespace GigaHurtzApi.Services;

public class FirebaseService : IDbService
{
    private const string PROJECT_ID_PATH = "Firebase:ProjectId";
    private const string FIREBASE_ENV = "FIREBASE_SECRET";
    private const string FIREBASE_SECRET_CONFIG_PATH = "FirebaseSecret";
    private const string USER_COLLECTION_NAME = "users";
    
    private readonly FirestoreDb _db;

    public CollectionReference UserCollection => _db.Collection(USER_COLLECTION_NAME);

    public FirebaseService(IConfiguration configuration)
    {
        var base64EncodedSecret = Environment.GetEnvironmentVariable(FIREBASE_ENV) ?? 
            configuration[FIREBASE_SECRET_CONFIG_PATH];
        var convertedSecret = Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedSecret));
        _db = new FirestoreDbBuilder
        {
            ProjectId = configuration[PROJECT_ID_PATH],
            JsonCredentials = convertedSecret
        }.Build();
    }

    public async Task AddHost(Host host)
    {
        throw new NotImplementedException();
    }

    public async Task AddRefugee(Refugee refugee)
    {
        throw new NotImplementedException();
    }

    public async Task<Host?> GetHost(string id)
    {
        var hostDocumentReference = UserCollection.Document(id);
        var documentSnapshot = await hostDocumentReference.GetSnapshotAsync();
        if (!documentSnapshot.Exists) return null;
        var hostDict = documentSnapshot.ToDictionary();
        return FromDict(hostDict);
    }

    public async Task<Refugee?> GetRefugee(string id)
    {
        throw new NotImplementedException();
    }

    private Host FromDict(Dictionary<string, object> hostDict)
    {
        var role = (int) hostDict["role"];
        if (role != 0)
        {
            throw new IDbService.DbException("This user is not a host!");
        }

        var data = (KeyValuePair<string, Object>) hostDict["data"];
        var host = new Host
        {
            Address = (string) data["address"],
            AvailableRooms = (int) data["availableRooms"],
            Cooks = (bool) data["cooks"],
        };
        return host;
    }
}
