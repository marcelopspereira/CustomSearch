using CreateIndex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProcessFileTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ProcessFiles()
        {
            string[] args = {"C:\\Users\\visouza\\Repos\\FleuryBot\\src\\SearchAPI\\Data\\Exams.csv",
                "C:\\Users\\visouza\\Repos\\FleuryBot\\src\\SearchAPI\\Data\\Synonym.csv",
            "C:\\Users\\visouza\\Repos\\FleuryBot\\src\\SearchAPI\\Data\\dictionary.csv" };

            Program.Main(args);

        }
    }
}
