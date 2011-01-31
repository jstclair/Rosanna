namespace Rosanna.Tests
{
    public class TestConfiguration : RosannaConfiguration
    {
        public TestConfiguration()
        {
            Url = "http://example.com";
            Prefix = "web";
            Author = "Author";
        }
    }
}