using ExtendingEditUi.Business.Repositories;
using NUnit.Framework;

namespace ExtendingEditUi.Test
{
    [TestFixture]
    public class ConfigRepositoryTests
    {
        [Test]
        public void CanGetConnectionString()
        {
            //This is not a unit test since we are actually hitting the web.config so it is more of a integration test
            var configRepository = new ConfigRepository();

            var conString = configRepository.GetConnectionString("EPiServerDB");

            Assert.That(conString, Is.EqualTo("Data Source=.;Initial Catalog=ExtendingEditUi;Connection Timeout=60;Trusted_Connection=True;MultipleActiveResultSets=True"));
        }
    }
}
