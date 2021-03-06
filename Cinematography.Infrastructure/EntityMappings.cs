using Cinematography.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Cinematography.Infrastructure
{
    public static class EntityMappings
    {
        public static void Map()
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

            BsonClassMap.RegisterClassMap<Movie>(x =>
            {
                x.AutoMap();
                x.SetIgnoreExtraElements(true);
                x.MapIdMember(y => y.Id);
            });

            BsonClassMap.RegisterClassMap<Person>(x =>
            {
                x.AutoMap();
                x.SetIgnoreExtraElements(true);
            });
        }
    }
}
