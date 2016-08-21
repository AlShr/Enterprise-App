using System;
using System.Collections.Generic;
using NUnit.Framework;
using ProjectBase.Utils;
using Enterprise.CoreData.Domain;

namespace Enterprise.NUnitTests.Data.Domain
{
    [TestFixture]
    public class ItemTests
    {
        [Test]
        public void CanCreateItem()
        {
            Item item = new Item(TestGlobals.Book){InventorySerialCode="001211"};
            Assert.AreEqual("001211", item.InventorySerialCode);
            item.InventorySerialCode="001212";
            Assert.AreEqual("001212", item.InventorySerialCode);
        }
    }
}
