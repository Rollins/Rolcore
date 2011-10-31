using Rolcore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Rolcore.Tests
{
    
    
    /// <summary>
    ///This is a test class for UriExtensionsTest and is intended
    ///to contain all UriExtensionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UriExtensionsTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        
        [TestMethod()]
        public void ParseKeyWordsEmptyUriTest()
        {
            Uri uri = new Uri(string.Empty, UriKind.Relative);
            string actual = uri.ParseKeyWords();
            Assert.AreEqual(null, actual);
        }

        
        [TestMethod()]
        public void ParseKeyWordsYahooDotComTest()
        {
            Uri uri = new Uri("http://search.yahoo.com/search?fr=cb-hp06&type=advisor&ei=UTF-8&p=13%2epest%20threat%2ecom");
            string actual = uri.ParseKeyWords();
            Assert.AreEqual("13.pest threat.com", actual);

            uri = new Uri("http://yp.yahoo.com/py/ypResults.py?&city=Gaithersburg&state=MD&zip=20882&uzip=20882&msa=8840&slt=39.23&sln=-77.1624&cs=5&stx=8109702&stp=y&desc=Pest+Control&offset=0&FBoffset=20&stat=ClkNxtUpper");
            actual = uri.ParseKeyWords();
            Assert.AreEqual("Pest Control", actual);
        }

        [TestMethod()]
        public void ParseKeyWordsPestanalysisComTest()
        {
            Uri uri = new Uri("http://46.pestanalysis.com/search.php?d=pestanalysis.com&cachekey=1210547694&rc=true&term=Pest+analysis&append=");
            string actual = uri.ParseKeyWords();
            Assert.AreEqual("Pest analysis", actual);
        }

        [TestMethod()]
        public void ParseKeyWordsEntryNotFoundDotComTest()
        {
            Uri uri = new Uri("http://wwww23.entry-not-found.com/search?qo=www.51.pest%2520threat.com&rn=AHbSGJpsHOpxcb_");
            string actual = uri.ParseKeyWords();
            Assert.AreEqual("www.51.pest%20threat.com", actual);
        }

        [TestMethod()]
        public void ParseKeyWordsAttDotNetTest()
        {
            Uri uri = new Uri("http://my.att.net/s/s.dll?num=10&spage=search%2Fresultshome1.htm&searchType=web&string=16+Pest+Threats.com&where=Location+%28e.g.+Address%29&sm.x=7&sm.y=11");
            string actual = uri.ParseKeyWords();
            Assert.AreEqual("16 Pest Threats.com", actual);
        }

        [TestMethod()]
        public void ParseKeyWordsSwitchboardDotComTest()
        {
            Uri uri = new Uri("http://switchboard.com/swbd.main/dir/results.htm?cid=&MEM=1&ypcobrand=1&PR=&ST=&inputwhat_dirty=1&KW=tabor+pest+control&initial=&inputwhere_dirty=1&LO=dothan%2C+al+36303&SD=-1&semChannelId=&semSessionId=&search.x=58&search.y=15");
            string actual = uri.ParseKeyWords();
            Assert.AreEqual("tabor pest control", actual);
        }

        [TestMethod()]
        public void ParseKeyWordsVerizonDotNetTest()
        {
            Uri uri = new Uri("http://wwwz.websearch.verizon.net/search?qo=www.pest%2520threat.com&rn=yl1ciCfHRyzBX_A");
            string actual = uri.ParseKeyWords();
            Assert.AreEqual("www.pest%20threat.com", actual);
        }

        [TestMethod()]
        public void ParseKeyWordsNetscapeDotComTest()
        {
            Uri uri = new Uri("http://search.hp.netscape.com/hp/search?query=28.PestThreat.com&st=webresults&fromPage=HPResultsT");
            string actual = uri.ParseKeyWords();
            Assert.AreEqual("28.PestThreat.com", actual);
        }

        [TestMethod()]
        public void ParseKeyWordsYellowpagesDotComTest()
        {
            Uri uri = new Uri("http://www.yellowpages.com/Fairfax-VA/Pest-Control-Services/city-Fairfax?search_terms=Pest+Control");
            string actual = uri.ParseKeyWords();
            Assert.AreEqual("Pest Control", actual);
        }

        [TestMethod()]
        public void ParseKeyWordsInformationDotComTest()
        {
            Uri uri = new Uri("http://searchportal.information.com/?epl=01510006R1UMXGYWVlEFDVFQClNQAVcNVA9FUVgMAFxbVllZVFhBURBVUAtLGgJfD0VLAAdBAlw5RwxGChEORUBUWUZuCFRdDGdVBF1QCF4FRUsAB0ECXDlDGkVSXA1cW1MeVW4MUQlTDwFXARIKRz0RWQsNDlU&query=Termites");
            string actual = uri.ParseKeyWords();
            Assert.AreEqual("Termites", actual);
        }

        [TestMethod()]
        public void ParseKeyWordsAolDotComTest()
        {
            Uri uri = new Uri("http://search.aol.com/aol/search?invocationType=comsearch30&query=termtie+companies+springboro&do=Search");
            string actual = uri.ParseKeyWords();
            Assert.AreEqual("termtie companies springboro", actual);

            uri = new Uri("http://aim.search.aol.com/search/search?query=41.pestthreat.com&invocationType=TB50TRie7");
            actual = uri.ParseKeyWords();
            Assert.AreEqual("41.pestthreat.com", actual);


            uri = new Uri("http://aolsearch.aol.com/aol/search?invocationType=topsearchbox.search&query=termidor");
            actual = uri.ParseKeyWords();
            Assert.AreEqual("termidor", actual);
        }
    }
}
