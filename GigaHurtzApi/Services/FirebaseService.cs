using System.Collections.Immutable;
using System.Net.Mime;
using System.Text;
using GigaHurtz.Common.Models;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Storage.V1;
using Host = GigaHurtz.Common.Models.Host;

namespace GigaHurtzApi.Services;

public class FirebaseService : IDbService
{
    private const string PROJECT_ID_PATH = "Firebase:ProjectId";
    private const string FIREBASE_ENV = "FIREBASE_SECRET";
    private const string FIREBASE_SECRET_CONFIG_PATH = "FirebaseSecret";
    private const string USER_COLLECTION_NAME = "users";
    private const string BUCKET_NAME = "gigahurtz-45d32.appspot.com";
    
    private readonly FirestoreDb _db;
    private readonly StorageClient _storage;

    private CollectionReference UserCollection => _db.Collection(USER_COLLECTION_NAME);

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
        _storage = StorageClient.Create(GoogleCredential.FromJson(convertedSecret));
    }

    public async Task AddHost(Host host)
    {
        var dict = ToHostDict(host);
        await UserCollection.Document(host.Id).SetAsync(dict);
    }

    public async Task AddRefugee(Refugee refugee)
    {
        var dict = ToRefugeeDict(refugee);
        await UserCollection.Document(refugee.Id).SetAsync(dict);
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

    /// <inheritdoc/>
    public async Task<string> UploadFile(string path, Stream fileStream, ContentType contentType)
    {
        var mimeType = contentType.MediaType;
        var file = await _storage.UploadObjectAsync(BUCKET_NAME, path, mimeType, fileStream);
        return file.SelfLink;
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
            MaxTenants: (long)data["maxTenants"],
            ImageUrl: (string)data["imageUrl"]);
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

    private static Dictionary<string, object> ToHostDict(Host host)
    {
        var dataDict = new Dictionary<string, object>
        {
            { "address", host.Address },
            { "availableRooms", host.AvailableRooms },
            { "cooks", host.Cooks },
            { "email", host.Email },
            { "kids", host.Kids },
            { "languages", host.Languages.ToList() },
            { "name", host.Name },
            { "phone", host.Phone },
            { "genderPref", host.GenderPref.ToList() },
            { "maxTenants", host.MaxTenants },
            { "imageUrl", host.ImageUrl }
        };
        return new Dictionary<string, object>
        {
            { "role", 0 },
            { "email", host.Email },
            { "data", dataDict }
        };
    }

    private static Dictionary<string, object> ToRefugeeDict(Refugee refugee)
    {
        var dataDict = new Dictionary<string, object>
        {
            { "hasKids", refugee.HasKids },
            { "languages", refugee.Languages.ToList() },
            { "name", refugee.Name },
            { "phone", refugee.Phone },
            { "activelyLooking", refugee.ActivelyLooking },
            { "gender", refugee.Gender },
            { "householdSize", refugee.HouseholdSize },
            { "location", refugee.Location }
        };
        return new Dictionary<string, object>
        {
            { "role", 1 },
            { "email", refugee.Email },
            { "data", dataDict }
        };
    }

}
