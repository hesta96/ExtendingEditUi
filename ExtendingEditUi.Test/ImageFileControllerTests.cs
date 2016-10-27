using System.Web.Mvc;
using EPiServer.Core;
using EPiServer.Web.Routing;
using ExtendingEditUi.Controllers;
using ExtendingEditUi.Models.Media;
using ExtendingEditUi.Models.ViewModels;
using FakeItEasy;
using NUnit.Framework;

namespace ExtendingEditUi.Test
{
    [TestFixture]
    public class ImageFileControllerTests
    {
        [Test]
        public void CanGetViewModelForIndexAction()
        {
            var urlResolver = A.Fake<UrlResolver>();
            A.CallTo(() => urlResolver.GetUrl(new ContentReference(1))).Returns("/myimage.jpg");

            var imageFileController = new ImageFileController(urlResolver);

            var imageFile = new ImageFile() {Name = "ImageFile", Copyright = "Copyright", ContentLink = new ContentReference(1)};

            var result = imageFileController.Index(imageFile) as PartialViewResult;

            var model = result.Model as ImageViewModel;

            Assert.That(model.Name, Is.EqualTo("ImageFile"));
            Assert.That(model.Copyright, Is.EqualTo("Copyright"));
            Assert.That(model.Url, Is.EqualTo("/myimage.jpg"));

        }
    }
}
