using System.Net.Mime;
using MimeTypes;

namespace GigaHurtz.Common.Extensions;

public static class ContentTypeExtensions
{
    public static string GetFileExtension(this ContentType contentType) =>
        MimeTypeMap.GetExtension(contentType.MediaType);
}
