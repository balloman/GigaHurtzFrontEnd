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
        var hostDict = await GetUserDocument(id);
        return hostDict is null ? null : HostFromDict(id, hostDict);
    }

    public async Task<Refugee?> GetRefugee(string id)
    {
        var refugeeDict = await GetUserDocument(id);
        return refugeeDict is null ? null : RefugeeFromDict(id, refugeeDict);
    }

    private async Task<Dictionary<string, object>?> GetUserDocument(string id)
    {
        var hostDocumentReference = UserCollection.Document(id);
        var documentSnapshot = await hostDocumentReference.GetSnapshotAsync();
        return !documentSnapshot.Exists ? null : documentSnapshot.ToDictionary();
    }

    private static Host HostFromDict(string hostId, IReadOnlyDictionary<string, object> hostDict)
    {
        var role = (long)hostDict["role"];
        if (role != 0)
        {
            throw new IDbService.DbException("This user is not a host!");
        }

        var data = (Dictionary<string, object>)hostDict["data"];
        var languages = ((List<object>)data["languages"]).Select(o => (string)o);
        var genderPref = ((List<object>)data["genderPref"]).Select(o => (string)o);
        var host = new Host(Address: (string)data["address"],
            AvailableRooms: (long)data["availableRooms"],
            Cooks: (bool)data["cooks"],
            Email: (string)hostDict["email"],
            Id: hostId,
            Kids: (bool)data["kids"],
            Languages: languages.ToImmutableArray(),
            Name: (string)data["name"],
            Phone: (string)data["phone"],
            GenderPref: genderPref.ToImmutableArray(),
            MaxTenants: (long)data["maxTenants"]);
        return host;
    }

    private static Refugee RefugeeFromDict(string refugeeId, IReadOnlyDictionary<string, object> refugeeDict)
    {
        var role = (long)refugeeDict["role"];
        if (role != 1) throw new IDbService.DbException("This user is not a refugee!");
        var data = (Dictionary<string, object>)refugeeDict["data"];
        var languages = ((List<object>)data["languages"]).Select(o => (string)o);
        var refugee = new Refugee(
            Email: (string)refugeeDict["email"],
            Id: refugeeId,
            HasKids: (bool)data["hasKids"],
            Languages: languages.ToImmutableArray(),
            Name: (string)data["name"],
            Phone: (string)data["phone"],
            Gender: (string)data["gender"],
            HouseholdSize: (long)data["householdSize"],
            Location: (string)data["location"],
            ActivelyLooking: (bool)data["activelyLooking"]);
        return refugee;
    }
}
