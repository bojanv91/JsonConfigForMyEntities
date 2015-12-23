using System.Linq;
using JsonConfigForMyEntities.Model;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace JsonConfigForMyEntities
{
    public class EntityDeserializationTests
    {
        private readonly JsonSerializerSettings _settings;

        public EntityDeserializationTests()
        {
            var contractResolver = new Tests();
            _settings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };
        }

        [Fact]
        public void deserialize_with_private_set_property()
        {
            string json = "{'FullName':'Hulk'}";
            var entity = JsonConvert.DeserializeObject<Person>(json, _settings);
            entity.FullName.ShouldBe("Hulk");
        }

        [Fact]
        public void deserialize_with_private_readonly_collection_which_is_publicly_exposed_via_property_getter()
        {
            string json = "{'Addresses':[{'Name':'Mars'},{'Name':'Venera'}]}";
            var entity = JsonConvert.DeserializeObject<Person>(json, _settings);
            entity.Addresses.Count().ShouldBe(2);
            entity.Addresses.ElementAt(0).Name.ShouldBe("Mars");
            entity.Addresses.ElementAt(1).Name.ShouldBe("Venera");
        }

        [Fact]
        public void should_serialize()
        {
            var entity = new Person("Hulk");
            entity.AddAddress("Mars");
            entity.AddAddress("Venera");

            string json = JsonConvert.SerializeObject(entity, _settings);
        }
    }
}
