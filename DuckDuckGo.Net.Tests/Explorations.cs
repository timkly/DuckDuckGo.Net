using NUnit.Framework;
using System;
using System.Linq;

namespace DuckDuckGo.Net.Tests
{
    [TestFixture]
    public class Explorations
    {
        private const string ApplicationName = "DuckDuckGo.NET test project";

        [Test]
        public void FamousThingsHaveAnOfficialSite()
        {
            var result = new Search().Query("studio ghibli", ApplicationName);

            Assert.AreEqual(result.Results.First().Text, "Official site"); 
        }

        [Test]
        public void FamousThingsAlsoHaveAnAbstract()
        {
            var result = new Search().Query("studio ghibli", ApplicationName);

            Assert.IsNotEmpty(result.Abstract);
            Assert.IsNotEmpty(result.AbstractSource);
            Assert.IsNotEmpty(result.AbstractUrl);
            Assert.IsNotEmpty(result.AbstractText);
        }

        [Test]
        public void SomeThingsHaveRelatedTopics()
        {
            var result = new Search().Query("black", ApplicationName);

            Assert.Greater(result.RelatedTopics.Count, 1);
        }

        [Test]
        public void SomeThingsHaveADefinition()
        {
            var result = new Search().Query("black", ApplicationName);

            Assert.IsNotEmpty(result.Definition);
            Assert.IsNotEmpty(result.DefinitionUrl);
            Assert.IsNotEmpty(result.DefinitionSource);
        }
    }
}
