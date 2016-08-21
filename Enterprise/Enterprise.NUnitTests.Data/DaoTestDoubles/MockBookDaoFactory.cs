using Enterprise.CoreData.DataInterfaces;
using Enterprise.NUnitTests.Data.TestFactories;
using Rhino.Mocks;

namespace Enterprise.NUnitTests.Data.DaoTestDoubles
{
    public class MockBookDaoFactory
    {
        public IBookDao CreateMockBookDao()
        {
            MockRepository mocks = new MockRepository();
            IBookDao mockedBookDao = mocks.DynamicMock<IBookDao>();
            Expect.Call(mockedBookDao.Get(null)).IgnoreArguments()
                .Return(new TestBooksFactory().CreateBooks());
            Expect.Call(mockedBookDao.GetBy(null)).IgnoreArguments()
                .Return(new TestBooksFactory().CreateBook());

            mocks.Replay(mockedBookDao);
            return mockedBookDao;
        }
    }
}
