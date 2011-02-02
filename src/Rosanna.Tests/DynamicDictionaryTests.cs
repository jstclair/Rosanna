using Xunit;

namespace Rosanna.Tests
{
    public class DynamicDictionaryTests
    {
        private readonly dynamic _dictionary;

        public DynamicDictionaryTests()
        {
            const string metaData = @"title: Rosanna
                                      author: Author";

            _dictionary = metaData.ToDynamicDictionary();
        }

        [Fact]
        public void Can_access_data_through_dynamic_properties()
        {
            ((string)_dictionary.title).ShouldEqual("Rosanna");
            ((string)_dictionary.author).ShouldEqual("Author");
        }

        [Fact]
        public void Can_access_data_through_indexer()
        {
            ((string)_dictionary["title"]).ShouldEqual("Rosanna");
            ((string)_dictionary["author"]).ShouldEqual("Author");
        }

        [Fact]
        public void Returns_null_on_missing_properties_through_dynamic_properties()
        {
            ((object)_dictionary.missing).ShouldBeNull();
        }

        [Fact]
        public void Returns_null_on_missing_properties_through_indexer()
        {
            ((object)_dictionary["missing"]).ShouldBeNull();
        }
    }
}